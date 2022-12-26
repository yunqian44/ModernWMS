<template>
  <v-autocomplete
    v-model="data.model"
    v-model:search="data.search"
    :loading="data.isLoading"
    :items="data.selectItems"
    hide-no-data
    hide-selected
    hide-details
    item-title="carrier"
    item-value="carrier"
    label="Public APIs"
    return-object
    clearable
    auto-select-first
    variant="solo"
    @update:modelValue="method.changeValue"
  ></v-autocomplete>
</template>
<script setup lang="ts">
import { reactive, watch, ref } from 'vue'
import { getFreightAll } from '@/api/base/freightSetting'
import { DEBOUNCE_TIME } from '@/constant/system'

// defineProps({
// })

const emit = defineEmits(['sureSelect'])

const data = reactive({
  model: null,
  search: '',
  selectItems: [],
  isLoading: false,
  timer: ref<any>(null)
})

const method = reactive({
  handleSearch: async (value?: any) => {
    // Items have already been requested
    if (data.isLoading) return

    data.isLoading = true

    const { data: res } = await getFreightAll()
    data.isLoading = false

    if (!res.isSuccess) {
      data.selectItems = []
      return
    }
    data.selectItems = res.data
  },
  changeValue: (selectItem:any) => {
    emit('sureSelect', selectItem)
  }
})

watch(
  () => data.search,
  () => {
    // debounce
    if (data.timer) {
      clearTimeout(data.timer)
    }
    data.timer = setTimeout(() => {
      data.timer = null
      // 放入业务逻辑
      method.handleSearch()
    }, DEBOUNCE_TIME)
  }
)
</script>
<style lang="less"></style>
