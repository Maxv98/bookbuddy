<script setup lang="ts">
import { defineProps, ref } from 'vue';
import { type Post } from '../../composables/usePost';
import { useBookbuddy } from '../../composables/useBookbuddy';
import Button from './Button.vue';
import TextInput from './TextInput.vue';
import Popup from './Popup.vue';

const { savePost } = useBookbuddy();

const props = defineProps<{
    post: Post;
}>();

const usernameInput = ref('');

const showPopup = ref(false);
const popupMessage = ref('');

const togglePopup = () => {
    showPopup.value = !showPopup.value;
};

const handleSavePost = () => {
    console.log("username: ", usernameInput.value, "postid: ", props.post.id);
    try {
        if (!usernameInput.value.trim()) {
        popupMessage.value = 'Please enter your username!';
        togglePopup();
    }
    else {
        savePost(usernameInput.value, props.post.id);
        popupMessage.value = `Post "${props.post.title}" saved to account "${usernameInput.value}" successfully!`;
        togglePopup();
    }
    }
    catch (error) {
        popupMessage.value = `Failed to save post! \n${error.message}`;
        togglePopup();
    }
    
};
</script>

<template>
    <div class="post-container">
        <header class="post-header">
            <h2 class="post-title">{{ post.title }}</h2>
            <p class="post-username">By: {{ post.bookbuddyUsername }}</p>
        </header>
        <main class="post-body">
            <p class="post-text">{{ post.text }}</p>
        </main>
        <footer class="post-footer">
            <TextInput v-model="usernameInput" placeholder="Enter your username" />
            <Button text="Save Post" :onClick="handleSavePost"/>
        </footer>
    </div>
    <Popup
      v-if="showPopup"
      :message="popupMessage"
      :onClose="togglePopup"
    />
</template>

<style scoped>
.post-container {
  background-color: #ffffff;
  border: 1px solid #e0e0e0;
  border-radius: 8px;
  padding: 20px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  max-width: 600px;
  margin: 0 auto;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.post-container:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 10px rgba(0, 0, 0, 0.15);
}

.post-header {
  margin-bottom: 16px;
}

.post-title {
  font-size: 22px;
  font-weight: bold;
  color: #333333;
  margin: 0;
}

.post-username {
  font-size: 14px;
  color: #777777;
  margin: 4px 0 0 0;
}

.post-body {
  margin: 16px 0;
}

.post-text {
  font-size: 16px;
  color: #666666;
  line-height: 1.6;
  margin: 0;
}

.post-footer {
  display: flex;
  flex-direction: column;
  gap: 12px;
  align-items: flex-start;
}

.post-footer .text-input {
  width: 100%;
}

.post-footer .button {
  align-self: flex-end;
}

@media (min-width: 768px) {
  .post-footer {
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
  }
}
</style>
