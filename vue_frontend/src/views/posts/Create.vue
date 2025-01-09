<script setup lang="ts">
import { ref } from 'vue';
import { type Post, usePost } from '../../composables/usePost';

const { postPost } = usePost();
const post = ref<Post | null>(null);
const error = ref<string | null>(null);

const handlePostPost = async () => {
  try {
    await postPost(post.value);
    post.value = {
      id: 0,
      bookbuddyId: 0,
      title: '',
      text: ''
    };
  } catch (error) {
    error.value = error.message;
    console.error('Post error:', error);
  }
};

</script>

<template>
  <div class="new-post">
    <h1>Create a New Post</h1>
    <form @submit.prevent="handlePostPost">
      <div>
        <label for="bookbuddyId">Bookbuddy ID:</label>
        <input id="bookbuddyId" v-model="post.bookbuddyId" type="number" />
      </div>
      <div>
        <label for="title">Title:</label>
        <input id="title" v-model="post.title" type="text" />
      </div>
      <div>
        <label for="text">Text:</label>
        <textarea id="text" v-model="post.text"></textarea>
      </div>
      <button type="submit">Submit</button>
      <div v-if="error" class="error">{{ error }}</div>
    </form>
  </div>
</template>

<style scoped>
.new-post {
  padding: 20px;
}
.new-post label {
  display: block;
  margin-top: 10px;
}
.new-post input,
.new-post textarea {
  width: 100%;
  padding: 8px;
  margin-top: 5px;
}
.new-post button {
  margin-top: 20px;
  padding: 10px 20px;
}
.error {
  color: red;
  margin-top: 10px;
}
</style>