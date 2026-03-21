<template>
  <div class="glass-panel rounded-3xl p-6">
    <div class="mb-5 text-lg font-medium text-white">历史查询</div>
    <div class="grid grid-cols-4 gap-4">
      <a-select placeholder="选择收费站" />
      <a-select placeholder="选择事件类型" />
      <a-range-picker />
      <a-button type="primary">查询</a-button>
    </div>
    <div class="mt-6 rounded-2xl border border-sky-500/10 bg-slate-900/40 p-8 text-center text-slate-400">
      预留历史事件检索与导出表格
    </div>
  </div>
</template>
