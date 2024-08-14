<script setup>
import { filterDepartmentRequest } from '@/interfaces/requestModels/filerDepartmentRequest'
import { registerRequest } from '@/interfaces/requestModels/registerRequest'
import { AuthService } from '@/services/authService'
import { DeparmentService } from '@/services/deparmentService'
import { PositionService } from '@/services/positionService'
import AuthProvider from '@/views/pages/authentication/AuthProvider.vue'
import { useGenerateImageVariant } from '@core/composable/useGenerateImageVariant'
import authV2RegisterIllustrationBorderedDark from '@images/pages/auth-v2-register-illustration-bordered-dark.png'
import authV2RegisterIllustrationBorderedLight from '@images/pages/auth-v2-register-illustration-bordered-light.png'
import authV2RegisterIllustrationDark from '@images/pages/auth-v2-register-illustration-dark.png'
import authV2RegisterIllustrationLight from '@images/pages/auth-v2-register-illustration-light.png'
import authV2MaskDark from '@images/pages/misc-mask-dark.png'
import authV2MaskLight from '@images/pages/misc-mask-light.png'
import { VNodeRenderer } from '@layouts/components/VNodeRenderer'
import { themeConfig } from '@themeConfig'
import {
  alphaDashValidator,
  emailValidator,
  requiredValidator,
} from '@validators'
import { onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useToast } from 'vue-toast-notification'
import { VForm } from 'vuetify/components/VForm'

const refVForm = ref()
const $toast = useToast();
const privacyPolicies = ref(true)
const loading = ref(false)
const disabled = ref(false)
const genders = ref(["Male", "Female", "UnKnown"])
const businessExecute = ref(registerRequest)
const filterDepartment = ref(filterDepartmentRequest)
const listDepartment = ref([])
const listPosition = ref([])
// Router
const router = useRouter()
const time = ref();

// Form Errors
const errors = ref({
  email: undefined,
  password: undefined,
})

const register = async () => {
  loading.value = false;
  const result = await AuthService.register(businessExecute.value);
  if(result.status === 200){
    $toast.open({
    message: result.message,
    type: 'success',
    dismissible : true,
    duration: 1500
  });
  time.value = setTimeout(() =>{
    router.push('/login')
  }, 1500)
  }
  else{
    $toast.open({
    message: result.message,
    type: 'error',
    dismissible: true,
    duration: 1500
  });
  loading.value = false
  }
}

const imageVariant = useGenerateImageVariant(authV2RegisterIllustrationLight, authV2RegisterIllustrationDark, authV2RegisterIllustrationBorderedLight, authV2RegisterIllustrationBorderedDark, true)
const authThemeMask = useGenerateImageVariant(authV2MaskLight, authV2MaskDark)
const isPasswordVisible = ref(false)

const onSubmit = () => {
  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid)
      register()
  })
}

onMounted( async () => {
  const resultDepartment = await DeparmentService.getAllDepartments(filterDepartment);
  const resultPosition = await PositionService.getAllPositions();
  listDepartment.value = resultDepartment
  listPosition.value = resultPosition
})
</script>

<template>
  <VRow
    no-gutters
    class="auth-wrapper bg-surface"
  >
    <VCol
      lg="8"
      class="d-none d-lg-flex"
    >
      <div class="position-relative bg-background rounded-lg w-100 ma-8 me-0">
        <div class="d-flex align-center justify-center w-100 h-100">
          <VImg
            max-width="441"
            :src="imageVariant"
            class="auth-illustration mt-16 mb-2"
          />
        </div>

        <VImg
          class="auth-footer-mask"
          :src="authThemeMask"
        />
      </div>
    </VCol>

    <VCol
      cols="12"
      lg="4"
      class="d-flex align-center justify-center"
    >
      <VCard
        flat
        :max-width="500"
        class="mt-12 mt-sm-0 pa-4"
      >
        <VCardText>
          <VNodeRenderer
            :nodes="themeConfig.app.logo"
            class="mb-6"
          />
          <h5 class="text-h5 mb-1">
            Adventure starts here 🚀
          </h5>
          <p class="mb-0">
            Make your app management easy and fun!
          </p>
        </VCardText>

        <VCardText>
          <VForm
            ref="refVForm"
            @submit.prevent="onSubmit"
          >
            <VRow>
              <!-- Username -->
              <VCol cols="12">
                <AppTextField
                  v-model="businessExecute.username"
                  autofocus
                  type="text"
                  :loading="loading"
                  :rules="[requiredValidator, alphaDashValidator]"
                  label="Username"
                />
              </VCol>

              <!-- email -->
              <VCol cols="12">
                <AppTextField
                  v-model="businessExecute.email"
                  :loading="loading"
                  :rules="[requiredValidator, emailValidator]"
                  label="Email"
                  type="email"
                />
              </VCol>

              <!-- fullname -->
              <VCol cols="12">
                <AppTextField
                  v-model="businessExecute.fullName"
                  :loading="loading"
                  :rules="[requiredValidator]"
                  label="FullName"
                  type="text"
                />
              </VCol>

              <!-- phone number -->
              <VCol cols="12">
                <AppTextField
                  v-model="businessExecute.phoneNumber"
                  :loading="loading"
                  :rules="[requiredValidator]"
                  label="Phone number"
                  type="text"
                />
              </VCol>

              <!-- date of birth -->
              <VCol cols="12">
                <AppDateTimePicker
                  v-model="businessExecute.dateOfBirth"
                  :rules="[requiredValidator]"
                  :format="'YYYY-MM-DD'"
                  label="Date of birth"
                  placeholder="yyyy-mm-dd"
                  prepend-inner-icon="tabler-calendar"
                  clearable
                  class="date-picker-input"
                />
              </VCol>

              <!-- password -->
              <VCol cols="12">
                <AppTextField
                  v-model="businessExecute.password"
                  :loading="loading"
                  :rules="[requiredValidator]"
                  label="Password"
                  style="margin-bottom: 20px"
                  :type="isPasswordVisible ? 'text' : 'password'"
                  :append-inner-icon="isPasswordVisible ? 'tabler-eye-off' : 'tabler-eye'"
                  @click:append-inner="isPasswordVisible = !isPasswordVisible"
                />

                <VLabel style="font-size:13px; color: #D0D4F1C7; margin-bottom: 4px">Gender</VLabel>
                <VSelect
                  class="select-ant mb-5"
                  ref="select"
                  :rules="[requiredValidator]"
                  v-model="businessExecute.gender"
                  :items="genders"
                >
                </VSelect>

                <VLabel style="font-size:13px; color: #D0D4F1C7; margin-bottom: 4px">Department</VLabel>
                <VSelect
                  class="select-ant"
                  ref="select"
                  :rules="[requiredValidator]"
                  v-model="businessExecute.departmentId"
                  style="margin-bottom: 20px"
                  item-value="id"
                  item-title="name"
                  :items="listDepartment"
                ></VSelect>

                <VLabel style="font-size:13px; color: #D0D4F1C7; margin-bottom: 4px">Position</VLabel>
                <VSelect
                  class="select-ant"
                  ref="select"
                  :rules="[requiredValidator]"
                  v-model="businessExecute.positionId"
                  item-value="id"
                  item-title="name"
                  :items="listPosition"
                ></VSelect>

                <div class="d-flex align-center mt-2 mb-4">
                  <VCheckbox
                    id="privacy-policy"
                    v-model="privacyPolicies"
                    :rules="[requiredValidator]"
                    inline
                  >
                    <template #label>
                      <span class="me-1">
                        I agree to
                        <a
                          href="javascript:void(0)"
                          class="text-primary"
                        >privacy policy & terms</a>
                      </span>
                    </template>
                  </VCheckbox>
                </div>

                <VBtn
                  block
                  type="submit"
                >
                  Sign up
                </VBtn>
              </VCol>

              <!-- create account -->
              <VCol
                cols="12"
                class="text-center text-base"
              >
                <span>Already have an account?</span>
                <RouterLink
                  class="text-primary ms-2"
                  :to="{ name: 'login' }"
                >
                  Sign in instead
                </RouterLink>
              </VCol>

              <VCol
                cols="12"
                class="d-flex align-center"
              >
                <VDivider />
                <span class="mx-4">or</span>
                <VDivider />
              </VCol>

              <!-- auth providers -->
              <VCol
                cols="12"
                class="text-center"
              >
                <AuthProvider />
              </VCol>
            </VRow>
          </VForm>
        </VCardText>
      </VCard>
    </VCol>
  </VRow>
</template>

<style lang="scss">
@use "@core/scss/template/pages/page-auth.scss";
</style>

<route lang="yaml">
meta:
  layout: blank
  action: read
  subject: Auth
  redirectIfLoggedIn: true
</route>
