import { defineStore } from 'pinia'
import { mockAppState } from '@/api/mock'

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
}

export interface App {
  overview: OverviewMetrics
  trend: number[]
  plazas: PlazaStatusItem[]
  events: EventItem[]
}

export const useAppStore = defineStore('app', {
  state: (): App => mockAppState,
  getters: {
    eventById: (state) => (id: string) => state.events.find((item) => item.id === id)
  }
})
