<script setup lang="ts">
const props = defineProps({
  id: {
    type: String,
    required: false
  },
  label: {
    type: String,
    required: false
  },
  modelValue: {
    type: String,
    required: true
  },
  type: {
    type: String,
    default: 'text'
  },
  placeholder: {
    type: String,
    required: false
  },
  readonly: {
    type: Boolean,
    default: false
  },
  required: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits(['update:modelValue']);

const updateValue = (event: Event) => {
  emit('update:modelValue', (event.target as HTMLInputElement).value);
};
</script>

<template>
  <div class="input-container">
    <label v-if="label">{{ label }}</label>
    <input
      :id="id"
      :type="type"
      :value="modelValue"
      @input="updateValue"
      :placeholder="placeholder"
      :readonly="readonly"
      :required="required"
    />
  </div>
</template>

<style scoped>
.input-container {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

label {
  margin-bottom: 0.5rem; /* Space between label and input */
}

input {
  width: 400px; /* Set a fixed width */
  max-width: 100%; /* Responsive */
  padding: 0.5rem;
  border: 1px solid #ccc; /* Default border color */
  border-radius: 4px; /* Rounded corners */
  font-size: 1rem;
  transition: border-color 0.3s ease; /* Smooth transition */
}

input:focus {
  border-color: #007BFF; /* Change border color when focused */
  outline: none; /* Remove default outline */
}
</style>