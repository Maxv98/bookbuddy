<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import PostComponent from '../../components/UI/Post.vue';
import { usePost, type Post } from '../../composables/usePost';
import { useBookbuddy, type Bookbuddy } from '../../composables/useBookbuddy';

const { fetchPostsSavedByBookbuddy } = usePost();
const { fetchBookbuddyById } = useBookbuddy();
const route = useRoute();

const posts = ref<Post[]>([]);
const bookbuddy = ref<Bookbuddy>();
const loading = ref(true);
const errorMessage = ref('');

const fetchPosts = async () => {
    try {
        posts.value = await fetchPostsSavedByBookbuddy(Number(route.params.id));
    } catch (error) {
        errorMessage.value = 'Failed to fetch posts: ' + error.message;
    } finally {
        loading.value = false;
    }
};

const fetchBookbuddy = async () => {
    bookbuddy.value = await fetchBookbuddyById(Number(route.params.id));
    console.log("bookbuddy: ", bookbuddy.value);
};

onMounted(() => {
    fetchPosts();
    fetchBookbuddy();
});
</script>

<template>
    <div class="posts-collection-page">
        <h1 v-if="bookbuddy">All Posts saved by {{ bookbuddy.username }}</h1>
        <h1 v-else>Loading...</h1>
        <div v-if="loading">Loading posts...</div>
        <div v-if="errorMessage" class="error">{{ errorMessage }}</div>
        <div v-else>
            <PostComponent v-for="post in posts" :key="post.id" :post="post" />
        </div>
    </div>
</template>

<style scoped>
/* General styling for the posts collection page */
.posts-collection-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20px;
  font-family: Arial, sans-serif;
  background-color: #f5f5f5;
  height: 100vh;
  overflow: scroll;
}

.posts-collection-page h1 {
  font-size: 28px;
  color: #333;
  margin-bottom: 20px;
  text-align: center;
}

/* Loading state */
.posts-collection-page div[loading] {
  font-size: 16px;
  color: #777;
  margin-top: 10px;
}

/* Error message styling */
.error {
  color: #d9534f;
  background-color: #f9d6d5;
  padding: 10px 15px;
  border: 1px solid #d9534f;
  border-radius: 5px;
  text-align: center;
  margin-bottom: 20px;
}

/* Posts list container */
.posts-collection-page > div {
  display: flex;
  flex-direction: column;
  gap: 15px;
  width: 100%;
  max-width: 600px;
}

/* Responsive styling for smaller screens */
@media (max-width: 768px) {
  .posts-collection-page h1 {
    font-size: 24px;
  }

  .posts-collection-page > div {
    gap: 10px;
  }
}

</style>
