import { VxeGridProps } from 'vxe-table'
import { UniformFileNaming } from '../System/Form'

export interface UserVO extends UniformFileNaming {
  id?: number
  user_num: string
  user_name: string
  contact_tel?: string
  user_role: string
  sex?: string
  auth_string?: string
}

export interface TablePage {
  total: number
  currentPage: number
  pageSize: number
}

export interface dataProps {
  tableData: UserVO[]
  tablePage: TablePage
  gridOptions: VxeGridProps
}

export interface ChangePwdAPIParams {
  id: number
  old_password: string
  new_password: string
}