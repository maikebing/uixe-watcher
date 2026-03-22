<template>
  <div class="grid gap-6 xl:grid-cols-[1.2fr_1fr]">
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

    <BulkTransportConfirmPanel :model-value="bulkTransportDraft" @confirm="confirmBulkTransport" @cancel="resetBulkTransport" />

    <BillInfoConfirmPanel :model-value="billInfoDraft" @confirm="confirmBillInfo" @cancel="resetBillInfo" />

    <div v-if="bulkTransportResult" class="glass-panel rounded-3xl p-4 xl:col-span-2 text-sm text-slate-300">
      {{ bulkTransportResult }}
    </div>

    <div v-if="billInfoResult" class="glass-panel rounded-3xl p-4 xl:col-span-2 text-sm text-slate-300">
      {{ billInfoResult }}
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue'
import BillInfoConfirmPanel from '@/components/BillInfoConfirmPanel.vue'
import BulkTransportConfirmPanel from '@/components/BulkTransportConfirmPanel.vue'
import { useAppStore } from '@/stores/app'

const store = useAppStore()

const bulkTransportDraft = reactive({
  vehId: '浙A12345',
  alex: 6,
  weight: 49.5,
  isValid: true,
  title: '大件运输车确认',
  largeWoods: {
    enStationId: '3301',
    exStationId: '3310',
    carLength: '18.5m',
    carWidth: '3.2m',
    carHeight: '4.5m',
    carAxleNum: '6'
  }
})

const bulkTransportResult = ref('')
const billInfoResult = ref('')

const billInfoDraft = reactive({
  operatorSummary: '杭州北收费站 · 001车道 · 张三(10001)',
  billCode: '3300198765',
  billNumber: '00012345'
})

function confirmBulkTransport() {
  bulkTransportResult.value = `已在 Web 承载层完成大件运输确认展示，待后续接入正式 API 提交。车辆：${bulkTransportDraft.vehId}`
}

function resetBulkTransport() {
  bulkTransportResult.value = '已取消本次大件运输确认。'
}

function confirmBillInfo() {
  billInfoResult.value = `已在 Web 承载层完成发票信息确认展示，待后续接入正式 API 提交。发票：${billInfoDraft.billCode}-${billInfoDraft.billNumber}`
}

function resetBillInfo() {
  billInfoResult.value = '已取消本次发票信息确认。'
}
</script>
