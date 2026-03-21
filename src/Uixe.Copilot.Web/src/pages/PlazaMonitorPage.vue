<template>
  <div class="glass-panel rounded-3xl p-6">
    <div class="mb-5 text-lg font-medium text-white">收费站监控</div>
    <a-table :data="store.plazas" :pagination="false">
      <template #columns>
        <a-table-column title="站点" data-index="name" />
        <a-table-column title="编号" data-index="id" />
        <a-table-column title="在线车道">
          <template #cell="{ record }">{{ record.lanesOnline }}/{{ record.lanesTotal }}</template>
        </a-table-column>
        <a-table-column title="告警数" data-index="alerts" />
        <a-table-column title="状态">
          <template #cell="{ record }">
            <a-tag :color="record.status === 'warning' ? 'orange' : 'green'">{{ record.status }}</a-tag>
          </template>
        </a-table-column>
      </template>
    </a-table>
  </div>
</template>

<script setup lang="ts">
import { useAppStore } from '@/stores/app'
const store = useAppStore()
</script>
