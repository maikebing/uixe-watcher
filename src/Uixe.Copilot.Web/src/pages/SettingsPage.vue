<template>
  <div class="grid gap-6 lg:grid-cols-2">
    <div class="glass-panel rounded-3xl p-6">
      <div class="mb-5 text-lg font-medium text-white">播报与通知策略</div>
      <a-form layout="vertical">
        <a-form-item label="语音播报">
          <a-switch :model-value="true" />
        </a-form-item>
        <a-form-item label="本地通知">
          <a-switch :model-value="true" />
        </a-form-item>
        <a-form-item label="深色主题">
          <a-switch :model-value="true" />
        </a-form-item>
      </a-form>
    </div>
    <div class="glass-panel rounded-3xl p-6">
      <div class="mb-5 text-lg font-medium text-white">迁移状态</div>
      <a-timeline>
        <a-timeline-item label="Phase 1">后端解耦进行中</a-timeline-item>
        <a-timeline-item label="Phase 2">实时通信待接入</a-timeline-item>
        <a-timeline-item label="Phase 3">Web 前端一期已建骨架</a-timeline-item>
      </a-timeline>
    </div>

    <div class="glass-panel rounded-3xl p-6 lg:col-span-2">
      <div class="mb-5 text-lg font-medium text-white">TrafficEvent 调试提交</div>
      <div class="grid gap-4 lg:grid-cols-4">
        <a-input v-model="form.recordId" placeholder="recordId" />
        <a-input v-model="form.eventType" placeholder="eventType" />
        <a-input v-model="form.laneNo" placeholder="LaneNo" />
        <a-button type="primary" @click="submit">提交测试事件</a-button>
      </div>
      <div class="mt-4 rounded-2xl border border-sky-500/10 bg-slate-900/40 p-4 text-sm text-slate-300">
        {{ resultText }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue'
import { submitTrafficEvent } from '@/api/mock'

const form = reactive({
  recordId: 'debug-evt-001',
  eventType: '45',
  laneNo: '001'
})

const resultText = ref('尚未提交')

async function submit() {
  const result = await submitTrafficEvent({
    recordId: form.recordId,
    eventType: form.eventType,
    LaneNo: form.laneNo
  })

  resultText.value = `${result.ok ? '成功' : '失败'}：${JSON.stringify(result.data)}`
}
</script>
