<script setup>
import { RouterView } from 'vue-router'
</script>

<template>
  <div class="centralized-container">
    <RouterView/>
  </div>
</template>

<style scoped>
 .centralized-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: lightgray; /* Helps you see if it's full height */
}
</style>
