<template>
  <div class="appViewContainer">
    <v-dialog v-model="loadingFlag" :scrim="false" persistent max-width="200">
      <v-card color="primary">
        <v-card-text>
          Loading...
          <v-progress-linear indeterminate color="white"></v-progress-linear>
        </v-card-text>
      </v-card>
    </v-dialog>
    <router-view></router-view>
  </div>
</template>

<script setup lang="ts">
import { onBeforeMount, onMounted, ref } from 'vue'
import { emitter } from './utils/bus'
import { store } from './store'

const loadingFlag = ref(false)

onMounted(() => {
  emitter.on('showLoading', () => {
    // console.log('showloading')
    loadingFlag.value = true
  })
  emitter.on('closeLoading', () => {
    // console.log('closeLoading')
    loadingFlag.value = false
  })

  // 监听浏览器大小
  window.onresize = function () {
    const clientHeight = document.documentElement.clientHeight
    const clientWidth = document.documentElement.clientWidth
    store.commit('system/setClientHeight', clientHeight)
    store.commit('system/setClientWidth', clientWidth)
  }
})
</script>

<style scoped>
.appViewContainer {
  height: 100%;
  width: 100%;
  background-color: #f4f5fa;
}
</style>
