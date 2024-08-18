<script setup>
import { paginationMeta } from "@/@fake-db/utils";
import ConfirmDeleteDialog from "@/components/ConfirmDeleteDialog.vue";
import { filterContractTypeRequest } from "@/interfaces/requestModels/contractType/filterContractTypeRequest";
import ModalAddContractType from "@/pages/contractType/modules/ModalAddContractType.vue";
import ModalUpdateContractType from "@/pages/contractType/modules/ModalUpdateContractType.vue";
import { ContractTypeService } from "@/services/contractTypeService";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VDataTableServer } from "vuetify/labs/VDataTable";
const totalInvoices = ref(0);
const invoices = ref([]);
const selectedRows = ref([]);
const props = defineProps({
  contractTypeData: {
    type: Object,
    required: true,
  },
});
const instance = getCurrentInstance();
const isContractTypeAddDialogVisible = ref(false);
const isContractTypeUpdateDialogVisible = ref(false);
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
const contractTypeId = ref();


currentPage.value = options.value.page;

// 👉 headers
const headers = [
  {
    title: "STT",
    key: "stt",
    sortable: false,
  },
  {
    title: "Name",
    key: "name",
    sortable: false,
  },
  {
    title: "Description",
    key: "description",
  },
  {
    title: "Actions",
    key: "actions",
    sortable: false,
  },
];

const filterContractType = ref(filterContractTypeRequest);

const getAllContractType = async (filter) => {
  const result = await ContractTypeService.getAllContractType({
    name: filter.name,
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
  await getAllContractType(filterContractType.value);
};

const openDialogUpdateContractType = (id) => {
  isContractTypeUpdateDialogVisible.value = true;
  dataId.value = id
}

const onConfirmed = async () => {
  try{
    const result = await ContractTypeService.deleteContractType(contractTypeId.value);
    toast(result, {
      type: "success",
      transition: "flip",
      "autoClose": 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
    await getAllContractType(filterContractType.value);
  }catch(error){
    toast(error, {
      type: "error",
      transition: "flip",
      "autoClose": 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
  }
}

const onclickDeleteItem = (id) => {
  contractTypeId.value = id;
  instance?.refs.deleteDialog.show();
}

watchEffect(async () => {
  await getAllContractType(filterContractType.value);
});

onMounted(async () => {
  await getAllContractType(filterContractType.value);
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
            @click="isContractTypeAddDialogVisible = true"
          >
            Create
          </VBtn>
        </div>

        <VSpacer />

        <div class="d-flex align-center flex-wrap gap-4">
          <!-- 👉 Search  -->
          <div class="invoice-list-filter">
            <AppTextField
              v-model="filterContractType.name"
              placeholder="Search..."
              density="compact"
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

        <template #item.name="{ item }">
          {{ item.raw.name }}
        </template>

        <template #item.description="{ item }">
          {{ item.raw.description }}
        </template>

        <!-- Actions -->
        <template #item.actions="{ item }">
          <IconBtn @click="openDialogUpdateContractType(item.raw.id)">
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
    <ModalAddContractType
      v-model:isDialogVisible="isContractTypeAddDialogVisible"
      :contractTypeData="props.contractTypeData"
      @submit="refreshData"
    />
    <ModalUpdateContractType
      v-model:isDialogVisible="isContractTypeUpdateDialogVisible"
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
</style>
