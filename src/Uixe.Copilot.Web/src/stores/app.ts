import { defineStore } from 'pinia'
import { createTrafficEventsConnection, fetchEventById, fetchLaneStatusSnapshots, fetchOverview, type PlazaLaneSnapshotResponse } from '@/api/mock'

export interface OverviewMetrics {
  onlineStations: number
  totalStations: number
  activeAlerts: number
  todayEvents: number
  realtimeMessages: number
}

export interface PlazaStatusItem {
  id: string
  name: string
  status: 'online' | 'warning' | 'offline'
  lanesOnline: number
  lanesTotal: number
  alerts: number
  laneDetails?: LaneStatusItem[]
}

export interface LaneStatusItem {
  id: string
  laneNo: string
  status: 'online' | 'warning' | 'offline'
  vehicleCount: number
  lastMessage: string
  lastHeartbeat: string
  hasWarning: boolean
  collectorName?: string
  workMode?: string
  videoRtsp?: string
  networkStatus?: boolean
  cameraStatus?: boolean
  printerStatus?: boolean
  barrierStatus?: boolean
}

export interface EventItem {
  id: string
  title: string
  plazaName: string
  laneNo: string
  level: 'high' | 'medium' | 'low'
  time: string
  status: string
  recordId?: string
  eventType?: string
  capTime?: string
  startTime?: string
  durationText?: string
  queueLengthText?: string
  summary?: string
  imageUrl?: string
  videoUrl?: string
  imageUrls?: string[]
  videoUrls?: string[]
}

export interface App {
  overview: OverviewMetrics
  trend: number[]
  plazas: PlazaStatusItem[]
  events: EventItem[]
}

interface OverviewResponse {
  onlineStations: number
  totalStations: number
  activeAlerts: number
  todayEvents: number
  realtimeMessages: number
  trend: number[]
  plazas: PlazaStatusItem[]
  events: EventItem[]
}

export const useAppStore = defineStore('app', {
  state: (): App => ({
    overview: {
      onlineStations: 0,
      totalStations: 0,
      activeAlerts: 0,
      todayEvents: 0,
      realtimeMessages: 0
    },
    trend: [],
    plazas: [],
    events: []
  }),
  getters: {
    eventById: (state) => (id: string) => state.events.find((item) => item.id === id)
  },
  actions: {
    async loadOverview() {
      const data = (await fetchOverview()) as OverviewResponse
      this.overview = {
        onlineStations: data.onlineStations,
        totalStations: data.totalStations,
        activeAlerts: data.activeAlerts,
        todayEvents: data.todayEvents,
        realtimeMessages: data.realtimeMessages
      }
      this.trend = data.trend
      this.plazas = data.plazas.map((item) => ({
        id: item.id,
        name: item.name,
        status: item.status as 'online' | 'warning' | 'offline',
        lanesOnline: item.lanesOnline,
        lanesTotal: item.lanesTotal,
        alerts: item.alerts,
        laneDetails: []
      }))
      await this.loadLaneStatusSnapshots()
      this.events = data.events.map((item) => ({
        id: item.id,
        title: item.title,
        plazaName: item.plazaName,
        laneNo: item.laneNo,
        level: item.level as 'high' | 'medium' | 'low',
        time: item.time,
        status: item.status,
        recordId: item.recordId,
        eventType: item.eventType,
        capTime: item.capTime,
        startTime: item.startTime,
        durationText: item.durationText,
        queueLengthText: item.queueLengthText,
        summary: item.summary,
        imageUrl: item.imageUrl,
        videoUrl: item.videoUrl,
        imageUrls: item.imageUrls,
        videoUrls: item.videoUrls
      }))
    },
    async loadEvent(eventId: string) {
      const event = await fetchEventById(eventId)
      if (!event) {
        return null
      }

      const exists = this.events.some((item) => item.id === event.id)
      if (!exists) {
        this.events.push({
          id: event.id,
          title: event.title,
          plazaName: event.plazaName,
          laneNo: event.laneNo,
          level: event.level as 'high' | 'medium' | 'low',
          time: event.time,
          status: event.status,
          recordId: event.recordId,
          eventType: event.eventType,
          capTime: event.capTime,
          startTime: event.startTime,
          durationText: event.durationText,
          queueLengthText: event.queueLengthText,
          summary: event.summary,
          imageUrl: event.imageUrl,
          videoUrl: event.videoUrl,
          imageUrls: event.imageUrls,
          videoUrls: event.videoUrls
        })
      }

      return event
    },
    async refreshEvent(eventId: string) {
      return this.loadEvent(eventId)
    },
    async loadLaneStatusSnapshots() {
      const snapshots = await fetchLaneStatusSnapshots()
      const snapshotMap = new Map(snapshots.map((item) => [item.plazaId, item]))

      this.plazas = this.plazas.map((item) => applyLaneSnapshot(item, snapshotMap.get(item.id)))
    },
    async connectRealtime() {
      const connection = createTrafficEventsConnection()
      connection.off('trafficEventSubmitted')
      connection.on('trafficEventSubmitted', async () => {
        await this.loadOverview()
      })

      if (connection.state === 'Disconnected') {
        await connection.start()
      }
    }
  }
})

function applyLaneSnapshot(item: PlazaStatusItem, snapshot?: PlazaLaneSnapshotResponse): PlazaStatusItem {
  if (!snapshot) {
    return {
      ...item,
      laneDetails: []
    }
  }

  return {
    ...item,
    status: (snapshot.alerts > 0 ? 'warning' : snapshot.lanesOnline > 0 ? 'online' : 'offline') as 'online' | 'warning' | 'offline',
    lanesOnline: snapshot.lanesOnline,
    lanesTotal: snapshot.lanesTotal,
    alerts: snapshot.alerts,
    laneDetails: snapshot.lanes.map((lane) => ({
      id: lane.laneId || `${snapshot.plazaId}-${lane.laneNo}`,
      laneNo: lane.laneNo,
      status: lane.status as 'online' | 'warning' | 'offline',
      vehicleCount: lane.hasWarning ? 1 : lane.status === 'online' ? 1 : 0,
      lastMessage: lane.lastMessage,
      lastHeartbeat: lane.lastHeartbeat,
      hasWarning: lane.hasWarning,
      collectorName: lane.collectorName,
      workMode: lane.workMode,
      videoRtsp: lane.videoRtsp,
      networkStatus: lane.networkStatus,
      cameraStatus: lane.cameraStatus,
      printerStatus: lane.printerStatus,
      barrierStatus: lane.barrierStatus
    }))
  }
}
