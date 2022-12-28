import i18n from '@/languages/i18n'
import { PROCESS_JOB_COMBINE, PROCESS_JOB_SPLIT } from '@/constant/warehouseWorking'

export const formatProcessJobType = (value: boolean) => (value === PROCESS_JOB_COMBINE
    ? i18n.global.t('wms.warehouseWorking.warehouseProcessing.process_combine')
    : i18n.global.t('wms.warehouseWorking.warehouseProcessing.process_split'))
