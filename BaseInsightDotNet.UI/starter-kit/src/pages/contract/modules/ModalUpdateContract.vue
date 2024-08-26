<script setup>
import { updateContractRequest } from "@/interfaces/requestModels/contract/updateContractRequest";
import { filterContractTypeRequest } from "@/interfaces/requestModels/contractType/filterContractTypeRequest";
import { filterUserRequest } from "@/interfaces/requestModels/filterUserRequest";
import { ContractService } from "@/services/contractService";
import { ContractTypeService } from "@/services/contractTypeService";
import { UserService } from "@/services/userService";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import { VForm } from 'vuetify/components/VForm';
const props = defineProps({
  dataId: Number,
  isDialogVisible: {
    type: Boolean,
    required: true,
  },
});
const refVForm = ref()
const loading = ref(false);
const dataUser = ref([]);
const filterUser = ref(filterUserRequest);
const dataContractType = ref([])
const updateContract = ref(updateContractRequest);
const filterContractType = ref(filterContractTypeRequest);
const emit = defineEmits(["submit", "update:isDialogVisible"]);

const onFormSubmit = () => {

  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid)
     onClickButtonSubmit();
  })
};

const onFormReset = () => {
  emit("update:isDialogVisible", false);
};

const dialogModelValueUpdate = (val) => {
  emit("update:isDialogVisible", val);
};

const getAllUser = async () => {
  const result = await UserService.getAllUsers(filterUser);
  dataUser.value = result;
}

const getAllContractType = async (filter) => {
  const result = await ContractTypeService.getAllContractType(filterContractType.value);
  dataContractType.value = result
};

const onClickButtonSubmit = async () => {
  try{
    loading.value = false;
    updateContract.value.id = props.dataId
    console.log(updateContract.value);
    const result = await ContractService.updateContract(updateContract.value);
    if(result.status === 200){
      loading.value = true;
      toast(result.message, {
      type: "success",
      transition: "flip",
      "autoClose": 2000,
      theme: "dark",
      dangerouslyHTMLString: true,
    });
    emit("update:isDialogVisible", false);
      emit("submit");
    }
    else{
      loading.value = true
      toast(result.message, {
      type: "error",
      transition: "flip",
      theme: "dark",
      "autoClose": 2000,
      dangerouslyHTMLString: true,
    });
    }
    loading.value = false;
  }catch (error) {
    console.error(error);
    toast("An error occurred. Please try again.", {
      type: "error",
      transition: "flip",
      theme: "dark",
      autoClose: 2000,
      dangerouslyHTMLString: true,
    });
  } finally {
    loading.value = false;
  }
}

onMounted(() => {
  getAllUser();
  getAllContractType();
})
</script>

<template>
  <VDialog
    :width="$vuetify.display.smAndDown ? 'auto' : 677"
    :model-value="props.isDialogVisible"
    @update:model-value="dialogModelValueUpdate"
  >
    <!-- Dialog close btn -->
    <DialogCloseBtn @click="dialogModelValueUpdate(false)" />

    <VCard class="pa-sm-8 pa-5">
      <VCardItem class="text-center">
        <VCardTitle class="text-h5 mb-3"> Update contract </VCardTitle>
        <p class="mb-0">Updating user details will receive a privacy audit.</p>
      </VCardItem>

      <VCardText>
        <!-- 👉 Form -->
        <VForm class="mt-6" ref="refVForm" @submit.prevent="onFormSubmit">
          <VRow>
            <VCol cols="6">
              <VLabel
                style="font-size: 13px; color: #d0d4f1c7; margin-bottom: 4px"
                >Employee</VLabel
              >
              <VSelect
                class="select-ant mb-5"
                ref="select"
                v-model="updateContract.employeeId"
                item-value="id"
                item-title="fullName"
                :items="dataUser"
              >
              </VSelect>
            </VCol>

            <VCol cols="6">
              <VLabel
                style="font-size: 13px; color: #d0d4f1c7; margin-bottom: 4px"
                >Contract type</VLabel
              >
              <VSelect
                class="select-ant mb-5"
                ref="select"
                v-model="updateContract.contractTypeId"
                item-value="id"
                item-title="name"
                :items="dataContractType"
              >
              </VSelect>
            </VCol>

            <VCol cols="12" >
              <AppDateTimePicker
                v-model="updateContract.startDate"
                :format="'YYYY-MM-DD'"
                label="Start date"
                placeholder="yyyy-mm-dd"
                prepend-inner-icon="tabler-calendar"

                clearable
                class="date-picker-input"
              />
            </VCol>

            <VCol cols="12" style="margin-bottom: 20px;">
              <AppDateTimePicker
                v-model="updateContract.endDate"
                :format="'YYYY-MM-DD'"
                label="End date"
                placeholder="yyyy-mm-dd"
                prepend-inner-icon="tabler-calendar"
                clearable
                class="date-picker-input"
              />
            </VCol>

            <!-- 👉 First Name -->
            <VCol cols="4">
              <AppTextField v-model="updateContract.content" label="Content" />
            </VCol>

            <!-- 👉 Last Name -->
            <VCol cols="4">
              <AppTextField v-model="updateContract.baseSalary" label="Base Salary" />
            </VCol>

            <VCol cols="4">
              <AppTextField v-model="updateContract.TaxPercentage" label="Tax percentage" />
            </VCol>



            <!-- 👉 Submit and Cancel -->
            <VCol cols="12" class="d-flex flex-wrap justify-center gap-4">
              <VBtn type="submit"> Submit </VBtn>

              <VBtn color="secondary" variant="tonal" @click="onFormReset">
                Cancel
              </VBtn>
            </VCol>
          </VRow>
        </VForm>
      </VCardText>
    </VCard>
  </VDialog>
</template>
