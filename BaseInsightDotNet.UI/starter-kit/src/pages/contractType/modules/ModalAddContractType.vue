<script setup>
import { createContractTypeRequest } from "@/interfaces/requestModels/contractType/createContractTypeRequest";
import { ContractTypeService } from "@/services/contractTypeService";
import { toast } from "vue3-toastify";
import { VForm } from 'vuetify/components/VForm';
const props = defineProps({
  contractTypeData: {
    type: Object,
    required: false,
    default: () => ({
      id: 0,
      name: "",
      description: "",
    }),
  },
  isDialogVisible: {
    type: Boolean,
    required: true,
  },
});
const refVForm = ref()
const loading = ref(false);
const createContractType = ref(createContractTypeRequest);
const emit = defineEmits(["submit", "update:isDialogVisible"]);

watch(props, () => {
  createContractType.value = structuredClone(toRaw(createContractType.value));
});

const onFormSubmit = () => {

  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid)
     onClickButtonSubmit();
  })
};

const onFormReset = () => {
  createContractType.value = structuredClone(toRaw(createContractType.value));
  emit("update:isDialogVisible", false);
};

const dialogModelValueUpdate = (val) => {
  emit("update:isDialogVisible", val);
};

const onClickButtonSubmit = async () => {
  try{
    loading.value = false;
    const result = await ContractTypeService.createContractType(createContractType.value);
    console.log(result);
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
        <VCardTitle class="text-h5 mb-3"> Add new contract type </VCardTitle>
        <p class="mb-0">Updating user details will receive a privacy audit.</p>
      </VCardItem>

      <VCardText>
        <!-- 👉 Form -->
        <VForm class="mt-6" ref="refVForm" @submit.prevent="onFormSubmit">
          <VRow>
            <!-- 👉 First Name -->
            <VCol cols="12">
              <AppTextField v-model="createContractType.name" label="Name" />
            </VCol>

            <!-- 👉 Last Name -->
            <VCol cols="12">
              <AppTextField v-model="createContractType.description" label="Description" />
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
