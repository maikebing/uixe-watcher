<template>
  <div class="rounded-3xl border border-cyan-500/10 bg-slate-900/40 p-5">
    <div class="mb-4 flex items-center justify-between">
      <div>
        <div class="text-lg font-medium text-white">发票信息确认</div>
        <div class="mt-1 text-xs text-slate-400">对应旧窗体 `frmBillInfo` 的 Web 承载。</div>
      </div>
      <a-tag color="blue">待提交</a-tag>
    </div>

    <div class="grid gap-4 md:grid-cols-2">
      <a-form-item label="收费站 / 车道 / 收费员">
        <a-input :model-value="modelValue.operatorSummary" readonly />
      </a-form-item>
      <a-form-item label="发票代码">
        <a-input :model-value="modelValue.billCode" readonly />
      </a-form-item>
      <a-form-item label="发票号码" class="md:col-span-2">
        <a-input :model-value="modelValue.billNumber" readonly />
      </a-form-item>
    </div>

    <div class="mt-5 flex justify-end gap-3">
      <a-button @click="emit('cancel')">取消</a-button>
      <a-button type="primary" @click="emit('confirm', modelValue)">确认提交</a-button>
    </div>
  </div>
</template>

<script setup lang="ts">
export interface BillInfoViewModel {
  operatorSummary?: string
  billCode?: string
  billNumber?: string
}

defineProps<{
  modelValue: BillInfoViewModel
}>()

const emit = defineEmits<{
  (e: 'confirm', value: BillInfoViewModel): void
  (e: 'cancel'): void
}>()
</script>