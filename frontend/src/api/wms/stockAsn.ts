import http from '@/utils/http/request'
import { PageConfigProps } from '@/types/System/Form'
import { StockAsnVO } from '@/types/WMS/StockAsn'

export const getStockAsnList = (data: PageConfigProps) => http({
    url: '/asn/list',
    method: 'post',
    data
  })

export const addAsn = (data: StockAsnVO) => http({
    url: '/asn',
    method: 'post',
    data
  })

export const updateAsn = (data: StockAsnVO) => http({
    url: '/asn',
    method: 'put',
    data
  })
