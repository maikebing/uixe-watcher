import { defineStore } from 'pinia'
import { createTrafficEventsConnection, fetchEventById, fetchOverview } from '@/api/mock'

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
}

export interface EventItem {
  id: string
  title: string
  plazaName: string
  laneNo: string
  level: 'high' | 'medium' | 'low'
  time: string
  status: string
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
      const data = await fetchOverview()
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
        alerts: item.alerts
      }))
      this.events = data.events.map((item) => ({
        id: item.id,
        title: item.title,
        plazaName: item.plazaName,
        laneNo: item.laneNo,
        level: item.level as 'high' | 'medium' | 'low',
        time: item.time,
        status: item.status,
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
