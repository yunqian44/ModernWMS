import { UniformFileNaming } from '../System/Form'

export interface DeliveryManagementVO extends UniformFileNaming {
  id: number;
  dispatch_no?: string
  dispatch_status?: string
  qty?: number
  weight?: number
  volume?: number
  customer_id?: number
  customer_name?: string
  detailList: DeliveryManagementDetailListVO[]
}

export interface DeliveryManagementDetailListVO {
  id: number
  qty: number
  sku_id?: number
  spu_code?: string
  spu_name?: string
  sku_code?: string
}

export interface DeliveryManagementDetailVO extends DeliveryManagementVO {
  id: number
  spu_code?: string
  spu_name?: string
  sku_code?: string
}

export interface addRequestVO {
  customer_id: number
  customer_name: string
  sku_id: number
  qty: number
}
