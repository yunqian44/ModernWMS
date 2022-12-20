<template>
  <div class="container">
    <div>
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
                    <tooltip-btn icon="mdi-export-variant" :tooltip-text="$t('system.page.export')"></tooltip-btn>
                  </v-col>

                  <!-- Search Input -->
                  <v-col cols="12" sm="9">
                    <v-row no-gutters @keyup.enter="method.sureSearch">
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
                class="mt-5 tableContainer"
                :style="{
                  height: tableHeight
                }"
              >
                <vxe-grid v-bind="data.gridOptions">
                  <template #pager>
                    <vxe-pager
                      v-model:current-page="data.tablePage.currentPage"
                      v-model:page-size="data.tablePage.pageSize"
                      :layouts="[
                        'Sizes',
                        'PrevJump',
                        'PrevPage',
                        'Number',
                        'NextPage',
                        'NextJump',
                        'FullJump',
                        'Total'
                      ]"
                      :total="data.tablePage.total"
                      @page-change="handlePageChange"
                    >
                    </vxe-pager>
                  </template>
                </vxe-grid>
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
import { VxeGridProps, VxePagerEvents } from 'vxe-table'
import { computedTableHeight } from '@/utils/globalStyle'
import tooltipBtn from '@/components/tooltip-btn.vue'

// Table Model
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
  }),
  gridOptions: reactive<VxeGridProps>({
    height: 'auto',
    loading: false,
    columnConfig: {
      resizable: true
    },
    data: [],
    columns: [
      { type: 'seq', width: 60 },
      { type: 'checkbox', width: 50 },
      { field: 'name', title: 'Name' },
      { field: 'role', title: 'Role' },
      { field: 'address', title: 'Address', showOverflow: true }
    ]
  })
})

const method = reactive({
  sureSearch: () => {
    console.log(data.searchForm)
  }
})

onMounted(() => {
  data.gridOptions.data = data.tableData
})

const handlePageChange: VxePagerEvents.PageChange = ({ currentPage, pageSize }) => {
  data.tablePage.currentPage = currentPage
  data.tablePage.pageSize = pageSize
  // TODO 重新获取数据
}

const tableHeight = computed(() => computedTableHeight({}))
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
