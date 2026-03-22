<template>
  <div class="rounded-3xl border border-emerald-500/10 bg-slate-900/40 p-5">
    <div class="mb-4 flex items-center justify-between">
      <div>
        <div class="text-lg font-medium text-white">大件运输确认</div>
        <div class="mt-1 text-xs text-slate-400">对应旧窗体 `frmBulktrans` 的 Web 承载。</div>
      </div>
      <a-tag :color="modelValue.isValid ? 'green' : 'red'">{{ modelValue.isValid ? '合法' : '需复核' }}</a-tag>
    </div>

    <div class="grid gap-4 md:grid-cols-2">
      <a-form-item label="车牌号">
        <a-input :model-value="modelValue.vehId" readonly />
      </a-form-item>
      <a-form-item label="标题">
        <a-input :model-value="modelValue.title" readonly />
      </a-form-item>
      <a-form-item label="轴数">
        <a-input-number :model-value="modelValue.alex" readonly class="w-full" />
      </a-form-item>
      <a-form-item label="重量(吨)">
        <a-input-number :model-value="modelValue.weight" readonly class="w-full" />
      </a-form-item>
    </div>

    <div v-if="modelValue.largeWoods" class="mt-4 rounded-2xl border border-slate-700/60 bg-slate-950/40 p-4">
      <div class="mb-3 text-sm font-medium text-slate-200">大件信息</div>
      <div class="grid gap-3 md:grid-cols-2 text-sm text-slate-300">
        <div>入口站：{{ modelValue.largeWoods.enStationId || '无' }}</div>
        <div>出口站：{{ modelValue.largeWoods.exStationId || '无' }}</div>
        <div>车长：{{ modelValue.largeWoods.carLength || '无' }}</div>
        <div>车宽：{{ modelValue.largeWoods.carWidth || '无' }}</div>
        <div>车高：{{ modelValue.largeWoods.carHeight || '无' }}</div>
        <div>轴数：{{ modelValue.largeWoods.carAxleNum || '无' }}</div>
      </div>
    </div>

    <div class="mt-5 flex justify-end gap-3">
      <a-button @click="emit('cancel')">取消</a-button>
      <a-button type="primary" @click="emit('confirm', modelValue)">确认提交</a-button>
    </div>
  </div>
</template>

<script setup lang="ts">
export interface LargeWoodsViewModel {
  enStationId?: string
  exStationId?: string
  carLength?: string
  carWidth?: string
  carHeight?: string
  carAxleNum?: string
}

export interface BulkTransportViewModel {
  vehId?: string
  alex: number
  weight: number
  isValid: boolean
  title?: string
  largeWoods?: LargeWoodsViewModel
}

defineProps<{
  modelValue: BulkTransportViewModel
}>()

const emit = defineEmits<{
  (e: 'confirm', value: BulkTransportViewModel): void
  (e: 'cancel'): void
}>()
</script>