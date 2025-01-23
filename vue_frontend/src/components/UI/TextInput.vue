<script setup lang="ts">
import {ref, watch } from 'vue';

const props = defineProps<{
  modelValue: string;
  placeholder?: string;
  disabled?: boolean;
  label?: string;
  type?: string;
  validation?: (value: string, secondValue?: string) => Promise<string | null>;
  secondvalue?: string;
}>();

const errorMessage = ref<string | null>(null);

const emit = defineEmits<{
  (event: 'update:modelValue', value: string): void;
  (event: 'update:error', value: string | null): void;
}>();

async function handleInput(event: Event) {
  const target = event.target as HTMLInputElement;
  const value = target.value;
  emit('update:modelValue', value);
  if (props.validation) {
    errorMessage.value = await props.validation(value, props.secondvalue);
    emit('update:error', errorMessage.value);
  }
}
</script>

<template>
  <div class="text-input-container">
    <label v-if="label" class="text-input-label">{{ label }}
      <input :type="type || 'text'" :v-model="modelValue" :placeholder="placeholder" :disabled="disabled" class="text-input"
        @input="handleInput" />
    </label>
    <span v-if="errorMessage" class="error-message">{{ errorMessage }}</span>
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
  text-align: left;
}

.text-input {
  width: 100%;
  padding: 0.5rem 1rem;
  font-size: 1rem;
  color: #333;
  border: 1px solid #ccc;
  border-radius: 8px;
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

.error-message {
  color: #721c24;
  background-color: #f8d7da;
  border: 1px solid #f5c6cb;
  border-radius: 4px;
  padding: 0.5rem;
  font-size: 0.875rem;
  text-align: left; 
  align-self: flex-start;
  display: flex;
  align-items: center;
}

.error-message::before {
  content: '⚠️';
  margin-right: 0.5rem;
}
</style>
