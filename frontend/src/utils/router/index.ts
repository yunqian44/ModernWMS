import { menuItem } from '@/types/system/store'
import { CustomerRouterProps } from '@/types/system/router'
import { sideBarMenu } from '@/types/home'
import { store } from '@/store'
import i18n from '@/languages/i18n'

// Convert menu permissions to data required for dynamic routing
export function menusToRouter(menuList: menuItem[]): CustomerRouterProps[] {
  const result: CustomerRouterProps[] = []
  for (const menu of menuList) {
    result.push({
      name: menu.vue_path,
      path: `/${ menu.vue_path }`,
      directory: menu.vue_directory,
      redirect: '',
      component: null
    })
  }
  return result
}

// Convert the menu permission returned from the back end to the sidebar
export function menusToSideBar(): sideBarMenu[] {
  const result: sideBarMenu[] = []
  const menuList: menuItem[] = store.getters['user/menulist']
  for (const menu of menuList) {
    // Get the module index and check whether this group exists
    const moduleIndex = result.findIndex((item) => item.lable === i18n.global.t(`router.sideBar.${ menu.module }`))
    const item = GetMenuNameAndModule(menu.vue_path)
    if (item.lable) {
      // Primary menu
      if (!menu.module) {
        result.push({
          ...item,
          icon: GetModuleAndIcon(menu.vue_path),
          routerPath: menu.vue_path
        })
        continue
      }
      // Secondary menu
      if (moduleIndex > -1) {
        result[moduleIndex].children?.push({
          ...item,
          routerPath: menu.vue_path
        })
      } else {
        result.push({
          lable: i18n.global.t(`router.sideBar.${ menu.module }`),
          icon: GetModuleAndIcon(menu.module),
          showDetail: true,
          children: [
            {
              ...item,
              routerPath: menu.vue_path
            }
          ]
        })
      }
    }
  }
  return result
}

// Get the menu name, module and icon
function GetMenuNameAndModule(path: string): sideBarMenu {
  switch (path) {
    case 'homepage':
      return { lable: i18n.global.t('router.sideBar.homepage') }
    case 'ownerOfCargo':
      return { lable: i18n.global.t('router.sideBar.ownerOfCargo') }
    case 'menuBasicSettings':
      return { lable: i18n.global.t('router.sideBar.menuBasicSettings') }
    case 'userManagement':
      return { lable: i18n.global.t('router.sideBar.userManagement') }
    case 'commodityCategorySetting':
      return { lable: i18n.global.t('router.sideBar.commodityCategorySetting') }
    case 'commodityManagement':
      return { lable: i18n.global.t('router.sideBar.commodityManagement') }
    case 'userRoleSetting':
      return { lable: i18n.global.t('router.sideBar.userRoleSetting') }
    case 'CompanySetting':
      return { lable: i18n.global.t('router.sideBar.companySetting') }
    default:
      return { lable: '' }
  }
}
function GetModuleAndIcon(name: string) {
  switch (name) {
    case 'baseModule':
      return 'cog'
    case 'homepage':
      return 'home'
    default:
      return ''
  }
}
