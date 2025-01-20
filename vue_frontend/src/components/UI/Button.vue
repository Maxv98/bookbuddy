<script setup lang="ts">
import { defineProps } from 'vue';

const props = defineProps<{
  text: string;
  onClick?: () => void | Promise<void>;
}>();

async function handleClick() {
  if (props.onClick) {
    try {
      await props.onClick();
    } catch (error) {
      console.error('Error in onClick handler:', error);
    }
  } else {
    console.log('No onClick handler provided');
  }
}
</script>

<template>
  <button class="custom-button" @click="handleClick">
    {{ props.text }}
  </button>
</template>

<style scoped>
.custom-button {
  display: inline-block;
  padding: 0.75rem 1.5rem;
  font-size: 1rem;
  font-weight: 600;
  color: #fff;
  background-color: #007bff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.2s ease;
}

.custom-button:hover {
  background-color: #0056b3;
}

.custom-button:active {
  transform: scale(0.95);
}

.custom-button:focus {
  outline: none;
  box-shadow: 0 0 4px rgba(0, 123, 255, 0.5);
}
</style>
