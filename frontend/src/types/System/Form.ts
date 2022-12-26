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

export interface NavListOptions {
  title: string
  labelKey: string
  indexKey: string
  indexValue: string
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

export interface SearchObject {
  name?: string,
  operators?: SearchOperator,
  text?: string,
  value?: string
}

export enum SearchOperator {
  INCLUDE = 6,
  EQUAL = 1
}