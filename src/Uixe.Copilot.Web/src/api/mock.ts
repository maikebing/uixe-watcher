import type { App } from '@/stores/app'

export const mockAppState: App = {
  overview: {
    onlineStations: 24,
    totalStations: 28,
    activeAlerts: 6,
    todayEvents: 132,
    realtimeMessages: 18
  },
  trend: [32, 45, 41, 66, 58, 75, 89],
  plazas: [
    { id: '6500256', name: '城北收费站', status: 'online', lanesOnline: 8, lanesTotal: 10, alerts: 2 },
    { id: '6500257', name: '高新收费站', status: 'warning', lanesOnline: 6, lanesTotal: 8, alerts: 3 },
    { id: '6500258', name: '机场收费站', status: 'online', lanesOnline: 12, lanesTotal: 12, alerts: 0 }
  ],
  events: [
    { id: 'evt-1', title: '排队溢出', plazaName: '城北收费站', laneNo: '103', level: 'high', time: '12:18:47', status: '待处理' },
    { id: 'evt-2', title: '发票信息确认', plazaName: '高新收费站', laneNo: '205', level: 'medium', time: '12:22:11', status: '处理中' },
    { id: 'evt-3', title: '入口信息确认', plazaName: '机场收费站', laneNo: '008', level: 'low', time: '12:25:33', status: '已完成' }
  ]
}
