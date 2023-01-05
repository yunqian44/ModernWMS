<template>
  <div class="operateArea">
    <v-row no-gutters>
      <!-- Operate Btn -->
      <v-col cols="3" class="col">
        <tooltip-btn icon="mdi-refresh" :tooltip-text="$t('system.page.refresh')" @click="method.refresh"></tooltip-btn>
        <tooltip-btn icon="mdi-export-variant" :tooltip-text="$t('system.page.export')" @click="method.exportTable"> </tooltip-btn>
      </v-col>

      <!-- Search Input -->
      <v-col cols="9">
        <v-row no-gutters @keyup.enter="method.sureSearch">
          <v-col cols="4"></v-col>
          <v-col cols="4"></v-col>
          <v-col cols="4">
            <!-- <v-text-field
              v-model="data.searchForm.warehouse_name"
              clearable
              hide-details
              density="comfortable"
              class="searchInput ml-5 mt-1"
              :label="$t('base.warehouseSetting.warehouse_name')"
              variant="solo"
            >
            </v-text-field> -->
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </div>

  <!-- Table -->
  <div
    class="mt-5"
    :style="{
      height: cardHeight
    }"
  >
    <vxe-table ref="xTable" :column-config="{ minWidth: '100px' }" :data="data.tableData" :height="tableHeight" align="center">
      <vxe-column type="seq" width="60"></vxe-column>
      <!-- <vxe-column type="checkbox" width="50"></vxe-column> -->
      <vxe-column field="dispatch_no" :title="$t('wms.deliveryManagement.dispatch_no')"></vxe-column>
      <vxe-column field="spu_code" :title="$t('wms.deliveryManagement.spu_code')"></vxe-column>
      <vxe-column field="spu_name" :title="$t('wms.deliveryManagement.spu_name')"></vxe-column>
      <vxe-column field="sku_code" :title="$t('wms.deliveryManagement.sku_code')"></vxe-column>
      <vxe-column field="qty" :title="$t('wms.deliveryManagement.qty')"></vxe-column>
      <vxe-column field="weight" :title="$t('wms.deliveryManagement.weight')"></vxe-column>
      <vxe-column field="volume" :title="$t('wms.deliveryManagement.volume')"></vxe-column>
      <vxe-column field="customer_name" :title="$t('wms.deliveryManagement.customer_name')"></vxe-column>
      <vxe-column field="creator" :title="$t('wms.deliveryManagement.creator')"></vxe-column>
      <vxe-column field="create_time" width="170px" :title="$t('wms.deliveryManagement.create_time')" :formatter="['formatDate']"></vxe-column>
      <vxe-column field="operate" :title="$t('system.page.operate')" width="80" :resizable="false" show-overflow>
        <template #default="{ row }">
          <tooltip-btn
            :flat="true"
            icon="mdi-pencil-outline"
            :tooltip-text="$t('wms.deliveryManagement.delivery')"
            @click="method.handleDeliver(row)"
          ></tooltip-btn>
        </template>
      </vxe-column>
    </vxe-table>
    <vxe-pager
      :current-page="data.tablePage.pageIndex"
      :page-size="data.tablePage.pageSize"
      perfect
      :total="data.tablePage.total"
      :page-sizes="PAGE_SIZE"
      :layouts="PAGE_LAYOUT"
      @page-change="method.handlePageChange"
    >
    </vxe-pager>
  </div>
</template>

<script lang="ts" setup>
import { computed, ref, reactive } from 'vue'
import { VxePagerEvents } from 'vxe-table'
import { computedCardHeight, computedTableHeight, errorColor } from '@/constant/style'
import { DeliveryManagementDetailVO } from '@/types/DeliveryManagement/DeliveryManagement'
import { PAGE_SIZE, PAGE_LAYOUT } from '@/constant/vxeTable'
import { hookComponent } from '@/components/system'
import { getToBeDelivery, handleDelivery } from '@/api/wms/deliveryManagement'
import tooltipBtn from '@/components/tooltip-btn.vue'
import i18n from '@/languages/i18n'

const xTable = ref()

const data = reactive({
  showDialog: false,
  dialogForm: {
    id: 0
  },
  searchForm: {},
  activeTab: null,
  tableData: ref<DeliveryManagementDetailVO[]>([]),
  tablePage: reactive({
    total: 0,
    pageIndex: 1,
    pageSize: 10
  })
})

const method = reactive({
  handleDeliver: async (row: DeliveryManagementDetailVO) => {
    hookComponent.$dialog({
      content: `${ i18n.global.t('wms.deliveryManagement.irreversible') }, ${ i18n.global.t('wms.deliveryManagement.confirmDelivery') }?`,
      handleConfirm: async () => {
        const { data: res } = await handleDelivery([
          {
            id: row.id,
            dispatch_no: row.dispatch_no,
            dispatch_status: row.dispatch_status,
            picked_qty: row.picked_qty
          }
        ])
        if (!res.isSuccess) {
          hookComponent.$message({
            type: 'error',
            content: res.errorMessage
          })
          return
        }
        hookComponent.$message({
          type: 'success',
          content: res.data
        })
        method.refresh()
      }
    })
  },
  // Refresh data
  refresh: () => {
    method.getToBeDelivery()
  },
  getToBeDelivery: async () => {
    const { data: res } = await getToBeDelivery(data.tablePage)
    if (!res.isSuccess) {
      hookComponent.$message({
        type: 'error',
        content: res.errorMessage
      })
      return
    }
    data.tableData = res.data.rows
    data.tablePage.total = res.data.totals
  },
  handlePageChange: ref<VxePagerEvents.PageChange>(({ currentPage, pageSize }) => {
    data.tablePage.pageIndex = currentPage
    data.tablePage.pageSize = pageSize

    method.getToBeDelivery()
  }),
  exportTable: () => {
    const $table = xTable.value
    try {
      $table.exportData({
        type: 'csv',
        filename: i18n.global.t('wms.deliveryManagement.toBeDelivered'),
        columnFilterMethod({ column }: any) {
          return !['checkbox'].includes(column?.type) && !['operate'].includes(column?.field)
        }
      })
    } catch (error) {
      hookComponent.$message({
        type: 'error',
        content: `${ i18n.global.t('system.page.export') }${ i18n.global.t('system.tips.fail') }`
      })
    }
  },
  sureSearch: () => {
    console.log(data.searchForm)
  }
})

const cardHeight = computed(() => computedCardHeight({}))
const tableHeight = computed(() => computedTableHeight({}))

defineExpose({
  getToBeDelivery: method.getToBeDelivery
})
</script>

<style lang="less" scoped>
.operateArea {
  width: 100%;
  min-width: 760px;
  display: flex;
  align-items: center;
  border-radius: 10px;
  padding: 0 10px;
}

.col {
  display: flex;
  align-items: center;
}
</style>
