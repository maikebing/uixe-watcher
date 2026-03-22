<template>
  <div class="grid gap-6 xl:grid-cols-[1.2fr_1fr]">
    <div class="glass-panel rounded-3xl p-6">
      <div class="mb-5 flex items-center justify-between gap-3">
        <div class="text-lg font-medium text-white">收费站监控</div>
        <div class="rounded-full border border-emerald-500/20 bg-emerald-500/10 px-3 py-1 text-xs text-emerald-300">
          {{ statusText }}
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
          <div class="mt-3 text-xs text-slate-400">{{ lane.lastMessage }}</div>
        </div>
      </div>
    </div>

    <div class="glass-panel rounded-3xl p-6">
      <div class="mb-5 flex items-center justify-between gap-3">
        <div class="text-lg font-medium text-white">消息流</div>
        <div class="text-xs text-slate-400">对应旧 `messageView` 与状态播报入口</div>
      </div>
      <a-timeline>
        <a-timeline-item v-for="item in activityFeed" :key="item.id" :label="item.time">
          <div class="text-sm text-white">{{ item.title }}</div>
          <div class="mt-1 text-xs text-slate-400">{{ item.detail }}</div>
        </a-timeline-item>
      </a-timeline>
    </div>

    <BulkTransportConfirmPanel :model-value="bulkTransportDraft" @confirm="confirmBulkTransport" @cancel="resetBulkTransport" />

    <BillInfoConfirmPanel :model-value="billInfoDraft" @confirm="confirmBillInfo" @cancel="resetBillInfo" />

    <ConfirmEnInfoPanel :model-value="confirmEnInfoDraft" @confirm="confirmEnInfo" @cancel="resetConfirmEnInfo" />

    <div class="glass-panel rounded-3xl p-6 xl:col-span-2">
      <div class="mb-5 flex items-center justify-between gap-3">
        <div class="text-lg font-medium text-white">交通事件快捷入口</div>
        <div class="text-xs text-slate-400">继续承接旧 `frmTrafficEvent` / `frmPlaza` 联动职责</div>
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

    <div v-if="bulkTransportResult" class="glass-panel rounded-3xl p-4 xl:col-span-2 text-sm text-slate-300">
      {{ bulkTransportResult }}
    </div>

    <div v-if="billInfoResult" class="glass-panel rounded-3xl p-4 xl:col-span-2 text-sm text-slate-300">
      {{ billInfoResult }}
    </div>

    <div v-if="confirmEnInfoResult" class="glass-panel rounded-3xl p-4 xl:col-span-2 text-sm text-slate-300">
      {{ confirmEnInfoResult }}
    </div>

    <div v-if="selectedLaneNotice" class="glass-panel rounded-3xl p-4 xl:col-span-2 text-sm text-slate-300">
      {{ selectedLaneNotice }}
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { fetchSystemSettings } from '@/api/mock'
import { submitBillInfo, submitBulkTransport, submitConfirmEnInfo } from '@/api/mock'
import { openVncByAgent, playVideoByAgent } from '@/api/agentApi'
import BillInfoConfirmPanel from '@/components/BillInfoConfirmPanel.vue'
import ConfirmEnInfoPanel from '@/components/ConfirmEnInfoPanel.vue'
import BulkTransportConfirmPanel from '@/components/BulkTransportConfirmPanel.vue'
import { useAppStore, type EventItem, type LaneStatusItem, type PlazaStatusItem } from '@/stores/app'

const store = useAppStore()
const router = useRouter()
const plazaId = '3301001'
const selectedPlazaId = ref(plazaId)
const selectedLaneNotice = ref('')

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
  const result = store.events.slice(0, 5).map((event) => ({
    id: event.id,
    time: event.time,
    title: `${event.plazaName} ${event.laneNo} 车道 ${event.title}`,
    detail: event.summary || event.status
  }))

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
const selectedPlaza = computed(() => store.plazas.find((item) => item.id === selectedPlazaId.value) ?? store.plazas[0])
const selectedLaneDetails = computed<LaneStatusItem[]>(() => {
  const current = selectedPlaza.value
  return current?.laneDetails ?? []
})

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

void loadPlazaPreferences()

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

function focusPlazaEvents(record: PlazaStatusItem) {
  const match = store.events.find((event) => event.plazaName.includes(record.name))
  if (match) {
    router.push(`/events/${match.id}`)
    return
  }

  confirmEnInfoResult.value = `${record.name} 当前暂无可跳转的事件记录。`
}

function openEventDetail(eventId: string) {
  router.push(`/events/${eventId}`)
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

function focusLane(lane: LaneStatusItem) {
  selectedLaneNotice.value = `${selectedPlaza.value?.name || '当前收费站'} ${lane.laneNo} 车道：${lane.lastMessage}，最近心跳 ${lane.lastHeartbeat}`
}
</script>
