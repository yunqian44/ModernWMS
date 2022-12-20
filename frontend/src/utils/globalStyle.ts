import { store } from '@/store'

interface pageHasElement {
  hasTab?: boolean
  hasOperateBtn?: boolean
}

export const primaryColor = '#9C27B0'
export const primaryLightColor = '#ECE7F6'
export const lightGrey = '#999999'

export const SYSTEM_HEIGHT = {
  HEADER: 60,
  TAB: 70,
  OPERATE_BAR: 52
}
export const computedTableHeight = ({ hasTab = true, hasOperateBtn = true }:pageHasElement) => {
  const clientHeight = store.getters['system/clientHeight']
  const EXTRA_MARGIN = 100

  let res = clientHeight - SYSTEM_HEIGHT.HEADER - EXTRA_MARGIN

  if (hasTab) {
    res -= SYSTEM_HEIGHT.TAB 
  }
  if (hasOperateBtn) {
    res -= SYSTEM_HEIGHT.OPERATE_BAR
  }

  return `${ res }px`
}
