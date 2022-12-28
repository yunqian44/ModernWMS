<template>
  <v-dialog v-model="isShow" width="70%" transition="dialog-top-transition" :persistent="true">
    <template #default>
      <v-card>
        <v-toolbar color="white" :title="`${$t('router.sideBar.warehouseProcessing')}（${jobTypeComp}）`"></v-toolbar>
        <v-card-text>
          <v-row>
            <v-col cols="6">
              <div class="dataTable">
                <div class="toolbar">
                  <div class="toolbarTitle">
                    <p style="color: #999">{{ $t('wms.warehouseWorking.warehouseProcessing.source') }}</p>
                  </div>
                  <tooltip-btn
                    icon="mdi-plus"
                    :tooltip-text="$t('system.page.add')"
                    size="x-small"
                    @click="method.openSelect('source')"
                  ></tooltip-btn>
                  <!-- <tooltip-btn
                    icon="mdi-export-variant"
                    :tooltip-text="$t('system.page.export')"
                    size="x-small"
                    @click="method.exportTable('source')"
                  ></tooltip-btn> -->
                </div>
                <vxe-table
                  ref="xTableSource"
                  :column-config="{ minWidth: '100px' }"
                  :data="data.form.source_detail_list"
                  :height="SYSTEM_HEIGHT.SELECT_TABLE"
                  align="center"
                >
                  <vxe-column type="seq" width="60"></vxe-column>
                  <vxe-column field="spu_code" :title="$t('wms.warehouseWorking.warehouseProcessing.spu_code')"></vxe-column>
                  <vxe-column field="spu_name" :title="$t('wms.warehouseWorking.warehouseProcessing.spu_name')"></vxe-column>
                  <vxe-column field="qty" :title="$t('wms.warehouseWorking.warehouseProcessing.qty')"></vxe-column>
                  <vxe-column field="sku_code" :title="$t('wms.warehouseWorking.warehouseProcessing.sku_code')"></vxe-column>
                  <vxe-column field="unit" :title="$t('wms.warehouseWorking.warehouseProcessing.unit')"></vxe-column>
                  <!-- <vxe-column field="is_source" :title="$t('wms.warehouseWorking.warehouseProcessing.is_source')"></vxe-column> -->
                  <vxe-column field="is_update_stock" width="150" :title="$t('wms.warehouseWorking.warehouseProcessing.is_update_stock')">
                    <template #default="{ row, column }">
                      <span>{{ formatIsValid(row[column.property]) }}</span>
                    </template>
                  </vxe-column>
                </vxe-table>
              </div>
            </v-col>
            <v-col cols="6">
              <div class="dataTable">
                <div class="toolbar">
                  <div class="toolbarTitle">
                    <p style="color: #999">{{ $t('wms.warehouseWorking.warehouseProcessing.target') }}</p>
                  </div>
                  <tooltip-btn
                    icon="mdi-plus"
                    :tooltip-text="$t('system.page.add')"
                    size="x-small"
                    @click="method.openSelect('target')"
                  ></tooltip-btn>
                  <!-- <tooltip-btn
                    icon="mdi-export-variant"
                    :tooltip-text="$t('system.page.export')"
                    size="x-small"
                    @click="method.exportTable('target')"
                  ></tooltip-btn> -->
                </div>
                <vxe-table
                  ref="xTableTarget"
                  :column-config="{ minWidth: '100px' }"
                  :data="data.form.target_detail_list"
                  :height="SYSTEM_HEIGHT.SELECT_TABLE"
                  align="center"
                >
                  <vxe-column type="seq" width="60"></vxe-column>
                  <vxe-column field="spu_code" :title="$t('wms.warehouseWorking.warehouseProcessing.spu_code')"></vxe-column>
                  <vxe-column field="spu_name" :title="$t('wms.warehouseWorking.warehouseProcessing.spu_name')"></vxe-column>
                  <vxe-column field="qty" :title="$t('wms.warehouseWorking.warehouseProcessing.qty')"></vxe-column>
                  <vxe-column field="sku_code" :title="$t('wms.warehouseWorking.warehouseProcessing.sku_code')"></vxe-column>
                  <vxe-column field="unit" :title="$t('wms.warehouseWorking.warehouseProcessing.unit')"></vxe-column>
                  <!-- <vxe-column field="is_source" :title="$t('wms.warehouseWorking.warehouseProcessing.is_source')"></vxe-column> -->
                  <vxe-column field="is_update_stock" width="150" :title="$t('wms.warehouseWorking.warehouseProcessing.is_update_stock')">
                    <template #default="{ row, column }">
                      <span>{{ formatIsValid(row[column.property]) }}</span>
                    </template>
                  </vxe-column>
                </vxe-table>
              </div>
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
  <commodity-select :show-dialog="data.showDialogSelect" @close="method.closeDialogSelect" @sureSelect="method.sureSelect" />
</template>

<script lang="ts" setup>
import { reactive, computed, ref, watch } from 'vue'
import { WarehouseProcessingVO, JobType, ProcessStatus } from '@/types/WarehouseWorking/WarehouseProcessing'
import i18n from '@/languages/i18n'
import { hookComponent } from '@/components/system/index'
import { addStockProcess, updateStockProcess } from '@/api/wms/warehouseProcessing'
import { SYSTEM_HEIGHT } from '@/constant/style'
import { formatIsValid } from '@/utils/format/formatSystem'
import commoditySelect from '@/components/select/commodity-select.vue'
import tooltipBtn from '@/components/tooltip-btn.vue'

const formRef = ref()
const emit = defineEmits(['close', 'saveSuccess'])
const xTableSource = ref()
const xTableTarget = ref()

const props = defineProps<{
  showDialog: boolean
  form: WarehouseProcessingVO
  processType: number
}>()

const isShow = computed(() => props.showDialog)

const dialogTitle = computed(() => {
  if (props.form.id && props.form.id > 0) {
    return 'update'
  }
  return 'add'
})

const data = reactive({
  form: ref<WarehouseProcessingVO>({
    id: 0,
    job_code: '',
    job_type: JobType.COMBINE,
    process_status: ProcessStatus.UNFINISH,
    processor: '',
    process_time: '',
    source_detail_list: [],
    target_detail_list: []
  }),
  tableData: [],
  // 'source' | 'target'
  curSelectType: '',
  showDialogSelect: false,
  rules: {
    // company_name: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.companySetting.company_name') }!`],
  }
})

const method = reactive({
  initForm: () => {
    data.form = props.form
    data.form.job_type = props.processType
  },

  closeDialog: () => {
    emit('close')
  },

  openSelect: (type: string) => {
    data.curSelectType = type
    data.showDialogSelect = true
  },

  closeDialogSelect: () => {
    data.showDialogSelect = false
  },

  // TODO 改为商品的 Model
  sureSelect: (selectRecords: any) => {
    console.log(selectRecords)
    if (data.curSelectType === 'source') {
      method.insertSourceData(selectRecords)
    } else if (data.curSelectType === 'target') {
      method.insertTargetData(selectRecords)
    }
  },

  insertSourceData: (selectRecords: any) => {
    // TODO 改为商品的 Model
    // TODO 判断重复id（写一个公共方法）
    // Combine: That can select more source commodity
    if (data.form.job_type === JobType.COMBINE) {
      selectRecords.forEach((record: any) => {
        xTableSource.value.insertAt(record, -1)
      })
    } else if (data.form.job_type === JobType.SPLIT) {
      // Split: That just can select one source commodity
      xTableSource.value.insertAt(selectRecords[0], -1)
    }
  },

  insertTargetData: (selectRecords: any) => {
    // Combine: That just can select one target commodity
    if (data.form.job_type === JobType.COMBINE) {
      xTableTarget.value.insertAt(selectRecords[0], -1)
    } else if (data.form.job_type === JobType.SPLIT) {
      // Split: That can select more target commodity
      selectRecords.forEach((record: any) => {
        xTableTarget.value.insertAt(record, -1)
      })
    }
  },

  // Export table
  exportTable: (type: string) => {
    const $table = type === 'source' ? xTableSource.value : xTableTarget.value
    try {
      $table.exportData({
        type: 'csv',
        filename: i18n.global.t('router.sideBar.commodityManagement'),
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

  submit: async () => {
    const { valid } = await formRef.value.validate()
    if (valid) {
      // TODO 改为只能查看不能编辑
      const { data: res } = dialogTitle.value === 'add' ? await addStockProcess(data.form) : await updateStockProcess(data.form)
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
  }
})

const jobTypeComp = computed(() => (data.form.job_type === JobType.COMBINE
    ? i18n.global.t('wms.warehouseWorking.warehouseProcessing.process_combine')
    : i18n.global.t('wms.warehouseWorking.warehouseProcessing.process_split')))

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
.mainForm {
  background-color: #f9f9f9;
  border-radius: 5px;
  padding: 20px;
  box-sizing: border-box;
  overflow: auto;
}

.toolbar {
  height: 40px;
  display: flex;
  justify-content: space-between;
}

.toolbarTitle {
  display: flex;
  // justify-content: center;
  // align-items: center;
}
</style>
