import { UniformFileNaming } from '../System/Form'

export interface DeliveryManagementVO extends UniformFileNaming {
  id: number
  dispatch_no?: string
  dispatch_status?: number
  qty?: number
  weight?: number
  volume?: number
  customer_id?: number
  customer_name?: string
  picked_qty?: number
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

export interface ConfirmOrderVO {
  spu_code: string
  sku_code: string
  qty: number
  confirm: boolean
  pick_list: ConfirmOrderPickListVO[]
}

export interface ConfirmOrderPickListVO {
  warehouse_name: string
  location_name: string
  pick_qty: number
}

export interface PackageVO {
  id?: number
  dispatch_no?: string
  dispatch_status?: number
  package_qty?: number
  picked_qty?: number
}

export interface WeighVO {
  id?: number
  dispatch_no?: string
  dispatch_status?: number
  weighing_qty?: number
  weighing_weight?: number
  picked_qty?: number
}

export interface DeliveryVO {
  id?: number
  dispatch_no?: string
  dispatch_status?: number
  picked_qty?: number
}

export interface SignInVO {
  id?: number
  dispatch_no?: string
  dispatch_status?: number
  damage_qty?: number
}