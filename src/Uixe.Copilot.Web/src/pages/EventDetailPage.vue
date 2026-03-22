<template>
  <div class="grid gap-6 lg:grid-cols-[1.4fr_1fr]">
    <div class="glass-panel rounded-3xl p-6">
      <div class="text-lg font-medium text-white">事件详情</div>
      <div v-if="event" class="mt-5 space-y-4 text-slate-200">
        <div class="text-2xl font-semibold text-white">{{ event.title }}</div>
        <div class="grid gap-3 md:grid-cols-2">
          <div>收费站：{{ event.plazaName }}</div>
          <div>车道：{{ event.laneNo }}</div>
          <div>时间：{{ event.time }}</div>
          <div>状态：{{ event.status }}</div>
          <div>记录号：{{ event.recordId ?? '无' }}</div>
          <div>事件类型：{{ event.eventType ?? '无' }}</div>
          <div>抓拍时间：{{ event.capTime ?? '无' }}</div>
          <div>开始时间：{{ event.startTime ?? '无' }}</div>
          <div>持续时长：{{ event.durationText ?? '无' }}</div>
          <div>排队长度：{{ event.queueLengthText ?? '无' }}</div>
        </div>
        <div class="rounded-2xl border border-slate-700/60 bg-slate-950/40 p-4 text-sm text-slate-300">
          {{ event.summary ?? '暂无事件摘要' }}
        </div>
        <a-button type="outline" @click="refresh">刷新详情</a-button>
      </div>
    </div>
    <div class="glass-panel rounded-3xl p-6">
      <div class="text-lg font-medium text-white">媒体预览</div>
      <div class="mt-5">
        <MediaPreviewPanel :image-urls="event?.imageUrls ?? []" :video-urls="event?.videoUrls ?? []" @play-video="playVideoByAgentFromDetail" />
      </div>
      <div v-if="agentPlayResult" class="mt-4 rounded-2xl border border-sky-500/10 bg-slate-900/40 p-3 text-sm text-slate-300">
        {{ agentPlayResult }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useAppStore } from '@/stores/app'
import MediaPreviewPanel from '@/components/MediaPreviewPanel.vue'
import { playVideoByAgent } from '@/api/agentApi'

const route = useRoute()
const store = useAppStore()
const event = computed(() => store.eventById(String(route.params.id)))
const agentPlayResult = ref('')

onMounted(async () => {
  if (!event.value) {
    await store.loadEvent(String(route.params.id))
  }
})

async function refresh() {
  await store.refreshEvent(String(route.params.id))
}

async function playVideoByAgentFromDetail(videoUrl: string) {
  try {
    const result = await playVideoByAgent(
      videoUrl,
      event.value?.title ? `${event.value.title} 视频` : '事件视频',
      1280,
      720,
      event.value?.id ? `event-${event.value.id}` : 'event-detail-video'
    )
    agentPlayResult.value = `视频播放调用成功：${result.message}`
  } catch (error) {
    agentPlayResult.value = `视频播放调用失败：${error instanceof Error ? error.message : String(error)}`
  }
}
</script>
