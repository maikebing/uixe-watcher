<template>
  <div class="grid gap-6 xl:grid-cols-[1.2fr_1fr]">
    <div v-if="pageError" class="glass-panel rounded-3xl p-4 xl:col-span-2 text-sm text-red-200">
      {{ pageError }}
    </div>

    <div v-if="isLoading" class="glass-panel rounded-3xl p-4 xl:col-span-2 text-sm text-slate-300">
      正在加载收费站监控数据...
    </div>

    <div class="glass-panel rounded-3xl p-6">
      <div class="mb-5 flex items-center justify-between gap-3">
        <div class="text-lg font-medium text-white">收费站监控</div>
        <div class="flex items-center gap-2">
          <div class="rounded-full border border-emerald-500/20 bg-emerald-500/10 px-3 py-1 text-xs text-emerald-300">
            {{ statusText }}
          </div>
          <a-button size="mini" @click="refreshMonitor">刷新监控</a-button>
        </div>
      </div>
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
          <a-table-column title="快捷操作">
            <template #cell="{ record }">
              <div class="flex flex-wrap gap-2">
                <a-button size="mini" @click="openPlazaVnc(record)">打开 VNC</a-button>
                <a-button size="mini" status="warning" @click="focusPlazaEvents(record)">事件详情</a-button>
                <a-button size="mini" type="outline" @click="openPlazaEventCenter(record)">事件中心</a-button>
              </div>
            </template>
          </a-table-column>
        </template>
      </a-table>
    </div>

    <div class="glass-panel rounded-3xl p-6">
      <div class="mb-5 flex items-center justify-between gap-3">
        <div class="text-lg font-medium text-white">车道视图</div>
        <div class="text-xs text-slate-400">继续承接旧 `lanView` 状态监控</div>
      </div>
      <a-select v-model="selectedPlazaId" class="mb-4" placeholder="选择收费站">
        <a-option v-for="item in store.plazas" :key="item.id" :value="item.id">{{ item.name }}</a-option>
      </a-select>
      <div class="mb-4 text-xs text-slate-400">
        当前查看：{{ selectedPlaza?.name || '未选择收费站' }}，在线车道 {{ selectedPlaza?.lanesOnline ?? 0 }}/{{ selectedPlaza?.lanesTotal ?? 0 }}
      </div>
      <div class="grid gap-3 md:grid-cols-2 xl:grid-cols-3">
        <div
          v-for="lane in selectedLaneDetails"
          :key="lane.id"
          class="rounded-2xl border border-sky-500/10 bg-slate-900/40 p-4"
          @click="focusLane(lane)"
        >
          <div class="flex items-center justify-between gap-3">
            <div>
              <div class="text-sm font-medium text-white">{{ lane.laneNo }} 车道</div>
              <div class="mt-1 text-xs text-slate-400">最近心跳：{{ lane.lastHeartbeat }}</div>
            </div>
            <a-tag :color="lane.status === 'warning' ? 'orange' : lane.status === 'online' ? 'green' : 'gray'">
              {{ lane.status }}
            </a-tag>
          </div>
          <div v-if="lane.isLost" class="mt-3 rounded-xl border border-red-500/20 bg-red-500/10 px-3 py-2 text-xs text-red-200">
            车道掉线，请检查网络或设备心跳。
          </div>
          <div class="mt-4 grid grid-cols-2 gap-3 text-xs text-slate-300">
            <div class="rounded-xl bg-slate-950/40 p-3">
              <div class="text-slate-400">车流</div>
              <div class="mt-1 text-base font-semibold text-white">{{ lane.vehicleCount }}</div>
            </div>
            <div class="rounded-xl bg-slate-950/40 p-3">
              <div class="text-slate-400">告警</div>
              <div class="mt-1 text-base font-semibold" :class="lane.hasWarning ? 'text-orange-300' : 'text-emerald-300'">
                {{ lane.hasWarning ? '待处理' : '正常' }}
              </div>
            </div>
          </div>
          <div class="mt-3 grid grid-cols-2 gap-2 text-[11px] text-slate-400">
            <div>收费员：{{ lane.collectorName || '未知' }}</div>
            <div>模式：{{ lane.workMode || '未知' }}</div>
            <div>网络：{{ lane.networkStatus ? '正常' : '异常' }}</div>
            <div>相机：{{ lane.cameraStatus ? '正常' : '异常' }}</div>
          </div>
          <div v-if="lane.alerts?.length" class="mt-3 rounded-xl border border-orange-500/10 bg-orange-500/5 p-3 text-xs text-orange-100">
            <div class="mb-2 font-medium">最近告警</div>
            <div v-for="alert in lane.alerts.slice(0, 2)" :key="`${lane.id}-${alert.time}-${alert.category}`" class="mb-2 last:mb-0">
              <div>{{ alert.title }}</div>
              <div class="mt-1 text-[11px] text-orange-200/80">{{ alert.content }} · {{ alert.time }}</div>
            </div>
          </div>
          <div v-if="lane.messages?.length" class="mt-3 rounded-xl border border-sky-500/10 bg-sky-500/5 p-3 text-xs text-slate-200">
            <div class="mb-2 font-medium">最近消息</div>
            <div v-for="message in lane.messages.slice(0, 2)" :key="`${lane.id}-${message.time}-${message.type}`" class="mb-2 last:mb-0">
              <div>{{ message.type }} · {{ message.content }}</div>
              <div class="mt-1 text-[11px] text-slate-400">{{ message.time }}</div>
            </div>
          </div>
          <div class="mt-3 text-xs text-slate-400">{{ lane.lastMessage }}</div>
          <div class="mt-3 flex flex-wrap gap-2">
            <a-button size="mini" status="danger" @click.stop="markLaneLost(lane)">标记掉线</a-button>
            <a-button size="mini" type="outline" @click.stop="notifyLaneAlert(lane)">本地通知</a-button>
            <a-button size="mini" @click.stop="broadcastLaneStatus(lane)">语音播报</a-button>
          </div>
        </div>
      </div>
    </div>

    <LaneActivityTimeline :items="activityFeed" />

    <div class="glass-panel rounded-3xl p-6 xl:col-span-2">
      <div class="mb-5 flex items-center justify-between gap-3">
        <div class="text-lg font-medium text-white">最近联动入口</div>
        <div class="text-xs text-slate-400">用于快速从监控页跳转到事件中心与详情</div>
      </div>
      <div class="grid gap-3 md:grid-cols-2 xl:grid-cols-3">
        <div
          v-for="item in linkedActivityItems"
          :key="item.id"
          class="rounded-2xl border border-sky-500/10 bg-slate-900/40 p-4"
        >
          <div class="text-sm font-medium text-white">{{ item.title }}</div>
          <div class="mt-2 text-xs text-slate-400">{{ item.detail }}</div>
          <div class="mt-3 text-[11px] text-slate-500">{{ item.time }}</div>
          <div class="mt-4 flex flex-wrap gap-2">
            <a-button size="mini" type="primary" @click="openActivityEvent(item.relatedEventId)">查看事件</a-button>
            <a-button size="mini" @click="openEventCenterByKeyword(item.keyword)">事件中心</a-button>
          </div>
        </div>
      </div>
    </div>

    <BulkTransportConfirmPanel :model-value="bulkTransportDraft" @confirm="confirmBulkTransport" @cancel="resetBulkTransport" />

    <BillInfoConfirmPanel :model-value="billInfoDraft" @confirm="confirmBillInfo" @cancel="resetBillInfo" />

    <ConfirmEnInfoPanel :model-value="confirmEnInfoDraft" @confirm="confirmEnInfo" @cancel="resetConfirmEnInfo" />

    <div class="glass-panel rounded-3xl p-6 xl:col-span-2">
      <div class="mb-5 flex items-center justify-between gap-3">
        <div class="text-lg font-medium text-white">交通事件快捷入口</div>
        <div class="text-xs text-slate-400">继续承接旧 `frmTrafficEvent` / `frmPlaza` 联动职责</div>
      </div>
      <div class="mb-4 grid gap-3 md:grid-cols-[1fr_1fr_auto]">
        <a-input v-model="quickEventForm.recordId" placeholder="recordId" />
        <a-input v-model="quickEventForm.laneNo" placeholder="laneNo" />
        <a-button type="primary" @click="submitQuickTrafficEvent">快速触发测试事件</a-button>
      </div>
      <div class="grid gap-4 md:grid-cols-3">
        <div
          v-for="event in recentEvents"
          :key="event.id"
          class="rounded-2xl border border-sky-500/10 bg-slate-900/40 p-4"
        >
          <div class="flex items-start justify-between gap-3">
            <div>
              <div class="text-sm font-medium text-white">{{ event.title }}</div>
              <div class="mt-1 text-xs text-slate-400">{{ event.plazaName }} · {{ event.laneNo }} 车道</div>
            </div>
            <a-tag :color="event.level === 'high' ? 'red' : event.level === 'medium' ? 'orange' : 'arcoblue'">
              {{ event.level }}
            </a-tag>
          </div>
          <div class="mt-3 text-xs text-slate-300">{{ event.summary || event.status }}</div>
          <div class="mt-4 flex flex-wrap gap-2">
            <a-button size="mini" type="primary" @click="openEventDetail(event.id)">查看详情</a-button>
            <a-button size="mini" @click="playEventVideo(event)">本地播放</a-button>
          </div>
        </div>
      </div>
    </div>

    <div class="glass-panel rounded-3xl p-6 xl:col-span-2">
      <div class="mb-5 flex items-center justify-between gap-3">
        <div class="text-lg font-medium text-white">链路结果面板</div>
        <div class="text-xs text-slate-400">以阶段卡片展示当前监控页联调结果</div>
      </div>
      <div class="grid gap-4 md:grid-cols-2 xl:grid-cols-3">
        <div
          v-for="card in resultCards"
          :key="card.key"
          class="rounded-2xl border p-4"
          :class="card.toneClass"
        >
          <div class="flex items-center justify-between gap-3">
            <div class="text-sm font-medium text-white">{{ card.title }}</div>
            <a-tag :color="card.tagColor">{{ card.statusText }}</a-tag>
          </div>
          <div v-if="card.key === 'quick-stage'" class="mt-4 space-y-3">
            <div
              v-for="step in quickEventSteps"
              :key="step.key"
              class="rounded-xl border p-3"
              :class="step.toneClass"
            >
              <div class="flex items-center justify-between gap-3">
                <div class="text-xs font-medium text-white">{{ step.title }}</div>
                <a-tag size="small" :color="step.tagColor">{{ step.statusText }}</a-tag>
              </div>
              <div class="mt-2 text-[11px] leading-5 text-slate-300 whitespace-pre-wrap">{{ step.message }}</div>
            </div>
          </div>
          <div v-else-if="card.key === 'agent'" class="mt-4 space-y-3">
            <div
              v-for="step in agentSteps"
              :key="step.key"
              class="rounded-xl border p-3"
              :class="step.toneClass"
            >
              <div class="flex items-center justify-between gap-3">
                <div class="text-xs font-medium text-white">{{ step.title }}</div>
                <a-tag size="small" :color="step.tagColor">{{ step.statusText }}</a-tag>
              </div>
              <div class="mt-2 text-[11px] leading-5 text-slate-300 whitespace-pre-wrap">{{ step.message }}</div>
            </div>
          </div>
          <div v-else-if="card.key === 'lane'" class="mt-4 space-y-3">
            <div
              v-for="step in laneActionSteps"
              :key="step.key"
              class="rounded-xl border p-3"
              :class="step.toneClass"
            >
              <div class="flex items-center justify-between gap-3">
                <div class="text-xs font-medium text-white">{{ step.title }}</div>
                <a-tag size="small" :color="step.tagColor">{{ step.statusText }}</a-tag>
              </div>
              <div class="mt-2 text-[11px] leading-5 text-slate-300 whitespace-pre-wrap">{{ step.message }}</div>
            </div>
          </div>
          <div v-else class="mt-3 text-xs leading-6 text-slate-300 whitespace-pre-wrap">{{ card.message }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted, reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { fetchSystemSettings, submitBillInfo, submitBulkTransport, submitConfirmEnInfo, submitLaneLost, submitTrafficEvent } from '@/api/mock'
import { notifyByAgent, openVncByAgent, playVideoByAgent, speakByAgent } from '@/api/agentApi'
import BillInfoConfirmPanel from '@/components/BillInfoConfirmPanel.vue'
import ConfirmEnInfoPanel from '@/components/ConfirmEnInfoPanel.vue'
import BulkTransportConfirmPanel from '@/components/BulkTransportConfirmPanel.vue'
import LaneActivityTimeline from '@/components/LaneActivityTimeline.vue'
import { useAppStore, type EventItem, type LaneStatusItem, type PlazaStatusItem } from '@/stores/app'

const store = useAppStore()
const router = useRouter()
const plazaId = '3301001'
const selectedPlazaId = ref(plazaId)
const selectedLaneNotice = ref('')
const isLoading = ref(false)
const pageError = ref('')

const plazaPreferences = reactive({
  preferredVoiceName: '',
  preferredRing: '默认铃声',
  enableLaneSpecialBroadcast: true,
  enableTrafficEventAudio: true
})

const statusText = computed(() => {
  const warnings = store.plazas.filter((item) => item.status === 'warning').length
  if (warnings > 0) {
    return `当前有 ${warnings} 个站点需要关注`
  }

  return '系统运行正常'
})

const activityFeed = computed(() => {
  const laneMessages = store.plazas
    .flatMap((plaza) => (plaza.laneDetails ?? []).flatMap((lane) =>
      (lane.messages ?? []).slice(0, 1).map((message) => ({
        id: `${lane.id}-${message.time}-${message.type}`,
        time: message.time,
        title: `${plaza.name} ${lane.laneNo} 车道 ${message.type}`,
        detail: message.content
      }))
    ))

  const eventMessages = store.events.slice(0, 5).map((event) => ({
    id: event.id,
    time: event.time,
    title: `${event.plazaName} ${event.laneNo} 车道 ${event.title}`,
    detail: event.summary || event.status
  }))

  const result = [...laneMessages, ...eventMessages]
    .sort((a, b) => b.time.localeCompare(a.time))
    .slice(0, 5)

  if (result.length > 0) {
    return result
  }

  return [
    {
      id: 'default-1',
      time: '刚刚',
      title: '等待实时事件接入',
      detail: '后续将继续替代 frmPlaza 中的消息流与状态播报视图。'
    }
  ]
})

const recentEvents = computed(() => store.events.slice(0, 3))
const linkedActivityItems = computed(() => {
  const eventMap = new Map(store.events.map((event) => [event.id, event]))

  return activityFeed.value.slice(0, 3).map((item) => {
    const matchedEvent = Array.from(eventMap.values()).find((event) => item.title.includes(event.plazaName) || item.title.includes(event.laneNo))

    return {
      ...item,
      relatedEventId: matchedEvent?.id,
      keyword: matchedEvent?.plazaName || selectedPlaza.value?.name || ''
    }
  })
})
const selectedPlaza = computed(() => store.plazas.find((item) => item.id === selectedPlazaId.value) ?? store.plazas[0])
const selectedLaneDetails = computed<LaneStatusItem[]>(() => {
  const current = selectedPlaza.value
  return current?.laneDetails ?? []
})

const resultCards = computed(() => {
  const cards = [
    {
      key: 'quick-event',
      title: '测试事件提交',
      message: quickEventResult.value || '尚未触发测试事件。',
      statusText: quickEventResult.value ? (quickEventResult.value.includes('成功') ? '成功' : '失败') : '待执行',
      tagColor: quickEventResult.value ? (quickEventResult.value.includes('成功') ? 'green' : 'red') : 'gray',
      toneClass: quickEventResult.value
        ? (quickEventResult.value.includes('成功') ? 'border-emerald-500/20 bg-emerald-500/5' : 'border-red-500/20 bg-red-500/5')
        : 'border-slate-700/50 bg-slate-950/30'
    },
    {
      key: 'quick-stage',
      title: '阶段状态',
      message: quickEventStageResult.value || '将按“提交 -> Agent -> 刷新”显示链路阶段。',
      statusText: quickEventStageResult.value
        ? (quickEventStageResult.value.includes('异常') || quickEventStageResult.value.includes('未通过') ? '异常' : quickEventStageResult.value.includes('阶段完成') ? '完成' : '进行中')
        : '待执行',
      tagColor: quickEventStageResult.value
        ? (quickEventStageResult.value.includes('异常') || quickEventStageResult.value.includes('未通过') ? 'red' : quickEventStageResult.value.includes('阶段完成') ? 'green' : 'arcoblue')
        : 'gray',
      toneClass: quickEventStageResult.value
        ? (quickEventStageResult.value.includes('异常') || quickEventStageResult.value.includes('未通过') ? 'border-red-500/20 bg-red-500/5' : quickEventStageResult.value.includes('阶段完成') ? 'border-emerald-500/20 bg-emerald-500/5' : 'border-sky-500/20 bg-sky-500/5')
        : 'border-slate-700/50 bg-slate-950/30'
    },
    {
      key: 'agent',
      title: 'Agent 联动',
      message: agentFeedback.value || '尚未触发 Agent 联动。',
      statusText: agentFeedback.value ? (agentFeedback.value.includes('成功') ? '成功' : '失败') : '待执行',
      tagColor: agentFeedback.value ? (agentFeedback.value.includes('成功') ? 'green' : 'red') : 'gray',
      toneClass: agentFeedback.value
        ? (agentFeedback.value.includes('成功') ? 'border-emerald-500/20 bg-emerald-500/5' : 'border-red-500/20 bg-red-500/5')
        : 'border-slate-700/50 bg-slate-950/30'
    },
    {
      key: 'lane',
      title: '车道操作反馈',
      message: selectedLaneNotice.value || '尚未触发车道相关操作。',
      statusText: selectedLaneNotice.value ? '已反馈' : '待执行',
      tagColor: selectedLaneNotice.value ? 'arcoblue' : 'gray',
      toneClass: selectedLaneNotice.value ? 'border-sky-500/20 bg-sky-500/5' : 'border-slate-700/50 bg-slate-950/30'
    },
    {
      key: 'bulk',
      title: '确认类表单反馈',
      message: bulkTransportResult.value || billInfoResult.value || confirmEnInfoResult.value || '尚未触发确认类表单提交。',
      statusText: bulkTransportResult.value || billInfoResult.value || confirmEnInfoResult.value ? '已反馈' : '待执行',
      tagColor: bulkTransportResult.value || billInfoResult.value || confirmEnInfoResult.value ? 'purple' : 'gray',
      toneClass: bulkTransportResult.value || billInfoResult.value || confirmEnInfoResult.value ? 'border-purple-500/20 bg-purple-500/5' : 'border-slate-700/50 bg-slate-950/30'
    }
  ]

  return cards
})

const quickEventSteps = computed(() => {
  const stage = quickEventStageResult.value
  const submitSuccess = quickEventResult.value.includes('成功')
  const submitFailed = quickEventResult.value.includes('失败')
  const agentSuccess = agentFeedback.value.includes('成功')
  const agentFailed = agentFeedback.value.includes('失败')
  const refreshSuccess = stage.includes('阶段完成')
  const refreshFailed = stage.includes('页面刷新失败')

  return [
    buildQuickStep(
      'submit',
      '步骤 1：提交测试事件',
      submitSuccess ? '提交成功，已收到接口返回。' : submitFailed ? quickEventResult.value : '尚未开始提交。',
      submitSuccess ? 'success' : submitFailed ? 'error' : stage.includes('阶段 1/3') ? 'running' : 'idle'
    ),
    buildQuickStep(
      'agent',
      '步骤 2：联动 Agent',
      agentSuccess ? agentFeedback.value : agentFailed ? agentFeedback.value : submitSuccess ? '等待或正在执行 Agent 联动。' : '需先完成事件提交。',
      agentSuccess ? 'success' : agentFailed ? 'error' : stage.includes('阶段 2/3') ? 'running' : submitSuccess ? 'pending' : 'idle'
    ),
    buildQuickStep(
      'refresh',
      '步骤 3：刷新页面数据',
      refreshSuccess ? '页面事件与监控数据已刷新完成。' : refreshFailed ? stage : agentSuccess || submitSuccess ? '等待或正在刷新页面数据。' : '需先完成前置步骤。',
      refreshSuccess ? 'success' : refreshFailed ? 'error' : stage.includes('阶段 3/3') ? 'running' : agentSuccess || submitSuccess ? 'pending' : 'idle'
    )
  ]
})

const agentSteps = computed(() => {
  const hasAgentMessage = agentFeedback.value.length > 0
  const success = agentFeedback.value.includes('成功')
  const failed = agentFeedback.value.includes('失败')

  return [
    buildQuickStep(
      'agent-dispatch',
      'Agent 指令下发',
      hasAgentMessage ? agentFeedback.value : '尚未触发 Agent 指令。',
      success ? 'success' : failed ? 'error' : 'idle'
    ),
    buildQuickStep(
      'agent-scene',
      '现场反馈观察',
      hasAgentMessage
        ? '请结合现场机器确认通知气泡、语音播报或视频播放是否已真正执行。'
        : '等待触发本地通知、播报或视频播放。',
      hasAgentMessage ? 'pending' : 'idle'
    )
  ]
})

const laneActionSteps = computed(() => {
  const hasLaneMessage = selectedLaneNotice.value.length > 0
  const laneFailed = selectedLaneNotice.value.includes('失败') || selectedLaneNotice.value.includes('无法')

  return [
    buildQuickStep(
      'lane-selection',
      '车道操作反馈',
      hasLaneMessage ? selectedLaneNotice.value : '尚未触发车道点击、掉线上报或其它车道操作。',
      hasLaneMessage ? (laneFailed ? 'error' : 'success') : 'idle'
    ),
    buildQuickStep(
      'lane-followup',
      '后续观察',
      hasLaneMessage
        ? '如执行了掉线上报，请继续观察车道卡片、消息流和快照视图是否同步更新。'
        : '等待从监控页触发车道相关操作。',
      hasLaneMessage ? 'pending' : 'idle'
    )
  ]
})

function buildQuickStep(key: string, title: string, message: string, state: 'idle' | 'pending' | 'running' | 'success' | 'error') {
  const config = {
    idle: { statusText: '待执行', tagColor: 'gray', toneClass: 'border-slate-700/50 bg-slate-950/30' },
    pending: { statusText: '待继续', tagColor: 'gray', toneClass: 'border-slate-700/50 bg-slate-950/30' },
    running: { statusText: '进行中', tagColor: 'arcoblue', toneClass: 'border-sky-500/20 bg-sky-500/5' },
    success: { statusText: '成功', tagColor: 'green', toneClass: 'border-emerald-500/20 bg-emerald-500/5' },
    error: { statusText: '失败', tagColor: 'red', toneClass: 'border-red-500/20 bg-red-500/5' }
  }[state]

  return {
    key,
    title,
    message,
    ...config
  }
}

const bulkTransportDraft = reactive({
  vehId: '浙A12345',
  vehColor: 2,
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

const billInfoDraft = reactive({
  operatorSummary: '杭州北收费站 · 001车道 · 张三(10001)',
  billCode: '3300198765',
  billNumber: '00012345',
  head: {
    netNo: '33',
    plazaNo: '01001',
    laneId: '001'
  },
  subHead: {
    staffId: '10001',
    staffName: '张三'
  }
})

const confirmEnInfoDraft = reactive({
  laneId: '3301001001',
  plazaId,
  laneNo: '001',
  genTime: '2026-03-22 10:15:00',
  vehicleId: '浙A12345',
  vehicleType: 2,
  resCount: 1,
  retQuery: 1,
  code: 0,
  msg: 'OK',
  enStations: [
    {
      enStationId: '3301',
      enTime: '2026-03-22T08:10:00',
      enTollLaneId: '001',
      mediaNo: 'ETC-0001'
    }
  ]
})

const bulkTransportResult = ref('')
const billInfoResult = ref('')
const confirmEnInfoResult = ref('')
const agentFeedback = ref('')
const quickEventResult = ref('')
const quickEventStageResult = ref('')

const quickEventForm = reactive({
  recordId: 'plaza-quick-evt-001',
  laneNo: '001',
  eventType: '45'
})

onMounted(async () => {
  await initializePage()
})

async function initializePage() {
  isLoading.value = true
  pageError.value = ''

  try {
    if (!store.plazas.length || !store.events.length) {
      await store.loadOverview()
    } else {
      await store.loadLaneStatusSnapshots()
    }

    if (!selectedPlazaId.value && store.plazas.length > 0) {
      selectedPlazaId.value = store.plazas[0].id
    }

    await loadPlazaPreferences()
  } catch (error) {
    pageError.value = `收费站监控数据加载失败：${error instanceof Error ? error.message : String(error)}`
  } finally {
    isLoading.value = false
  }
}

async function confirmBulkTransport() {
  const result = await submitBulkTransport(plazaId, {
    vehId: bulkTransportDraft.vehId,
    vehColor: bulkTransportDraft.vehColor,
    alex: bulkTransportDraft.alex,
    weight: bulkTransportDraft.weight,
    isValid: bulkTransportDraft.isValid,
    title: bulkTransportDraft.title,
    largeWoods: bulkTransportDraft.largeWoods
  })

  bulkTransportResult.value = `${result.ok ? '提交成功' : '提交失败'}：${JSON.stringify(result.data)}`
}

function resetBulkTransport() {
  bulkTransportResult.value = '已取消本次大件运输确认。'
}

async function confirmBillInfo() {
  const result = await submitBillInfo(plazaId, {
    head: billInfoDraft.head,
    subHead: billInfoDraft.subHead,
    billCode: billInfoDraft.billCode,
    billNumber: billInfoDraft.billNumber
  })

  billInfoResult.value = `${result.ok ? '提交成功' : '提交失败'}：${JSON.stringify(result.data)}`
}

function resetBillInfo() {
  billInfoResult.value = '已取消本次发票信息确认。'
}

async function confirmEnInfo() {
  const result = await submitConfirmEnInfo(plazaId, {
    laneId: confirmEnInfoDraft.laneId,
    plazaId: confirmEnInfoDraft.plazaId,
    laneNo: confirmEnInfoDraft.laneNo,
    genTime: confirmEnInfoDraft.genTime,
    vehicleId: confirmEnInfoDraft.vehicleId,
    vehicleType: confirmEnInfoDraft.vehicleType,
    resCount: confirmEnInfoDraft.resCount,
    retQuery: confirmEnInfoDraft.retQuery,
    code: confirmEnInfoDraft.code,
    msg: confirmEnInfoDraft.msg,
    enStations: confirmEnInfoDraft.enStations
  })

  confirmEnInfoResult.value = `${result.ok ? '提交成功' : '提交失败'}：${JSON.stringify(result.data)}`
}

function resetConfirmEnInfo() {
  confirmEnInfoResult.value = '已取消本次入口信息确认。'
}

async function loadPlazaPreferences() {
  try {
    const data = await fetchSystemSettings()
    plazaPreferences.preferredVoiceName = typeof data.preferredVoiceName === 'string' ? data.preferredVoiceName : ''
    plazaPreferences.preferredRing = typeof data.preferredRing === 'string' ? data.preferredRing : '默认铃声'
    plazaPreferences.enableLaneSpecialBroadcast = typeof data.enableLaneSpecialBroadcast === 'boolean' ? data.enableLaneSpecialBroadcast : true
    plazaPreferences.enableTrafficEventAudio = typeof data.enableTrafficEventAudio === 'boolean' ? data.enableTrafficEventAudio : true
  } catch {
    // 保持默认值
  }
}

async function openPlazaVnc(record: PlazaStatusItem) {
  try {
    const result = await openVncByAgent('127.0.0.1', 5900, 'kissme', `${record.name} 远程值守`)

    billInfoResult.value = `VNC 打开成功：${result.message}；当前铃声策略：${plazaPreferences.preferredRing}`
  } catch (error) {
    billInfoResult.value = `VNC 打开失败：${error instanceof Error ? error.message : String(error)}`
  }
}

async function refreshMonitor() {
  await initializePage()
}

function focusPlazaEvents(record: PlazaStatusItem) {
  const match = store.events.find((event) => event.plazaName.includes(record.name))
  if (match) {
    router.push(`/events/${match.id}`)
    return
  }

  confirmEnInfoResult.value = `${record.name} 当前暂无可跳转的事件记录。`
}

function openPlazaEventCenter(record: PlazaStatusItem) {
  router.push({
    path: '/events',
    query: {
      keyword: record.name
    }
  })
}

function openEventDetail(eventId: string) {
  router.push(`/events/${eventId}`)
}

function openActivityEvent(eventId?: string) {
  if (!eventId) {
    confirmEnInfoResult.value = '当前活动项还未关联到具体事件，可先进入事件中心查看。'
    return
  }

  openEventDetail(eventId)
}

function openEventCenterByKeyword(keyword: string) {
  router.push({
    path: '/events',
    query: {
      keyword
    }
  })
}

async function playEventVideo(event: EventItem) {
  const videoSource = event.videoUrls?.[0] || event.videoUrl
  if (!videoSource) {
    confirmEnInfoResult.value = `事件 ${event.title} 暂无可播放视频。`
    return
  }

  try {
    const title = plazaPreferences.enableTrafficEventAudio
      ? `${event.title} 本地播放 · ${plazaPreferences.preferredRing}`
      : `${event.title} 本地播放`
    const result = await playVideoByAgent(videoSource, title, 1280, 720, `plaza-${event.id}`)
    confirmEnInfoResult.value = `视频播放成功：${result.message}${plazaPreferences.preferredVoiceName ? `；播报员：${plazaPreferences.preferredVoiceName}` : ''}`
  } catch (error) {
    confirmEnInfoResult.value = `视频播放失败：${error instanceof Error ? error.message : String(error)}`
  }
}

async function submitQuickTrafficEvent() {
  try {
    quickEventStageResult.value = '阶段 1/3：正在提交测试事件...'
    const result = await submitTrafficEvent({
      recordId: quickEventForm.recordId,
      eventType: quickEventForm.eventType,
      LaneNo: quickEventForm.laneNo
    })

    quickEventResult.value = `${result.ok ? '测试事件提交成功' : '测试事件提交失败'}：${JSON.stringify(result.data)}`
    confirmEnInfoResult.value = quickEventResult.value

    if (result.ok) {
      quickEventStageResult.value = '阶段 2/3：测试事件已提交，正在联动 Agent...'

      try {
        const notifyResult = await notifyByAgent(
          '测试交通事件已触发',
          `记录号 ${quickEventForm.recordId} 已提交，车道 ${quickEventForm.laneNo}`,
          {
            playSpeech: plazaPreferences.enableTrafficEventAudio,
            text: `${selectedPlaza.value?.name || '当前收费站'} ${quickEventForm.laneNo} 车道测试事件已触发`,
            voiceName: plazaPreferences.preferredVoiceName || undefined
          }
        )
        agentFeedback.value = `测试事件联动 Agent 成功：${notifyResult.message}`
      } catch (agentError) {
        agentFeedback.value = `测试事件提交成功，但 Agent 联动失败：${agentError instanceof Error ? agentError.message : String(agentError)}`
      }

      quickEventStageResult.value = '阶段 3/3：正在刷新页面事件与监控数据...'

      try {
        await store.loadOverview()
        quickEventStageResult.value = '阶段完成：测试事件提交、Agent 联动、页面刷新均已完成。'
      } catch (refreshError) {
        quickEventStageResult.value = `阶段异常：测试事件已提交，但页面刷新失败：${refreshError instanceof Error ? refreshError.message : String(refreshError)}`
      }
    } else {
      quickEventStageResult.value = '阶段结束：测试事件提交未通过，未继续执行 Agent 联动与页面刷新。'
    }
  } catch (error) {
    quickEventResult.value = `测试事件提交失败：${error instanceof Error ? error.message : String(error)}`
    confirmEnInfoResult.value = quickEventResult.value
    quickEventStageResult.value = `阶段异常：测试事件提交阶段失败：${error instanceof Error ? error.message : String(error)}`
  }
}

function focusLane(lane: LaneStatusItem) {
  const lostText = lane.isLost ? '，当前处于掉线状态' : ''
  selectedLaneNotice.value = `${selectedPlaza.value?.name || '当前收费站'} ${lane.laneNo} 车道：${lane.lastMessage}，最近心跳 ${lane.lastHeartbeat}${lostText}`
}

async function markLaneLost(lane: LaneStatusItem) {
  const currentPlazaId = selectedPlaza.value?.id
  if (!currentPlazaId) {
    selectedLaneNotice.value = '未找到当前收费站，无法标记掉线。'
    return
  }

  const result = await submitLaneLost(currentPlazaId, lane.laneNo)
  selectedLaneNotice.value = `${result.ok ? '掉线上报成功' : '掉线上报失败'}：${JSON.stringify(result.data)}`
  await store.loadLaneStatusSnapshots()
}

async function notifyLaneAlert(lane: LaneStatusItem) {
  const plazaName = selectedPlaza.value?.name || '当前收费站'
  const message = `${plazaName} ${lane.laneNo} 车道状态：${lane.lastMessage || '请关注现场状态'}`

  try {
    const result = await notifyByAgent('收费站监控提醒', message, {
      playSpeech: false
    })
    agentFeedback.value = `本地通知发送成功：${result.message}`
  } catch (error) {
    agentFeedback.value = `本地通知发送失败：${error instanceof Error ? error.message : String(error)}`
  }
}

async function broadcastLaneStatus(lane: LaneStatusItem) {
  const plazaName = selectedPlaza.value?.name || '当前收费站'
  const text = `${plazaName}${lane.laneNo}车道，${lane.isLost ? '当前掉线，请及时处理。' : lane.lastMessage || '请关注现场状态。'}`

  try {
    const result = await speakByAgent(text, {
      voiceName: plazaPreferences.preferredVoiceName || undefined
    })
    agentFeedback.value = `语音播报成功：${result.message}`
  } catch (error) {
    agentFeedback.value = `语音播报失败：${error instanceof Error ? error.message : String(error)}`
  }
}
</script>
