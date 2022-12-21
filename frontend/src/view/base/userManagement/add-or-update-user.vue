<template>
  <v-dialog v-model="isShow" :width="'50%'" transition="dialog-top-transition">
    <template #default>
      <v-card>
        <v-toolbar color="primary" :title="`${dialogTitle} User`"></v-toolbar>
        <v-card-text>
          <!-- Hello world! -->
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn variant="text" @click="method.closeDialog">Close</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>

<script lang="ts" setup>
import { reactive, computed } from 'vue'
import { UserVO } from '@/types/Base/UserManagement'

const emit = defineEmits(['close'])

const props = defineProps<{
  showDialog: boolean
  form: UserVO
}>()

const isShow = computed(() => props.showDialog)

const dialogTitle = computed(() => {
  if (props.form.id && props.form.id > 0) {
    return 'Update'
  }
  return 'Add'
})

// const data = reactive({})

const method = reactive({
  closeDialog: () => {
    emit('close')
  }
})
</script>

<style scoped lang="less"></style>
