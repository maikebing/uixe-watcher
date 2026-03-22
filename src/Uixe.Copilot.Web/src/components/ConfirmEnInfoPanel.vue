<template>
  <div class="rounded-3xl border border-violet-500/10 bg-slate-900/40 p-5">
    <div class="mb-4 flex items-center justify-between">
      <div>
        <div class="text-lg font-medium text-white">入口信息确认</div>
        <div class="mt-1 text-xs text-slate-400">对应旧窗体 `frmConfirmEnInfo` 的 Web 承载。</div>
      </div>
      <a-tag :color="modelValue.resCount > 0 ? 'green' : 'orange'">{{ modelValue.resCount > 0 ? '已查询到入口' : '待人工确认' }}</a-tag>
    </div>

    <div class="grid gap-4 md:grid-cols-2">
      <a-form-item label="车道号">
        <a-input :model-value="modelValue.laneId" readonly />
      </a-form-item>
      <a-form-item label="车牌号">
        <a-input :model-value="modelValue.vehicleId" readonly />
      </a-form-item>
      <a-form-item label="车型">
        <a-input :model-value="String(modelValue.vehicleType)" readonly />
      </a-form-item>
      <a-form-item label="生成时间">
        <a-input :model-value="modelValue.genTime" readonly />
      </a-form-item>
    </div>

    <div class="mt-4 rounded-2xl border border-slate-700/60 bg-slate-950/40 p-4">
      <div class="mb-3 text-sm font-medium text-slate-200">入口站记录</div>
      <a-table :data="modelValue.enStations" :pagination="false" size="small">
        <template #columns>
          <a-table-column title="入口站" data-index="enStationId" />
          <a-table-column title="入口时间" data-index="enTime" />
          <a-table-column title="入口车道" data-index="enTollLaneId" />
          <a-table-column title="介质号" data-index="mediaNo" />
        </template>
      </a-table>
    </div>

    <div class="mt-5 flex justify-end gap-3">
      <a-button @click="emit('cancel')">取消</a-button>
      <a-button type="primary" @click="emit('confirm', modelValue)">确认提交</a-button>
    </div>
  </div>
</template>

<script setup lang="ts">
export interface EnStationViewModel {
  enStationId?: string
  enTime?: string
  enTollLaneId?: string
  mediaNo?: string
}

export interface ConfirmEnInfoViewModel {
  laneId?: string
  genTime?: string
  vehicleId?: string
  vehicleType: number
  resCount: number
  enStations: EnStationViewModel[]
}

defineProps<{
  modelValue: ConfirmEnInfoViewModel
}>()

const emit = defineEmits<{
  (e: 'confirm', value: ConfirmEnInfoViewModel): void
  (e: 'cancel'): void
}>()
</script>