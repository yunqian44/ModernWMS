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
                <tooltip-btn
                  icon="mdi-refresh"
                  :tooltip-text="$t('system.page.refresh')"
                  @click="method.refresh()"
                ></tooltip-btn>
                <tooltip-btn icon="mdi-export-variant" :tooltip-text="$t('system.page.export')"></tooltip-btn>
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
            class="mt-5 tableContainer"
            :style="{
              height: tableHeight
            }"
          >
            <vxe-grid v-bind="data.gridOptions">
              <template #pager>
                <vxe-pager
                  v-model:current-page="data.tablePage.pageIndex"
                  v-model:page-size="data.tablePage.pageSize"
                  :layouts="['Sizes', 'PrevJump', 'PrevPage', 'Number', 'NextPage', 'NextJump', 'FullJump', 'Total']"
                  :total="data.tablePage.total"
                  @page-change="handlePageChange"
                >
                </vxe-pager>
              </template>
            </vxe-grid>
          </div>
        </v-card-text>
      </v-card>
    </div>
    <!-- Add or modify data mode window -->
    <addOrUpdateUserDialog :show-dialog="data.showDialog" :form="data.dialogForm" @close="method.closeDialog" />
  </div>
</template>

<script lang="ts" setup>
import { computed, reactive, onMounted } from 'vue'
import { VxePagerEvents } from 'vxe-table'
import { computedTableHeight } from '@/utils/globalStyle'
import tooltipBtn from '@/components/tooltip-btn.vue'
import { DataProps } from '@/types/Base/UserManagement'
import i18n from '@/languages/i18n'
import { getUserList } from '@/api/base/userManagement'
import { hookComponent } from '@/components/system'
import addOrUpdateUserDialog from './add-or-update-user.vue'

const data: DataProps = reactive({
  // searchForm: {
  //   userName: '',
  //   userName1: '',
  //   userName2: ''
  // },
  tablePage: {
    total: 0,
    pageIndex: 1,
    pageSize: 10
  },
  gridOptions: {
    height: 'auto',
    loading: false,
    columnConfig: {
      resizable: true
    },
    data: [],
    columns: [
      { type: 'seq', width: 60 },
      { type: 'checkbox', width: 50 },
      { field: 'user_num', title: i18n.global.t('base.userManagement.user_num') },
      { field: 'user_name', title: i18n.global.t('base.userManagement.user_name') },
      { field: 'user_role', title: i18n.global.t('base.userManagement.user_role') },
      { field: 'sex', title: i18n.global.t('base.userManagement.sex') },
      { field: 'contact_tel', title: i18n.global.t('base.userManagement.contact_tel') },
      {
        field: 'is_valid',
        title: i18n.global.t('base.userManagement.is_valid'),
        slots: {
          default: ({ row }) => [row.is_valid ? '是' : '否']
        }
      }
    ]
  },
  // Dialog info
  showDialog: false,
  dialogForm: {
    id: 0,
    user_num: '',
    user_name: '',
    contact_tel: '',
    user_role: '',
    sex: '',
    auth_string: '',
    is_valid: true
  }
})

const method = reactive({
  sureSearch: () => {
    // console.log(data.searchForm)
  },
  // Find Data by Pagination
  getUserList: async () => {
    const { data: res } = await getUserList(data.tablePage)
    if (!res.isSuccess) {
      hookComponent.$message({
        type: 'error',
        content: res.errorMessage
      })
      return
    }
    data.gridOptions.data = res.data.rows
    data.tablePage.total = res.data.totals
  },
  // Add user
  add: () => {
    data.showDialog = true
  },
  // Shut add or update dialog
  closeDialog: () => {
    data.showDialog = false
  },
  // Refresh data
  refresh: () => {
    method.getUserList()
  }
})

onMounted(async () => {
  await method.getUserList()
})

const handlePageChange: VxePagerEvents.PageChange = ({ currentPage, pageSize }) => {
  data.tablePage.pageIndex = currentPage
  data.tablePage.pageSize = pageSize
  // TODO 重新获取数据
}

const tableHeight = computed(() => computedTableHeight({ hasTab: false }))
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

.tableContainer {
  height: 500px;
}
</style>
