<template>
  <div class="glass-panel rounded-3xl p-6">
    <div class="mb-5 flex items-center justify-between gap-3">
      <div class="text-lg font-medium text-white">消息流</div>
      <div class="text-xs text-slate-400">对应旧 `messageView` 与状态播报入口</div>
    </div>
    <a-timeline>
      <a-timeline-item v-for="item in items" :key="item.id" :label="item.time">
        <div class="text-sm text-white">{{ item.title }}</div>
        <div class="mt-1 text-xs text-slate-400">{{ item.detail }}</div>
      </a-timeline-item>
    </a-timeline>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  items: Array<{
    id: string
    time: string
    title: string
    detail: string
  }>
}>()
</script>