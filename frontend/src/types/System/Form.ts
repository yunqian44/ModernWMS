export interface UniformFileNaming {
  creator?: string
  create_time?: string
  last_update_time?: string
  tenant_id?: number
}

// VxeTable Required paging parameters
export interface TablePage {
  total: number
  pageIndex: number
  pageSize: number
}

// API Required paging parameters
export interface PageConfigProps {
  pageIndex: number
  pageSize: number
  sqlTitle?: string
  searchObjects?: any
}

export interface VxeTableRow {
  _X_ROW_KEY?: string
}