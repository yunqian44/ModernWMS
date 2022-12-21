<template>
  <div class="container">
    <div>
      <!-- According to your own need to decide whether to the tab, if you don't need that you can delete <v-tabs> -->
      <v-tabs v-model="data.activeTab" stacked>
        <v-tab v-for="(item, index) of tabsConfig" :key="index" :value="item.value">
          <v-icon>{{ item.icon }}</v-icon>
          <p class="tabItemTitle">{{ item.tabName }}</p>
        </v-tab>
      </v-tabs>

      <!-- Main Content -->
      <v-card class="mt-5">
        <v-card-text>
          <v-window v-model="data.activeTab">
            <v-window-item v-for="(item, index) of tabsConfig" :key="index" :value="item.value">
              <div class="operateArea">
                <v-row no-gutters>
                  <!-- Operate Btn -->
                  <v-col cols="12" sm="3" class="col">
                    <tooltip-btn icon="mdi-plus" :tooltip-text="$t('system.page.add')"></tooltip-btn>
                    <tooltip-btn icon="mdi-refresh" :tooltip-text="$t('system.page.refresh')"></tooltip-btn>
                    <tooltip-btn
                      icon="mdi-export-variant"
                      :tooltip-text="$t('system.page.export')"
                      @click="exportTable"
                    ></tooltip-btn>
                  </v-col>

                  <!-- Search Input -->
                  <v-col cols="12" sm="9">
                    <v-row no-gutters @keyup.enter="method.sureSearch">
                      <!-- 
                        Don't delete v-col whether you don't need.
                        If you only need one query, you should write: 

                        <v-col cols="12" sm="4"></v-col>
                        <v-col cols="12" sm="4"></v-col>
                        <v-col cols="12" sm="4">Some Thing</v-col>
                       -->
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
                    </v-row>
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
                <vxe-table ref="xTable" :data="data.tableData" :height="tableHeight" align="center">
                  <vxe-column type="seq" width="60"></vxe-column>
                  <vxe-column type="checkbox" width="50"></vxe-column>
                  <vxe-column :title="$t('system.page.operate')" width="160" :resizable="false" show-overflow>
                    <template #default="{ row }">
                      <tooltip-btn
                        :flat="true"
                        icon="mdi-pencil-outline"
                        :tooltip-text="$t('system.page.edit')"
                        @click="method.editRow(row)"
                      ></tooltip-btn>
                      <tooltip-btn
                        :flat="true"
                        icon="mdi-delete-outline"
                        :tooltip-text="$t('system.page.delete')"
                        :icon-color="errorColor"
                        @click="method.deleteRow(row)"
                      ></tooltip-btn>
                    </template>
                  </vxe-column>
                </vxe-table>
                <vxe-pager
                  :current-page="data.tablePage.currentPage"
                  :page-size="data.tablePage.pageSize"
                  perfect
                  :total="data.tablePage.total"
                  :page-sizes="PAGE_SIZE"
                  :layouts="PAGE_LAYOUT"
                  @page-change="handlePageChange"
                >
                </vxe-pager>
              </div>
            </v-window-item>
          </v-window>
        </v-card-text>
      </v-card>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { computed, ref, reactive, onMounted } from 'vue'
import { VxePagerEvents, VxeButtonEvents } from 'vxe-table'
import { computedCardHeight, computedTableHeight, errorColor } from '@/constant/style'
import { PAGE_SIZE, PAGE_LAYOUT } from '@/constant/vxeTable'
import tooltipBtn from '@/components/tooltip-btn.vue'

const xTable = ref()

// Table Model, this just a example
// You should put it on the 'types' folder
interface UserVO {
  id: number
  name: string
  role: string
  sex: string
  age: number
  address: string
}

const tabsConfig = [
  {
    value: 'one',
    icon: 'mdi-phone',
    tabName: 'one'
  },
  {
    value: 'two',
    icon: 'mdi-phone',
    tabName: 'two'
  },
  {
    value: 'three',
    icon: 'mdi-phone',
    tabName: 'three'
  }
]

const data = reactive({
  // TODO Adjust the search prop what you want
  searchForm: {
    userName: '',
    userName1: '',
    userName2: ''
  },
  activeTab: null,
  tableData: ref<UserVO[]>([
    { id: 10001, name: 'Test1', role: 'Develop', sex: 'Man', age: 28, address: 'test abc' },
    { id: 10002, name: 'Test2', role: 'Test', sex: 'Women', age: 22, address: 'Guangzhou' },
    { id: 10003, name: 'Test3', role: 'PM', sex: 'Man', age: 32, address: 'Shanghai' },
    { id: 10004, name: 'Test4', role: 'Designer', sex: 'Women', age: 24, address: 'Shanghai' }
  ]),
  tablePage: reactive({
    total: 0,
    currentPage: 1,
    pageSize: 10
  })
})

const method = reactive({
  editRow(row: any) {
    console.log(row)
  },
  deleteRow(row: any) {
    console.log(row)
  },
  sureSearch: () => {
    console.log(data.searchForm)
  }
})

onMounted(() => {
  // TODO Get datas what you want
})

const handlePageChange: VxePagerEvents.PageChange = ({ currentPage, pageSize }) => {
  data.tablePage.currentPage = currentPage
  data.tablePage.pageSize = pageSize
  // TODO Get datas what you want
}

const exportTable: VxeButtonEvents.Click = () => {
  const $table = xTable.value
  try {
    $table[0].exportData({
      type: 'csv',
      columnFilterMethod({ column }: any) {
        return !['checkbox'].includes(column?.type)
      }
    })
  } catch (error) {
    console.error('导出时发生未知错误', error)
  }
}

/**
 * computedCardHeight({ hasTab, hasOperate }) 
 * Must enter the params if you don't need tab or operate area
 * Defaultly, the 'hasTab' and 'hasOperate' are true
 */
const cardHeight = computed(() => computedCardHeight({ }))

/**
 * computedTableHeight({ hasPager, hasTab, hasOperate }) 
 * Must enter the params if you don't need pager or tab or operate area
 * Defaultly, the 'hasPager' and 'hasTab' and 'hasOperate' are true
 */
const tableHeight = computed(() => computedTableHeight({ }))
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
</style>
