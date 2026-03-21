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
        <a-form-item label="桌面通知">
          <a-switch v-model="settings.enableDesktopToast" />
        </a-form-item>
        <a-form-item label="允许 VNC 调起">
          <a-switch v-model="settings.enableVncLaunch" />
        </a-form-item>
        <a-form-item label="交通事件音频播报">
          <a-switch v-model="settings.enableTrafficEventAudio" />
        </a-form-item>
        <a-form-item label="首选播报员">
          <a-input v-model="settings.preferredVoiceName" placeholder="例如：默认播报员" />
        </a-form-item>
        <a-form-item label="首选主题">
          <a-select v-model="settings.preferredTheme">
            <a-option value="dark">dark</a-option>
            <a-option value="light">light</a-option>
          </a-select>
        </a-form-item>
        <a-form-item label="TrafficEvent 存储模式">
          <a-select v-model="settings.trafficEventStorageMode">
            <a-option value="PostgreSQL">PostgreSQL</a-option>
            <a-option value="File">File</a-option>
            <a-option value="SQLite">SQLite</a-option>
          </a-select>
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

    <div class="glass-panel rounded-3xl p-6 lg:col-span-2">
      <div class="mb-5 text-lg font-medium text-white">本地 Agent 调试</div>
      <div class="grid gap-4 lg:grid-cols-4">
        <a-input v-model="agentForm.title" placeholder="通知标题" />
        <a-input v-model="agentForm.message" placeholder="通知内容 / 播报内容" />
        <a-input v-model="agentForm.vncHost" placeholder="VNC Host" />
        <a-input v-model="agentForm.vncPassword" placeholder="VNC Password" />
      </div>
      <div class="mt-4 flex flex-wrap gap-3">
        <a-button type="primary" @click="sendAgentNotification">发送本地告警</a-button>
        <a-button @click="sendAgentSpeech">本地语音播报</a-button>
        <a-button status="warning" @click="openAgentVnc">打开 VNC</a-button>
      </div>
      <div class="mt-4 rounded-2xl border border-sky-500/10 bg-slate-900/40 p-4 text-sm text-slate-300">
        {{ agentResultText }}
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue'
import { fetchSystemSettings, saveSystemSettings, submitTrafficEvent } from '@/api/mock'
import { notifyByAgent, openVncByAgent, speakByAgent } from '@/api/agentApi'

const settings = reactive({
  enableVoiceBroadcast: true,
  enableLocalNotification: true,
  enableDarkTheme: true,
  enableDesktopToast: true,
  enableVncLaunch: true,
  enableTrafficEventAudio: true,
  preferredVoiceName: '',
  preferredTheme: 'dark',
  trafficEventStorageMode: 'PostgreSQL',
  currentPhase: 'Phase 2',
  phaseMilestones: [] as string[]
})

const form = reactive({
  recordId: 'debug-evt-001',
  eventType: '45',
  laneNo: '001'
})

const agentForm = reactive({
  title: '交通事件提醒',
  message: 'X02 车道发现异常车辆',
  vncHost: '127.0.0.1',
  vncPassword: 'kissme'
})

const resultText = ref('尚未提交')
const agentResultText = ref('尚未调用本地 Agent')

async function loadSettings() {
  const data = await fetchSystemSettings()
  settings.enableVoiceBroadcast = data.enableVoiceBroadcast
  settings.enableLocalNotification = data.enableLocalNotification
  settings.enableDarkTheme = data.enableDarkTheme
  settings.enableDesktopToast = data.enableDesktopToast
  settings.enableVncLaunch = data.enableVncLaunch
  settings.enableTrafficEventAudio = data.enableTrafficEventAudio
  settings.preferredVoiceName = data.preferredVoiceName
  settings.preferredTheme = data.preferredTheme
  settings.trafficEventStorageMode = data.trafficEventStorageMode
  settings.currentPhase = data.currentPhase
  settings.phaseMilestones = data.phaseMilestones
}

async function save() {
  const data = await saveSystemSettings({ ...settings })
  settings.enableVoiceBroadcast = data.enableVoiceBroadcast
  settings.enableLocalNotification = data.enableLocalNotification
  settings.enableDarkTheme = data.enableDarkTheme
  settings.enableDesktopToast = data.enableDesktopToast
  settings.enableVncLaunch = data.enableVncLaunch
  settings.enableTrafficEventAudio = data.enableTrafficEventAudio
  settings.preferredVoiceName = data.preferredVoiceName
  settings.preferredTheme = data.preferredTheme
  settings.trafficEventStorageMode = data.trafficEventStorageMode
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

async function sendAgentNotification() {
  try {
    const result = await notifyByAgent(agentForm.title, agentForm.message, {
      playSpeech: settings.enableVoiceBroadcast,
      text: agentForm.message,
      voiceName: settings.preferredVoiceName || undefined
    })

    agentResultText.value = `告警调用成功：${result.message}`
  } catch (error) {
    agentResultText.value = `告警调用失败：${error instanceof Error ? error.message : String(error)}`
  }
}

async function sendAgentSpeech() {
  try {
    const result = await speakByAgent(agentForm.message, {
      voiceName: settings.preferredVoiceName || undefined
    })

    agentResultText.value = `语音调用成功：${result.message}`
  } catch (error) {
    agentResultText.value = `语音调用失败：${error instanceof Error ? error.message : String(error)}`
  }
}

async function openAgentVnc() {
  try {
    const result = await openVncByAgent(agentForm.vncHost, 5900, agentForm.vncPassword, `${agentForm.vncHost} 远程桌面`)
    agentResultText.value = `VNC 调用成功：${result.message}`
  } catch (error) {
    agentResultText.value = `VNC 调用失败：${error instanceof Error ? error.message : String(error)}`
  }
}

onMounted(loadSettings)
</script>
