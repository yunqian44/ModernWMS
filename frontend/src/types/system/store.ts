// user
export interface menuItem {
  menu_name: string
  module: string
  vue_path: string
  vue_path_detail: string
  vue_directory: string
}

export interface userStateProps {
  userInfo: any
  token: string
  refreshToken: string
  expirationTime: number
  effectiveMinutes: number
  isRefreshingToken: boolean
  menulist: menuItem[]
}
