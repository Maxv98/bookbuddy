<script setup>
import InputField from '@/components/UI/InputField.vue';
import Form from '@/components/UI/Form.vue';
import Popup from '@/components/UI/Popup.vue';
import { ref } from 'vue';
import { useBookbuddy } from '@/components/composables/useBookbuddy';
import router from '@/router';

const { Register } = useBookbuddy();

const showPopup = ref(false);
const popupMessage = ref('');
const shouldRedirect = ref(false);

const id = ref(null);
const email = ref('');
const username = ref('');
const password = ref('');

function togglePopup() {
  showPopup.value = !showPopup.value;
  if (shouldRedirect.value) {
    router.push(`/bookbuddy/${id.value}`);
  }
}

function showSuccessPopup() {
  showPopup.value = true;
  popupMessage.value = 'Account successfully registered!';
}

function showErrorPopup(message) {
  showPopup.value = true;
  popupMessage.value = 'Registration failed! \n' + message;
}

const registerBookbuddy = async () => {
  try {
    const bookbuddy = {
      email: email.value,
      username: username.value,
      password: password.value,
    }
    id.value = await Register(bookbuddy);
    shouldRedirect.value = true;
    showSuccessPopup();
  } catch (error) {
    showErrorPopup(error.message);
  }
};
</script>


<template>
  <Popup :show="showPopup" :buttonText="'Close'" @close="togglePopup">{{ popupMessage }}</Popup>
  <div class="register_page">
    <Form :onSubmit="registerBookbuddy" title="Create a BookBuddy Account" buttonText="Create Account">
      <template #input-fields>
        <InputField :label="'Email'" v-model="email" type="email" required />
        <InputField :label="'Username'" v-model="username" type="text" required />
        <InputField :label="'Password'" v-model="password" type="password" required />
      </template>
    </Form>
  </div>
</template>

<style scoped>
h1 {
  text-align: center; /* Center the header */
}

.register_page {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
}

.window {
  display: flex;
  width: fit-content; /* Fixed width for form */
  padding: 1rem;
  flex-direction: column;
  text-align: center;
  gap: 1rem;
  border-radius: 1rem;
  background: var(--white-95, rgba(250, 250, 250, 0.95));
  box-shadow: var(--shadow-four-sides);
}

.profile_data {
  display: flex;
  padding: 0 1rem;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 0.625rem;
}

.register-button-container {
  margin-top: 1rem;
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 2.5rem;
  align-self: stretch;
}
</style>