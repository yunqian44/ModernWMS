export interface SupplierVO {
  id: number
  supplier_name: string
  city: string
  address: string
  manager: string
  email: string
  contact_tel: string
  creator: string
  create_time: Date
  last_update_time: Date
  is_valid: boolean
}

export interface DataProps {
  tableData: SupplierVO[]
  showDialog: boolean
  dialogForm: SupplierVO
}
