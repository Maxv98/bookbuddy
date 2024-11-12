<script setup>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { useBookbuddy } from '@/components/composables/useBookbuddy';
import ProfileInformation from '@/components/UI/ProfileInformation.vue';
import Popup from '@/components/UI/Popup.vue';

const { Get, Update } = useBookbuddy();
const route = useRoute();
const id = route.params.id;
const bookbuddy = ref({ username: '', email: '', password: '' });
const error = ref(null);

onMounted(async () => {
    try {
        const data = await Get(id);
        bookbuddy.value = data;
        console.log(`${bookbuddy.value.email}`);
    } catch (err) {
        error.value = err.message;
        console.error('Fetch error:', err);
    }
});
</script>

<template>
    <Popup v-if="error" @close="error = null">{{ error }}</Popup>
    <ProfileInformation :bookbuddy="bookbuddy" @updateProfile="updateProfile" :editable="false" />
</template>
  
<style scoped>
</style>