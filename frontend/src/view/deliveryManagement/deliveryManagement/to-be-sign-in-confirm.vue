<template>
  <v-dialog v-model="isShow" :width="'30%'" transition="dialog-top-transition" :persistent="true">
    <template #default>
      <v-card class="formCard">
        <v-toolbar color="white" :title="`${$t('wms.deliveryManagement.signIn')}`"></v-toolbar>
        <v-card-text>
          <v-form ref="formRef">
            <v-text-field v-model="data.form.qty" :label="$t('wms.deliveryManagement.signInQty')" variant="outlined" clearable></v-text-field>
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
import { reactive, computed, defineEmits, watch } from 'vue'
import { hookComponent } from '@/components/system/index'
import i18n from '@/languages/i18n'

const emit = defineEmits(['close', 'submit'])

const props = defineProps<{
  showDialog: boolean
  dialogDefaultQty: number
}>()

const isShow = computed(() => props.showDialog)

const data = reactive({
  form: { qty: 0 }
})

const method = reactive({
  closeDialog: () => {
    emit('close')
  },
  submit: () => {
    if (Number(data.form.qty) >= 0) {
      hookComponent.$dialog({
        content: `${ i18n.global.t('wms.deliveryManagement.irreversible') }, ${ i18n.global.t('wms.deliveryManagement.confirmSignIn') }?`,
        handleConfirm: async () => {
          emit('submit', data.form.qty)
        }
      })
    } else {
      hookComponent.$message({
        type: 'error',
        content: `${ i18n.global.t('wms.deliveryManagement.validQtyMsgSignIn') }!`
      })
    }
  }
})

watch(
  () => isShow.value,
  (val) => {
    if (val) {
      data.form.qty = props.dialogDefaultQty
    }
  }
)
</script>

<style scoped lang="less"></style>
