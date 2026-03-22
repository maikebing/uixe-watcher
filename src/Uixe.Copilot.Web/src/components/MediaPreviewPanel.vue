<template>
  <div class="space-y-4">
    <div v-if="imageUrls.length" class="rounded-2xl border border-sky-500/10 bg-slate-900/40 p-3">
      <div class="mb-2 flex items-center justify-between text-sm text-slate-300">
        <span>Õº∆¨‘§¿¿</span>
        <span>{{ imageUrls.length }} ’≈</span>
      </div>
      <div class="grid gap-3 md:grid-cols-2">
        <div v-for="image in imageUrls" :key="image" class="overflow-hidden rounded-xl border border-slate-700/60 bg-slate-950/50">
          <img :src="image" alt=" ¬º˛Õº∆¨" class="h-56 w-full object-cover" />
          <div class="truncate px-3 py-2 text-xs text-slate-400">{{ image }}</div>
        </div>
      </div>
    </div>

    <div v-if="videoUrls.length" class="rounded-2xl border border-sky-500/10 bg-slate-900/40 p-3">
      <div class="mb-2 flex items-center justify-between text-sm text-slate-300">
        <span> ”∆µ‘§¿¿</span>
        <span>{{ videoUrls.length }} ∂Œ</span>
      </div>
      <div class="grid gap-3">
        <div v-for="video in videoUrls" :key="video" class="overflow-hidden rounded-xl border border-slate-700/60 bg-slate-950/50 p-2">
          <video :src="video" controls class="max-h-72 w-full rounded-xl" />
          <div class="mt-2 flex justify-end">
            <a-button size="mini" type="outline" @click="emit('play-video', video)">?? Agent ??</a-button>
          </div>
          <div class="truncate px-1 pt-2 text-xs text-slate-400">{{ video }}</div>
        </div>
      </div>
    </div>

    <div v-if="!imageUrls.length && !videoUrls.length" class="rounded-2xl border border-dashed border-sky-400/20 bg-slate-900/40 p-8 text-center text-slate-400">
      ‘›ŒﬁÕº∆¨ /  ”∆µ‘§¿¿
    </div>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  imageUrls: string[]
  videoUrls: string[]
}>()

const emit = defineEmits<{
  (e: 'play-video', videoUrl: string): void
}>()
</script>