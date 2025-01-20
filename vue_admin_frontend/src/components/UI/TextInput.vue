<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';

const props = defineProps<{
  modelValue: string;
  placeholder?: string;
  disabled?: boolean;
  label?: string;
}>();

const emit = defineEmits<{
  (e: 'update:modelValue', value: string): void;
}>();

function handleInput(event: Event) {
  const target = event.target as HTMLInputElement;
  emit('update:modelValue', target.value);
}
</script>

<template>
  <div class="text-input-container">
    <label v-if="label" class="text-input-label">{{ label }}
      <input type="text" :value="modelValue" :placeholder="placeholder" :disabled="disabled" class="text-input"
        @input="handleInput" />
    </label>
  </div>
</template>

<style scoped>
.text-input-container {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.text-input-label {
  font-size: 1rem;
  color: #333;
  font-weight: 600;
  align-self: flex-start;
}

.text-input {
  width: 100%;
  padding: 0.5rem 1rem;
  font-size: 1rem;
  color: #333;
  border: 1px solid #ccc;
  border-radius: 4px;
  outline: none;
  transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

.text-input:focus {
  border-color: #007bff;
  box-shadow: 0 0 4px rgba(0, 123, 255, 0.5);
}

.text-input:disabled {
  background-color: #f5f5f5;
  cursor: not-allowed;
}
</style>
