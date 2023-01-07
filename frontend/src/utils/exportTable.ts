import * as XLSX from 'xlsx'
import { hookComponent } from '@/components/system'
import i18n from '@/languages/i18n'
/**
 * Export table
 * Default type is 'xlsx'
 */
export const exportData = ({ table, filename, columnFilterMethod }: IExportTable): void => {
  try {
    const columns = table?.getColumns()
    const theadDOM = table.$el.querySelector('.body--wrapper>.vxe-table--header')
    const theadDOMCopy = theadDOM.cloneNode(true)
    const parentHeader = theadDOMCopy.querySelector('.vxe-header--row')
    const header = theadDOMCopy.querySelectorAll('.vxe-header--column')
    
    // Remove tr in table
    for (let i = 0; i < columns.length; i++) {
      const column = columns[i]
      if (columnFilterMethod && !columnFilterMethod({ column })) {
        parentHeader.removeChild(header[i])
      }
    }

    const workBook = XLSX.utils.table_to_book(theadDOMCopy as HTMLElement)
    XLSX.writeFile(workBook, `${ filename }.xlsx`)
  } catch (error) {
    hookComponent.$message({
      type: 'error',
      content: `${ i18n.global.t('system.page.export') }${ i18n.global.t('system.tips.fail') }`
    })
  }
}

interface IExportTable {
  table: any
  filename?: string
  exceptIndex?: Array<number>
  columnFilterMethod?: FilterMEthod
}

type FilterMEthod = ({ column, row }: any) => boolean
