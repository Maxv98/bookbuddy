<script setup lang = 'ts';>
import { onMounted, ref } from 'vue';
import { usePost, type Post } from '../src/composables/usePost';

const { createPost, fetchPosts }  = usePost();

const post = ref<Post>({
  id: 0,
  bookbuddyId: 42,
  title: '',
  text: '',
});

onMounted(async () => {
  await fetchPosts();
});

const handleCreatePost = async () => {
  try {
    await createPost(post.value);

  } catch (error) {
    console.error('Error creating post:', error);
  }
};

</script>

<template>
  <div class="post-creator">
    <h2>Create a Post</h2>
    <form @submit.prevent="handleCreatePost">
      <div class="form-group">
      </div>
      <div class="form-group">
        <label for="title">Title</label>
        <input
          type="text"
          id="title"
          v-model="post.title"
          placeholder="Enter Title"
          required
        />
      </div>
      <div class="form-group">
        <label for="text">Text</label>
        <textarea
          id="text"
          v-model="post.text"
          placeholder="Enter Post Text"
          required
        ></textarea>
      </div>
      <button type="submit" class="submit-btn">Create Post</button>
    </form>
  </div>
</template>

<style scoped>
.post-creator {
  max-width: 400px;
  margin: 0 auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  background-color: #f9f9f9;
}
.form-group {
  margin-bottom: 15px;
}
label {
  display: block;
  font-weight: bold;
  margin-bottom: 5px;
}
input,
textarea {
  width: 100%;
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 14px;
}
textarea {
  resize: vertical;
  height: 80px;
}
.submit-btn {
  width: 100%;
  padding: 10px;
  background-color: #4caf50;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 16px;
  cursor: pointer;
}
.submit-btn:hover {
  background-color: #45a049;
}
</style>
