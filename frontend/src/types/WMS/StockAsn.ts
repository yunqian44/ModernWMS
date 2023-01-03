import { UniformFileNaming } from '../System/Form'

export interface StockAsnVO extends UniformFileNaming {
  id: number
  asn_no: string
  asn_status: number
  spu_id: number
  spu_code: string
  spu_name: string
  sku_id: number
  sku_code: string
  sku_name: string
  origin: string
  length_unit: number
  volume_unit: number
  weight_unit: number
  asn_qty: number
  actual_qty: number
  sorted_qty: number
  shortage_qty: number
  more_qty: number
  damage_qty: number
  weight: number
  volume: number
  supplier_id: number
  supplier_name: string
  goods_owner_id: number
  goods_owner_name: string
  is_valid: boolean
}
