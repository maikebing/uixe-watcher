<template>
  <div class="grid gap-6 lg:grid-cols-[1.5fr_1fr]">
    <div class="glass-panel rounded-3xl p-6">
      <div class="text-lg font-medium text-white">事件详情</div>
      <div v-if="event" class="mt-5 space-y-4 text-slate-200">
        <div class="text-2xl font-semibold text-white">{{ event.title }}</div>
        <div>收费站：{{ event.plazaName }}</div>
        <div>车道：{{ event.laneNo }}</div>
        <div>时间：{{ event.time }}</div>
        <div>状态：{{ event.status }}</div>
        <a-button type="outline" @click="refresh">刷新详情</a-button>
      </div>
    </div>
    <div class="glass-panel rounded-3xl p-6">
      <div class="text-lg font-medium text-white">媒体预览</div>
      <div class="mt-5 space-y-4">
        <div v-if="event?.imageUrls?.length" class="rounded-2xl border border-sky-500/10 bg-slate-900/40 p-3">
          <div class="mb-2 text-sm text-slate-300">图片预览</div>
          <div class="grid gap-3">
            <img v-for="image in event.imageUrls" :key="image" :src="image" alt="事件图片" class="max-h-64 w-full rounded-xl object-cover" />
          </div>
        </div>
        <div v-if="event?.videoUrls?.length" class="rounded-2xl border border-sky-500/10 bg-slate-900/40 p-3">
          <div class="mb-2 text-sm text-slate-300">视频预览</div>
          <div class="grid gap-3">
            <video v-for="video in event.videoUrls" :key="video" :src="video" controls class="max-h-64 w-full rounded-xl" />
          </div>
        </div>
        <div v-if="!event?.imageUrls?.length && !event?.videoUrls?.length" class="rounded-2xl border border-dashed border-sky-400/20 bg-slate-900/40 p-8 text-center text-slate-400">
          暂无图片 / 视频预览
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { useAppStore } from '@/stores/app'

const route = useRoute()
const store = useAppStore()
const event = computed(() => store.eventById(String(route.params.id)))

onMounted(async () => {
  if (!event.value) {
    await store.loadEvent(String(route.params.id))
  }
})

async function refresh() {
  await store.refreshEvent(String(route.params.id))
}
</script>
