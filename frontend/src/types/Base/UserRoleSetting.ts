import { UniformFileNaming, TablePage } from '../System/Form'

export interface UserRoleVO extends UniformFileNaming {
  id: number
  role_name: string
  is_valid: boolean
}

export interface DataProps {
  tableData: UserRoleVO[]
  tablePage: TablePage
  showDialog: boolean
  dialogForm: UserRoleVO
}
