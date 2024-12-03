<script setup lang='ts'>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { type Bookbuddy, useBookbuddy } from '@/components/composables/useBookbuddy';
import Popup from '@/components/UI/Popup.vue';
import InputField from '@/components/UI/InputField.vue';
import CustomInputFields from '@/components/UI/CustomInputFields.vue';
import Button from '@/components/UI/Button.vue';


const { fetchBookbuddyById, updateBookbuddy, deleteBookbuddy } = useBookbuddy();
const bookbuddy = ref<Bookbuddy>({
    id: 0,
    email: '',
    username: '',
    password: '',
});

const route = useRoute();
const id = Number(route.params.id);

const error = ref(null);
const isEditable = ref(false);

onMounted(async () => {
    try {
        bookbuddy.value = await fetchBookbuddyById(id);
        console.log(`${bookbuddy.value.email}`);
    } catch (err) {
        error.value = err.message;
        console.error('Fetch error:', err);
    }
});

const update = async (updatedBookbuddy) => {
    try {
        await updateBookbuddy(updatedBookbuddy);
        isEditable.value = false;
    } catch (err: any) {
        error.value = err.message;
        console.error('Update error:', err);
    }
};

const toggleEdit = () => {
  console.log('toggleEdit function called');
  console.log('isEditable before:', isEditable.value);
  isEditable.value = !isEditable.value;
  console.log('isEditable after:', isEditable.value);
};

</script>

<template>
    <Popup v-if="error" @close="error = null">{{ error }}</Popup>
    <div class="profile_page">
      <h1>Profile Information</h1>
      <div class = "profile_data">
        <InputField id="username" label="Username" v-model="bookbuddy.username" :readonly="!isEditable" required/>
        <InputField id="email" label="Email" v-model="bookbuddy.email" :readonly="!isEditable" required/>
        <InputField id="password" label="Password" v-model="bookbuddy.password" type="password" :readonly="!isEditable" required/>
      </div>
      <div class="button_container">
        <Button v-if="!isEditable" :content="'Edit Profile'" @click="toggleEdit" />
        <Button v-if="isEditable" :content="'Save Profile'" @click="update(bookbuddy)" />
      </div>
    </div>
  </template>
  
<style scoped>
.profile_data {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}
</style>