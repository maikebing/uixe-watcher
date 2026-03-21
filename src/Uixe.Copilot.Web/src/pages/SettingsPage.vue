<template>
  <div class="grid gap-6 lg:grid-cols-2">
    <div class="glass-panel rounded-3xl p-6">
      <div class="mb-5 text-lg font-medium text-white">播报与通知策略</div>
      <a-form layout="vertical">
        <a-form-item label="语音播报">
          <a-switch v-model="settings.enableVoiceBroadcast" />
        </a-form-item>
        <a-form-item label="本地通知">
          <a-switch v-model="settings.enableLocalNotification" />
        </a-form-item>
        <a-form-item label="深色主题">
          <a-switch v-model="settings.enableDarkTheme" />
        </a-form-item>
        <a-button type="primary" @click="save">保存配置</a-button>
      </a-form>
    </div>
    <div class="glass-panel rounded-3xl p-6">
      <div class="mb-5 text-lg font-medium text-white">迁移状态</div>
      <a-timeline>
        <a-timeline-item v-for="item in settings.phaseMilestones" :key="item" :label="settings.currentPhase">{{ item }}</a-timeline-item>
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
import { onMounted, reactive, ref } from 'vue'
import { fetchSystemSettings, saveSystemSettings, submitTrafficEvent } from '@/api/mock'

const settings = reactive({
  enableVoiceBroadcast: true,
  enableLocalNotification: true,
  enableDarkTheme: true,
  currentPhase: 'Phase 2',
  phaseMilestones: [] as string[]
})

const form = reactive({
  recordId: 'debug-evt-001',
  eventType: '45',
  laneNo: '001'
})

const resultText = ref('尚未提交')

async function loadSettings() {
  const data = await fetchSystemSettings()
  settings.enableVoiceBroadcast = data.enableVoiceBroadcast
  settings.enableLocalNotification = data.enableLocalNotification
  settings.enableDarkTheme = data.enableDarkTheme
  settings.currentPhase = data.currentPhase
  settings.phaseMilestones = data.phaseMilestones
}

async function save() {
  const data = await saveSystemSettings({ ...settings })
  settings.enableVoiceBroadcast = data.enableVoiceBroadcast
  settings.enableLocalNotification = data.enableLocalNotification
  settings.enableDarkTheme = data.enableDarkTheme
  settings.currentPhase = data.currentPhase
  settings.phaseMilestones = data.phaseMilestones
  resultText.value = '配置保存成功'
}

async function submit() {
  const result = await submitTrafficEvent({
    recordId: form.recordId,
    eventType: form.eventType,
    LaneNo: form.laneNo
  })

  resultText.value = `${result.ok ? '成功' : '失败'}：${JSON.stringify(result.data)}`
}

onMounted(loadSettings)
</script>
