<template>
  <v-dialog v-model="isShow" :width="'70%'" transition="dialog-top-transition" :persistent="true">
    <template #default>
      <v-card class="formCard">
        <v-toolbar color="white" :title="`${$t('wms.deliveryManagement.confirmOrder')}`"></v-toolbar>
        <v-card-text>
          <v-row>
            <v-col :cols="4">
              <vxe-table
                ref="xTable"
                keep-source
                :column-config="{ minWidth: '100px' }"
                :data="data.treeData"
                :height="'500px'"
                align="center"
                :cell-style="{
                  cursor: 'pointer'
                }"
                @cell-click="method.cellClick"
              >
                <vxe-column type="seq" width="40"></vxe-column>
                <vxe-column width="40">
                  <template #default="{ row }">
                    <div style="height: 100%; display: flex; align-items: center; justify-content: center">
                      <vxe-checkbox v-model="row.confirm"></vxe-checkbox>
                    </div>
                  </template>
                </vxe-column>
                <vxe-column field="spu_code" :title="$t('wms.deliveryManagement.spu_code')"> </vxe-column>
                <vxe-column field="sku_code" :title="$t('wms.deliveryManagement.sku_code')">
                  <template #default="{ row }">
                    <span :style="data.validList.includes(row.sku_code) ? 'color: red' : ''">{{ row.sku_code }}</span>
                  </template>
                </vxe-column>
              </vxe-table>
              <!-- <v-card :height="tableHeight"> -->
              <!-- <NavListVue
                  :list-data="data.treeData"
                  :title="data.navListOptions.title"
                  :label-key="data.navListOptions.labelKey"
                  :index-key="data.navListOptions.indexKey"
                  :index-value="data.navListOptions.indexValue"
                  @item-click="method.navListClick"
                /> -->
              <!-- </v-card> -->
            </v-col>
            <v-col :cols="8">
              <vxe-table
                ref="detailXTable"
                keep-source
                :column-config="{ minWidth: '100px' }"
                :data="data.tableData"
                :height="'500px'"
                align="center"
                :edit-config="{ trigger: 'click', mode: 'cell' }"
              >
                <vxe-column type="seq" width="60"></vxe-column>
                <vxe-column field="warehouse_name" :title="$t('wms.stock.warehouse')"> </vxe-column>
                <vxe-column field="location_name" :title="$t('wms.stock.location_name')"> </vxe-column>
                <vxe-column field="pick_qty" :title="$t('wms.deliveryManagement.detailQty')" :edit-render="{}">
                  <template #edit="{ row }">
                    <vxe-input v-model="row.pick_qty" type="text"></vxe-input>
                  </template>
                </vxe-column>
              </vxe-table>
            </v-col>
          </v-row>
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
import { reactive, computed, ref, watch } from 'vue'
import { ConfirmOrderVO, ConfirmOrderPickListVO } from '@/types/DeliveryManagement/DeliveryManagement'
import i18n from '@/languages/i18n'
import { getConfirmOrderInfoAndStock, confirmOrder } from '@/api/wms/deliveryManagement'
import { hookComponent } from '@/components/system/index'

const xTable = ref()

const emit = defineEmits(['close', 'saveSuccess'])

const props = defineProps<{
  showDialog: boolean
  confirmOrderNo: string
}>()

const isShow = computed(() => props.showDialog)

const data = reactive({
  navListOptions: {
    title: i18n.global.t('wms.deliveryManagement.orderDetail'),
    labelKey: 'sku_code',
    indexKey: 'sku_code',
    indexValue: ''
  },
  treeData: ref<ConfirmOrderVO[]>([]),
  tableData: ref<ConfirmOrderPickListVO[]>([]),
  // List of verification results for each commodity
  validList: ref<string[]>([])
})

const method = reactive({
  closeDialog: () => {
    emit('close')
  },
  beforeSubmit: (): boolean => {
    // let flag = true
    data.validList = []
    const msgList: string[] = []
    let orderCount = 0
    if (data.treeData.filter((fl) => fl.confirm).length <= 0) {
      hookComponent.$message({
        type: 'error',
        content: i18n.global.t('wms.deliveryManagement.NoItemSelected')
      })
      return false
    }
    for (const item of data.treeData) {
      if (item.confirm) {
        let count = 0
        for (const dItem of item.pick_list) {
          count += Number(dItem.pick_qty)
        }
        orderCount += count
        if (count > item.qty) {
          data.validList.push(item.sku_code)
          msgList.push(`${ item.sku_code } ${ i18n.global.t('wms.deliveryManagement.quantityOverflow') } ${ item.qty }`)
          // flag = false
        }
      }
    }
    // return flag
    if (msgList.length > 0) {
      hookComponent.$message({
        type: 'error',
        content: msgList.join(';\n')
      })
      return false
    }
    if (orderCount <= 0) {
      hookComponent.$message({
        type: 'error',
        content: i18n.global.t('wms.deliveryManagement.quantityIsZero')
      })
      return false
    }
    return true
  },
  submit: async () => {
    if (method.beforeSubmit()) {
      // console.log(data.treeData)
      const { data: res } = await confirmOrder(data.treeData)
      if (!res.isSuccess) {
        hookComponent.$message({
          type: 'error',
          content: res.errorMessage
        })
        return
      }
      hookComponent.$message({
        type: 'success',
        content: i18n.global.t('wms.deliveryManagement.confirmSuccess')
      })
      emit('saveSuccess')
    }
  },
  getInfoByNo: async () => {
    const { data: res } = await getConfirmOrderInfoAndStock(props.confirmOrderNo)
    if (!res.isSuccess) {
      return
    }
    data.treeData = res.data
    data.tableData = []
  },
  cellClick: ({ row }: { row: ConfirmOrderVO }) => {
    data.navListOptions.indexValue = row.sku_code
    data.tableData = row.pick_list
  },
  // When clicking the left tree
  navListClick: (item: ConfirmOrderVO) => {
    data.navListOptions.indexValue = item.sku_code
    data.tableData = item.pick_list
  }
})

watch(
  () => isShow.value,
  (val) => {
    if (val) {
      method.getInfoByNo()
    }
  }
)

const tableHeight = computed(() => '500px')
</script>

<style scoped lang="less">
.detailBtnContainer {
  height: 56px;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>