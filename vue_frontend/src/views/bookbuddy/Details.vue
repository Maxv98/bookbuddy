<script setup lang="ts">
import { ref, reactive, onMounted, watch } from 'vue';
import { useBookbuddy, type Bookbuddy } from '../../composables/useBookbuddy';
import { usePost, type Post, ReceivePost } from '../../composables/usePost';
import { useWebSocket } from '../../composables/useWebSocket';
import { useRoute, useRouter } from 'vue-router';
import Popup from '../../components/UI/Popup.vue';
import TextInput from '../../components/UI/TextInput.vue';
import Button from '../../components/UI/Button.vue';
import PostComponent from '../../components/UI/Post.vue';

const { fetchBookbuddyById, updateBookbuddy } = useBookbuddy();
const { fetchPostsByBookbuddy } = usePost();
const { newMessage } = useWebSocket();
const route = useRoute();
const router = useRouter();

const bookbuddy = reactive<Bookbuddy>({
  id: 0,
  username: '',
  email: '',
  password: '',
});

const posts = ref<Post[]>([]);

const showPopup = ref(false);
const popupMessage = ref('');
const isEditing = ref(false);

const fetchBookbuddyDetails = async () => {
  try {
    const id = Number(route.params.id);
    const fetchedBookbuddy = await fetchBookbuddyById(id);
    Object.assign(bookbuddy, fetchedBookbuddy);
    console.log('Fetched bookbuddy:', bookbuddy);
  } catch (error) {
    showPopup.value = true;
    popupMessage.value = 'Failed to fetch account details! \n' + error.message;
  }
};

const fetchBookbuddyPosts = async () => {
  try {
    const fetchedPosts = await fetchPostsByBookbuddy(Number(route.params.id));
    posts.value = fetchedPosts;
  } catch (error) {
    showPopup.value = true;
    popupMessage.value = 'Failed to fetch posts! \n' + error.message;
  }
};

const saveChanges = async () => {
  try {
    await updateBookbuddy(bookbuddy);
    showPopup.value = true;
    popupMessage.value = 'Account details successfully updated!';
    isEditing.value = false;
  } catch (error) {
    showPopup.value = true;
    popupMessage.value = 'Failed to update account details! \n' + error.message;
  }
};

const viewSavedPosts = () => {
  router.push(`/bookbuddy/SavedPosts/${bookbuddy.id}`);
};

const toggleEditing = () => {
  isEditing.value = !isEditing.value;
};

const togglePopup = () => {
  showPopup.value = false;
};

onMounted(() => {
  fetchBookbuddyDetails();
  fetchBookbuddyPosts();
});

watch(newMessage, async () => {
  try {
    const receivedPost = JSON.parse(newMessage.value) as ReceivePost;
    console.log('New post received:', receivedPost);

    const newPost: Post = {
      id: receivedPost.Id,
      bookbuddyId: receivedPost.BookbuddyId,
      title: receivedPost.Title,
      text: receivedPost.Text,
    };
    console.log('bookbuddyid:', bookbuddy.id, 'newpost.bookbuddyid:', newPost.bookbuddyId);
    if (newPost.bookbuddyId === bookbuddy.id) {
      console.log('Adding new post to list:', newPost);
      posts.value.push(newPost);
    }
    else {
      console.log('New post does not belong to this bookbuddy.');
    }
  } catch (error) {
    console.error("Failed to process WebSocket message:", error);
  }
});

</script>

<template>
  <div class="page-wrapper">
    <header class="page-header">
      <h2>Account Details</h2>
    </header>
    <main class="content-container">
      <div v-if="bookbuddy.id" class="account-details">
        <div v-if="!isEditing" class="details-display">
          <div class="detail">
            <strong>ID:</strong>
            <span>{{ bookbuddy.id }}</span>
          </div>
          <div class="detail">
            <strong>Username:</strong>
            <span>{{ bookbuddy.username }}</span>
          </div>
          <div class="detail">
            <strong>Email:</strong>
            <span>{{ bookbuddy.email }}</span>
          </div>
          <div class="button-container">
            <Button text="Edit Details" :onClick="toggleEditing" />
            <Button text="View Saved Posts" :onClick="viewSavedPosts" />
          </div>
        </div>

        <form v-else class="details-form" @submit.prevent="saveChanges">
          <div class="form-group">
            <TextInput v-model="bookbuddy.username" label="Username" placeholder="Enter a new username" />
          </div>
          <div class="form-group">
            <TextInput v-model="bookbuddy.email" label="Email" placeholder="Enter a new email" />
          </div>
          <div class="form-group">
            <TextInput v-model="bookbuddy.password" label="Password" placeholder="Enter a new password" />
          </div>
          <div class="form-actions">
            <Button text="Save Changes" :onClick="saveChanges" />
            <Button text="Cancel" :onClick="toggleEditing" />
          </div>
        </form>
      </div>

      <div v-else class="loading">
        <p>Loading account details...</p>
      </div>

      <div class="posts-section" v-if="posts.length">
        <h3>Posts by {{ bookbuddy.username }}</h3>
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
