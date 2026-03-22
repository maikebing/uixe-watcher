<template>
  <div class="glass-panel rounded-3xl p-6">
    <div class="mb-5 flex items-center justify-between">
      <div class="text-lg font-medium text-white">事件中心</div>
      <a-input-search placeholder="搜索事件/车道/收费站" class="max-w-xs" />
    </div>
    <div class="space-y-3">
      <div v-for="event in store.events" :key="event.id" class="rounded-2xl border border-sky-500/10 bg-slate-900/40 p-4 transition hover:border-cyan-400/30">
        <div class="flex items-center justify-between gap-4">
          <router-link :to="`/events/${event.id}`" class="block min-w-0 flex-1 no-underline">
            <div class="text-base font-medium text-white">{{ event.title }}</div>
            <div class="mt-1 text-sm text-slate-400">{{ event.plazaName }} · {{ event.laneNo }} 车道 · {{ event.time }}</div>
          </router-link>
          <div class="flex items-center gap-3">
            <a-tag :color="event.level === 'high' ? 'red' : event.level === 'medium' ? 'orange' : 'green'">{{ event.level }}</a-tag>
            <span class="text-sm text-slate-300">{{ event.status }}</span>
            <a-button v-if="getPreferredVideo(event)" size="mini" type="outline" @click="playEventVideo(event)">播放视频</a-button>
          </div>
        </div>
      </div>
    </div>
    <div v-if="agentPlayResult" class="mt-4 rounded-2xl border border-sky-500/10 bg-slate-900/40 p-3 text-sm text-slate-300">
      {{ agentPlayResult }}
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { playVideoByAgent } from '@/api/agentApi'
import { useAppStore, type EventItem } from '@/stores/app'

const store = useAppStore()
const agentPlayResult = ref('')

onMounted(async () => {
  if (!store.events.length) {
    await store.loadOverview()
  }
})

function getPreferredVideo(event: EventItem) {
  return event.videoUrls?.[0] ?? event.videoUrl
}

async function playEventVideo(event: EventItem) {
  const videoUrl = getPreferredVideo(event)
  if (!videoUrl) {
    agentPlayResult.value = '当前事件没有可播放的视频'
    return
  }

  try {
    const result = await playVideoByAgent(videoUrl, `${event.title} 视频`, 1280, 720, `event-${event.id}`)
    agentPlayResult.value = `视频播放调用成功：${result.message}`
  } catch (error) {
    agentPlayResult.value = `视频播放调用失败：${error instanceof Error ? error.message : String(error)}`
  }
}
</script>
