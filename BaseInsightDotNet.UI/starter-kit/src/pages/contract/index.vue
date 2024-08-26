<script setup>
import { paginationMeta } from "@/@fake-db/utils";
import ConfirmDeleteDialog from "@/components/ConfirmDeleteDialog.vue";
import { filterContractRequest } from "@/interfaces/requestModels/contract/filterContractRequest";
import { filterContractTypeRequest } from "@/interfaces/requestModels/contractType/filterContractTypeRequest";
import { filterUserRequest } from "@/interfaces/requestModels/filterUserRequest";
import ModalAddContract from "@/pages/contract/modules/ModalAddContract.vue";
import ModalUpdateContract from "@/pages/contract/modules/ModalUpdateContract.vue";
import { ContractService } from "@/services/contractService";
import { ContractTypeService } from "@/services/contractTypeService";
import { UserService } from "@/services/userService";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VDataTableServer } from "vuetify/labs/VDataTable";

const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const props = defineProps({
  contractData: {
    type: Object,
    required: true,
  },
});
const instance = getCurrentInstance();
const isContractAddDialogVisible = ref(false);
const isContractUpdateDialogVisible = ref(false);
const options = ref({
  page: 1,
  itemsPerPage: 10,
  sortBy: [],
  groupBy: [],
  search: undefined,
});

const isLoading = ref(false);
const currentPage = ref(1);
const dataId = ref();
const dataUser = ref([]);
const filterUser = ref(filterUserRequest);
const dataContractType = ref([])
const filterContractType = ref(filterContractTypeRequest);
const contractId = ref();
const status = ref(["HetHan", "BanNhap", "DaHuy", "DaGiaHan", "DaChamDut", "DangCho"])

currentPage.value = options.value.page;

// 👉 headers
const headers = [
  {
    title: "STT",
    key: "stt",
    sortable: false,
  },
  {
    title: "Employee",
    key: "employee",
    sortable: false,
  },
  {
    title: "Base Salary",
    key: "baseSalary",
  },
  {
    title: "Salary before tax",
    key: "salaryBeforeTax",
  },
  {
    title: "Start date",
    key: "startDate",
  },
  {
    title: "End date",
    key: "endDate",
  },
  {
    title: "Actions",
    key: "actions",
    sortable: false,
  },
];

const filterContract = ref(filterContractRequest);

const getAllContracts = async (filter) => {
  const result = await ContractService.getAllContract({
    contractTypeId: filter.contractTypeId,
    employeeId: filter.employeeId,
    contractStatus: filter.contractStatus,
    fromDate: filter.fromDate,
    toDate: filter.toDate,
  });
  invoices.value = result;
  totalInvoices.value = result.length;
};

const paginatedData = computed(() => {
  const start = (options.value.page - 1) * options.value.itemsPerPage;
  const end = start + options.value.itemsPerPage;
  return invoices.value.slice(start, end);
});

const totalPages = computed(() => {
  return Math.ceil((invoices.value.length * 1.0) / options.value.itemsPerPage);
});

const refreshData = async () => {
  await getAllContracts(filterContract.value);
};

const formatDate = (dateString) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");

  return `${year}-${month}-${day}`;
};

const getAllUser = async () => {
  const result = await UserService.getAllUsers(filterUser);
  dataUser.value = result;
}

const getAllContractType = async (filter) => {
  const result = await ContractTypeService.getAllContractType(filterContractType.value);
  dataContractType.value = result
};

const openDialogUpdateContract = (id) => {
  isContractUpdateDialogVisible.value = true;
  dataId.value = id;
};

const onConfirmed = async () => {
  try {
    const result = await ContractService.deleteContract(
      contractId.value
    );
    toast(result, {
      type: "success",
      transition: "flip",
      autoClose: 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
    await getAllContracts(filterContract.value);
  } catch (error) {
    toast(error, {
      type: "error",
      transition: "flip",
      autoClose: 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
  }
};

const onclickDeleteItem = (id) => {
  contractId.value = id;
  instance?.refs.deleteDialog.show();
};

watchEffect(async () => {
  await getAllContracts(filterContract.value);
});

onMounted(async () => {
  await getAllUser();
  await getAllContractType();
  await getAllContracts(filterContract.value);
});
</script>

<template>
  <div>
    <VCard v-if="invoices" id="invoice-list">
      <VCardText class="d-flex align-center flex-wrap gap-4">
        <div class="me-3 d-flex gap-3">
          <AppSelect
            :model-value="options.itemsPerPage"
            :items="[
              { value: 5, title: '5' },
              { value: 10, title: '10' },
              { value: 25, title: '25' },
              { value: 50, title: '50' },
              { value: 100, title: '100' },
              { value: -1, title: 'All' },
            ]"
            style="width: 6.25rem"
            @update:model-value="options.itemsPerPage = parseInt($event, 10)"
          />
          <!-- 👉 Create invoice -->
          <VBtn
            prepend-icon="tabler-plus"
            variant="elevated"
            class="me-4"
            @click="isContractAddDialogVisible = true"
          >
            Create
          </VBtn>
        </div>

        <VSpacer />

        <div class="d-flex align-center flex-wrap gap-4">
          <!-- 👉 Search  -->
          <div class="invoice-list-filter" style="margin-bottom: 24px;">
            <AppDateTimePicker
                v-model="filterContract.fromDate"
                :format="'YYYY-MM-DD'"
                label="From date"
                placeholder="yyyy-mm-dd"
                prepend-inner-icon="tabler-calendar"
                clearable
                class="date-picker-input"
              />
          </div>

          <div class="invoice-list-filter" style="margin-bottom: 24px;">
            <AppDateTimePicker
                v-model="filterContract.toDate"
                :format="'YYYY-MM-DD'"
                label="To date"
                placeholder="yyyy-mm-dd"
                prepend-inner-icon="tabler-calendar"
                clearable
                class="date-picker-input"
              />
          </div>

          <div class="invoice-list-filter">
            <AppSelect
              v-model="filterContract.employeeId"
              placeholder="Employee"
              clearable
              ref="select"
              clear-icon="tabler-x"
              single-line
              item-value="id"
              item-title="fullName"
              :items="dataUser"
            />
          </div>

          <div class="invoice-list-filter">
            <AppSelect
              v-model="filterContract.contractTypeId"
              placeholder="ContractType"
              clearable
              ref="select"
              clear-icon="tabler-x"
              single-line
              item-value="id"
              item-title="name"
              :items="dataContractType"
            />
          </div>

          <div class="invoice-list-filter">
            <AppSelect
              v-model="filterContract.contractStatus"
              placeholder="Status"
              clearable
              ref="select"
              clear-icon="tabler-x"
              single-line
              :items="status"
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
        :items="paginatedData"
        class="text-no-wrap"
        @update:options="options = $event"
      >
        <!-- Trending Header -->
        <template #item.stt="{ index }">
          {{ (options.page - 1) * options.itemsPerPage + index + 1 }}
        </template>

        <template #item.employee="{ item }">
          {{ item.raw.employee.fullName }}
        </template>

        <template #item.baseSalary="{ item }">
          {{ item.raw.baseSalary }} VNĐ
        </template>

        <template #item.salaryBeforeTax="{ item }">
          {{ item.raw.salaryBeforeTax }} VNĐ
        </template>

        <template #item.startDate="{ item }">
          {{ formatDate(item.raw.startDate) }}
        </template>

        <template #item.endDate="{ item }">
          {{ formatDate(item.raw.endDate) }}
        </template>

        <!-- Actions -->
        <template #item.actions="{ item }">
          <IconBtn @click="openDialogUpdateContract(item.raw.id)">
            <VIcon icon="tabler-settings-check" />
          </IconBtn>

          <IconBtn @click="onclickDeleteItem(item.raw.id)">
            <VIcon icon="tabler-trash" />
          </IconBtn>
        </template>

        <!-- pagination -->

        <template #bottom>
          <VDivider />
          <div
            class="d-flex align-center justify-sm-space-between justify-center flex-wrap gap-3 pa-5 pt-3"
          >
            <p class="text-sm text-disabled mb-0">
              {{ paginationMeta(options, totalInvoices) }}
            </p>

            <VPagination
              v-model="options.page"
              :length="totalPages"
              :total-visible="$vuetify.display.xs ? 1 : totalPages"
              rounded="circle"
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
    <ModalAddContract
      v-model:isDialogVisible="isContractAddDialogVisible"
      :contractData="props.contractData"
      @submit="refreshData"
    />
    <ModalUpdateContract
      v-model:isDialogVisible="isContractUpdateDialogVisible"
      :dataId="dataId"
      @submit="refreshData"
    />
    <ConfirmDeleteDialog
      @onConfirmed="onConfirmed"
      title="Xác nhận"
      content="Bạn có muốn xóa không"
      ref="deleteDialog"
    ></ConfirmDeleteDialog>
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
.calendar-date-picker {
  display: none;

  +.flatpickr-input {
    +.flatpickr-calendar.inline {
      border: none;
      box-shadow: none;

      .flatpickr-months {
        border-block-end: none;
      }
    }
  }

  & ~ .flatpickr-calendar .flatpickr-weekdays {
    margin-block: 0 4px;
  }
}
</style>
