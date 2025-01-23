<script setup lang="ts">
import Popup from '../../components/UI/Popup.vue';
import TextInput from '../../components/UI/TextInput.vue';
import Button from '../../components/UI/Button.vue';
import { computed, ref } from 'vue';
import { type Bookbuddy, useBookbuddy } from '../../composables/useBookbuddy';
import { useValidation } from '../../composables/useValidation';
import router from '../../router';

const { registerBookbuddy } = useBookbuddy();
const { validateEmail, validateUsername, validatePassword, validateConfirmPassword } = useValidation();

const bookbuddy: Bookbuddy = {
  id: 0,
  email: '',
  username: '',
  password: '',
};

const confirmPassword = ref('');

const showPopup = ref(false);
const popupMessage = ref('');
const shouldRedirect = ref(false);

const usernameError = ref<string | null>(null);
const emailError = ref<string | null>(null);
const passwordError = ref<string | null>(null);
const confirmPasswordError = ref<string | null>(null);

const isFormInvalid = computed(() => {
  return (
    usernameError.value !== null ||
    emailError.value !== null ||
    passwordError.value !== null ||
    confirmPasswordError.value !== null ||
    !bookbuddy.username ||
    !bookbuddy.email ||
    !bookbuddy.password ||
    !confirmPassword.value
    
  );
});

const register = async () => {
  try {
    bookbuddy.id = await registerBookbuddy(bookbuddy);
    shouldRedirect.value = true;
    showSuccessPopup();
  } catch (error: any) {
    showErrorPopup(error.message);
  }
};

function togglePopup() {
  showPopup.value = !showPopup.value;
  if (shouldRedirect.value) {
    router.push(`/bookbuddy/${bookbuddy.id}`);
  }
}

function showSuccessPopup() {
  showPopup.value = true;
  popupMessage.value = 'Account successfully registered!';
  console.log("success popup should show");
}

function showErrorPopup(message) {
  showPopup.value = true;
  popupMessage.value = 'Registration failed! \n' + message;
  console.log("error popup should show");
}
</script>


<template>
  <form class="registration-form" @submit.prevent="register">
    <h2>Register Account</h2>
    <div class="form-group">
      <TextInput v-model="bookbuddy.username" placeholder="Enter your username" label="Username" :validation="validateUsername" @update:error="usernameError = $event" />
    </div>

    <div class="form-group">
      <TextInput v-model="bookbuddy.email" placeholder="Enter your email" label="Email" type="email" :validation="validateEmail" @update:error="emailError = $event"/>
    </div>

    <div class="form-group">
      <TextInput v-model="bookbuddy.password" placeholder="Enter your password" label="Password" type="password" :validation="validatePassword" @update:error="passwordError = $event"/>
    </div>

    <div class="form-group">
      <TextInput v-model="confirmPassword" placeholder="Confirm your password" label="Confirm Password" type="password" :validation="validateConfirmPassword" :secondvalue="bookbuddy.password" @update:error="confirmPasswordError = $event"/>
    </div>

    <div class="form-actions">
      <Button text="Create Account" :disabled="isFormInvalid" />
    </div>
  </form>

  <Popup v-if="showPopup" :message="popupMessage" :onClose="togglePopup" />
</template>

<style scoped>

.registration-form {
  background: white;
  border-radius: 12px;
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
  padding: 2rem;
  width: 100%;
  max-width: 400px;
  text-align: center;
}

h2 {
  font-size: 1.75rem;
  font-weight: bold;
  color: #333;
  margin-bottom: 0.5rem;
}

.form-group {
  margin-bottom: 1.25rem;
}

input {
  width: 100%;
  padding: 0.75rem;
  font-size: 1rem;
  border: 1px solid #ddd;
  border-radius: 6px;
  box-sizing: border-box;
  transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

input:focus {
  outline: none;
  border-color: #007bff;
  box-shadow: 0 0 5px rgba(0, 123, 255, 0.3);
}

button {
  width: 100%;
  padding: 0.75rem;
  font-size: 1rem;
  font-weight: bold;
  color: white;
  background-color: #007bff;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

button:hover {
  background-color: #0056b3;
}
</style>
