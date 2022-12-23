import i18n from '@/languages/i18n'

export const formatIsValid = (value: boolean) => (value ? i18n.global.t('system.combobox.yesOrNo.yes') : i18n.global.t('system.combobox.yesOrNo.no'))