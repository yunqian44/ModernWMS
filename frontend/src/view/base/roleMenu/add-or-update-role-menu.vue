<template>
  <v-dialog v-model="isShow" :width="'30%'" transition="dialog-top-transition" :persistent="true">
    <template #default>
      <v-card>
        <v-toolbar color="white" :title="`${$t('router.sideBar.roleMenu')}`"></v-toolbar>
        <v-card-text>
          <v-form ref="formRef">
            <v-select
              v-model="data.form.userrole_id"
              :items="data.combobox.role_name"
              item-title="label"
              item-value="value"
              :rules="data.rules.role_name"
              :label="$t('base.roleMenu.role_name')"
              variant="outlined"
              clearable
            ></v-select>
            <v-row v-for="(item, index) of data.form.detailList" :key="index">
              <v-col :cols="10">
                <v-select
                  v-model="item.menu_id"
                  :items="data.combobox.menu_name"
                  item-title="label"
                  item-value="value"
                  :rules="data.rules.menu_name"
                  :label="$t('base.roleMenu.menu_name')"
                  variant="outlined"
                  clearable
                ></v-select>
              </v-col>
              <v-col :cols="2">
                <div class="detailBtnContainer">
                  <tooltip-btn
                    :flat="true"
                    icon="mdi-delete-outline"
                    :tooltip-text="$t('system.page.delete')"
                    :icon-color="errorColor"
                    @click="method.removeItem(index, item)"
                  ></tooltip-btn>
                </div>
              </v-col>
            </v-row>
            <v-btn
              style="font-size: 20px; margin-bottom: 15px; margin-top: 10px; float: right"
              color="primary"
              :width="40"
              @click="data.form.detailList.push({ id: 0 })"
            >
              +
            </v-btn>
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
import { remove } from '@vue/shared'
import { RoleMenuVO, RoleMenuDetailVo } from '@/types/Base/RoleMenu'
import { UserRoleVO } from '@/types/Base/UserRoleSetting'
import i18n from '@/languages/i18n'
import { errorColor } from '@/constant/style'
import { hookComponent } from '@/components/system/index'
import { addRoleMenu, updateRoleMenu, getMenus } from '@/api/base/roleMenu'
import { getUserRoleAll } from '@/api/base/userRoleSetting'
import { MenuItem } from '@/types/System/Store'
import tooltipBtn from '@/components/tooltip-btn.vue'

const formRef = ref()
const emit = defineEmits(['close', 'saveSuccess'])

const props = defineProps<{
  showDialog: boolean
  form: RoleMenuVO
}>()

const isShow = computed(() => props.showDialog)

const dialogTitle = computed(() => {
  if (props.form.userrole_id && props.form.userrole_id > 0) {
    return 'update'
  }
  return 'add'
})

const data = reactive({
  form: ref<RoleMenuVO>({
    userrole_id: 0,
    role_name: '',
    detailList: []
  }),
  removeDetailList: ref<RoleMenuDetailVo[]>([]),
  combobox: ref<{
    role_name: {
      label: string
      value: number
    }[]
    menu_name: {
      label: string
      value: number
    }[]
  }>({
    role_name: [],
    menu_name: []
  }),
  rules: {
    role_name: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.roleMenu.role_name') }!`],
    menu_name: [(val: string) => !!val || `${ i18n.global.t('system.checkText.mustInput') }${ i18n.global.t('base.roleMenu.menu_name') }!`]
  }
})

const method = reactive({
  // Get the options required by the drop-down box
  getCombobox: async () => {
    data.combobox.role_name = []
    const { data: res } = await getUserRoleAll()
    if (!res.isSuccess) {
      return
    }
    const roleNameList: UserRoleVO[] = res.data
    for (const role of roleNameList) {
      data.combobox.role_name.push({
        label: role.role_name,
        value: role.id
      })
    }
    data.combobox.menu_name = []
    const { data: menuRes } = await getMenus()
    if (!menuRes.isSuccess) {
      return
    }
    const menus: MenuItem[] = menuRes.data
    for (const menu of menus) {
      data.combobox.menu_name.push({
        label: i18n.global.t(`router.sideBar.${ menu.menu_name }`),
        value: menu.id
      })
    }
  },
  closeDialog: () => {
    emit('close')
  },
  submit: async () => {
    const { valid } = await formRef.value.validate()
    if (valid) {
      const { data: res } = dialogTitle.value === 'add'
          ? await addRoleMenu(data.form)
          // Merge the deleted list and the original list
          : await updateRoleMenu({ ...data.form, detailList: [...data.form.detailList, ...data.removeDetailList] })
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
  },
  // remove detail
  removeItem: (index: number, item: RoleMenuDetailVo) => {
    hookComponent.$dialog({
      content: i18n.global.t('system.tips.beforeDeleteDetailMessage'),
      handleConfirm: async () => {
        if (item.id > 0) {
          item.id = -item.id
          data.removeDetailList.push(item) // Cache remove row
        }
        data.form.detailList.splice(index, 1) // Remove row in detailList
      }
    })
  }
})

watch(
  () => isShow.value,
  (val) => {
    if (val) {
      method.getCombobox()
      data.form = props.form
    }
  }
)
</script>

<style scoped lang="less">
// .v-form {
//   div {
//     margin-bottom: 7px;
//   }
// }
.detailBtnContainer {
  height: 56px;
  display: flex;
  justify-content: center;
  align-items: center;
}
// .v-col {
//   padding: 0 !important;
// }
</style>
