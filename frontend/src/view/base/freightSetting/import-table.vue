<!-- Freight Setting Import Dialog -->
<template>
  <v-dialog v-model="isShow" width="70%" transition="dialog-top-transition" :persistent="true">
    <template #default>
      <v-card>
        <v-toolbar color="white" :title="`${$t('system.page.import')}`"></v-toolbar>
        <v-card-text>
          <div class="mb-4">
            <tooltip-btn icon="mdi-plus" :tooltip-text="$t('system.page.chooseFile')" size="x-small" @click="method.chooseFile"></tooltip-btn>
            <tooltip-btn icon="mdi-export-variant" :tooltip-text="$t('system.page.export')" size="x-small" @click="method.exportTable"></tooltip-btn>
            <input v-show="false" id="open" ref="uploadExcel" type="file" accept=".xls, .xlsx, .csv" @change="method.readExcel" />
          </div>
          <vxe-table
            ref="xTable"
            :column-config="{ minWidth: '100px' }"
            :data="data.importData"
            :height="SYSTEM_HEIGHT.SELECT_TABLE"
            :edit-config="{ trigger: 'click', mode: 'cell' }"
            :edit-rules="data.validRules"
            :export-config="{}"
            align="center"
          >
            <vxe-column type="seq" width="60"></vxe-column>
            <vxe-column field="carrier" :title="$t('base.freightSetting.carrier')"></vxe-column>
            <vxe-column field="departure_city" :title="$t('base.freightSetting.departure_city')"></vxe-column>
            <vxe-column field="arrival_city" :title="$t('base.freightSetting.arrival_city')"></vxe-column>
            <vxe-column field="price_per_weight" :title="$t('base.freightSetting.price_per_weight')"></vxe-column>
            <vxe-column field="price_per_volume" :title="$t('base.freightSetting.price_per_volume')"></vxe-column>
            <vxe-column field="min_payment" :title="$t('base.freightSetting.min_payment')"></vxe-column>
          </vxe-table>
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
import { reactive, computed, ref } from 'vue'
import { VxeTablePropTypes } from 'vxe-table'
import * as XLSX from 'xlsx'
import i18n from '@/languages/i18n'
import { excelImport } from '@/api/base/freightSetting'
import { hookComponent } from '@/components/system/index'
import { SYSTEM_HEIGHT } from '@/constant/style'
import tooltipBtn from '@/components/tooltip-btn.vue'
import { FreightVO } from '@/types/Base/Freight'

const formRef = ref()
const emit = defineEmits(['close', 'saveSuccess'])
const uploadExcel = ref()
const xTable = ref()

const props = defineProps<{
  showDialog: boolean
}>()

const isShow = computed(() => props.showDialog)

const data = reactive({
  importData: ref<Array<FreightVO>>([]),
  path: '',
  curFile: ref<File>(),
  validRules: ref<VxeTablePropTypes.EditRules>({})
})

const method = reactive({
  closeDialog: () => {
    emit('close')
  },
  submit: async () => {
    // const { valid } = await formRef.value.validate()
    // if (valid) {
      const { data: res } = await excelImport(data.importData)
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
    // } 
    // else {
    //   hookComponent.$message({
    //     type: 'error',
    //     content: i18n.global.t('system.checkText.checkFormFail')
    //   })
    // }
  },

  chooseFile: async () => {
    uploadExcel.value.click()
  },

  readExcel: async (evnt: any) => {
    const files = evnt.target.files
    if (files.length <= 0) {
      return false
    }

    const file = files[0]
    const fileReader = new FileReader()

    fileReader.onload = (ev: ProgressEvent<FileReader>) => {
      const fileData = ev?.target?.result
      if (fileData) {
        const workbook = XLSX.read(fileData, { type: 'binary' })
        const wsname = workbook.SheetNames[0]

        let ws = XLSX.utils.sheet_to_json(workbook.Sheets[wsname])
        let str = JSON.stringify(ws)
        str = str.replace(/（/g, '(')
        str = str.replace(/）/g, ')')
        ws = JSON.parse(str)

        data.importData = []
        ws.forEach((value: any, index: number, ws: any) => {
          data.importData.push({
            carrier: ws[index][i18n.global.t('base.freightSetting.carrier')],
            departure_city: ws[index][i18n.global.t('base.freightSetting.departure_city')],
            arrival_city: ws[index][i18n.global.t('base.freightSetting.arrival_city')],
            price_per_weight: ws[index][i18n.global.t('base.freightSetting.price_per_weight')],
            price_per_volume: ws[index][i18n.global.t('base.freightSetting.price_per_volume')],
            min_payment: ws[index][i18n.global.t('base.freightSetting.min_payment')]
          })
        })
        // TODO match the base setting
      }
    }
    fileReader.readAsBinaryString(file)
  },

  exportTable: () => {
    const $table = xTable.value
    try {
      $table.exportData({
        type: 'csv',
        filename: i18n.global.t('router.sideBar.freightSetting'),
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
  }
})
</script>

<style scoped lang="less">
.v-form {
  div {
    margin-bottom: 7px;
  }
}
</style>
