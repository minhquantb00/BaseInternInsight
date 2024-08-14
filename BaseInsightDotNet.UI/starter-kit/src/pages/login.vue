<script setup>
import LocalStorageKey from "@/constants/LocalStorageKey";
import { loginRequest } from "@/interfaces/requestModels/loginRequest";
import { AuthService } from "@/services/authService";
import AuthProvider from "@/views/pages/authentication/AuthProvider.vue";
import { useGenerateImageVariant } from "@core/composable/useGenerateImageVariant";
import { passwordValidator, requiredValidator } from "@core/utils/validators";
import authV2LoginIllustrationBorderedDark from "@images/pages/auth-v2-login-illustration-bordered-dark.png";
import authV2LoginIllustrationBorderedLight from "@images/pages/auth-v2-login-illustration-bordered-light.png";
import authV2LoginIllustrationDark from "@images/pages/auth-v2-login-illustration-dark.png";
import authV2LoginIllustrationLight from "@images/pages/auth-v2-login-illustration-light.png";
import authV2MaskDark from "@images/pages/misc-mask-dark.png";
import authV2MaskLight from "@images/pages/misc-mask-light.png";
import { VNodeRenderer } from "@layouts/components/VNodeRenderer";
import { themeConfig } from "@themeConfig";
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useToast } from "vue-toast-notification";
import { VForm } from "vuetify/components/VForm";

const router = useRouter();
const time = ref();
const authThemeImg = useGenerateImageVariant(
  authV2LoginIllustrationLight,
  authV2LoginIllustrationDark,
  authV2LoginIllustrationBorderedLight,
  authV2LoginIllustrationBorderedDark,
  true
);
const authThemeMask = useGenerateImageVariant(authV2MaskLight, authV2MaskDark);
const isPasswordVisible = ref(false);
const refVForm = ref();
const rememberMe = ref(false);
const loading = ref(false);
const disabled = ref(false);
const $toast = useToast();
const businessExecute = ref(loginRequest);

const login = async () => {
  loading.value = true;
  console.log(businessExecute.value);
  const result = await AuthService.login(businessExecute.value);
  console.log(result);
  const decode = parseJwt(result.data.accessToken);
  if (rememberMe) {
    localStorage.setItem(LocalStorageKey.ACCESS_TOKEN, result.data.accessToken);
    localStorage.setItem("refreshToken", result.data.refreshToken);
    localStorage.setItem(LocalStorageKey.USER_INFO, JSON.stringify(decode));
  } else {
    sessionStorage.setItem(
      LocalStorageKey.ACCESS_TOKEN,
      result.data.accessToken
    );
    sessionStorage.setItem("refreshToken", result.data.refreshToken);
    sessionStorage.setItem(LocalStorageKey.USER_INFO, JSON.stringify(decode));
  }
  if (result.status === 200) {
    $toast.open({
      message: result.message,
      type: "success",
      dismissible: true,
      duration: 2000,
    });
    disabled.value = true;
    time.value = setTimeout(() => {
      router.push("/");
    }, 2000);
  } else {
    $toast.open({
      message: result.message,
      type: "error",
      dismissible: true,
      duration: 2000,
    });
  }

  loading.value = false;
};

const parseJwt = (token) => {
  var base64Url = token.split(".")[1];
  var base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
  var jsonPayload = decodeURIComponent(
    window
      .atob(base64)
      .split("")
      .map(function (c) {
        return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
      })
      .join("")
  );

  return JSON.parse(jsonPayload);
};

const onSubmitForm = () => {
  refVForm.value?.validate().then(({ valid: isValid }) => {
    if (isValid) {
      login();
    }
  });
};
</script>

<template>
  <VRow no-gutters class="auth-wrapper bg-surface">
    <VCol lg="8" class="d-none d-lg-flex">
      <div class="position-relative bg-background rounded-lg w-100 ma-8 me-0">
        <div class="d-flex align-center justify-center w-100 h-100">
          <VImg
            max-width="505"
            :src="authThemeImg"
            class="auth-illustration mt-16 mb-2"
          />
        </div>

        <VImg :src="authThemeMask" class="auth-footer-mask" />
      </div>
    </VCol>

    <VCol
      cols="12"
      lg="4"
      class="auth-card-v2 d-flex align-center justify-center"
    >
      <VCard flat :max-width="500" class="mt-12 mt-sm-0 pa-4">
        <VCardText>
          <VNodeRenderer :nodes="themeConfig.app.logo" class="mb-6" />

          <h5 class="text-h5 mb-1">
            Welcome to
            <span class="text-capitalize"> {{ themeConfig.app.title }} </span>!
            👋🏻
          </h5>

          <p class="mb-0">
            Please sign-in to your account and start the adventure
          </p>
        </VCardText>

        <VCardText>
          <VAlert color="primary" variant="tonal">
            <p class="text-caption mb-2">
              Admin Email: <strong>admin@demo.com</strong> / Pass:
              <strong>admin</strong>
            </p>

            <p class="text-caption mb-0">
              Client Email: <strong>client@demo.com</strong> / Pass:
              <strong>client</strong>
            </p>
          </VAlert>
        </VCardText>

        <VCardText>
          <VForm ref="refVForm" @submit.prevent="onSubmitForm">
            <VRow>
              <!-- email -->
              <VCol cols="12">
                <AppTextField
                  v-model="businessExecute.userName"
                  label="Username"
                  type="text"
                  autofocus
                  :loading="loading"
                  :rules="[requiredValidator]"
                />
              </VCol>

              <!-- password -->
              <VCol cols="12">
                <AppTextField
                  v-model="businessExecute.password"
                  label="Password"
                  :loading="loading"
                  :type="isPasswordVisible ? 'text' : 'password'"
                  :append-inner-icon="
                    isPasswordVisible ? 'tabler-eye-off' : 'tabler-eye'
                  "
                  :rules="[requiredValidator, passwordValidator]"
                  @click:append-inner="isPasswordVisible = !isPasswordVisible"
                />

                <div
                  class="d-flex align-center flex-wrap justify-space-between mt-2 mb-4"
                >
                  <VCheckbox
                    v-model="rememberMe"
                    :loading="loading"
                    label="Remember me"
                  />
                  <a class="text-primary ms-2 mb-1" href="#">
                    Forgot Password?
                  </a>
                </div>

                <VBtn
                  block
                  type="submit"
                  :loading="loading"
                  :disabled="disabled"
                >
                  Login
                </VBtn>
              </VCol>

              <!-- create account -->
              <VCol cols="12" class="text-center">
                <span>New on our platform?</span>

                <a class="text-primary ms-2" href="#"> Create an account </a>
              </VCol>

              <VCol cols="12" class="d-flex align-center">
                <VDivider />

                <span class="mx-4">or</span>

                <VDivider />
              </VCol>

              <!-- auth providers -->
              <VCol cols="12" class="text-center">
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
</route>
