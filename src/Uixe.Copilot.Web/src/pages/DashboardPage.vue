<template>
  <div class="grid gap-6">
    <section class="grid grid-cols-5 gap-4">
      <div v-for="card in cards" :key="card.label" class="glass-panel rounded-2xl p-5">
        <div class="text-sm text-slate-400">{{ card.label }}</div>
        <div class="mt-3 text-3xl font-semibold text-white">{{ card.value }}</div>
      </div>
    </section>

    <section class="grid grid-cols-[2fr_1fr] gap-6">
      <div class="glass-panel rounded-3xl p-6">
        <div class="mb-4 text-lg font-medium text-white">今日事件趋势</div>
        <v-chart class="h-[320px]" :option="chartOption" autoresize />
      </div>
      <div class="glass-panel rounded-3xl p-6">
        <div class="mb-4 text-lg font-medium text-white">重点站点态势</div>
        <div class="space-y-4">
          <div v-for="plaza in store.plazas" :key="plaza.id" class="rounded-2xl border border-sky-400/10 bg-slate-900/40 p-4">
            <div class="flex items-center justify-between">
              <div>
                <div class="font-medium text-white">{{ plaza.name }}</div>
                <div class="text-xs text-slate-400">{{ plaza.id }}</div>
              </div>
              <a-tag :color="plaza.status === 'warning' ? 'orange' : 'green'">{{ plaza.status }}</a-tag>
            </div>
            <div class="mt-3 text-sm text-slate-300">在线车道 {{ plaza.lanesOnline }}/{{ plaza.lanesTotal }} · 告警 {{ plaza.alerts }}</div>
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import VChart from 'vue-echarts'
import { useAppStore } from '@/stores/app'

const store = useAppStore()

const cards = computed(() => [
  { label: '在线站点', value: `${store.overview.onlineStations}/${store.overview.totalStations}` },
  { label: '活跃告警', value: store.overview.activeAlerts },
  { label: '今日事件', value: store.overview.todayEvents },
  { label: '实时消息', value: store.overview.realtimeMessages },
  { label: '迁移进度', value: 'Phase 1+' }
])

const chartOption = computed(() => ({
  tooltip: { trigger: 'axis' },
  xAxis: {
    type: 'category',
    data: ['08:00', '09:00', '10:00', '11:00', '12:00', '13:00', '14:00'],
    axisLine: { lineStyle: { color: '#4b6b9b' } }
  },
  yAxis: {
    type: 'value',
    splitLine: { lineStyle: { color: 'rgba(148,163,184,0.15)' } }
  },
  series: [
    {
      type: 'line',
      smooth: true,
      data: store.trend,
      lineStyle: { color: '#22d3ee', width: 3 },
      areaStyle: { color: 'rgba(34,211,238,0.18)' }
    }
  ]
}))
</script>
