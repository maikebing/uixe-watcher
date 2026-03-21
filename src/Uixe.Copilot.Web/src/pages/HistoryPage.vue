<template>
  <div class="glass-panel rounded-3xl p-6">
    <div class="mb-5 text-lg font-medium text-white">历史查询</div>
    <div class="grid grid-cols-4 gap-4">
      <a-input v-model="filters.plazaName" placeholder="输入收费站" />
      <a-input v-model="filters.eventType" placeholder="输入事件类型" />
      <a-input v-model="filters.status" placeholder="输入状态" />
      <a-button type="primary" @click="loadHistory">查询</a-button>
    </div>

    <a-table :data="items" :pagination="false" class="mt-6">
      <template #columns>
        <a-table-column title="事件ID" data-index="id" />
        <a-table-column title="事件类型" data-index="title" />
        <a-table-column title="收费站" data-index="plazaName" />
        <a-table-column title="车道" data-index="laneNo" />
        <a-table-column title="级别" data-index="level" />
        <a-table-column title="状态" data-index="status" />
        <a-table-column title="时间" data-index="time" />
      </template>
    </a-table>
  </div>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue'
import { fetchHistory } from '@/api/mock'

const filters = reactive({
  plazaName: '',
  eventType: '',
  status: ''
})

const items = ref<Array<Record<string, string>>>([])

async function loadHistory() {
  const data = await fetchHistory({
    plazaName: filters.plazaName,
    eventType: filters.eventType,
    status: filters.status
  })

  items.value = data.items
}

onMounted(loadHistory)
</script>
