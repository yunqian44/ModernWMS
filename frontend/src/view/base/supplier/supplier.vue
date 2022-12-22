<!-- supplier Setting -->
<template>
  <div class="container">
    <div>
      <!-- Main Content -->
      <v-card class="mt-5">
        <v-card-text>
          <v-window v-model="data.activeTab">
            <v-window-item>
              <div class="operateArea">
                <v-row no-gutters>
                  <!-- Operate Btn -->
                  <v-col cols="12" sm="3" class="col">
                    <tooltip-btn icon="mdi-plus" :tooltip-text="$t('system.page.add')"></tooltip-btn>
                    <tooltip-btn icon="mdi-refresh" :tooltip-text="$t('system.page.refresh')"></tooltip-btn>
                    <tooltip-btn icon="mdi-export-variant" :tooltip-text="$t('system.page.export')" @click="method.exportTable"> </tooltip-btn>
                  </v-col>

                  <!-- Search Input -->
                  <v-col cols="12" sm="9">
                    <v-row no-gutters @keyup.enter="method.sureSearch">
                      <v-col cols="12" sm="4"></v-col>
                      <v-col cols="12" sm="4"></v-col>
                      <v-col cols="12" sm="4">
                        <v-text-field
                          v-model="data.searchForm.supplier_name"
                          clearable
                          hide-details
                          density="comfortable"
                          class="searchInput ml-5 mt-1"
                          :label="$t('base.supplier.supplier_name')"
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
                  <vxe-column field="supplier_name" :title="$t('base.supplier.supplier_name')"></vxe-column>
                  <vxe-column field="city" :title="$t('base.supplier.city')"></vxe-column>
                  <vxe-column field="address" :title="$t('base.supplier.address')"></vxe-column>
                  <vxe-column field="manager" :title="$t('base.supplier.manager')"></vxe-column>
                  <vxe-column field="email" :title="$t('base.supplier.email')"></vxe-column>
                  <vxe-column field="contact_tel" :title="$t('base.supplier.contact_tel')"></vxe-column>
                  <vxe-column field="creater" :title="$t('base.supplier.creator')"></vxe-column>
                  <vxe-column
                    field="create_time"
                    :title="$t('base.supplier.create_time')"
                    :formatter="['formatDate', 'yyyy-MM-dd HH:mm:ss']"
                  ></vxe-column>
                  <vxe-column
                    field="last_update_time"
                    :title="$t('base.supplier.last_update_time')"
                    :formatter="['formatDate', 'yyyy-MM-dd HH:mm:ss']"
                  ></vxe-column>
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
                  :current-page="data.tablePage.pageIndex"
                  :page-size="data.tablePage.pageSize"
                  perfect
                  :total="data.tablePage.total"
                  :page-sizes="PAGE_SIZE"
                  :layouts="PAGE_LAYOUT"
                  @page-change="method.handlePageChange"
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

<script lang="tsx" setup>
import { computed, ref, reactive, onMounted } from 'vue'
import { VxePagerEvents } from 'vxe-table'
import { computedCardHeight, computedTableHeight, errorColor } from '@/constant/style'
import { SupplierVO } from '@/types/Base/Supplier'
import { PAGE_SIZE, PAGE_LAYOUT } from '@/constant/vxeTable'
import tooltipBtn from '@/components/tooltip-btn.vue'

const xTable = ref()

const data = reactive({
  searchForm: {
    supplier_name: ''
  },
  activeTab: null,
  tableData: ref<SupplierVO[]>([]),
  tablePage: reactive({
    total: 0,
    pageIndex: 1,
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
  handlePageChange: ref<VxePagerEvents.PageChange>(({ currentPage, pageSize }) => {
    data.tablePage.pageIndex = currentPage
    data.tablePage.pageSize = pageSize
    method.getData()
  }),
  exportTable: () => {
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
  },
  sureSearch: () => {
    console.log(data.searchForm)
  },
  getData() {
    // TODO get data
    data.tableData.push(
      {
        id: 10001,
        supplier_name: 'Supplier Name',
        city: 'City',
        address: 'Address',
        manager: 'Manager',
        email: 'Email',
        contact_tel: 'Contact Tel',
        creator: 'Creater',
        create_time: '',
        last_update_time: '',
        is_valid: true
      },
      {
        id: 10002,
        supplier_name: 'Supplier Name',
        city: 'City',
        address: 'Address',
        manager: 'Manager',
        email: 'Email',
        contact_tel: 'Contact Tel',
        creator: 'Creater',
        create_time: '',
        last_update_time: '',
        is_valid: true
      }
    )
  }
})

onMounted(() => {
  method.getData()
})

const cardHeight = computed(() => computedCardHeight({ hasTab: false }))
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
