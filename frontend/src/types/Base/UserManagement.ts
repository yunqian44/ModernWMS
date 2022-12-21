import { UniformFileNaming, TablePage } from '../System/Form'

export interface UserVO extends UniformFileNaming {
  id: number
  user_num: string
  user_name: string
  contact_tel?: string
  user_role?: string
  sex?: string
  auth_string?: string
  is_valid: boolean
}

export interface DataProps {
  tableData: UserVO[]
  tablePage: TablePage
  showDialog: boolean
  dialogForm: UserVO
}

export interface ChangePwdAPIParams {
  id: number
  old_password: string
  new_password: string
}
