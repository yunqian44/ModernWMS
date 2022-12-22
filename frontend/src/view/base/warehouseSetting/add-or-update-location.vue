<!-- Freight Setting Operate Dialog -->
<template>
  <v-dialog v-model="isShow" :width="'30%'" transition="dialog-top-transition" :persistent="true">
    <template #default>
      <v-card>
        <v-toolbar color="white" :title="`${$t('base.warehouseSetting.locationSetting')}`"></v-toolbar>
        <v-card-text>
          <v-form ref="formRef">
            <v-text-field
              v-model="data.form.warehouse_name"
              :label="$t('base.warehouseSetting.warehouse_name')"
              :rules="data.rules.warehouse_name"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.warehouse_area_name"
              :label="$t('base.warehouseSetting.area_name')"
              :rules="data.rules.warehouse_area_name"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.warehouse_area_property"
              :label="$t('base.warehouseSetting.area_property')"
              :rules="data.rules.warehouse_area_property"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.location_name"
              :label="$t('base.warehouseSetting.location_name')"
              :rules="data.rules.location_name"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.location_length"
              :label="$t('base.warehouseSetting.location_length')"
              :rules="data.rules.location_length"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.location_width"
              :label="$t('base.warehouseSetting.location_width')"
              :rules="data.rules.location_width"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.location_heigth"
              :label="$t('base.warehouseSetting.location_heigth')"
              :rules="data.rules.location_heigth"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.location_volume"
              :label="$t('base.warehouseSetting.location_volume')"
              :rules="data.rules.location_volume"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.location_load"
              :label="$t('base.warehouseSetting.location_load')"
              :rules="data.rules.location_load"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.roadway_number"
              :label="$t('base.warehouseSetting.roadway_number')"
              :rules="data.rules.roadway_number"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.shelf_number"
              :label="$t('base.warehouseSetting.shelf_number')"
              :rules="data.rules.shelf_number"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.layer_number"
              :label="$t('base.warehouseSetting.layer_number')"
              :rules="data.rules.layer_number"
              variant="outlined"
            ></v-text-field>
            <v-text-field
              v-model="data.form.tag_number"
              :label="$t('base.warehouseSetting.tag_number')"
              :rules="data.rules.tag_number"
              variant="outlined"
            ></v-text-field>
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
import { addGoodsLocation, updateGoodsLocation } from '@/api/base/warehouseSetting'
import { GoodsLocationVO } from '@/types/Base/warehouse'

const formRef = ref()
const emit = defineEmits(['close', 'saveSuccess'])

const props = defineProps<{
  showDialog: boolean
  form: GoodsLocationVO
}>()

const isShow = computed(() => props.showDialog)

const dialogTitle = computed(() => {
  if (props.form.id && props.form.id > 0) {
    return 'update'
  }
  return 'add'
})

const data = reactive({
  form: ref<GoodsLocationVO>({
    id: 0,
    warehouse_id: 0,
    warehouse_area_id: 0,
    warehouse_name: '',
    warehouse_area_name: '',
    warehouse_area_property: 0,
    location_name: '',
    location_length: 0,
    location_width: 0,
    location_heigth: 0,
    location_volume: 0,
    location_load: 0,
    roadway_number: '',
    shelf_number: '',
    layer_number: '',
    tag_number: '',
    is_valid: true
  }),
  rules: {
    warehouse_name: [
      (val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.warehouseSetting.warehouse_name') }!`
    ],
    warehouse_area_name: [
      (val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.warehouseSetting.area_name') }!`
    ],
    warehouse_area_property: [
      (val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.warehouseSetting.area_property') }!`
    ],
    location_name: [
      (val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.warehouseSetting.location_name') }!`
    ],
    location_length: [],
    location_width: [],
    location_heigth: [],
    location_volume: [],
    location_load: [],
    roadway_number: [],
    shelf_number: [],
    layer_number: [],
    tag_number: [],
    is_valid: []
  }
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
      const { data: res } = dialogTitle.value === 'add' ? await addGoodsLocation(data.form) : await updateGoodsLocation(data.form)
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
