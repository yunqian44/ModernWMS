import { VxeGridProps } from 'vxe-table'
import { UniformFileNaming } from '../System/Form'

// Common request res
export interface HttpModel {
  code: number
  isSuccess: boolean
  data: any
  errorMessage: string
}

export interface UserVO extends UniformFileNaming {
  id?: number
  user_num: string
  user_name: string
  contact_tel?: string
  user_role: string
  sex?: string
  auth_string?: string
  is_valid: boolean
}

export interface TablePage {
  total: number
  pageIndex: number
  pageSize: number
}

export interface DataProps {
  tablePage: TablePage
  gridOptions: VxeGridProps
  showDialog: boolean
  dialogForm: UserVO
}

export interface ChangePwdAPIParams {
  id: number
  old_password: string
  new_password: string
}
