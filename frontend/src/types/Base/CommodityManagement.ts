import { UniformFileNaming } from '../System/Form'

export interface CommodityVO extends UniformFileNaming {
  id: number
  spu_code: string
  spu_name: string
  category_name: string
  spu_description: string
  bar_code: string
  sku_name: string
  supplier_name: string
  brand: string
  unit: string
  cost: string
  price: string
}

export interface CommodityDetailVO {
  id: number
}

export interface DataProps {
  tableData: CommodityVO[]
  showDialog: boolean
  dialogForm: CommodityVO
}
