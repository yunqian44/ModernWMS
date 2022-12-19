<template>
  <div class="HomeContainer">
    <HomeSideBar />
    <div class="homeRight">
      <HomeHeader />
      <div class="homeRouterContainer">
        <RouterView v-slot="{ Component }">
          <keep-alive :include="openedMenus">
            <component :is="Component"></component>
          </keep-alive>
        </RouterView>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { computed, reactive } from 'vue'
import { router } from '@/router/index'
import { store } from '@/store'
import HomeHeader from './homeHeader.vue'
import HomeSideBar from './homeSideBar.vue'

const data = reactive({})

const method = reactive({})

const openedMenus = computed(() => store.getters['system/openedMenus'])
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
      padding-top: 20px;
    }
  }
}
</style>
