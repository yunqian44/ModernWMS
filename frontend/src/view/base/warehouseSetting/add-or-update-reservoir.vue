<!-- Freight Setting Operate Dialog -->
<template>
  <v-dialog v-model="isShow" :width="'30%'" transition="dialog-top-transition" :persistent="true">
    <template #default>
      <v-card>
        <v-toolbar color="white" :title="`${$t('base.warehouseSetting.reservoirSetting')}`"></v-toolbar>
        <v-card-text>
          <v-form ref="formRef">
            <v-text-field
              v-model="data.form.warehouse_name"
              :label="$t('base.warehouseSetting.warehouse_name')"
              :rules="data.rules.warehouse_name"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.area_name"
              :label="$t('base.warehouseSetting.area_name')"
              :rules="data.rules.area_name"
              variant="outlined"
            ></v-text-field>
            <v-select
              v-model="data.form.area_property"
              :items="data.combobox.area_property"
              item-title="label"
              item-value="value"
              :rules="data.rules.area_property"
              :label="$t('base.warehouseSetting.area_property')"
              variant="outlined"
              clearable
            ></v-select>
            <v-switch
              v-model="data.form.is_valid"
              color="primary"
              :label="$t('base.warehouseSetting.is_valid')"
              :rules="data.rules.is_valid"
            ></v-switch>
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
import { reactive, computed, ref, watch } from 'vue'
import i18n from '@/languages/i18n'
import { hookComponent } from '@/components/system/index'
import { addWarehouseArea, updateWarehouseArea } from '@/api/base/warehouseSetting'
import { WarehouseAreaVO } from '@/types/Base/warehouse'

const formRef = ref()
const emit = defineEmits(['close', 'saveSuccess'])

const props = defineProps<{
  showDialog: boolean
  form: WarehouseAreaVO
}>()

const isShow = computed(() => props.showDialog)

const dialogTitle = computed(() => {
  if (props.form.id && props.form.id > 0) {
    return 'update'
  }
  return 'add'
})

const data = reactive({
  form: ref<WarehouseAreaVO>({
    id: 0,
    warehouse_id: 0,
    parent_id: 0,
    warehouse_name: '',
    area_name: '',
    area_property: 0,
    is_valid: true
  }),
  rules: {
    warehouse_name: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.warehouseSetting.warehouse_name') }!`],
    area_name: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.warehouseSetting.area_name') }!`],
    area_property: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.warehouseSetting.area_property') }!`],
    is_valid: []
  },
  combobox: ref<{
    area_property: {
      label: string
      value: number
    }[]
  }>({
    area_property: [],
  })
})

const method = reactive({
  closeDialog: () => {
    emit('close')
  },
  initForm: () => {
    data.form = props.form
  },
  submit: async () => {
    const { valid } = await formRef.value.validate()
    if (valid) {
      const { data: res } = dialogTitle.value === 'add' ? await addWarehouseArea(data.form) : await updateWarehouseArea(data.form)
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
