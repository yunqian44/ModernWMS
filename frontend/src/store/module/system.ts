interface stateProps {
  language: string
  currentRouterPath: string
  openedMenus: string[]
  clientWidth: number
  clientHeight: number
  refreshFlag: boolean
}

export const system = {
  namespaced: true,
  state: {
    language: '',
    openedMenus: [],
    // Window Size
    clientWidth: 0,
    clientHeight: 0,
    currentRouterPath: '',
    refreshFlag: false
  },
  mutations: {
    setCurrentRouterPath(state: stateProps, path: string) {
      state.currentRouterPath = path
    },
    setRefreshFlag(state: stateProps, flag: boolean) {
      state.refreshFlag = flag
    },
    setLanguage(state: stateProps, lang: string) {
      state.language = lang
    },
    addOpenedMenu(state: stateProps, menuName: string) {
      if (!state.openedMenus.includes(menuName)) {
        state.openedMenus.push(menuName)
      }
    },
    delOpenedMenu(state: stateProps, menuName: string) {
      const menuIndex = state.openedMenus.findIndex((item) => item === menuName)
      if (menuIndex > -1) {
        state.openedMenus.splice(menuIndex, 1)
      }
    },
    clearOpenedMenu(state: stateProps) {
      state.openedMenus = []
    },
    setClientWidth(state: stateProps, clientWidth: number) {
      state.clientWidth = clientWidth
    },
    setClientHeight(state: stateProps, clientHeight: number) {
      state.clientHeight = clientHeight
    }
  },
  actions: {},
  getters: {
    currentRouterPath(state: stateProps) {
      return state.currentRouterPath
    },
    language(state: stateProps) {
      return state.language
    },
    openedMenus(state: stateProps) {
      return state.openedMenus
    },
    clientWidth(state: stateProps) {
      return state.clientWidth
    },
    clientHeight(state: stateProps) {
      return state.clientHeight
    },
    refreshFlag(state: stateProps) {
      return state.refreshFlag
    }
  }
}
