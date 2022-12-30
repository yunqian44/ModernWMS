import http from '@/utils/http/request'
import { PageConfigProps } from '@/types/System/Form'
import { WarehouseTakingVO } from '@/types/WarehouseWorking/WarehouseTaking'

// Get list
export const getStockTakingList = (data: PageConfigProps) => http({
    url: '/stocktaking/list',
    method: 'post',
    data
  })

// Get all
export const getStockTakingAll = () => http({
    url: '/stocktaking/all',
    method: 'get'
  })

// Get one
export const getStockTakingOne = (id: number) => http({
    url: '/stocktaking',
    method: 'get',
    params: {
      id
    }
  })

// Add a new form
export const addStockTaking = (data: WarehouseTakingVO) => http({
    url: '/stocktaking',
    method: 'post',
    data
  })

// Delete form
export const deleteStockTaking = (id: number) => http({
    url: '/stocktaking',
    method: 'delete',
    params: {
      id
    }
  })

  // Delete form
export const confirmStockTaking = (id: number) => http({
    url: '/stocktaking/confirm',
    method: 'put',
    params: {
      id
    }
  })
