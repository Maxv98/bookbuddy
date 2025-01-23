<script setup>
import { RouterView } from 'vue-router'
import Header from './components/UI/Header.vue';
</script>

<template>
  <div class="app-container">
    <Header/>
    <div class="content-container">
      <RouterView/>
    </div>
  </div>
</template>

<style scoped>
.app-container {
  display: flex;
  flex-direction: column;
  height: 100vh;
}

.content-container {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-grow: 1;
  background-color: lightgray; /* Helps you see if it's full height */
}
</style>
