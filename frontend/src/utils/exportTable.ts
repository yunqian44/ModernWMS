import * as XLSX from 'xlsx'
/**
 * Export table
 * Default type is 'xlsx'
 */
export const exportTable = ({ table, filename, exceptIndex = [0, 1] }: IExportTable): void => {
  const theadDOM = table.$el.querySelector('.body--wrapper>.vxe-table--header')
  const theadDOMCopy = theadDOM.cloneNode(true)
  const parentHeader = theadDOMCopy.querySelector('.vxe-header--row')
  const header = theadDOMCopy.querySelectorAll('.vxe-header--column')

  // Remove tr in table
  for (let i = 0; i < header.length; i++) {
    if (exceptIndex.includes(i)) {
      parentHeader.removeChild(header[i])
    }
  }

  const workBook = XLSX.utils.table_to_book(theadDOMCopy as HTMLElement)
  XLSX.writeFile(workBook, `${ filename }.xlsx`)
}

interface IExportTable {
  table: any
  filename?: string
  exceptIndex?: Array<number>
}
