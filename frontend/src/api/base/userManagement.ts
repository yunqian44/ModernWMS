import http from '@/utils/http/request'
import { UserVO, ChangePwdAPIParams } from '@/types/Base/UserManagement'

// Get all users
export const userAll = () => http({
    url: '/user/all',
    method: 'get'
  })

// Add a new user
export const addUser = (data: UserVO) => http({
    url: '/user',
    method: 'post',
    data
  })

// Update user
export const updateUser = (data: UserVO) => http({
    url: '/user',
    method: 'put',
    data
  })

// Delete user
export const deleteUser = (id: number) => http({
    url: '/user',
    method: 'put',
    data: {
      data: id
    }
  })

// Reset password
export const resetPassword = (id_list: number[]) => http({
    url: '/user/reset-pwd',
    method: 'post',
    data: {
      id_list
    }
  })

// Change password
export const changePassword = (data: ChangePwdAPIParams) => http({
    url: '/user/change-pwd',
    method: 'post',
    data
  })
