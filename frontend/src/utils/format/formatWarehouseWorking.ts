import i18n from '@/languages/i18n'
import { PROCESS_JOB_COMBINE, PROCESS_JOB_SPLIT, FREEZE_JOB_FREEZE,FREEZE_JOB_UNFREEZE } from '@/constant/warehouseWorking'
import { MoveStatus } from '@/types/WarehouseWorking/WarehouseMove'

export const formatProcessJobType = (value: boolean) =>
  value === PROCESS_JOB_COMBINE
    ? i18n.global.t('wms.warehouseWorking.warehouseProcessing.process_combine')
    : i18n.global.t('wms.warehouseWorking.warehouseProcessing.process_split')

export const formatFreezeJobType = (value: boolean) =>
  value === FREEZE_JOB_FREEZE
    ? i18n.global.t('wms.warehouseWorking.warehouseFreeze.freeze')
    : i18n.global.t('wms.warehouseWorking.warehouseFreeze.unfreeze')

export const formatMoveStatus = (value: number) =>
  value === MoveStatus.UNADJUST
    ? i18n.global.t('wms.warehouseWorking.warehouseMove.unadjust')
    : i18n.global.t('wms.warehouseWorking.warehouseMove.adjusted')
