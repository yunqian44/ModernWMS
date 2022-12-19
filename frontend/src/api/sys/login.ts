import http from '@/utils/http/request'
import { LoginParams } from './model/userModel'

export const login = (data: LoginParams) => http({
    url: '/login',
    method: 'post',
    data
  })
