import { UniformFileNaming } from '../System/Form'

export interface OwnerOfCargoVO extends UniformFileNaming {
  id: number
  goods_owner_name: string
  city: string
  address: string
  contact_tel: string
  manager: string
}

export interface ImportVO {
  goods_owner_name: string
  city: string
  address: string
  contact_tel: string
  manager: string
}

export interface DataProps {
  tableData: OwnerOfCargoVO[]
  showDialog: boolean
  showDialogImport: boolean
  dialogForm: OwnerOfCargoVO
}
