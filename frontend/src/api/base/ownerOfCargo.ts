import http from '@/utils/http/request'
import { OwnerOfCargoVO } from '@/types/Base/OwnerOfCargo'

// Get all
export const getOwnerOfCargoAll = () => http({
    url: '/goodsowner/all',
    method: 'get'
  })

// Add a new form
export const addOwnerOfCargo = (data: OwnerOfCargoVO) => http({
    url: '/goodsowner',
    method: 'post',
    data
  })

// Update form
export const updateOwnerOfCargo = (data: OwnerOfCargoVO) => http({
    url: '/goodsowner',
    method: 'put',
    data
  })

// Delete form
export const deleteOwnerOfCargo = (id: number) => http({
    url: '/goodsowner',
    method: 'delete',
    params: {
      id
    }
  })
