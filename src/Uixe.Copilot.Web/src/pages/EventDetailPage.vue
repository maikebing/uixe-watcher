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
      </div>
    </div>
    <div class="glass-panel rounded-3xl p-6">
      <div class="text-lg font-medium text-white">媒体预览</div>
      <div class="mt-5 rounded-2xl border border-dashed border-sky-400/20 bg-slate-900/40 p-8 text-center text-slate-400">
        预留图片 / 视频预览区域
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { useAppStore } from '@/stores/app'

const route = useRoute()
const store = useAppStore()
const event = computed(() => store.eventById(String(route.params.id)))
</script>
