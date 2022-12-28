<!-- Warehouse Processing -->
<template>
  <div class="container">
    <div>
      <!-- Main Content -->
      <v-card class="mt-5">
        <v-card-text>
          <v-window v-model="data.activeTab">
            <v-window-item>
              <div class="operateArea">
                <v-row no-gutters>
                  <!-- Operate Btn -->
                  <v-col cols="3" class="col">
                    <tooltip-btn
                      icon="mdi-arrow-split-vertical "
                      :tooltip-text="$t('wms.warehouseWorking.warehouseProcessing.process_split')"
                      @click="method.add(JobType.SPLIT)"
                    ></tooltip-btn>
                    <tooltip-btn
                      icon="mdi-group"
                      :tooltip-text="$t('wms.warehouseWorking.warehouseProcessing.process_combine')"
                      @click="method.add(JobType.COMBINE)"
                    ></tooltip-btn>
                    <tooltip-btn icon="mdi-refresh" :tooltip-text="$t('system.page.refresh')" @click="method.refresh"></tooltip-btn>
                    <tooltip-btn icon="mdi-export-variant" :tooltip-text="$t('system.page.export')" @click="method.exportTable"> </tooltip-btn>
                  </v-col>

                  <!-- Search Input -->
                  <v-col cols="9">
                    <v-row no-gutters @keyup.enter="method.sureSearch">
                      <v-col cols="4"></v-col>
                      <v-col cols="4"> </v-col>
                      <v-col cols="4">
                        <!-- <v-text-field
                          v-model="data.searchForm.carrier"
                          clearable
                          hide-details
                          density="comfortable"
                          class="searchInput ml-5 mt-1"
                          :label="$t('wms.warehouseWorking.warehouseProcessing.carrier')"
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
                  <vxe-column type="checkbox" width="50"></vxe-column>
                  <vxe-column field="job_code" :title="$t('wms.warehouseWorking.warehouseProcessing.job_code')"></vxe-column>
                  <vxe-column field="job_type" :title="$t('wms.warehouseWorking.warehouseProcessing.job_type')"></vxe-column>
                  <vxe-column field="adjust_status" :title="$t('wms.warehouseWorking.warehouseProcessing.adjust_status')">
                    <template #default="{ row, column }">
                      <span>{{ formatIsValid(row[column.property]) }}</span>
                    </template>
                  </vxe-column>
                  <vxe-column field="processor" :title="$t('wms.warehouseWorking.warehouseProcessing.processor')"></vxe-column>
                  <vxe-column
                    field="process_time"
                    width="170px"
                    :title="$t('wms.warehouseWorking.warehouseProcessing.process_time')"
                    :formatter="['formatDate']"
                  ></vxe-column>
                  <vxe-column field="creator" :title="$t('wms.warehouseWorking.warehouseProcessing.creator')"></vxe-column>
                  <vxe-column
                    field="create_time"
                    width="170px"
                    :title="$t('wms.warehouseWorking.warehouseProcessing.create_time')"
                    :formatter="['formatDate']"
                  ></vxe-column>
                  <vxe-column field="operate" :title="$t('system.page.operate')" width="250" :resizable="false" show-overflow>
                    <template #default="{ row }">
                      <tooltip-btn
                        :flat="true"
                        icon="mdi-eye-outline"
                        :tooltip-text="$t('system.page.view')"
                        @click="method.viewRow(row)"
                      ></tooltip-btn>
                      <tooltip-btn
                        :flat="true"
                        icon="mdi-book-check-outline"
                        :tooltip-text="$t('wms.warehouseWorking.warehouseProcessing.confirmProcess')"
                        :disabled="method.confirmProcessBtnDisabled(row)"
                        @click="method.confirmProcess(row)"
                      ></tooltip-btn>
                      <tooltip-btn
                        :flat="true"
                        icon="mdi-book-open-outline"
                        :tooltip-text="$t('wms.warehouseWorking.warehouseProcessing.confirmAdjust')"
                        :disabled="method.confirmAdjustBtnDisabled(row)"
                        @click="method.confirmAdjust(row)"
                      ></tooltip-btn>
                      <tooltip-btn
                        :flat="true"
                        icon="mdi-delete-outline"
                        :tooltip-text="$t('system.page.delete')"
                        :icon-color="errorColor"
                        @click="method.deleteRow(row)"
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
            </v-window-item>
          </v-window>
        </v-card-text>
      </v-card>
      <addOrUpdateDialog :show-dialog="data.showDialog" :form="data.dialogForm" :process-type="data.processType" @close="method.closeDialog" />
    </div>
  </div>
</template>

<script lang="ts" setup>
import { computed, ref, reactive, onMounted, watch, nextTick } from 'vue'
import { VxePagerEvents } from 'vxe-table'
import { computedCardHeight, computedTableHeight, errorColor } from '@/constant/style'
import { WarehouseProcessingVO, WarehouseProcessingDetailVO, ProcessStatus, JobType } from '@/types/warehouseWorking/WarehouseProcessing'
import { PAGE_SIZE, PAGE_LAYOUT } from '@/constant/vxeTable'
import { hookComponent } from '@/components/system'
import { deleteStockProcess, getStockProcessList, getStockProcessOne, confirmAdjustment, confirmProcess } from '@/api/wms/warehouseProcessing'
import { DEBOUNCE_TIME } from '@/constant/system'
import { setSearchObject } from '@/utils/common'
import { SearchObject } from '@/types/System/Form'
import { formatIsValid } from '@/utils/format/formatSystem'
import tooltipBtn from '@/components/tooltip-btn.vue'
import addOrUpdateDialog from './add-or-update-process.vue'
import i18n from '@/languages/i18n'

const xTable = ref()

const data = reactive({
  showDialog: false,
  dialogForm: {
    id: 0,
    job_code: '',
    job_type: JobType.COMBINE,
    process_status: ProcessStatus.UNFINISH,
    processor: '',
    process_time: '',
    source_detail_list: ref<any[]>([]),
    target_detail_list: ref<any[]>([]),
    creator: '',
    create_time: '',
    adjust_status: false
  },
  processType: JobType.COMBINE,
  searchForm: {},
  activeTab: null,
  tableData: ref<WarehouseProcessingVO[]>([]),
  tablePage: reactive({
    total: 0,
    pageIndex: 1,
    pageSize: 10,
    searchObjects: ref<Array<SearchObject>>([])
  }),
  timer: ref<any>(null)
})

const method = reactive({
  // Open a dialog to add
  add: (jobType: JobType) => {
    data.processType = jobType
    data.dialogForm = {
      id: 0,
      job_code: '',
      job_type: jobType,
      process_status: ProcessStatus.UNFINISH,
      processor: '',
      process_time: '',
      source_detail_list: [],
      target_detail_list: [],
      creator: '',
      create_time: '',
      adjust_status: false
    }
    nextTick(() => {
      data.showDialog = true
    })
  },
  // Shut add or update dialog
  closeDialog: () => {
    data.showDialog = false
  },
  // After add or update success.
  saveSuccess: () => {
    method.refresh()
    method.closeDialog()
  },
  // Refresh data
  refresh: () => {
    method.getStockProcessList()
  },
  getStockProcessList: async () => {
    const { data: res } = await getStockProcessList(data.tablePage)
    if (!res.isSuccess) {
      hookComponent.$message({
        type: 'error',
        content: res.errorMessage
      })
      return
    }
    // data.tableData = res.data.rows
    // TODO 把mock数据去掉
    data.tableData = [
      {
        id: 1,
        job_code: 'code',
        job_type: JobType.SPLIT,
        process_status: ProcessStatus.UNFINISH,
        processor: '张三',
        process_time: '2022-02-02',
        creator: '李四',
        create_time: '2022-02-03',
        adjust_status: false
      }
    ]
    data.tablePage.total = res.data.totals
  },

  viewRow: async (row: WarehouseProcessingVO) => {
    try {
      // data.dialogForm = JSON.parse(JSON.stringify(row))
      await method.getOne(row.id)
      nextTick(() => {
        data.showDialog = true
      })
    } catch (error) {
      console.error(error)
    }
  },

  getOne: async (id: number) => {
    // const { data: res } = await getStockProcessOne(id)
    // if (!res.isSuccess) {
    //   hookComponent.$message({
    //     type: 'error',
    //     content: res.errorMessage
    //   })
    //   return
    // }
    // data.dialogForm = res.data
    // TODO 把mock数据去掉
    const form = {
      id: 1,
      job_code: 'code',
      job_type: JobType.SPLIT,
      process_status: ProcessStatus.UNFINISH,
      processor: '张三',
      process_time: '2022-02-02',
      creator: '李四',
      create_time: '2022-02-03',
      adjust_status: false,
      source_detail_list: [
        {
          id: 0,
          stock_process_id: 0,
          sku_id: 0,
          goods_owner_id: 0,
          goods_location_id: 0,
          qty: 0,
          last_update_time: '2022-12-27T09:33:21.423Z',
          tenant_id: 0,
          is_source: true,
          spu_code: 'string',
          spu_name: 'string',
          sku_code: 'string',
          unit: 'string',
          is_update_stock: true
        }
      ],
      target_detail_list: [
        {
          id: 0,
          stock_process_id: 0,
          sku_id: 0,
          goods_owner_id: 0,
          goods_location_id: 0,
          qty: 0,
          last_update_time: '2022',
          tenant_id: 0,
          is_source: true,
          spu_code: 'string',
          spu_name: 'string',
          sku_code: 'string',
          unit: 'string',
          is_update_stock: true
        },
        {
          id: 0,
          stock_process_id: 0,
          sku_id: 0,
          goods_owner_id: 0,
          goods_location_id: 0,
          qty: 0,
          last_update_time: '2023',
          tenant_id: 0,
          is_source: true,
          spu_code: '11',
          spu_name: '22',
          sku_code: '33',
          unit: 'string',
          is_update_stock: true
        }
      ]
    }
    data.dialogForm = form
  },
  deleteRow(row: WarehouseProcessingVO) {
    hookComponent.$dialog({
      content: i18n.global.t('system.tips.beforeDeleteMessage'),
      handleConfirm: async () => {
        if (row.id) {
          const { data: res } = await deleteStockProcess(row.id)
          if (!res.isSuccess) {
            hookComponent.$message({
              type: 'error',
              content: res.errorMessage
            })
            return
          }
          hookComponent.$message({
            type: 'success',
            content: `${ i18n.global.t('system.page.delete') }${ i18n.global.t('system.tips.success') }`
          })
          method.refresh()
        }
      }
    })
  },
  handlePageChange: ref<VxePagerEvents.PageChange>(({ currentPage, pageSize }) => {
    data.tablePage.pageIndex = currentPage
    data.tablePage.pageSize = pageSize

    method.getStockProcessList()
  }),
  exportTable: () => {
    const $table = xTable.value
    try {
      $table.exportData({
        type: 'csv',
        filename: i18n.global.t('router.sideBar.warehouseProcessing'),
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
    data.tablePage.searchObjects = setSearchObject(data.searchForm)
    method.getStockProcessList()
  },
  // The btn will become disabled when the 'process_status' is false
  confirmProcessBtnDisabled: (row: WarehouseProcessingVO) => !!row.process_status,

  // The btn will become disabled when the 'process_status' is false
  // or the 'adjust_status' is true
  confirmAdjustBtnDisabled: (row: WarehouseProcessingVO) => !row.process_status || !!row.adjust_status,

  confirmProcess: async (row: WarehouseProcessingDetailVO) => {
    const { data: res } = await confirmProcess(row.id)
    if (!res.isSuccess) {
      hookComponent.$message({
        type: 'error',
        content: res.errorMessage
      })
      return
    }
    hookComponent.$message({
      type: 'success',
      content: `${ i18n.global.t('wms.warehouseWorking.warehouseProcessing.confirmProcess') }${ i18n.global.t('system.tips.success') }`
    })
  },

  confirmAdjust: async (row: WarehouseProcessingDetailVO) => {
    const { data: res } = await confirmAdjustment(row.id)
    if (!res.isSuccess) {
      hookComponent.$message({
        type: 'error',
        content: res.errorMessage
      })
      return
    }
    hookComponent.$message({
      type: 'success',
      content: `${ i18n.global.t('wms.warehouseWorking.warehouseProcessing.confirmAdjust') }${ i18n.global.t('system.tips.success') }`
    })
  }
})

onMounted(() => {
  method.getStockProcessList()
})

const cardHeight = computed(() => computedCardHeight({ hasTab: false }))
const tableHeight = computed(() => computedTableHeight({ hasTab: false }))

watch(
  () => data.searchForm,
  () => {
    // debounce
    if (data.timer) {
      clearTimeout(data.timer)
    }
    data.timer = setTimeout(() => {
      data.timer = null
      // 放入业务逻辑
      method.sureSearch()
    }, DEBOUNCE_TIME)
  },
  {
    deep: true
  }
)
</script>

<style scoped lang="less">
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
