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
    <vxe-table ref="xTableWarehouse" :column-config="{minWidth: '100px'}" :data="data.tableData" :height="tableHeight" align="center">
      <vxe-column type="seq" width="60"></vxe-column>
      <vxe-column type="checkbox" width="50"></vxe-column>
      <vxe-column field="warehouse_name" :title="$t('base.warehouseSetting.warehouse_name')"></vxe-column>
      <vxe-column field="city" :title="$t('base.warehouseSetting.city')"></vxe-column>
      <vxe-column field="address" :title="$t('base.warehouseSetting.address')"></vxe-column>
      <vxe-column field="contact_tel" :title="$t('base.warehouseSetting.contact_tel')"></vxe-column>
      <vxe-column field="email" :title="$t('base.warehouseSetting.email')"></vxe-column>
      <vxe-column field="manager" :title="$t('base.warehouseSetting.manager')"></vxe-column>
      <vxe-column field="creator" :title="$t('base.warehouseSetting.creator')"></vxe-column>
      <vxe-column
        field="create_time"
        width="170px"
        :title="$t('base.warehouseSetting.create_time')"
        :formatter="['formatDate']"
      ></vxe-column>
      <vxe-column field="is_valid" :title="$t('base.warehouseSetting.is_valid')">
        <template #default="{ row, column }">
          <span>{{ formatIsValid(row[column.property]) }}</span>
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
import { WarehouseVO } from '@/types/Base/Warehouse'
import { PAGE_SIZE, PAGE_LAYOUT } from '@/constant/vxeTable'
import { hookComponent } from '@/components/system'
import { deleteWarehouse, getWarehouseList } from '@/api/base/warehouseSetting'
import tooltipBtn from '@/components/tooltip-btn.vue'
import i18n from '@/languages/i18n'
import { formatIsValid } from '@/utils/format/formatSystem'

const xTableWarehouse = ref()

const data = reactive({
  showDialog: false,
  dialogForm: {
    id: 0,
    warehouse_name: '',
    city: '',
    address: '',
    contact_tel: '',
    email: '',
    manager: '',
    is_valid: true
  },
  searchForm: {
    warehouse_name: ''
  },
  activeTab: null,
  tableData: ref<WarehouseVO[]>([]),
  tablePage: reactive({
    total: 0,
    pageIndex: 1,
    pageSize: 10
  })
})

const method = reactive({
  // Refresh data
  refresh: () => {
    method.getWarehouseList()
  },
  getWarehouseList: async () => {
    const { data: res } = await getWarehouseList(data.tablePage)
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

    method.getWarehouseList()
  }),
  exportTable: () => {
    const $table = xTableWarehouse.value
    try {
      $table.exportData({
        type: 'csv',
        filename: i18n.global.t('base.warehouseSetting.warehouseSetting'),
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
  getWarehouseList: method.getWarehouseList
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
