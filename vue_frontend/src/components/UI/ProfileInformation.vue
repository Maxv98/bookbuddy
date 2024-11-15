<script setup lang="ts">
import { ref, PropType, watch } from 'vue';
import InputField from '@/components/UI/InputField.vue';
import ButtonStandard from '@/components/UI/Button/Standard.vue';
import CustomizableInputFields from '@/components/UI/CustomInputFields.vue';

const props = defineProps({
  bookbuddy: {
    type: Object as PropType<{ username: string; email: string; password: string }>,
    required: true
  },
  editable: {
    type: Boolean,
    default: false
  }
});

const isEditable = ref(props.editable);

const emit = defineEmits(['updateProfile']);

const toggleEdit = () => {
  console.log('toggleEdit function called');
  console.log('isEditable before:', isEditable.value);
  isEditable.value = !isEditable.value;
  console.log('isEditable after:', isEditable.value);
};

const saveProfile = () => {
  emit('updateProfile', { ...props.bookbuddy });
  isEditable.value = false;
};

// Watch for changes in the editable prop to update isEditable
watch(() => props.editable, (newVal) => {
  isEditable.value = newVal;
});
</script>

<template>
  <div class="profile_page">
    <h1>Profile Information</h1>
    <CustomizableInputFields>
      <InputField id='username' label="Username" v-model="bookbuddy.username" :readonly="!isEditable" required/>
      <InputField id='email' label="Email" v-model="bookbuddy.email" :readonly="!isEditable" required/>
      <InputField id='password' label="Password" v-model="bookbuddy.password" type="password" :readonly="!isEditable" required/>
    </CustomizableInputFields>
    <div class="button_container">
      <ButtonStandard v-if="!isEditable" :content="'Edit Profile'" @click="toggleEdit" />
      <ButtonStandard v-if="isEditable" :content="'Save Profile'" @click="saveProfile" />
    </div>
  </div>
</template>

<style scoped>
.profile_page {
  background-color: #fff;
  padding: 2rem;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  max-width: 600px;
  margin: 0 auto;
  font-family: 'Arial', sans-serif;
}

h1 {
  font-size: 1.5rem;
  margin-bottom: 1rem;
  color: #333;
  text-align: center;
}

.profile_data {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.button_container {
  margin-top: 1.5rem;
  text-align: center;
}
</style>