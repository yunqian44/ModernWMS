<!-- Freight Setting -->
<template>
  <div class="container">
    <div>
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
                    >
                    </tooltip-btn>
                  </v-col>

                  <!-- Search Input -->
                  <v-col cols="12" sm="9">
                    <v-row no-gutters @keyup.enter="method.sureSearch">
                      <v-col cols="12" sm="4"></v-col>
                      <v-col cols="12" sm="4"></v-col>
                      <v-col cols="12" sm="4">
                        <v-text-field
                          v-model="data.searchForm.transportationSupplier"
                          clearable
                          hide-details
                          density="comfortable"
                          class="searchInput ml-5 mt-1"
                          :label="$t('base.freightSetting.transportationSupplier')"
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
                  height: tableHeight
                }"
              >
                <vxe-grid v-bind="data.gridOptions" ref="xTable">
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
import { VxeGridProps, VxePagerEvents, VxeButtonEvents } from 'vxe-table'
import { computedTableHeight } from '@/utils/globalStyle'
import tooltipBtn from '@/components/tooltip-btn.vue'
import i18n from '@/languages/i18n'
import { FreightVO } from '@/types/settings/freight'

const xTable = ref()

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
    transportationSupplier: ''
  },
  activeTab: null,
  tableData: ref<FreightVO[]>([]),
  tablePage: reactive({
    total: 0,
    currentPage: 1,
    pageSize: 10
  }),
  gridOptions: reactive<VxeGridProps>({
    height: 'auto',
    loading: false,
    align: 'center',
    columnConfig: {
      resizable: true
    },
    data: [],
    columns: [
      { type: 'seq', width: 60 },
      { type: 'checkbox', width: 50 },
      { field: 'transportation_supplier', title: i18n.global.t('base.freightSetting.transportationSupplier') },
      { field: 'send_city', title: i18n.global.t('base.freightSetting.sendCity') },
      { field: 'receiver_city', title: i18n.global.t('base.freightSetting.receiverCity') },
      { field: 'weight_fee', title: i18n.global.t('base.freightSetting.weightFee') },
      { field: 'volume_fee', title: i18n.global.t('base.freightSetting.volumeFee') },
      { field: 'min_payment', title: i18n.global.t('base.freightSetting.minPayment') }
    ]
  })
})

const method = reactive({
  sureSearch: () => {
    console.log(data.searchForm)
  },
  getData() {
    // TODO get data
    data.tableData.push(
      {
        id: 10001,
        transportation_supplier: '1',
        send_city: '1',
        receiver_city: '1',
        weight_fee: 1,
        volume_fee: 1,
        min_payment: 1
      },
      {
        id: 10002,
        transportation_supplier: '2',
        send_city: '2',
        receiver_city: '2',
        weight_fee: 2,
        volume_fee: 2,
        min_payment: 3
      }
    )
    data.gridOptions.data = data.tableData
  }
})

onMounted(() => {
  method.getData()
})

const handlePageChange: VxePagerEvents.PageChange = ({ currentPage, pageSize }) => {
  data.tablePage.currentPage = currentPage
  data.tablePage.pageSize = pageSize

  method.getData()
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
</style>
