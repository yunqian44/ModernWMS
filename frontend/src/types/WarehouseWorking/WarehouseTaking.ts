import { UniformFileNaming } from '../System/Form'

export interface WarehouseTakingVO extends UniformFileNaming {
  id: number
  job_code: string
  job_status: boolean
  sku_id: number
  goods_owner_id: number
  goods_location_id: number
  book_qty: number
  counted_qty: number
  difference_qty: number
  spu_code: string
  spu_name: string
  sku_code: string
  warehouse: string
  location_name: string
  handler: string
  handle_time: string
}
