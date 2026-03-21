import { createRouter, createWebHashHistory } from 'vue-router'
import DashboardPage from '@/pages/DashboardPage.vue'
import PlazaMonitorPage from '@/pages/PlazaMonitorPage.vue'
import EventCenterPage from '@/pages/EventCenterPage.vue'
import EventDetailPage from '@/pages/EventDetailPage.vue'
import HistoryPage from '@/pages/HistoryPage.vue'
import SettingsPage from '@/pages/SettingsPage.vue'
import MainLayout from '@/layouts/MainLayout.vue'

const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    {
      path: '/',
      component: MainLayout,
      children: [
        { path: '', redirect: '/dashboard' },
        { path: 'dashboard', component: DashboardPage },
        { path: 'plaza-monitor', component: PlazaMonitorPage },
        { path: 'events', component: EventCenterPage },
        { path: 'events/:id', component: EventDetailPage },
        { path: 'history', component: HistoryPage },
        { path: 'settings', component: SettingsPage }
      ]
    }
  ]
})

export default router
