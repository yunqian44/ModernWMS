import { userStateProps, menuItem } from '@/types/system/store'

export const user = {
  namespaced: true,
  state: {
    // TODO userInfo 类
    userInfo: {},
    token: '',
    // 用于刷新token用的串
    refreshToken: '',
    // token失效的时间
    expirationTime: '',
    // token有效期
    effectiveMinutes: '',
    // 当前是否正在refreshToken
    isRefreshingToken: false,
    // 菜单权限
    // 目前用于测试
    menulist: [
      // {
      //   name: 'testmenu',
      //   path: '/testmenu',
      //   directory: 'testmenu/testmenu'
      // }
    ]
  },
  mutations: {
    setUserInfo(state: userStateProps, userInfo: any) {
      // 这里的 `state` 对象是模块的局部状态
      state.userInfo = userInfo
    },
    resetUserInfo(state: userStateProps, userInfo = {}) {
      state.userInfo = { ...state.userInfo, ...userInfo }
    },
    setToken(state: userStateProps, token: string) {
      // 这里的 `state` 对象是模块的局部状态
      state.token = token
    },
    setExpirationTime(state: userStateProps, expirationTime: number) {
      // 这里的 `state` 对象是模块的局部状态
      state.expirationTime = expirationTime
    },
    setIsRefreshingToken(state: userStateProps, isRefreshingToken: boolean) {
      // 这里的 `state` 对象是模块的局部状态
      state.isRefreshingToken = isRefreshingToken
    },
    setRefreshToken(state: userStateProps, refreshToken: string) {
      // 这里的 `state` 对象是模块的局部状态
      state.refreshToken = refreshToken
    },
    setEffectiveMinutes(state: userStateProps, effectiveMinutes: number) {
      // 这里的 `state` 对象是模块的局部状态
      state.effectiveMinutes = effectiveMinutes
    },
    setUserMenuList(state: userStateProps, menulist: menuItem[]) {
      state.menulist = menulist
    }
  },
  actions: {},
  getters: {
    userInfo(state: userStateProps) {
      return state.userInfo
    },
    token(state: userStateProps) {
      return state.token
    },
    expirationTime(state: userStateProps) {
      return state.expirationTime
    },
    isRefreshingToken(state: userStateProps) {
      return state.isRefreshingToken
    },
    refreshToken(state: userStateProps) {
      return state.refreshToken
    },
    effectiveMinutes(state: userStateProps) {
      return state.effectiveMinutes
    },
    menulist(state: userStateProps) {
      return state.menulist
    }
  }
}
