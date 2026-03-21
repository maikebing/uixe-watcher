<template>
  <a-layout class="min-h-screen bg-transparent text-slate-100">
    <a-layout-sider :width="240" class="border-r border-sky-500/10 bg-[#091322]/80 backdrop-blur-xl">
      <div class="px-6 py-6">
        <div class="text-xs uppercase tracking-[0.35em] text-cyan-300/70">Uixe.Copilot</div>
        <div class="mt-2 text-2xl font-semibold text-white">收费监控中心</div>
      </div>
      <a-menu :selected-keys="[selectedKey]" theme="dark" class="bg-transparent px-3" @menu-item-click="navigate">
        <a-menu-item key="/dashboard">监控总览</a-menu-item>
        <a-menu-item key="/plaza-monitor">收费站监控</a-menu-item>
        <a-menu-item key="/events">事件中心</a-menu-item>
        <a-menu-item key="/history">历史查询</a-menu-item>
        <a-menu-item key="/settings">系统配置</a-menu-item>
      </a-menu>
    </a-layout-sider>

    <a-layout>
      <a-layout-header class="flex items-center justify-between border-b border-sky-500/10 bg-transparent px-8 py-4">
        <div>
          <div class="text-sm text-slate-400">新一代收费监控与事件联动平台</div>
          <div class="text-xl font-medium text-white">{{ title }}</div>
        </div>
        <div class="flex items-center gap-3">
          <a-tag color="arcoblue">并行迁移中</a-tag>
          <a-tag color="green">WinForms + Web 共存</a-tag>
        </div>
      </a-layout-header>
      <a-layout-content class="p-6">
        <router-view />
      </a-layout-content>
    </a-layout>
  </a-layout>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'

const route = useRoute()
const router = useRouter()

const selectedKey = computed(() => route.path.startsWith('/events/') ? '/events' : route.path)
const title = computed(() => {
  switch (selectedKey.value) {
    case '/dashboard': return '监控总览'
    case '/plaza-monitor': return '收费站监控'
    case '/events': return '事件中心'
    case '/history': return '历史查询'
    case '/settings': return '系统配置'
    default: return 'Uixe.Copilot'
  }
})

function navigate(path: string) {
  router.push(path)
}
</script>
