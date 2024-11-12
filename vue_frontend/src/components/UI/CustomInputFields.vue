<script setup lang="ts">
</script>

<template>
  <div class="profile_data">
    <slot></slot>
  </div>
</template>

<style scoped>
.profile_data {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}
</style>