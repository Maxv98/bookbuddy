<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue';
import Popup from '../../components/UI/Popup.vue';
import PostComponent from '../../components/UI/Post.vue';
import { usePost, type Post } from '../../composables/usePost';
import { useRoute, useRouter } from 'vue-router';

const { fetchPosts } = usePost();
const route = useRoute();
const router = useRouter();

const showPopup = ref(false);
const popupMessage = ref('');

const togglePopup = () => {
    showPopup.value = false;
};

const posts = ref<Post[]>([]);

const fetchAllPosts = async () => {
    try {
        const fetchedPosts = await fetchPosts();
        posts.value = fetchedPosts;
    } catch (error) {
        showPopup.value = true;
        popupMessage.value = 'Failed to fetch posts! \n' + error.message;
    }
};

onMounted(() => {
    fetchAllPosts();
});

</script>

<template>
    <div class="page-wrapper">
        <header class="page-header">
            <h2>Posts</h2>
        </header>
        <main class="content-container">
            <div class="posts-section" v-if="posts.length">
                <div class="posts-list">
                    <PostComponent v-for="post in posts" :key="post.id" :post="post" />
                </div>
            </div>
        </main>
        <Popup v-if="showPopup" :message="popupMessage" :onClose="togglePopup" />
    </div>
</template>

<style scoped>

.page-wrapper {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20px;
  font-family: Arial, sans-serif;
  color: #333;
  background-color: #f9f9f9;
  overflow-y: scroll;
  height: 100vh;
}

.page-header {
  width: 100%;
  text-align: center;
  margin-bottom: 20px;
  border-bottom: 2px solid #ddd;
  padding-bottom: 10px;
}

.page-header h2 {
  font-size: 24px;
  margin: 0;
  color: #555;
}

.content-container {
  width: 100%;
  max-width: 600px;
  background: #fff;
  padding: 20px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
}

.account-details {
  margin-bottom: 20px;
}

.details-display {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.detail {
  display: flex;
  justify-content: space-between;
  padding: 5px 0;
}

.detail strong {
  color: #444;
}

.detail span {
  color: #666;
}

.button-container {
  display: flex;
  justify-content: space-between;
  gap: 10px;
  margin-top: 15px;
}

button {
  padding: 10px 15px;
  border: none;
  border-radius: 5px;
  font-size: 14px;
  cursor: pointer;
}

button:hover {
  opacity: 0.9;
}

/* Form styling */
.details-form {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 5px;
}

.form-actions {
  display: flex;
  justify-content: space-between;
  gap: 10px;
}

.posts-section {
  margin-top: 30px;
}

.posts-section h3 {
  font-size: 20px;
  margin-bottom: 15px;
  color: #555;
}

.posts-list {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.loading p {
  font-size: 16px;
  color: #777;
  text-align: center;
}

</style>
