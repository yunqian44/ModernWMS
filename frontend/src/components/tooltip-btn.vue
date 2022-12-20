<template>
  <v-tooltip location="bottom">
    <template #activator="{ props }">
      <v-btn class="mr-3" size="small" icon v-bind="props" @click="method.clickEvent">
        <v-icon> {{ icon }} </v-icon>
      </v-btn>
    </template>
    <span>{{ tooltipText }}</span>
  </v-tooltip>
</template>
<script setup lang="ts">
import { reactive } from 'vue'

const emit = defineEmits(['click'])

defineProps({
  icon: {
    type: String,
    default: ''
  },
  tooltipText: {
    type: String,
    default: ''
  }
})

const method = reactive({
  clickEvent: () => {
    emit('click')
  }
})
</script>
<style lang="less"></style>
