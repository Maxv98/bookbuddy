<script setup lang="ts">
import { IconType } from "@/types/global-types";
import Icon from "@/components/UI/Icon.vue";
import { PropType } from 'vue';

const props = defineProps({
  content: {
    type: String,
    required: false
  },
  icon: {
    type: String as PropType<IconType>,
    required: false
  },
  action: {
    type: Function as PropType<(event: MouseEvent) => void>,
    required: false,
    default: () => {}
  }
});

const emit = defineEmits(['click']);

const handleClick = (event: MouseEvent) => {
  emit('click', event);
  props.action(event);
};
</script>

<template>
  <div class="button-container">
    <button @click="handleClick" class="button">
      {{ content }}
      <Icon v-if="icon" class="icon" :icon="icon" />
    </button>
  </div>
</template>

<style scoped>
.button-container {
  display: flex;
  justify-content: center; /* Center horizontally */
  align-items: center; /* Center vertically */
  width: 100%; /* Full width of the parent container */
  margin-top: 1rem; /* Space above the button */
}

.button {
  background-color: #007BFF; /* Match form button color */
  color: #fff; /* White text */
  padding: 0.75rem 1.5rem; /* Padding for spacing */
  border: none; /* Remove default border */
  border-radius: 4px; /* Rounded corners */
  font-size: 1rem; /* Font size */
  cursor: pointer; /* Pointer cursor on hover */
  transition: background-color 0.3s ease; /* Smooth transition */
  display: flex; /* Flexbox for alignment */
  align-items: center; /* Center items vertically */
  justify-content: center; /* Center items horizontally */
}

.button:hover {
  background-color: #0056b3; /* Darker blue on hover */
}

.icon {
  margin-left: 0.5rem; /* Space between text and icon */
}
</style>