<template>
  <div class="app-ckeditor" :class="$attrs.class">
    <VLabel
      v-if="label"
      :for="elementId"
      class="mb-1 text-body-2 text-high-emphasis"
      :text="label"
    />
    <VInput
      v-bind="{
        ...$attrs,
        class: null,
        id: elementId,
      }"
      :focused="isFocused"
    >
      <div style="width: 100%">
        <ckeditor
          :editor="ClassicEditor"
          v-bind="{ ...$attrs }"
          @focus="isFocused = true"
          @blur="isFocused = false"
        ></ckeditor>
      </div>
    </VInput>
  </div>
</template>

<script setup>
import { ClassicEditor } from "@/plugins/ckEditor/ClassicEditor";

defineOptions({
  name: "AppCkEditor",
  inheritAttrs: false,
});

const elementId = computed(() => {
  const attrs = useAttrs();
  const _elementIdToken = attrs.id || attrs.label;

  return _elementIdToken
    ? `app-ckeditor-${_elementIdToken}-${Math.random().toString(36).slice(2, 7)}`
    : undefined;
});

const label = computed(() => useAttrs().label);

const isFocused = ref(false);
</script>

<style lang="scss" scoped></style>
