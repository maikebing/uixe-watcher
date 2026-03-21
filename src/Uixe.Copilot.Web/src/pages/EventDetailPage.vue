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
      <div class="text-lg font-medium text-white">????</div>
      <div class="mt-5">
        <MediaPreviewPanel :image-urls="event?.imageUrls ?? []" :video-urls="event?.videoUrls ?? []" />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { useAppStore } from '@/stores/app'
import MediaPreviewPanel from '@/components/MediaPreviewPanel.vue'

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
