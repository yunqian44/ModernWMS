<template>
  <v-dialog v-model="isShow" :width="'30%'" transition="dialog-top-transition" :persistent="true">
    <template #default>
      <v-card>
        <v-toolbar color="white" :title="`${$t('wms.stockAsn.tabNotice')}`"></v-toolbar>
        <v-card-text>
          <v-form ref="formRef">
            <v-text-field
              v-model="data.form.spu_code"
              :label="$t('wms.stockAsnInfo.spu_code')"
              :rules="data.rules.spu_code"
              variant="outlined"
              readonly
              clearable
              @click="method.openSelect('target')"
              @click:clear="method.clearCommodity"
            ></v-text-field>
            <v-text-field
              v-model="data.form.spu_name"
              :label="$t('wms.stockAsnInfo.spu_name')"
              :rules="data.rules.spu_name"
              variant="outlined"
              disabled
            ></v-text-field>
            <v-text-field
              v-model="data.form.sku_code"
              :label="$t('wms.stockAsnInfo.sku_code')"
              :rules="data.rules.sku_code"
              variant="outlined"
              disabled
            ></v-text-field>
            <v-text-field
              v-model="data.form.sku_name"
              :label="$t('wms.stockAsnInfo.sku_name')"
              :rules="data.rules.sku_name"
              variant="outlined"
              disabled
            ></v-text-field>
            <v-text-field
              v-model="data.form.asn_qty"
              :label="$t('wms.stockAsnInfo.asn_qty')"
              :rules="data.rules.asn_qty"
              variant="outlined"
            ></v-text-field>
          </v-form>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn variant="text" @click="method.closeDialog">{{ $t('system.page.close') }}</v-btn>
          <v-btn color="primary" variant="text" @click="method.submit">{{ $t('system.page.submit') }}</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
  <sku-select :show-dialog="data.showSkuDialogSelect" @close="method.closeDialogSelect('target')" @sureSelect="method.sureSelect" />
</template>

<script lang="ts" setup>
import { reactive, computed, ref, watch } from 'vue'
import { StockAsnVO } from '@/types/WMS/StockAsn'
import i18n from '@/languages/i18n'
import { hookComponent } from '@/components/system/index'
import { addAsn, updateAsn } from '@/api/wms/stockAsn'
import skuSelect from '@/components/select/sku-select.vue'

const formRef = ref()
const emit = defineEmits(['close', 'saveSuccess'])

const props = defineProps<{
  showDialog: boolean
  form: StockAsnVO
}>()

const isShow = computed(() => props.showDialog)

const dialogTitle = computed(() => {
  if (props.form.id && props.form.id > 0) {
    return 'update'
  }
  return 'add'
})

const data = reactive({
  form: ref<StockAsnVO>({
    id: 0,
    asn_no: '',
    asn_status: 0,
    spu_id: 0,
    spu_code: '',
    spu_name: '',
    sku_id: 0,
    sku_code: '',
    sku_name: '',
    origin: '',
    length_unit: 0,
    volume_unit: 0,
    weight_unit: 0,
    asn_qty: 0,
    actual_qty: 0,
    sorted_qty: 0,
    shortage_qty: 0,
    more_qty: 0,
    damage_qty: 0,
    weight: 0,
    volume: 0,
    supplier_id: 0,
    supplier_name: '',
    goods_owner_id: 0,
    goods_owner_name: '',
    is_valid: true
  }),
  rules: {
    supplier_name: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('wms.stockAsnInfo.supplier_name') }!`],
    goods_owner_name: [],
    asn_qty: [],
    spu_code: [],
    spu_name: [],
    sku_code: [],
    sku_name: []
  },
  showSkuDialogSelect: false,
  curSelectType: ''
})

const method = reactive({
  closeDialog: () => {
    emit('close')
  },
  submit: async () => {
    const { valid } = await formRef.value.validate()
    if (valid) {
      const { data: res } = dialogTitle.value === 'add' ? await addAsn(data.form) : await updateAsn(data.form)
      if (!res.isSuccess) {
        hookComponent.$message({
          type: 'error',
          content: res.errorMessage
        })
        return
      }
      hookComponent.$message({
        type: 'success',
        content: `${ i18n.global.t('system.page.submit') }${ i18n.global.t('system.tips.success') }`
      })
      emit('saveSuccess')
    } else {
      hookComponent.$message({
        type: 'error',
        content: i18n.global.t('system.checkText.checkFormFail')
      })
    }
  },

  clearCommodity: () => {
    data.form.sku_id = 0
    data.form.spu_code = ''
    data.form.spu_name = ''
    data.form.sku_code = ''
    data.form.sku_name = ''
  },
  openSelect: (type: string) => {
    data.curSelectType = type

    if (type === 'target') {
      data.showSkuDialogSelect = true
    }
  },
  closeDialogSelect: (type: string) => {
    if (type === 'target') {
      data.showSkuDialogSelect = false
    }
  },
  sureSelect: (selectRecords: any) => {
    if (data.curSelectType === 'target') {
      data.form.sku_id = selectRecords[0].sku_id
      data.form.spu_code = selectRecords[0].spu_code
      data.form.spu_name = selectRecords[0].spu_name
      data.form.sku_code = selectRecords[0].sku_code
      data.form.sku_name = selectRecords[0].sku_name
    }
  }
})

watch(
  () => isShow.value,
  (val) => {
    if (val) {
      data.form = props.form
    }
  }
)
</script>

<style scoped lang="less">
.v-form {
  div {
    margin-bottom: 7px;
  }
}
</style>
