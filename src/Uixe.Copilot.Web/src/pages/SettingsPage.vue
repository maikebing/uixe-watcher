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
  </div>
</template>
