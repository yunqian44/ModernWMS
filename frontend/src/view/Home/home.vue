<template>
  <div v-if="data.isShow" class="HomeContainer">
    <HomeSideBar />
    <div class="homeRight">
      <HomeHeader />
      <div class="homeRouterContainer pt-2">
        <RouterView v-slot="{ Component }">
          <keep-alive :include="openedMenus.value">
            <component :is="Component"></component>
          </keep-alive>
        </RouterView>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { computed, reactive, nextTick, watch } from 'vue'
import { store } from '@/store'
import HomeHeader from './homeHeader.vue'
import HomeSideBar from './homeSideBar.vue'

const data = reactive({
  isShow: true // Used to refresh the interface when switching languages
})

const method = reactive({
  refreshSystem: () => {
    data.isShow = false
    nextTick(() => {
      data.isShow = true
      store.commit('system/setRefreshFlag', false)
    })
  }
})

const openedMenus = computed(() => store.getters['system/openedMenus'])
const refreshFlag = computed(() => store.getters['system/refreshFlag'])

watch(
  () => refreshFlag.value,
  (flag) => {
    if (flag) {
      method.refreshSystem()
    }
  }
)
</script>

<style scoped lang="less">
@headerHeight: 60px;
@sideBarWidth: 300px;
@sideBarTitleHeight: 80px;

.HomeContainer {
  width: 100vw;
  height: 100vh;
  display: flex;
  flex-wrap: wrap;

  .homeRight {
    width: calc(100% - @sideBarWidth);
    padding: 0 5%;

    .homeRouterContainer {
      height: calc(100% - @headerHeight);
      box-sizing: border-box;
    }
  }
}
</style>
