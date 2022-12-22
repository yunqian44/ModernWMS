import http from '@/utils/http/request'
import { PageConfigProps } from '@/types/System/Form'
import { FreightVO } from '@/types/Base/Freight'

// Find Data by Pagination
export const getFreightList = (data: PageConfigProps) => http({
    url: '/freight/list',
    method: 'post',
    data
  })

// Add a new freight
export const addFreight = (data: FreightVO) => http({
    url: '/freight',
    method: 'post',
    data
  })

// Update freight
export const updateFreight = (data: FreightVO) => http({
    url: '/freight',
    method: 'put',
    data
  })

// Delete freight
export const deleteFreight = (id: number) => http({
    url: '/freight',
    method: 'delete',
    params: {
      id
    }
  })
