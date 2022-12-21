import http from '@/utils/http/request'
import { UserRoleVO } from '@/types/Base/UserRoleSetting'

// Get all user
export const getUserRoleAll = () => http({
    url: '/userrole/all',
    method: 'get'
  })

// Add a new user
export const addUserRole = (data: UserRoleVO) => http({
    url: '/userrole',
    method: 'post',
    data
  })

// Update user
export const updateUserRole = (data: UserRoleVO) => http({
    url: '/userrole',
    method: 'put',
    data
  })

// Delete user
export const deleteUser = (id: number) => http({
    url: '/userrole',
    method: 'delete',
    params: {
      id
    }
  })
