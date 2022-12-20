<template>
  <div class="homeHeader">
    <div class="menuTitle">
      <v-breadcrumbs :items="data.breadcrumbItems">
        <template #prepend>
          <v-icon :color="lightGrey" size="small" icon="mdi-form-select"></v-icon>
        </template>
      </v-breadcrumbs>
    </div>
    <div class="toolsBar">
      <LanguagesSwitch />
      <div class="toolItems bell">
        <v-icon icon="mdi-bell-outline" color="#666666"></v-icon>
        <div class="hasNotify"></div>
      </div>
      <v-menu>
        <template #activator="{ props }">
          <div class="toolItems headPortrait" v-bind="props">
            <div class="alive"></div>
          </div>
        </template>
        <v-list>
          <v-list-item
            v-for="(item, index) in data.userOperationMenu"
            :key="index"
            :value="index"
            @click="method.operation(item.value)"
          >
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { reactive, watch } from 'vue'
import { useRouter } from 'vue-router'
import LanguagesSwitch from '@/components/system/languages.vue'
import { lightGrey } from '@/utils/globalStyle'
import i18n from '@/languages/i18n'
import { router } from '@/router'
import { store } from '@/store'

const routerInfo = useRouter()

const data = reactive({
  breadcrumbItems: [
    {
      title: '基础设置',
      disabled: true
    },
    {
      title: '运费设置',
      disabled: true,
      href: 'breadcrumbs_link_1'
    }
  ],
  // User Action List
  userOperationMenu: [
    {
      title: i18n.global.t('system.homeHeader.logout'),
      value: 'logout'
    }
  ]
})

watch(
  () => routerInfo,
  (newValue) => {
    // Do something when the routes changed
    console.log(newValue.currentRoute.value)
  },
  { immediate: true, deep: true }
)

const method = reactive({
  // User operation method
  operation: (value: string) => {
    if (value === 'logout') {
      store.commit('system/clearOpenedMenu')
      store.commit('system/setCurrentRouterPath', '')

      router.push('/login')
    }
  }
})
</script>

<style scoped lang="less">
@headerHeight: 60px;

.homeHeader {
  width: 100%;
  height: @headerHeight;
  position: relative;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 20px;
  box-sizing: border-box;
  border-radius: 0 0 10px 10px;
  transition: all 0.5s;

  box-shadow: 0 3px 3px -2px var(--v-shadow-key-umbra-opacity, rgba(0, 0, 0, 0.2)),
    0 3px 4px 0 var(--v-shadow-key-penumbra-opacity, rgba(0, 0, 0, 0.14)),
    0 1px 8px 0 var(--v-shadow-key-penumbra-opacity, rgba(0, 0, 0, 0.12));
  background-color: rgba(255, 255, 255, 0.5);

  .menuTitle {
    display: flex;

    .v-icon {
      margin-right: 10px;
    }

    span {
      color: #b9b8bc;
    }
  }

  .toolsBar {
    display: flex;
    align-items: center;
    justify-content: flex-end;

    .toolItems {
      height: 40px;
      width: 40px;
      font-size: 16px;
      display: flex;
      justify-content: center;
      align-items: center;
      transition: all 0.5s;
      border-radius: 40px;
      margin-right: 10px;
      cursor: pointer;

      &:hover {
        background-color: #e4e5ea;
      }

      &:active {
        background-color: #cdced3;
      }
    }

    .bell {
      position: relative;

      .hasNotify {
        margin: 0;
        width: 11px;
        height: 11px;
        border-radius: 11px;
        background-color: #ff4c51;
        border: 1px solid #fff;
        position: absolute;
        top: 5px;
        right: 7px;
      }
    }

    .headPortrait {
      border-radius: 40px;
      width: 40px;
      height: 40px;
      background-color: #e8e2fb;
      position: relative;

      .alive {
        margin: 0;
        width: 11px;
        height: 11px;
        border-radius: 11px;
        background-color: #56ca00;
        border: 1px solid #fff;
        position: absolute;
        bottom: 1px;
        right: 1px;
      }
    }
  }
}
</style>
