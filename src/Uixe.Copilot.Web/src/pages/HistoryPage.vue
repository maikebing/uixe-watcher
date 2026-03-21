<template>
  <div class="glass-panel rounded-3xl p-6">
    <div class="mb-5 text-lg font-medium text-white">历史查询</div>
    <div class="grid grid-cols-5 gap-4">
      <a-input v-model="filters.plazaName" placeholder="输入收费站" />
      <a-input v-model="filters.eventType" placeholder="输入事件类型" />
      <a-input v-model="filters.status" placeholder="输入状态" />
      <a-range-picker v-model="timeRange" show-time />
      <div class="flex gap-2">
        <a-button type="primary" @click="loadHistory">查询</a-button>
        <a-button @click="exportHistory">导出 CSV</a-button>
      </div>
    </div>

    <a-table :data="items" :pagination="pagination" @page-change="handlePageChange" class="mt-6">
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

const timeRange = ref([])

const items = ref<Array<Record<string, string>>>([])
const pagination = reactive({
  total: 0,
  current: 1,
  pageSize: 10
})

async function loadHistory() {
  const data = await fetchHistory({
    plazaName: filters.plazaName,
    eventType: filters.eventType,
    status: filters.status,
    startTime: timeRange.value?.[0] ?? '',
    endTime: timeRange.value?.[1] ?? '',
    pageNo: String(pagination.current),
    pageSize: String(pagination.pageSize)
  })

  items.value = data.items
  pagination.total = data.total
  pagination.current = data.pageNo
  pagination.pageSize = data.pageSize
}

async function handlePageChange(page: number) {
  pagination.current = page
  await loadHistory()
}

function exportHistory() {
  const headers = ['事件ID', '事件类型', '收费站', '车道', '级别', '状态', '时间']
  const rows = items.value.map(item => [item.id, item.title, item.plazaName, item.laneNo, item.level, item.status, item.time])
  const csv = [headers, ...rows]
    .map(row => row.map(cell => `"${String(cell ?? '').replace(/"/g, '""')}"`).join(','))
    .join('\n')

  const blob = new Blob(['\ufeff' + csv], { type: 'text/csv;charset=utf-8;' })
  const url = URL.createObjectURL(blob)
  const anchor = document.createElement('a')
  anchor.href = url
  anchor.download = `traffic-event-history-${Date.now()}.csv`
  anchor.click()
  URL.revokeObjectURL(url)
}

onMounted(loadHistory)
</script>
