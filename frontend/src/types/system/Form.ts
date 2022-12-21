export interface UniformFileNaming {
  creator?: string
  create_time?: string
  last_update_time?: string
  tenant_id?: number
}

export interface PageConfigProps {
  pageIndex: number
  pageSize: number
  sqlTitle?: string
  searchObjects?: any
}