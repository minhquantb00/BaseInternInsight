<script setup>
import { paginationMeta } from '@/@fake-db/utils'
import { filterDepartmentRequest } from '@/interfaces/requestModels/filerDepartmentRequest'
import { DeparmentService } from '@/services/deparmentService'
import { VDataTableServer } from 'vuetify/labs/VDataTable'
import ModalAddDepartment from './department/modules/ModalAddDepartment.vue'
const searchQuery = ref('')
const dateRange = ref('')
const selectedStatus = ref()
const totalInvoices = ref(0)
const invoices = ref([])
const selectedRows = ref([])

const props = defineProps({
  departmentData: {
    type: Object,
    required: true,
  },
})

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

// 👉 Invoice balance variant resolver
const resolveInvoiceBalanceVariant = (balance, total) => {
  if (balance === total)
    return {
      status: 'Unpaid',
      chip: { color: 'error' },
    }
  if (balance === 0)
    return {
      status: 'Paid',
      chip: { color: 'success' },
    }

  return {
    status: balance,
    chip: { variant: 'text' },
  }
}
const formatDate = (dateString) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');

  return `${year}-${month}-${day}`;
};

const resolveInvoiceStatusVariantAndIcon = status => {
  if (status === 'Partial Payment')
    return {
      variant: 'success',
      icon: 'tabler-circle-half-2',
    }
  if (status === 'Paid')
    return {
      variant: 'warning',
      icon: 'tabler-chart-pie',
    }
  if (status === 'Downloaded')
    return {
      variant: 'info',
      icon: 'tabler-arrow-down-circle',
    }
  if (status === 'Draft')
    return {
      variant: 'primary',
      icon: 'tabler-device-floppy',
    }
  if (status === 'Sent')
    return {
      variant: 'secondary',
      icon: 'tabler-circle-check',
    }
  if (status === 'Past Due')
    return {
      variant: 'error',
      icon: 'tabler-alert-circle',
    }

  return {
    variant: 'secondary',
    icon: 'tabler-x',
  }
}

const computedMoreList = computed(() => {
  return paramId => [
    {
      title: 'Download',
      value: 'download',
      prependIcon: 'tabler-download',
    },
    {
      title: 'Edit',
      value: 'edit',
      prependIcon: 'tabler-pencil',
      to: {
        name: 'apps-invoice-edit-id',
        params: { id: paramId },
      },
    },
    {
      title: 'Duplicate',
      value: 'duplicate',
      prependIcon: 'tabler-layers-intersect',
    },
  ]
})


watchEffect( async () => {
  const [start, end] = dateRange.value ? dateRange.value.split('to') : ''
  const result = await DeparmentService.getAllDepartments(filterDepartment);
  invoices.value = result
  totalInvoices.value = result.length
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
            v-model="searchQuery"
            placeholder="Search..."
            density="compact"
          />
        </div>

        <!-- 👉 Select status -->
        <div class="invoice-list-filter">
          <AppSelect
            v-model="selectedStatus"
            placeholder="Contract Type"
            clearable
            clear-icon="tabler-x"
            single-line
            :items="['Thử việc', 'Cộng tác viên', 'Chính thức']"
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
  <ModalAddDepartment v-model:isDialogVisible="isDepartmentAddDialogVisible" :departmentData="props.departmentData"/>
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
