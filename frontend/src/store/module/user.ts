import { UserStateProps, MenuItem } from '@/types/System/Store'

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
    setUserInfo(state: UserStateProps, userInfo: any) {
      // 这里的 `state` 对象是模块的局部状态
      state.userInfo = userInfo
    },
    resetUserInfo(state: UserStateProps, userInfo = {}) {
      state.userInfo = { ...state.userInfo, ...userInfo }
    },
    setToken(state: UserStateProps, token: string) {
      // 这里的 `state` 对象是模块的局部状态
      state.token = token
    },
    setExpirationTime(state: UserStateProps, expirationTime: number) {
      // 这里的 `state` 对象是模块的局部状态
      state.expirationTime = expirationTime
    },
    setIsRefreshingToken(state: UserStateProps, isRefreshingToken: boolean) {
      // 这里的 `state` 对象是模块的局部状态
      state.isRefreshingToken = isRefreshingToken
    },
    setRefreshToken(state: UserStateProps, refreshToken: string) {
      // 这里的 `state` 对象是模块的局部状态
      state.refreshToken = refreshToken
    },
    setEffectiveMinutes(state: UserStateProps, effectiveMinutes: number) {
      // 这里的 `state` 对象是模块的局部状态
      state.effectiveMinutes = effectiveMinutes
    },
    setUserMenuList(state: UserStateProps, menulist: MenuItem[]) {
      state.menulist = menulist
    }
  },
  actions: {},
  getters: {
    userInfo(state: UserStateProps) {
      return state.userInfo
    },
    token(state: UserStateProps) {
      return state.token
    },
    expirationTime(state: UserStateProps) {
      return state.expirationTime
    },
    isRefreshingToken(state: UserStateProps) {
      return state.isRefreshingToken
    },
    refreshToken(state: UserStateProps) {
      return state.refreshToken
    },
    effectiveMinutes(state: UserStateProps) {
      return state.effectiveMinutes
    },
    menulist(state: UserStateProps) {
      return state.menulist
    }
  }
}
