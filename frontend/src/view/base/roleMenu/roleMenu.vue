<template>
  <div class="container">
    <div>
      <!-- Main Content -->
      <v-card class="mt-5">
        <v-card-text>
          <div class="operateArea">
            <v-row no-gutters>
              <!-- Operate Btn -->
              <v-col cols="12" sm="3" class="col">
                <tooltip-btn icon="mdi-plus" :tooltip-text="$t('system.page.add')" @click="method.add()"></tooltip-btn>
                <tooltip-btn icon="mdi-pencil-outline" :tooltip-text="$t('system.page.edit')" @click="method.editForm()"></tooltip-btn>
                <tooltip-btn icon="mdi-delete-outline" :tooltip-text="$t('system.page.delete')" @click="method.deleteForm()"></tooltip-btn>
                <tooltip-btn icon="mdi-refresh" :tooltip-text="$t('system.page.refresh')" @click="method.refresh()"></tooltip-btn>
                <!-- <tooltip-btn icon="mdi-export-variant" :tooltip-text="$t('system.page.export')" @click="method.exportTable"></tooltip-btn> -->
              </v-col>

              <!-- Search Input -->
              <v-col cols="12" sm="9">
                <!-- <v-row no-gutters @keyup.enter="method.sureSearch">
                  <v-col cols="12" sm="4">
                    <v-text-field
                      v-model="data.searchForm.userName"
                      clearable
                      hide-details
                      density="comfortable"
                      class="searchInput ml-5 mt-1"
                      :label="$t('login.userName')"
                      variant="solo"
                    >
                    </v-text-field>
                  </v-col>
                  <v-col cols="12" sm="4">
                    <v-text-field
                      v-model="data.searchForm.userName1"
                      clearable
                      hide-details
                      density="comfortable"
                      class="searchInput ml-5 mt-1"
                      :label="$t('login.userName')"
                      variant="solo"
                    >
                    </v-text-field>
                  </v-col>
                  <v-col cols="12" sm="4">
                    <v-text-field
                      v-model="data.searchForm.userName2"
                      clearable
                      hide-details
                      density="comfortable"
                      class="searchInput ml-5 mt-1"
                      :label="$t('login.userName')"
                      variant="solo"
                    >
                    </v-text-field>
                  </v-col>
                </v-row> -->
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
            <v-row :no-gutters="true">
              <v-col :cols="3" class="dataListCol">
                <v-card>
                  <vxe-table
                    ref="xTable"
                    :data="data.roleList"
                    :height="tableHeight"
                    :show-header="false"
                    align="center"
                    :round="true"
                    :size="'small'"
                    :cell-style="method.roleMenuListCellStyle"
                    @cell-click="method.roleMenuListCellClick"
                  >
                    <vxe-column field="role_name" :title="$t('base.roleMenu.role_name')"> </vxe-column>
                  </vxe-table>
                </v-card>
              </v-col>
              <v-col :cols="9">
                <vxe-table ref="xTable" :data="data.dialogForm.detailList" :height="tableHeight" align="center">
                  <vxe-column type="seq" width="60"></vxe-column>
                  <vxe-column field="menu_name" :title="$t('base.roleMenu.menu_name')">
                    <template #default="{ row }">
                      <span>{{ $t(`router.sideBar.${row.menu_name}`) }}</span>
                    </template>
                  </vxe-column>
                </vxe-table>
              </v-col>
            </v-row>
          </div>
        </v-card-text>
      </v-card>
    </div>
    <!-- Add or modify data mode window -->
    <addOrUpdateDialog :show-dialog="data.showDialog" :form="data.dialogForm" @close="method.closeDialog" @saveSuccess="method.saveSuccess" />
  </div>
</template>

<script lang="ts" setup>
import { computed, reactive, onMounted, ref } from 'vue'
import { computedCardHeight, computedTableHeight } from '@/constant/style'
import tooltipBtn from '@/components/tooltip-btn.vue'
import { DataProps, xTableProperty } from '@/types/Base/RoleMenu'
import { getRoleMenuAll, getRoleMenuById, deleteRoleMenu } from '@/api/base/roleMenu'
import { hookComponent } from '@/components/system'
import addOrUpdateDialog from './add-or-update-role-menu.vue'
import i18n from '@/languages/i18n'

const xTable = ref()

const data: DataProps = reactive({
  // Activation id
  activeRoleMenuId: 0,
  // searchForm: {
  //   userName: '',
  //   userName1: '',
  //   userName2: ''
  // },
  roleList: [],
  // Dialog info
  showDialog: false,
  dialogForm: {
    userrole_id: 0,
    role_name: '',
    detailList: []
  }
})

const method = reactive({
  sureSearch: () => {
    // console.log(data.searchForm)
  },
  // Find Data by Pagination
  getCompanyList: async () => {
    // Clear detailed data before refreshing
    method.clearDialogForm()
    data.activeRoleMenuId = 0
    const { data: res } = await getRoleMenuAll()
    if (!res.isSuccess) {
      hookComponent.$message({
        type: 'error',
        content: res.errorMessage
      })
      return
    }
    data.roleList = res.data
  },
  // Add user
  add: () => {
    method.clearDialogForm()
    data.showDialog = true
  },
  // Shut add or update dialog
  closeDialog: () => {
    data.showDialog = false
  },
  // after Add or update success.
  saveSuccess: () => {
    method.refresh()
    method.closeDialog()
  },
  // Refresh data
  refresh: () => {
    method.getCompanyList()
  },
  editForm() {
    if (data.dialogForm.userrole_id <= 0) {
      hookComponent.$message({
        type: 'error',
        content: i18n.global.t('base.roleMenu.beforeUpdateOrDel')
      })
      return
    }
    data.showDialog = true
  },
  deleteForm() {
    if (data.dialogForm.userrole_id <= 0) {
      hookComponent.$message({
        type: 'error',
        content: i18n.global.t('base.roleMenu.beforeUpdateOrDel')
      })
      return
    }
    const row = data.dialogForm
    hookComponent.$dialog({
      content: i18n.global.t('system.tips.beforeDeleteMessage'),
      handleConfirm: async () => {
        if (row.userrole_id) {
          const { data: res } = await deleteRoleMenu(row.userrole_id)
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
  // Export table
  exportTable: () => {
    const $table = xTable.value
    try {
      $table.exportData({
        type: 'csv',
        columnFilterMethod({ column }: any) {
          return !['checkbox'].includes(column?.type)
        }
      })
    } catch (error) {
      hookComponent.$message({
        type: 'error',
        content: `${ i18n.global.t('system.page.export') }${ i18n.global.t('system.tips.fail') }`
      })
    }
  },
  // Table dynamic style, mainly used to display the currently active row
  roleMenuListCellStyle: ({ row }: xTableProperty) => {
    const styles = {
      cursor: 'pointer',
      backgroundColor: '',
      color: ''
    }
    if (data.activeRoleMenuId === row.userrole_id) {
      styles.backgroundColor = '#9256fd'
      styles.color = '#ffffff'
    }
    return styles
  },
  // Click role name filter menus
  roleMenuListCellClick: ({ row }: xTableProperty) => {
    data.activeRoleMenuId = row.userrole_id
    method.getRoleMenus()
  },
  // Get detailed data
  getRoleMenus: async () => {
    const { data: res } = await getRoleMenuById(data.activeRoleMenuId)
    if (!res.isSuccess) {
      hookComponent.$message({
        type: 'error',
        content: res.errorMessage
      })
      return
    }
    data.dialogForm = res.data
  },
  // Refresh dialog data
  clearDialogForm: () => {
    data.dialogForm = {
      userrole_id: 0,
      role_name: '',
      detailList: []
    }
  }
})

onMounted(async () => {
  await method.getCompanyList()
})

const cardHeight = computed(() => computedCardHeight({ hasTab: false }))

const tableHeight = computed(() => computedTableHeight({ hasTab: false, hasPager: false }))
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
// Adjust the spacing between lists and tables
.dataListCol {
  box-sizing: border-box;
  padding-right: 10px !important;
}
.roleNameCol {
  width: 100%;
  height: 100%;
  cursor: pointer;
  // &:hover: {
  //   background-color: #9156fd;
  // }
}
.roleNameCol:hover {
  background-color: #9156fd;
  color: white;
}
.activeRow {
  background-color: #9156fd;
}
</style>
