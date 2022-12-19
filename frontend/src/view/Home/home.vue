<template>
  <div class="HomeContainer">
    <div class="homeSidebar">
      <div class="sideBarTitle">
        <Logo :height="50" :top="15" :left="5" />
      </div>
      <!-- 测试数据 -->
      <div class="sideBarMenus">
        <div v-for="(item, index) in data.menuList" :key="index">
          <div class="menuItems" :class="method.getItemClass(item)" @click="method.openMenu(item)">
            <div style="display: flex; align-items: center; height: 100%">
              <div class="menuIcon">
                <v-icon
                  :icon="item.icon ? 'mdi-' + item.icon : 'mdi-checkbox-blank-circle-outline'"
                  :size="18"
                  :color="data.activeMenu === item.lable ? '#fff' : '#524e59'"
                ></v-icon>
              </div>
              <div class="menuLabel">{{ item.lable }}</div>
            </div>
            <div
              v-if="item.children && item.children.length > 0"
              :class="item.showDetail && 'rotate90'"
            >
              <v-icon icon="mdi-chevron-right" color="#524e59" :size="22"></v-icon>
            </div>
          </div>
          <Transition name="menu">
            <div v-show="item.showDetail">
              <div
                v-for="(detailItem, detailIndex) in item.children"
                :key="detailIndex"
                class="menuItems"
                :class="method.getItemClass(detailItem)"
                @click="method.openMenu(detailItem)"
              >
                <div style="display: flex; align-items: center; height: 100%">
                  <div class="menuIcon">
                    <v-icon
                      :icon="'mdi-checkbox-blank-circle-outline'"
                      :size="12"
                      :color="data.activeMenu === detailItem.lable ? '#fff' : '#524e59'"
                    ></v-icon>
                  </div>
                  <div class="menuLabel">{{ detailItem.lable }}</div>
                </div>
              </div>
            </div>
          </Transition>
        </div>
      </div>
    </div>
    <div class="homeRight">
      <div class="homeHeader">
        <div class="searchBar">
          <v-icon icon="mdi-crosshairs" color="#666666"></v-icon>
          <span>Search</span>
        </div>
        <div class="toolsBar">
          <LanguagesSwitch />
          <div class="toolItems bell">
            <v-icon icon="mdi-bell-outline" color="#666666"></v-icon>
            <div class="hasNotify"></div>
          </div>
          <div class="toolItems headPortrait">
            <div class="alive"></div>
          </div>
        </div>
      </div>
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
import Logo from '@/components/system/logo.vue'
import LanguagesSwitch from '@/components/system/languages.vue'

// test interface
type Menu = {
  icon?: string
  lable: string
  showDetail?: boolean
  children?: Menu[]
}

type DataProps = {
  menuList: Menu[]
  activeMenu: string
}

const data: DataProps = reactive({
  menuList: [
    {
      icon: 'radioactive-circle',
      lable: 'ActiveMenu',
      children: [
        {
          icon: 'radioactive-circle',
          lable: 'TestMenu2'
        },
        {
          icon: 'radioactive-circle',
          lable: 'TestMenu3'
        },
        {
          icon: 'radioactive-circle',
          lable: 'TestMenu4'
        }
      ]
    },
    {
      lable: 'TestMenu',
      children: []
    },
    {
      lable: 'TestMenuTwo',
      children: []
    },
    {
      lable: 'TestMenuThree',
      children: []
    }
  ],
  activeMenu: ''
})

const method = reactive({
  // Open menu
  openMenu: (item: Menu) => {
    if (item.children && item.children.length > 0) {
      item.showDetail = !item.showDetail
    } else {
      data.activeMenu = item.lable
    }
    // if (menuName === 'login') {
    //   store.commit('system/clearOpenedMenu', menuName)
    // } else {
    //   store.commit('system/addOpenedMenu', menuName)
    // }
    // router.push(menuName)
  },
  // Get item class
  getItemClass: (item: Menu) => {
    if (item.children && item.children.length > 0 && item.showDetail) {
      return 'openedMenuItems'
    }
    if (data.activeMenu === item.lable) {
      return 'activeMenuItems'
    }
    return ''
  }
})

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
  .homeSidebar {
    width: @sideBarWidth;
    box-shadow: 5px 5px 5px #dbdce2;
    height: 100%;
    .sideBarTitle {
      height: @sideBarTitleHeight;
      position: relative;
    }
    .sideBarMenus {
      height: calc(100% - @sideBarTitleHeight);
      overflow: auto;
      .menuItems {
        height: 42px;
        width: calc(100% - 20px);
        box-sizing: border-box;
        padding-left: 22px;
        padding-right: 8px;
        border-radius: 0 50px 50px 0;
        margin-bottom: 6px;
        display: flex;
        justify-content: space-between;
        align-items: center;
        color: #696671;
        cursor: pointer;
        .menuIcon {
          height: 100%;
          width: 20px;
          margin-right: 10px;
          display: flex;
          justify-content: center;
          align-items: center;
          .v-icon {
            width: 10px;
            height: 10px;
          }
        }
        &:hover {
          background-color: #edeef3;
        }
      }
      .openedMenuItems {
        background-color: #e8e9ed;
      }
      .activeMenuItems {
        background: linear-gradient(to right, #af85fc, #9155fd);
        color: #fff;
        &:hover {
          background: linear-gradient(to right, #af85fc, #9155fd);
        }
      }

      &::-webkit-scrollbar {
        width: 7px;
      }

      &::-webkit-scrollbar-thumb {
        border-radius: 20px;
        background-color: #e5e5e9;
      }
    }
  }
  .homeRight {
    width: calc(100% - @sideBarWidth);
    .homeHeader {
      margin: 0 5%;
      width: 90%;
      height: @headerHeight;
      position: relative;
      display: flex;
      justify-content: space-between;
      align-items: center;
      box-shadow: 0 3px 3px -2px var(--v-shadow-key-umbra-opacity, rgba(0, 0, 0, 0.2)),
        0 3px 4px 0 var(--v-shadow-key-penumbra-opacity, rgba(0, 0, 0, 0.14)),
        0 1px 8px 0 var(--v-shadow-key-penumbra-opacity, rgba(0, 0, 0, 0.12));
      box-sizing: border-box;
      padding: 0 20px;
      border-radius: 0 0 10px 10px;
      background-color: rgba(255, 255, 255, 0.5);
      .searchBar {
        display: flex;
        cursor: pointer;
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
    .homeRouterContainer {
      height: calc(100% - @headerHeight);
      box-sizing: border-box;
      padding: 20px;
    }
  }
}

/* 1.过渡动画 */
@keyframes axisY {
  from {
    transform: translateY(-10px);
  }
  to {
    transform: translateY(0px);
  }
}

/* 2. 过渡类名 */
/* 开始 */
.menu-enter-active {
  animation: axisY 0.1s;
}
/* 结束 */
.menu-leave-active {
  animation: axisY 0.1s reverse;
  overflow: hidden;
}

.rotate90 {
  transform: rotate(90deg);
}
</style>
