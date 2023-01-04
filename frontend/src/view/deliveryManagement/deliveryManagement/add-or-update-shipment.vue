<template>
  <v-dialog v-model="isShow" :width="'30%'" transition="dialog-top-transition" :persistent="true">
    <template #default>
      <v-card class="formCard">
        <v-toolbar color="white" :title="`${$t('router.sideBar.roleMenu')}`"></v-toolbar>
        <v-card-text>
          <v-form ref="formRef">
            <v-select
              v-model="data.form.customer_name"
              :items="data.combobox.customer_name"
              item-title="label"
              item-value="label"
              :rules="data.rules.customer_name"
              :label="$t('wms.deliveryManagement.customer_name')"
              variant="outlined"
              clearable
              @update:model-value="method.customerNameChange"
            ></v-select>
            <v-row v-for="(item, index) of data.form.detailList" :key="index">
              <v-col :cols="7">
                <v-select
                  v-model="item.sku_id"
                  :items="data.combobox.sku_code"
                  item-title="label"
                  item-value="value"
                  :rules="data.rules.sku_id"
                  :label="$t('wms.deliveryManagement.sku_code')"
                  variant="outlined"
                  clearable
                ></v-select>
              </v-col>
              <v-col :cols="3">
                <v-text-field
                  v-model="item.qty"
                  :rules="data.rules.qty"
                  :label="$t('wms.deliveryManagement.detailQty')"
                  variant="outlined"
                  clearable
                ></v-text-field>
              </v-col>
              <v-col :cols="2">
                <div class="detailBtnContainer">
                  <tooltip-btn
                    :flat="true"
                    icon="mdi-delete-outline"
                    :tooltip-text="$t('system.page.delete')"
                    :icon-color="errorColor"
                    @click="method.removeItem(index, item)"
                  ></tooltip-btn>
                </div>
              </v-col>
            </v-row>
            <v-btn style="font-size: 20px; margin-bottom: 15px; margin-top: 10px; float: right" color="primary" :width="40" @click="method.AddDetail">
              +
            </v-btn>
          </v-form>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn variant="text" @click="method.closeDialog">{{ $t('system.page.close') }}</v-btn>
          <v-btn color="primary" variant="text" @click="method.submit">{{ $t('system.page.submit') }}</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>

<script lang="ts" setup>
import { reactive, computed, ref, watch, nextTick } from 'vue'
import { DeliveryManagementVO, DeliveryManagementDetailListVO, addRequestVO } from '@/types/DeliveryManagement/DeliveryManagement'
import i18n from '@/languages/i18n'
import { errorColor } from '@/constant/style'
import { hookComponent } from '@/components/system/index'
import { addShipment } from '@/api/wms/deliveryManagement'
import { getSupplierAll } from '@/api/base/supplier'
import { getSpuList } from '@/api/base/commodityManagementSetting'
import tooltipBtn from '@/components/tooltip-btn.vue'
import { checkDetailRepeatGetBool } from '@/utils/dataVerification/page'

const formRef = ref()
const emit = defineEmits(['close', 'saveSuccess'])

const props = defineProps<{
  showDialog: boolean
  form: DeliveryManagementVO
}>()

const isShow = computed(() => props.showDialog)

const data = reactive({
  dialogTitle: '',
  form: ref<DeliveryManagementVO>({
    id: 0,
    detailList: []
  }),
  removeDetailList: ref<DeliveryManagementDetailListVO[]>([]),
  combobox: ref<{
    customer_name: {
      label: string
      value: number
    }[]
    sku_code: {
      label: string
      value: number
    }[]
  }>({
    customer_name: [],
    sku_code: []
  }),
  rules: {
    customer_name: [
      (val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('wms.deliveryManagement.customer_name') }!`
    ],
    sku_id: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('wms.deliveryManagement.sku_code') }!`],
    qty: [(val: number) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('wms.deliveryManagement.detailQty') }!`]
  }
})

const method = reactive({
  // When customers change
  customerNameChange: (val: string) => {
    if (!val) {
      data.form.customer_id = 0
    } else {
      data.form.customer_id = data.combobox.customer_name.filter((item) => item.label === val)[0].value
    }
  },
  // Add a new detail
  AddDetail: () => {
    data.form.detailList.push({ id: 0, qty: 0 })
    // Let the scroll bar go to the bottom, Improve user experience
    const carText = document.querySelector('.formCard')
    if (carText && carText.scrollHeight > carText.clientHeight) {
      nextTick(() => {
        // Set scroll bar to the bottom
        carText.scrollTop = carText.scrollHeight
      })
    }
  },
  // Get dialog type, add or update
  getDialogType: () => {
    // This is special because this document is not actually a table,
    // but the data associated with another table.
    // so directly use the details to determine whether it is a new document
    if (props.form.detailList.length > 0) {
      data.dialogTitle = 'update'
    } else {
      data.dialogTitle = 'add'
    }
  },
  // Get the options required by the drop-down box
  getCombobox: async () => {
    data.combobox.customer_name = []
    data.combobox.sku_code = []
    const { data: supplierRes } = await getSupplierAll()
    if (!supplierRes.isSuccess) {
      return
    }
    for (const item of supplierRes.data) {
      if (item.is_valid) {
        data.combobox.customer_name.push({
          label: item.supplier_name,
          value: item.id
        })
      }
    }
    const { data: skuRes } = await getSpuList({ pageIndex: 1, pageSize: 99999999 })
    if (!skuRes.isSuccess) {
      return
    }
    for (const item of skuRes.data.rows) {
      for (const dItem of item.detailList) {
        data.combobox.sku_code.push({
          label: dItem.sku_code,
          value: dItem.id
        })
      }
    }
  },
  closeDialog: () => {
    emit('close')
  },
  // Details verification before document submission
  beforeSubmit: (): boolean => {
    let flag = true
    if (data.form.detailList.length === 0) {
      hookComponent.$message({
        type: 'error',
        content: i18n.global.t('system.tips.detailLengthIsZero')
      })
      flag = false
    } else if (checkDetailRepeatGetBool(data.form.detailList, ['sku_id'])) {
      hookComponent.$message({
        type: 'error',
        content: i18n.global.t('system.tips.detailHasItemRepeat')
      })
      flag = false
    }
    return flag
  },
  submit: async () => {
    const beforeSubmitFlag = method.beforeSubmit()
    if (!beforeSubmitFlag) {
      return
    }
    const { valid } = await formRef.value.validate()
    if (valid) {
      const reqList: addRequestVO[] = []
      for (const item of data.form.detailList) {
        reqList.push({
          customer_id: data.form.customer_id ? data.form.customer_id : 0,
          customer_name: data.form.customer_name ? data.form.customer_name : '',
          qty: item.qty,
          sku_id: item.sku_id ? item.sku_id : 0
        })
      }
      const { data: res } = await addShipment(reqList)
      // const { data: res } = data.dialogTitle === 'add'
      // : await updateRoleMenu({ ...data.form, detailList: [...data.form.detailList, ...data.removeDetailList] }) // Merge the deleted list and the original list
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
  // remove detail
  removeItem: (index: number, item: DeliveryManagementDetailListVO) => {
    hookComponent.$dialog({
      content: i18n.global.t('system.tips.beforeDeleteDetailMessage'),
      handleConfirm: async () => {
        if (item.id > 0) {
          item.id = -item.id
          data.removeDetailList.push(item) // Cache remove row
        }
        data.form.detailList.splice(index, 1) // Remove row in detailList
      }
    })
  }
})

watch(
  () => isShow.value,
  (val) => {
    if (val) {
      method.getDialogType()
      method.getCombobox()
      data.form = props.form
    }
  }
)
</script>

<style scoped lang="less">
// .v-form {
//   div {
//     margin-bottom: 7px;
//   }
// }
.detailBtnContainer {
  height: 56px;
  display: flex;
  justify-content: center;
  align-items: center;
}
// .v-col {
//   padding: 0 !important;
// }
</style>
