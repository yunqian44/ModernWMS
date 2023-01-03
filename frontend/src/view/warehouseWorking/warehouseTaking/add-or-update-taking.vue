<!-- Warehouse Taking Operate Dialog -->
<template>
  <v-dialog v-model="isShow" :width="'30%'" transition="dialog-top-transition" :persistent="true">
    <template #default>
      <v-card>
        <v-toolbar class="" color="white" :title="`${$t('router.sideBar.warehouseTaking')}`"></v-toolbar>
        <v-card-text>
          <v-form ref="formRef">
            <v-text-field
              v-model="data.form.spu_code"
              :label="$t('base.commodityManagement.spu_code')"
              :rules="data.rules.spu_code"
              variant="outlined"
              readonly
              clearable
              @click="method.openCommoditySelect"
              @click:clear="method.clearCommodity"
            ></v-text-field>
            <v-text-field
              v-model="data.form.spu_name"
              :label="$t('base.commodityManagement.spu_name')"
              :rules="data.rules.spu_name"
              variant="outlined"
              disabled
            ></v-text-field>
            <v-text-field
              v-model="data.form.sku_code"
              :label="$t('base.commodityManagement.sku_code')"
              :rules="data.rules.sku_code"
              variant="outlined"
              disabled
            ></v-text-field>
            <v-text-field
              v-model="data.form.warehouse"
              :label="$t('wms.warehouseWorking.warehouseTaking.warehouse')"
              :rules="data.rules.warehouse"
              variant="outlined"
              disabled
            ></v-text-field>
            <v-text-field
              v-model="data.form.location_name"
              :label="$t('wms.warehouseWorking.warehouseTaking.location_name')"
              :rules="data.rules.location_name"
              variant="outlined"
              disabled
            ></v-text-field>
            <v-text-field
              v-model="data.form.book_qty"
              :label="$t('wms.warehouseWorking.warehouseTaking.book_qty')"
              :rules="data.rules.book_qty"
              variant="outlined"
              disabled
            ></v-text-field>
          </v-form>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn variant="text" @click="method.closeDialog">{{ $t('system.page.close') }}</v-btn>
          <v-btn color="primary" variant="text" :disabled="operateDisabled" @click="method.submit">{{ $t('system.page.submit') }}</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>

  <commodity-select
    :show-dialog="data.showCommodityDialogSelect"
    @close="method.closeCommodityDialogSelect"
    @sureSelect="method.sureSelectCommodity"
  />
</template>

<script lang="ts" setup>
import { reactive, computed, ref, watch } from 'vue'
import { hookComponent } from '@/components/system/index'
import { addStockTaking } from '@/api/wms/warehouseTaking'
import { WarehouseTakingVO } from '@/types/WarehouseWorking/WarehouseTaking'
import { removeObjectNull } from '@/utils/common'
import { TAKING_JOB_FINISH } from '@/constant/warehouseWorking'
import i18n from '@/languages/i18n'
import commoditySelect from '@/components/select/commodity-select.vue'

const formRef = ref()
const emit = defineEmits(['close', 'saveSuccess'])
const isUpdate = computed(() => props.form.id && props.form.id > 0)
const operateDisabled = computed(() => !!isUpdate.value)

const props = defineProps<{
  showDialog: boolean
  form: WarehouseTakingVO
}>()

const isShow = computed(() => props.showDialog)

const data = reactive({
  showCommodityDialogSelect: false,
  showLocationDialogSelect: false,

  form: ref<WarehouseTakingVO>({
    id: 0,
    job_code: '',
    job_status: TAKING_JOB_FINISH,
    sku_id: 0,
    goods_owner_id: 0,
    goods_location_id: 0,
    book_qty: 0,
    counted_qty: 0,
    difference_qty: 0,
    spu_code: '',
    spu_name: '',
    sku_code: '',
    warehouse: '',
    location_name: '',
    handler: '',
    handle_time: '',
    adjust_status: false,
    creator: '',
    create_time: ''
  }),
  rules: {
    job_type: [],
    book_qty: [
      (val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('wms.warehouseWorking.warehouseTaking.book_qty') }!`
    ],
    warehouse: [
      (val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('wms.warehouseWorking.warehouseTaking.warehouse') }!`
    ],
    location_name: [
      (val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('wms.warehouseWorking.warehouseTaking.location_name') }!`
    ],
    spu_code: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.commodityManagement.spu_code') }!`],
    spu_name: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.commodityManagement.spu_name') }!`],
    sku_code: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.commodityManagement.sku_code') }!`],
    sku_name: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.commodityManagement.sku_name') }!`]
  }
})

const method = reactive({
  closeDialog: () => {
    emit('close')
  },

  initForm: () => {
    data.form = props.form
  },

  openCommoditySelect: () => {
    // Open select modal after UI rendered.
    setTimeout(() => {
      data.showCommodityDialogSelect = true
    }, 100)
  },

  closeCommodityDialogSelect: () => {
    data.showCommodityDialogSelect = false
  },

  sureSelectCommodity: (selectRecords: any) => {
    try {
      data.form.sku_id = selectRecords[0].sku_id
      data.form.goods_location_id = selectRecords[0].goods_location_id
      data.form.warehouse = selectRecords[0].warehouse
      data.form.location_name = selectRecords[0].location_name
      data.form.spu_code = selectRecords[0].spu_code
      data.form.spu_name = selectRecords[0].spu_name
      data.form.sku_code = selectRecords[0].sku_code
      data.form.book_qty = selectRecords[0].qty_available
    } catch (error) {
      // console.error(error)
    }
  },

  clearCommodity: () => {
    data.form.sku_id = 0
    data.form.goods_location_id = 0
    data.form.warehouse = ''
    data.form.location_name = ''
    data.form.spu_code = ''
    data.form.spu_name = ''
    data.form.sku_code = ''
    data.form.book_qty = 0
  },

  submit: async () => {
    const { valid } = await formRef.value.validate()

    const form = method.constructFormBody()

    if (valid) {
      const { data: res } = await addStockTaking(form)
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

  constructFormBody: () => {
    let form = { ...data.form }
    form = removeObjectNull(form)

    return form
  }
})

watch(
  () => isShow.value,
  (val) => {
    if (val) {
      method.initForm()
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
