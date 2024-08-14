<script setup>
import { paginationMeta } from '@/@fake-db/utils'
import { VDataTableServer } from 'vuetify/labs/VDataTable'

const searchQuery = ref('')
const dateRange = ref('')
const selectedStatus = ref()
const totalInvoices = ref(0)
const invoices = ref([])
const selectedRows = ref([])

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
    title: '#ID',
    key: 'id',
  },
  {
    title: 'Trending',
    key: 'trending',
    sortable: false,
  },
  {
    title: 'Client',
    key: 'client',
  },
  {
    title: 'Total',
    key: 'total',
  },
  {
    title: 'Issued Date',
    key: 'date',
  },
  {
    title: 'Balance',
    key: 'balance',
  },
  {
    title: 'Actions',
    key: 'actions',
    sortable: false,
  },
]


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


watchEffect(() => {
  const [start, end] = dateRange.value ? dateRange.value.split('to') : ''
})
</script>

<template>
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
      :items-length="totalInvoices"
      :headers="headers"
      :items="invoices"
      class="text-no-wrap"
      @update:options="options = $event"
    >
      <!-- Trending Header -->

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
