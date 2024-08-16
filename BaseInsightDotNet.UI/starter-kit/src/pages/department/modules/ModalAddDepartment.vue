<script setup>
import { createDepartmentRequest } from "@/interfaces/requestModels/createDepartmentRequest";
import { filterUserRequest } from "@/interfaces/requestModels/filterUserRequest";
import { DeparmentService } from "@/services/deparmentService";
import { UserService } from "@/services/userService";
import { onMounted } from "vue";
import { toast } from "vue3-toastify";
import { VForm } from 'vuetify/components/VForm';
const props = defineProps({
  departmentData: {
    type: Object,
    required: false,
    default: () => ({
      id: 0,
      name: "",
      slogan: "",
      numberOfMember: null,
      managerId: "",
    }),
  },
  isDialogVisible: {
    type: Boolean,
    required: true,
  },
});
const refVForm = ref()
const loading = ref(false);
const createDepartment = ref(createDepartmentRequest);
const emit = defineEmits(["submit", "update:isDialogVisible"]);
const filterUser = ref(filterUserRequest);
const dataManager = ref([]);
const departmentData = ref(structuredClone(toRaw(props.departmentData)));

watch(props, () => {
  createDepartment.value = structuredClone(toRaw(createDepartment.value));
});

const onFormSubmit = () => {

  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid)
     onClickButtonSubmit();
  })
};

const onFormReset = () => {
  createDepartment.value = structuredClone(toRaw(createDepartment.value));
  emit("update:isDialogVisible", false);
};

const dialogModelValueUpdate = (val) => {
  emit("update:isDialogVisible", val);
};

const onClickButtonSubmit = async () => {
  try{
    loading.value = false;
    const result = await DeparmentService.createDepartment(createDepartment.value);
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
        <VCardTitle class="text-h5 mb-3"> Add new department </VCardTitle>
        <p class="mb-0">Updating user details will receive a privacy audit.</p>
      </VCardItem>

      <VCardText>
        <!-- 👉 Form -->
        <VForm class="mt-6" ref="refVForm" @submit.prevent="onFormSubmit">
          <VRow>
            <!-- 👉 First Name -->
            <VCol cols="12">
              <AppTextField v-model="createDepartment.name" label="Name" />
            </VCol>

            <!-- 👉 Last Name -->
            <VCol cols="12">
              <AppTextField v-model="createDepartment.slogan" label="Slogan" />
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
                v-model="createDepartment.managerId"
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
