<script setup lang="ts">

import Button from "@/components/UI/Button.vue";

const props = defineProps({
  show: Boolean,
  buttonText: String
})

const emit = defineEmits(['close']);

const closePopup = () => {
  emit('close');
};

</script>

<template>
  <div :class="['popup-overlay', { 'popup-show': show }]">
  <div class="popup-content">
    <div class="popup-text">
    <slot/>
    </div>
    <Button @click="closePopup" :content="buttonText"/>
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
  z-index: -1;
  opacity: 0;
  transition: all 0.3s ease;
}

.popup-show {
  opacity: 1;
  z-index: 1;
}

.popup-content {
  background: white;
  padding: 2rem;
  border-radius: 0.5rem;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  position: relative;
  transition: transform 0.3s ease;
  transform: scale(0.9);
  max-width: 50%;
}

.popup-text {
  margin-bottom: 1rem;
}

.popup-show .popup-content {
  transform: scale(1);
}

.close-button {
  position: absolute;
  top: 0.5rem;
  right: 0.5rem;
  background: none;
  border: none;
  font-size: 1.5rem;
  cursor: pointer;
}

</style>