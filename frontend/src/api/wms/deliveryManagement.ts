import http from '@/utils/http/request'
import { PageConfigProps } from '@/types/System/Form'
import { addRequestVO } from '@/types/DeliveryManagement/DeliveryManagement'

// Get Pre shipment
export const getShipment = (data: PageConfigProps) => http({
    url: '/dispatchlist/list',
    method: 'post',
    data: {
      ...data,
      sqlTitle: ''
    }
  })

// Get Pre shipment
export const getPreShipment = (data: PageConfigProps) => http({
    url: '/dispatchlist/list',
    method: 'post',
    data: {
      ...data,
      sqlTitle: ''
    }
  })

// Get new shipment
export const getNewShipment = (data: PageConfigProps) => http({
    url: '/dispatchlist/list',
    method: 'post',
    data: {
      ...data,
      sqlTitle: ''
    }
  })

// Get new shipment
export const getGoodsToBePicked = (data: PageConfigProps) => http({
    url: '/dispatchlist/advanced-list',
    method: 'post',
    data: {
      ...data,
      sqlTitle: ''
    }
  })

// Add
export const addShipment = (data: addRequestVO[]) => http({
    url: '/dispatchlist',
    method: 'post',
    data
  })
