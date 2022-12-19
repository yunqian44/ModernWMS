// test interface
export interface sideBarMenu {
  icon?: string
  lable: string
  routerPath?: string
  showDetail?: boolean
  children?: sideBarMenu[]
}

export interface sideBarDataProps {
  menuList: sideBarMenu[]
  activeMenu: string
}