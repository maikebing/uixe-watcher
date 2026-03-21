<template>
  <div class="glass-panel rounded-3xl p-6">
    <div class="mb-5 flex items-center justify-between">
      <div class="text-lg font-medium text-white">事件中心</div>
      <a-input-search placeholder="搜索事件/车道/收费站" class="max-w-xs" />
    </div>
    <div class="space-y-3">
      <router-link v-for="event in store.events" :key="event.id" :to="`/events/${event.id}`" class="block rounded-2xl border border-sky-500/10 bg-slate-900/40 p-4 no-underline transition hover:border-cyan-400/30">
        <div class="flex items-center justify-between">
          <div>
            <div class="text-base font-medium text-white">{{ event.title }}</div>
            <div class="mt-1 text-sm text-slate-400">{{ event.plazaName }} · {{ event.laneNo }} 车道 · {{ event.time }}</div>
          </div>
          <div class="flex items-center gap-3">
            <a-tag :color="event.level === 'high' ? 'red' : event.level === 'medium' ? 'orange' : 'green'">{{ event.level }}</a-tag>
            <span class="text-sm text-slate-300">{{ event.status }}</span>
          </div>
        </div>
      </router-link>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { useAppStore } from '@/stores/app'
const store = useAppStore()

onMounted(async () => {
  if (!store.events.length) {
    await store.loadOverview()
  }
})
</script>
