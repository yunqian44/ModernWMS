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

export const deleteAsn = (id: number) => http({
    url: '/asn',
    method: 'delete',
    params: {
      id
    }
  })

export const confirmAsn = (id: number) => http({
    url: `/asn/confirm/${ id }`,
    method: 'put'
  })
