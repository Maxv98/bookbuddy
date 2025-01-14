<script setup>
import { ref } from 'vue';
import Button from '../../components/UI/Button.vue';

const props = defineProps({
  message: {
    type: String,
    default: 'This is a popup message!',
  },
  show: {
    type: Boolean,
    default: true,
  },
});

const emit = defineEmits(['close']);

const isVisible = ref(props.show);

function closePopup() {
  isVisible.value = false;
  emit('close');
}
</script>

<template>
  <div v-if="isVisible" class="popup-overlay">
    <div class="popup">
      <div class="popup-message">
        <slot>{{ message }}</slot>
      </div>
      <Button text="Close" :onClick="closePopup"/>
    </div>
  </div>
</template>

<style scoped>
.popup-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.popup {
  background: white;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  text-align: center;
}

.popup-message {
  margin-bottom: 16px;
  font-size: 16px;
  color: #333;
}
</style>
