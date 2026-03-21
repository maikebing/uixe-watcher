export interface TrafficEventOverviewResponse {
  onlineStations: number
  totalStations: number
  activeAlerts: number
  todayEvents: number
  realtimeMessages: number
  trend: number[]
  plazas: Array<{ id: string; name: string; status: string; lanesOnline: number; lanesTotal: number; alerts: number }>
  events: Array<{ id: string; title: string; plazaName: string; laneNo: string; level: string; time: string; status: string; imageUrl?: string; videoUrl?: string }>
}

const baseUrl = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5057'

// SignalR 接入预留：后续迁移到实时推送时统一在此处初始化连接

export async function fetchOverview(): Promise<TrafficEventOverviewResponse> {
  const response = await fetch(`${baseUrl}/api/traffic-events/overview`)
  if (!response.ok) {
    throw new Error('无法加载事件总览数据')
  }

  return response.json()
}

export async function fetchEventById(eventId: string) {
  const response = await fetch(`${baseUrl}/api/traffic-events/${eventId}`)
  if (response.status === 404) {
    return null
  }

  if (!response.ok) {
    throw new Error('无法加载事件详情')
  }

  return response.json()
}

export async function submitTrafficEvent(payload: Record<string, unknown>) {
  const response = await fetch(`${baseUrl}/api/traffic-events`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(payload)
  })

  const data = await response.json()
  return { ok: response.ok, data }
}
