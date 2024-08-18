<template>
  <v-dialog
    v-model="showDialog"
    persistent
    transition="dialog-top-transition"
    width="400"
  >
    <DialogCloseBtn @click="onCancelClicked"/>
    <VCard :title="props.title" >
      <VContainer class="DeleteDialogText">
        {{ props.content }}
      </VContainer>
      <VCardActions class="DeleteDialogActions">
        <VSpacer/>
        <VBtn

          variant="outlined"
          @click="onCancelClicked"
          class="DeleteDialogCancelButton"
        >
          Hủy
        </VBtn>
        <VBtn

          variant="elevated"
          @click = "onConfirmClicked"
          class="DeleteDialogDeleteButton"
        >
          Xác nhận
        </VBtn>
      </VCardActions>
    </VCard>
  </v-dialog>
</template>

<script setup>
const props = defineProps({
  title: String,
  content: String
})

const showDialog = ref(false);
const emit = defineEmits(['onConfirmed'])
const data = ref();

const show = (d) => {
  showDialog.value = true
  data.value = d
}

const onConfirmClicked = () => {
  emit('onConfirmed', data.value)
  onCancelClicked()
}

const onCancelClicked = () => {
  showDialog.value = false
}

defineExpose({
  show
})

</script>

<style scoped>
  .DeleteDialogText {
    padding: 20px 24px 0 24px;
    margin-bottom: 60px;
    font-size: 15px;
    line-height: 22px;
  }

  .DeleteDialogActions {
    /* padding: 0 24px 20px 24px; */
  }

  .DeleteDialogCancelButton {
    display: flex;
    width: 69px;
    justify-content: center;
    align-items: center;
    flex-shrink: 0;
    border-radius: 6px;
    border: 1px solid var(--light-solid-color-danger-danger-500-base, #EA5455);
    color: #EA5455 !important;
  }

  .DeleteDialogDeleteButton {
    font-size: 15px;
    font-weight: 500;
    letter-spacing: 0.43px;
    padding: 0 20px !important;
    background-color: #0635AA !important;
  }
</style>
