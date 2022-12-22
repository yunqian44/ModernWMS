import { UniformFileNaming } from '../System/Form'

export interface RoleMenuVO extends UniformFileNaming {
  userrole_id: number
  role_name: string
  detailList: RoleMenuDetailVo[]
}

export interface RoleMenuDetailVo {
  id: number
  menu_name: string
}

// Attributes required for the table click method
export interface xTableProperty {
  row: RoleMenuVO
}

export interface DataProps {
  activeRoleMenuId: number
  roleList: RoleMenuVO[]
  showDialog: boolean
  dialogForm: RoleMenuVO
}
