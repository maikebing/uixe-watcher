<template>
  <div class="glass-panel rounded-3xl p-6">
    <div class="mb-5 flex flex-col gap-4 xl:flex-row xl:items-center xl:justify-between">
      <div>
        <div class="text-lg font-medium text-white">事件中心</div>
        <div class="mt-1 text-xs text-slate-400">作为正式事件工作台，承接收费站、车道、状态与现场联动操作</div>
      </div>
      <div class="flex items-center gap-3">
        <a-button size="small" @click="reloadEvents">刷新</a-button>
        <a-button size="small" type="outline" @click="clearFilters">清空筛选</a-button>
      </div>
    </div>

    <div class="mb-4 grid gap-3 md:grid-cols-4">
      <div class="rounded-2xl border border-slate-700/50 bg-slate-950/40 px-4 py-3">
        <div class="text-[11px] text-slate-400">页面状态</div>
        <div class="mt-2 text-sm font-medium text-white">{{ eventWorkbenchStatus }}</div>
      </div>
      <div class="rounded-2xl border border-slate-700/50 bg-slate-950/40 px-4 py-3">
        <div class="text-[11px] text-slate-400">最近操作反馈</div>
        <div class="mt-2 text-sm font-medium text-white">{{ operationSummary }}</div>
      </div>
      <div class="rounded-2xl border border-slate-700/50 bg-slate-950/40 px-4 py-3">
        <div class="text-[11px] text-slate-400">当前筛选数</div>
        <div class="mt-2 text-sm font-medium text-white">{{ filteredEvents.length }}</div>
      </div>
      <div class="rounded-2xl border border-slate-700/50 bg-slate-950/40 px-4 py-3">
        <div class="text-[11px] text-slate-400">工作台关注点</div>
        <div class="mt-2 text-sm font-medium text-white">{{ workbenchFocusText }}</div>
      </div>
    </div>

    <div class="mb-4 grid gap-3 md:grid-cols-2 xl:grid-cols-4">
      <a-input-search v-model="searchKeyword" placeholder="搜索事件/车道/收费站" allow-clear />
      <a-select v-model="selectedLevel" allow-clear placeholder="按事件级别筛选">
        <a-option value="high">high</a-option>
        <a-option value="medium">medium</a-option>
        <a-option value="low">low</a-option>
      </a-select>
      <a-select v-model="selectedStatus" allow-clear placeholder="按事件状态筛选">
        <a-option v-for="status in statusOptions" :key="status" :value="status">{{ status }}</a-option>
      </a-select>
      <a-select v-model="selectedPlaza" allow-clear placeholder="按收费站筛选">
        <a-option v-for="plaza in plazaOptions" :key="plaza" :value="plaza">{{ plaza }}</a-option>
      </a-select>
    </div>

    <div class="mb-4 grid gap-3 md:grid-cols-2">
      <a-input v-model="selectedLane" placeholder="按车道号筛选，例如 001 / X02" allow-clear />
      <div class="rounded-2xl border border-slate-700/50 bg-slate-950/40 px-4 py-3 text-xs text-slate-300">
        当前路由联动参数：plaza={{ selectedPlaza || '全部' }}；lane={{ selectedLane || '全部' }}；status={{ selectedStatus || '全部' }}；keyword={{ searchKeyword || '无' }}
      </div>
    </div>

    <div class="mb-4 grid gap-3 md:grid-cols-4">
      <div class="rounded-2xl border border-slate-700/50 bg-slate-950/40 px-4 py-3">
        <div class="text-[11px] text-slate-400">当前事件数</div>
        <div class="mt-2 text-sm font-medium text-white">{{ filteredEvents.length }}</div>
      </div>
      <div class="rounded-2xl border border-slate-700/50 bg-slate-950/40 px-4 py-3">
        <div class="text-[11px] text-slate-400">高优先级事件</div>
        <div class="mt-2 text-sm font-medium text-white">{{ highLevelCount }}</div>
      </div>
      <div class="rounded-2xl border border-slate-700/50 bg-slate-950/40 px-4 py-3">
        <div class="text-[11px] text-slate-400">当前收费站筛选</div>
        <div class="mt-2 text-sm font-medium text-white">{{ selectedPlaza || '全部收费站' }}</div>
      </div>
      <div class="rounded-2xl border border-slate-700/50 bg-slate-950/40 px-4 py-3">
        <div class="text-[11px] text-slate-400">筛选摘要</div>
        <div class="mt-2 text-sm font-medium text-white">{{ filterSummary }}</div>
      </div>
    </div>

    <div class="space-y-3">
      <div v-for="event in filteredEvents" :key="event.id" class="rounded-2xl border border-sky-500/10 bg-slate-900/40 p-4 transition hover:border-cyan-400/30">
        <div class="flex items-center justify-between gap-4">
          <router-link :to="`/events/${event.id}`" class="block min-w-0 flex-1 no-underline">
            <div class="text-base font-medium text-white">{{ event.title }}</div>
            <div class="mt-1 text-sm text-slate-400">{{ event.plazaName }} · {{ event.laneNo }} 车道 · {{ event.time }}</div>
          </router-link>
          <div class="flex items-center gap-3">
            <a-tag :color="event.level === 'high' ? 'red' : event.level === 'medium' ? 'orange' : 'green'">{{ event.level }}</a-tag>
            <span class="text-sm text-slate-300">{{ event.status }}</span>
            <a-button size="mini" @click="notifyEvent(event)">本地通知</a-button>
            <a-button size="mini" @click="speakEvent(event)">语音播报</a-button>
            <a-button v-if="getPreferredVideo(event)" size="mini" type="outline" @click="playEventVideo(event)">播放视频</a-button>
          </div>
        </div>
      </div>
    </div>
    <div v-if="!filteredEvents.length" class="mt-4 rounded-2xl border border-slate-700/40 bg-slate-950/40 p-4 text-sm text-slate-400">
      当前没有匹配的事件记录。
    </div>
    <div v-if="agentPlayResult" class="mt-4 rounded-2xl border border-sky-500/10 bg-slate-900/40 p-3 text-sm text-slate-300 whitespace-pre-wrap">
      {{ agentPlayResult }}
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted, ref, watch } from 'vue'
import { useRoute } from 'vue-router'
import { notifyByAgent, playVideoByAgent, speakByAgent } from '@/api/agentApi'
import { useAppStore, type EventItem } from '@/stores/app'

const store = useAppStore()
const route = useRoute()
const agentPlayResult = ref('')
const searchKeyword = ref('')
const selectedLevel = ref('')
const selectedStatus = ref('')
const selectedPlaza = ref('')
const selectedLane = ref('')

const plazaOptions = computed(() => Array.from(new Set(store.events.map((event) => event.plazaName))).filter(Boolean))
const statusOptions = computed(() => Array.from(new Set(store.events.map((event) => event.status))).filter(Boolean))

const filteredEvents = computed(() => {
  const keyword = searchKeyword.value.trim().toLowerCase()
  return store.events.filter((event) => {
    const matchesKeyword = !keyword || [event.title, event.plazaName, event.laneNo, event.status, event.summary]
      .filter((item): item is string => typeof item === 'string' && item.length > 0)
      .some((item) => item.toLowerCase().includes(keyword))

    const matchesLevel = !selectedLevel.value || event.level === selectedLevel.value
    const matchesStatus = !selectedStatus.value || event.status === selectedStatus.value
    const matchesPlaza = !selectedPlaza.value || event.plazaName === selectedPlaza.value
    const matchesLane = !selectedLane.value || event.laneNo === selectedLane.value || event.laneNo.includes(selectedLane.value)

    return matchesKeyword && matchesLevel && matchesStatus && matchesPlaza && matchesLane
  })
})

const highLevelCount = computed(() => filteredEvents.value.filter((event) => event.level === 'high').length)

const eventWorkbenchStatus = computed(() => filteredEvents.value.length > 0 ? '工作台已加载' : '等待事件数据')
const operationSummary = computed(() => agentPlayResult.value || '尚未触发本地操作')
const workbenchFocusText = computed(() => {
  if (highLevelCount.value > 0) {
    return `有 ${highLevelCount.value} 条高优先级事件`
  }

  if (filterSummary.value !== '当前未设置筛选') {
    return '当前工作台已设置筛选'
  }

  return '当前无额外关注项'
})

const filterSummary = computed(() => {
  const parts = []
  if (searchKeyword.value.trim()) parts.push(`关键字：${searchKeyword.value.trim()}`)
  if (selectedLevel.value) parts.push(`级别：${selectedLevel.value}`)
  if (selectedStatus.value) parts.push(`状态：${selectedStatus.value}`)
  if (selectedPlaza.value) parts.push(`收费站：${selectedPlaza.value}`)
  if (selectedLane.value) parts.push(`车道：${selectedLane.value}`)
  return parts.length ? parts.join(' / ') : '当前未设置筛选'
})

onMounted(async () => {
  syncKeywordFromRoute()

  if (!store.events.length) {
    await store.loadOverview()
  }
})

watch(
  () => route.query,
  () => {
    syncKeywordFromRoute()
  }
)

function syncKeywordFromRoute() {
  const keyword = route.query.keyword
  const plaza = route.query.plaza
  const lane = route.query.lane
  const status = route.query.status
  searchKeyword.value = typeof keyword === 'string' ? keyword : ''
  selectedPlaza.value = typeof plaza === 'string' ? plaza : ''
  selectedLane.value = typeof lane === 'string' ? lane : ''
  selectedStatus.value = typeof status === 'string' ? status : ''
}

function clearFilters() {
  searchKeyword.value = ''
  selectedLevel.value = ''
  selectedStatus.value = ''
  selectedPlaza.value = ''
  selectedLane.value = ''
}

async function reloadEvents() {
  await store.loadOverview()
}

function getPreferredVideo(event: EventItem) {
  return event.videoUrls?.[0] ?? event.videoUrl
}

async function playEventVideo(event: EventItem) {
  const videoUrl = getPreferredVideo(event)
  if (!videoUrl) {
    agentPlayResult.value = '当前事件没有可播放的视频'
    return
  }

  try {
    const result = await playVideoByAgent(videoUrl, `${event.title} 视频`, 1280, 720, `event-${event.id}`)
    agentPlayResult.value = `视频播放调用成功：${result.message}`
  } catch (error) {
    agentPlayResult.value = `视频播放调用失败：${error instanceof Error ? error.message : String(error)}`
  }
}

async function notifyEvent(event: EventItem) {
  try {
    const result = await notifyByAgent('事件中心提醒', `${event.plazaName} ${event.laneNo} 车道：${event.title}`, {
      playSpeech: false
    })
    agentPlayResult.value = `本地通知调用成功：${result.message}`
  } catch (error) {
    agentPlayResult.value = `本地通知调用失败：${error instanceof Error ? error.message : String(error)}`
  }
}

async function speakEvent(event: EventItem) {
  try {
    const result = await speakByAgent(`${event.plazaName}${event.laneNo}车道，${event.title}`)
    agentPlayResult.value = `语音播报调用成功：${result.message}`
  } catch (error) {
    agentPlayResult.value = `语音播报调用失败：${error instanceof Error ? error.message : String(error)}`
  }
}
</script>
