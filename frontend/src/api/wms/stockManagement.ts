import http from '@/utils/http/request'
import { PageConfigProps } from '@/types/System/Form'

// Get stock select
export const getStockSelectList = (data: PageConfigProps) => http({
    url: '/stock/select',
    method: 'post',
    data
  })

// Get sku select
export const getSkuSelectList = (data: PageConfigProps) => http({
    url: '/stock/sku-select',
    method: 'post',
    data
  })
