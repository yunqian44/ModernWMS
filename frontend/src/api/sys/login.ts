import http from '@/utils/http/request'
import { LoginParams } from '../../types/System/UserModel'

export const login = (data: LoginParams) => http({
    url: '/login',
    method: 'post',
    data
  })
