import http from '@/utils/http/request'
import { PageConfigProps } from '@/types/System/Form'

export const getStockAsnList = (data: PageConfigProps) => http({
    url: '/asn/list',
    method: 'post',
    data
  })
