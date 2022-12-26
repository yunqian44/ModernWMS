<template>
  <v-dialog v-model="isShow" width="70%" transition="dialog-top-transition" :persistent="true">
    <template #default>
      <v-card>
        <v-toolbar color="white" :title="`${$t('router.sideBar.commodityManagement')}`"></v-toolbar>
        <v-card-text>
          <v-row>
            <v-col cols="3">
              <div class="mainForm" :style="{ height: mainFormHeight }">
                <v-form ref="formRef">
                  <v-text-field
                    v-model="data.form.spu_code"
                    :label="$t('base.commodityManagement.spu_code')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.spu_name"
                    :label="$t('base.commodityManagement.spu_name')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.category_name"
                    :label="$t('base.commodityManagement.category_name')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.spu_description"
                    :label="$t('base.commodityManagement.spu_description')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.bar_code"
                    :label="$t('base.commodityManagement.bar_code')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.sku_name"
                    :label="$t('base.commodityManagement.sku_name')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.supplier_name"
                    :label="$t('base.commodityManagement.supplier_name')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.brand"
                    :label="$t('base.commodityManagement.brand')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.unit"
                    :label="$t('base.commodityManagement.unit')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.cost"
                    :label="$t('base.commodityManagement.cost')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.price"
                    :label="$t('base.commodityManagement.price')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.origin"
                    :label="$t('base.commodityManagement.origin')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.length_unit"
                    :label="$t('base.commodityManagement.length_unit')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.volume_unit"
                    :label="$t('base.commodityManagement.volume_unit')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                  <v-text-field
                    v-model="data.form.weight_unit"
                    :label="$t('base.commodityManagement.weight_unit')"
                    variant="outlined"
                    density="compact"
                    clearable
                    class="mb-4"
                  ></v-text-field>
                </v-form>
              </div>
            </v-col>
            <v-col cols="9">
              <div class="dataTable">
                <div class="toolbar">
                  <tooltip-btn
                    icon="mdi-plus"
                    :tooltip-text="$t('system.page.insertOneRow')"
                    size="x-small"
                    @click="method.insertOneRow"
                  ></tooltip-btn>
                  <tooltip-btn
                    icon="mdi-export-variant"
                    :tooltip-text="$t('system.page.export')"
                    size="x-small"
                    @click="method.exportTable"
                  ></tooltip-btn>
                </div>
                <vxe-table
                  ref="xTable"
                  :column-config="{ minWidth: '100px' }"
                  :data="data.tableData"
                  :height="SYSTEM_HEIGHT.SELECT_TABLE"
                  align="center"
                >
                  <vxe-column type="seq" width="60"></vxe-column>
                  <vxe-column field="spu_code" :title="$t('base.commodityManagement.spu_code')"></vxe-column>
                  <vxe-column field="spu_name" :title="$t('base.commodityManagement.spu_name')"></vxe-column>
                  <vxe-column field="category_name" :title="$t('base.commodityManagement.category_name')"></vxe-column>
                  <vxe-column field="spu_description" :title="$t('base.commodityManagement.spu_description')"></vxe-column>
                  <vxe-column field="bar_code" :title="$t('base.commodityManagement.bar_code')"></vxe-column>
                  <vxe-column field="sku_name" :title="$t('base.commodityManagement.sku_name')"></vxe-column>
                  <vxe-column field="supplier_name" :title="$t('base.commodityManagement.supplier_name')"></vxe-column>
                  <vxe-column field="brand" :title="$t('base.commodityManagement.brand')"></vxe-column>
                  <vxe-column field="unit" :title="$t('base.commodityManagement.unit')"></vxe-column>
                  <vxe-column field="cost" :title="$t('base.commodityManagement.cost')"></vxe-column>
                  <vxe-column field="price" :title="$t('base.commodityManagement.price')"></vxe-column>
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
</template>

<script lang="ts" setup>
import { reactive, computed, ref, watch } from 'vue'
import { CommodityVO } from '@/types/Base/CommodityManagement'
import i18n from '@/languages/i18n'
import { hookComponent } from '@/components/system/index'
import { addSpu, updateSpu } from '@/api/base/commodityManagementSetting'
import { computedSelectTableSearchHeight, SYSTEM_HEIGHT } from '@/constant/style'
import tooltipBtn from '@/components/tooltip-btn.vue'

const formRef = ref()
const emit = defineEmits(['close', 'saveSuccess'])
const xTable = ref()

const props = defineProps<{
  showDialog: boolean
  form: CommodityVO
}>()

const isShow = computed(() => props.showDialog)

const dialogTitle = computed(() => {
  if (props.form.id && props.form.id > 0) {
    return 'update'
  }
  return 'add'
})

const data = reactive({
  form: ref<CommodityVO>({
    id: 0,
    spu_code: '',
    spu_name: '',
    category_name: '',
    spu_description: '',
    bar_code: '',
    sku_name: '',
    supplier_name: '',
    brand: '',
    unit: '',
    cost: '',
    price: '',
    origin: '',
    length_unit: 0,
    volume_unit: 0,
    weight_unit: 0
  }),
  tableData: [],
  rules: {
    company_name: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.companySetting.company_name') }!`],
    city: [],
    address: [],
    manager: [],
    contact_tel: []
  }
})

const method = reactive({
  closeDialog: () => {
    emit('close')
  },
  insertOneRow: () => {
    xTable.value.insertAt(-1)
  },
  // Export table
  exportTable: () => {
    const $table = xTable.value
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
      const { data: res } = dialogTitle.value === 'add' ? await addSpu(data.form) : await updateSpu(data.form)
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

const mainFormHeight = computed(() => computedSelectTableSearchHeight({ hasPager: false, hasToolBar: true }))

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
.mainForm {
  background-color: #f9f9f9;
  border-radius: 5px;
  padding: 20px;
  box-sizing: border-box;
  overflow: auto;
}

.toolbar {
  height: 40px;
}
</style>
