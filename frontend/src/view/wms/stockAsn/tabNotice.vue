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
              v-model="data.searchForm.warehouse"
              clearable
              hide-details
              density="comfortable"
              class="searchInput ml-5 mt-1"
              :label="$t('base.warehouseSetting.warehouse')"
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
    <vxe-table ref="xTableStockLocation" :column-config="{ minWidth: '100px' }" :data="data.tableData" :height="tableHeight" align="center">
      <vxe-column type="seq" width="60"></vxe-column>
      <vxe-column field="warehouse" :title="$t('wms.stockLocation.warehouse')"></vxe-column>
      <vxe-column field="location_name" :title="$t('wms.stockLocation.location_name')"></vxe-column>
      <vxe-column field="spu_code" :title="$t('wms.stockLocation.spu_code')"></vxe-column>
      <vxe-column field="spu_name" :title="$t('wms.stockLocation.spu_name')"></vxe-column>
      <vxe-column field="sku_code" :title="$t('wms.stockLocation.sku_code')"></vxe-column>
      <vxe-column field="sku_name" :title="$t('wms.stockLocation.sku_name')"></vxe-column>
      <vxe-column field="qty" :title="$t('wms.stockLocation.qty')"></vxe-column>
      <vxe-column field="qty_available" :title="$t('wms.stockLocation.qty_available')"></vxe-column>
      <vxe-column field="qty_locked" :title="$t('wms.stockLocation.qty_locked')"></vxe-column>
      <vxe-column field="qty_frozen" :title="$t('wms.stockLocation.qty_frozen')"></vxe-column>
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
import { StockAsnVO } from '@/types/WMS/StockAsn'
import { PAGE_SIZE, PAGE_LAYOUT } from '@/constant/vxeTable'
import { hookComponent } from '@/components/system'
import { getStockAsnList } from '@/api/wms/stockAsn'
import tooltipBtn from '@/components/tooltip-btn.vue'
import i18n from '@/languages/i18n'

const xTableStockLocation = ref()

const data = reactive({
  showDialog: false,
  searchForm: {
    warehouse: ''
  },
  activeTab: null,
  tableData: ref<StockAsnVO[]>([]),
  tablePage: reactive({
    total: 0,
    sqlTitle: 'asn_status:0',
    pageIndex: 1,
    pageSize: 10
  })
})

const method = reactive({
  // Refresh data
  refresh: () => {
    method.getStockAsnList()
  },
  getStockAsnList: async () => {
    const { data: res } = await getStockAsnList(data.tablePage)
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

    method.getStockAsnList()
  }),
  exportTable: () => {
    const $table = xTableStockLocation.value
    try {
      $table.exportData({
        type: 'csv',
        filename: i18n.global.t('wms.stockManagement.stockLocation'),
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
  getStockAsnList: method.getStockAsnList
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
