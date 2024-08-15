<script setup>
const props = defineProps({
  departmentData: {
    type: Object,
    required: false,
    default: () => ({
      id: 0,
      name: '',
      slogan: '',
      numberOfMember: null,
      managerId: '',
    }),
  },
  isDialogVisible: {
    type: Boolean,
    required: true,
  },
})

const emit = defineEmits([
  'submit',
  'update:isDialogVisible',
])

const dataManager = ref([])
const departmentData = ref(structuredClone(toRaw(props.departmentData)))
const isUseAsBillingAddress = ref(false)

watch(props, () => {
  departmentData.value = structuredClone(toRaw(props.departmentData))
})

const onFormSubmit = () => {
  emit('update:isDialogVisible', false)
  emit('submit', departmentData.value)
}

const onFormReset = () => {
  userData.value = structuredClone(toRaw(props.departmentData))
  emit('update:isDialogVisible', false)
}

const dialogModelValueUpdate = val => {
  emit('update:isDialogVisible', val)
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
        <VCardTitle class="text-h5 mb-3">
          Add new department
        </VCardTitle>
        <p class="mb-0">
          Updating user details will receive a privacy audit.
        </p>
      </VCardItem>

      <VCardText>
        <!-- 👉 Form -->
        <VForm
          class="mt-6"
          @submit.prevent="onFormSubmit"
        >
          <VRow>
            <!-- 👉 First Name -->
            <VCol
              cols="12"
              md="6"
            >
              <AppTextField
                v-model="departmentData.name"
                label="Name"
              />
            </VCol>

            <!-- 👉 Last Name -->
            <VCol
              cols="12"
              md="6"
            >
              <AppTextField
                v-model="departmentData.slogan"
                label="Slogan"
              />
            </VCol>

            <!-- 👉 Status -->
            <VCol
              cols="12"
              md="6"
            >
            <VLabel style="font-size:13px; color: #D0D4F1C7; margin-bottom: 4px">Manager</VLabel>
            <VSelect
              class="select-ant mb-5"
              ref="select"
              v-model="departmentData.managerId"
              :items="dataManager"
            >
            </VSelect>
            </VCol>



            <!-- 👉 Switch -->
            <VCol cols="12">
              <VSwitch
                v-model="isUseAsBillingAddress"
                density="compact"
                label="Use as a billing address?"
              />
            </VCol>

            <!-- 👉 Submit and Cancel -->
            <VCol
              cols="12"
              class="d-flex flex-wrap justify-center gap-4"
            >
              <VBtn type="submit">
                Submit
              </VBtn>

              <VBtn
                color="secondary"
                variant="tonal"
                @click="onFormReset"
              >
                Cancel
              </VBtn>
            </VCol>
          </VRow>
        </VForm>
      </VCardText>
    </VCard>
  </VDialog>
</template>
