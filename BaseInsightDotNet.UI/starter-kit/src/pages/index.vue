<script setup>
import { paginationMeta } from '@/@fake-db/utils'
import { filterDepartmentRequest } from '@/interfaces/requestModels/filerDepartmentRequest'
import { filterUserRequest } from '@/interfaces/requestModels/filterUserRequest'
import { DeparmentService } from '@/services/deparmentService'
import { UserService } from '@/services/userService'
import { onMounted } from 'vue'
import { VDataTableServer } from 'vuetify/labs/VDataTable'
import ModalAddDepartment from './department/modules/ModalAddDepartment.vue'

const totalInvoices = ref(0)
const invoices = ref([])
const selectedRows = ref([])
const dataManager = ref([])

const props = defineProps({
  departmentData: {
    type: Object,
    required: true,
  },
})
const filterUser = ref(filterUserRequest)
const isDepartmentAddDialogVisible = ref(false)
const options = ref({
  page: 1,
  itemsPerPage: 10,
  sortBy: [],
  groupBy: [],
  search: undefined,
})

const isLoading = ref(false)
const currentPage = ref(1)

currentPage.value = options.value.page

// 👉 headers
const headers = [
  {
    title: 'STT',
    key: 'stt',
    sortable: false,
  },
  {
    title: 'Name',
    key: 'name',
    sortable: false,
  },
  {
    title: 'Slogan',
    key: 'slogan',
  },
  {
    title: 'NumberOfMember',
    key: 'numberOfMember',
  },
  {
    title: 'Manager',
    key: 'manager',
  },
  {
    title: 'Create time',
    key: 'createTime',
  },
  {
    title: 'Actions',
    key: 'actions',
    sortable: false,
  },
]

const filterDepartment = ref(filterDepartmentRequest)



const getAllDepartment = async (filter) => {
  const result = await DeparmentService.getAllDepartments({
    name: filter.name,
    managerId: filter.managerId
  });
  invoices.value = result
  totalInvoices.value = result.length
}

const getAllManger = async (filter) => {
  const result = await UserService.getAllUsers(filterUser);
  dataManager.value = result
}

const refreshData = async () => {
  await getAllDepartment();

}

watchEffect( async () => {
  await getAllDepartment(filterDepartment);
  await getAllManger();
})


onMounted(async () => {
  await getAllManger();
  await getAllDepartment(filterDepartment);

})

</script>

<template>
  <div>
    <VCard
    v-if="invoices"
    id="invoice-list"
  >
    <VCardText class="d-flex align-center flex-wrap gap-4">
      <div class="me-3 d-flex gap-3">
        <AppSelect
          :model-value="options.itemsPerPage"
          :items="[
            {value: 5, title: '5'},
            { value: 10, title: '10' },
            { value: 25, title: '25' },
            { value: 50, title: '50' },
            { value: 100, title: '100' },
            { value: -1, title: 'All' },
          ]"
          style="width: 6.25rem;"
          @update:model-value="options.itemsPerPage = parseInt($event, 10)"
        />
        <!-- 👉 Create invoice -->
        <VBtn
          prepend-icon="tabler-plus"
          variant="elevated"
            class="me-4"
            @click="isDepartmentAddDialogVisible = true"
        >
          Create Department
        </VBtn>
      </div>

      <VSpacer />

      <div class="d-flex align-center flex-wrap gap-4">
        <!-- 👉 Search  -->
        <div class="invoice-list-filter">
          <AppTextField
            v-model="filterDepartment.name"
            placeholder="Search..."
            density="compact"
          />
        </div>

        <!-- 👉 Select status -->
        <div class="invoice-list-filter">
          <AppSelect
            v-model="filterDepartment.managerId"
            placeholder="Contract Type"
            clearable
            ref="select"
            clear-icon="tabler-x"
            single-line
            item-value="id"
            item-title="fullName"
            :items="dataManager"
          />
        </div>
      </div>
    </VCardText>

    <VDivider />

    <!-- SECTION Datatable -->
    <VDataTableServer
      v-model="selectedRows"
      v-model:items-per-page="options.itemsPerPage"
      v-model:page="options.page"
      :loading="isLoading"
      :headers="headers"
      :items="invoices"
      class="text-no-wrap"
      @update:options="options = $event"
    >
      <!-- Trending Header -->
      <template #item.stt="{ index }">
        {{ (options.page - 1) * options.itemsPerPage + index + 1 }}
      </template>

      <template #item.name="{ item }">
        {{ item.raw.name }}
    </template>

    <template #item.numberOfMember="{ item }">
      {{ item.raw.numberOfMember }}
  </template>
  <template #item.createTime="{ item }">
    {{ formatDate(item.raw.createTime) }}
</template>

<template #item.manager="{ item }">
  <div class="d-flex align-center">
    <VAvatar
      size="38"
      class="me-3"
    >
      <VImg
        v-if="item.raw.manager.avatarUrl.length"
        src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTUOWoK4J49TD8SPsBIXrk4_kOumIRofJDwkA&s"
      />
      <span v-else>{{ avatarText(item.raw.manager.fullName) }}</span>
    </VAvatar>
    <div class="d-flex flex-column">
      <h6 class="text-body-1 font-weight-medium mb-0">
        {{ item.raw.manager.fullName }}
      </h6>
      <span class="text-sm text-disabled">{{ item.raw.manager.email }}</span>
    </div>
  </div>
</template>

<!-- Actions -->
<template #item.actions="{ item }">
  <IconBtn>
    <VIcon icon="tabler-trash" />
  </IconBtn>

  <IconBtn>
    <VIcon icon="tabler-eye" />
  </IconBtn>

</template>


      <!-- pagination -->

      <template #bottom>
        <VDivider />
        <div class="d-flex align-center justify-sm-space-between justify-center flex-wrap gap-3 pa-5 pt-3">
          <p class="text-sm text-disabled mb-0">
            {{ paginationMeta(options, totalInvoices) }}
          </p>

          <VPagination
            v-model="options.page"
            :length="Math.ceil(totalInvoices / options.itemsPerPage)"
            :total-visible="$vuetify.display.xs ? 1 : Math.ceil(totalInvoices / options.itemsPerPage)"
          >
            <template #prev="slotProps">
              <VBtn
                variant="tonal"
                color="default"
                v-bind="slotProps"
                :icon="false"
              >
                Previous
              </VBtn>
            </template>

            <template #next="slotProps">
              <VBtn
                variant="tonal"
                color="default"
                v-bind="slotProps"
                :icon="false"
              >
                Next
              </VBtn>
            </template>
          </VPagination>
        </div>
      </template>
    </VDataTableServer>
    <!-- !SECTION -->
  </VCard>
  <ModalAddDepartment v-model:isDialogVisible="isDepartmentAddDialogVisible" :departmentData="props.departmentData" @submit="refreshData"/>
  </div>
</template>

<style lang="scss">
#invoice-list {
  .invoice-list-actions {
    inline-size: 8rem;
  }

  .invoice-list-filter {
    inline-size: 12rem;
  }
}
</style>
