<script setup>
import { filterUserRequest } from "@/interfaces/requestModels/filterUserRequest";
import { updateDepartmentRequest } from "@/interfaces/requestModels/updateDepartmentRequest";
import { DeparmentService } from "@/services/deparmentService";
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
const updateDepartment = ref(updateDepartmentRequest);
const emit = defineEmits(["submit", "update:isDialogVisible"]);
const filterUser = ref(filterUserRequest);
const dataManager = ref([]);
const departmentData = ref(structuredClone(toRaw(props.departmentData)));

watch(props, () => {
  departmentData.value = structuredClone(toRaw(updateDepartment.value));
});

const onFormSubmit = () => {

  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid)
     onClickButtonSubmit();
  })
};

const onFormReset = () => {
  departmentData.value = structuredClone(toRaw(updateDepartment.value));
  emit("update:isDialogVisible", false);
};

const dialogModelValueUpdate = (val) => {
  emit("update:isDialogVisible", val);
};

const onClickButtonSubmit = async () => {
  try{
    loading.value = false;
    updateDepartment.value.id = props.dataId
    const result = await DeparmentService.updateDepartment(updateDepartment.value);
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

const getAllUser = async () => {
  const result = await UserService.getAllUsers(filterUser);
  dataManager.value = result;
}


onMounted(async () => {
  await getAllUser();
});
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
        <VCardTitle class="text-h5 mb-3"> Update department </VCardTitle>
        <p class="mb-0">Updating user details will receive a privacy audit.</p>
      </VCardItem>

      <VCardText>
        <!-- 👉 Form -->
        <VForm class="mt-6" ref="refVForm" @submit.prevent="onFormSubmit">
          <VRow>
            <!-- 👉 First Name -->
            <VCol cols="12">
              <AppTextField v-model="updateDepartment.name" label="Name" />
            </VCol>

            <!-- 👉 Last Name -->
            <VCol cols="12">
              <AppTextField v-model="updateDepartment.slogan" label="Slogan" />
            </VCol>

            <!-- 👉 Status -->
            <VCol cols="12">
              <VLabel
                style="font-size: 13px; color: #d0d4f1c7; margin-bottom: 4px"
                >Manager</VLabel
              >
              <VSelect
                class="select-ant mb-5"
                ref="select"
                v-model="updateDepartment.managerId"
                item-value="id"
                item-title="fullName"
                :items="dataManager"
              >
              </VSelect>
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
