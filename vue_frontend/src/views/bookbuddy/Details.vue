<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue';
import { useBookbuddy, type Bookbuddy } from '../../composables/useBookbuddy';
import { usePost, type Post } from '../../composables/usePost';
import { useRoute, useRouter } from 'vue-router';
import Popup from '../../components/UI/Popup.vue';
import TextInput from '../../components/UI/TextInput.vue';
import Button from '../../components/UI/Button.vue';
import PostComponent from '../../components/UI/Post.vue';

const { fetchBookbuddyById, updateBookbuddy } = useBookbuddy();
const { fetchPostsByBookbuddy } = usePost();
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
            <TextInput
              v-model="bookbuddy.username"
              label="Username"
              placeholder="Enter a new username"
            />
          </div>
          <div class="form-group">
            <TextInput
              v-model="bookbuddy.email"
              label="Email"
              placeholder="Enter a new email"
            />
          </div>
          <div class="form-group">
            <TextInput
              v-model="bookbuddy.password"
              label="Password"
              placeholder="Enter a new password"
            />
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
          <PostComponent
            v-for="post in posts"
            :key="post.id"
            :post="post"
          />
        </div>
      </div>
    </main>
    <Popup
      v-if="showPopup"
      :message="popupMessage"
      :onClose="togglePopup"
    />
  </div>
</template>

<style scoped>
/* General Page Styling */
.page-wrapper {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20px;
  font-family: 'Roboto', Arial, sans-serif;
  color: #333;
  background-color: #f4f4f9;
  height: 100vh;
  overflow-y: auto;
}

.page-header {
  width: 100%;
  text-align: center;
  margin-bottom: 20px;
  border-bottom: 2px solid #eaeaea;
  padding-bottom: 10px;
}

.page-header h2 {
  font-size: 26px;
  margin: 0;
  color: #444;
}

/* Content Container */
.content-container {
  width: 100%;
  max-width: 700px;
  background: #fff;
  padding: 20px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  border-radius: 12px;
}

/* Account Details Section */
.account-details {
  margin-bottom: 20px;
}

.details-display {
  display: flex;
  flex-direction: column;
  gap: 12px;
  background: #f9fafb;
  padding: 15px;
  border-radius: 8px;
  border: 1px solid #e0e0e0;
}

.detail {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 8px 0;
}

.detail strong {
  color: #555;
  font-weight: 600;
}

.detail span {
  color: #777;
  font-size: 14px;
}

.button-container {
  display: flex;
  justify-content: flex-start;
  gap: 15px;
  margin-top: 20px;
}

/* Form Styling */
.details-form {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.form-actions {
  display: flex;
  justify-content: flex-start;
  gap: 15px;
}

/* Posts Section */
.posts-section {
  margin-top: 30px;
}

.posts-section h3 {
  font-size: 22px;
  margin-bottom: 20px;
  color: #444;
}

.posts-list {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

/* Loading State */
.loading p {
  font-size: 16px;
  color: #888;
  text-align: center;
}

/* Buttons */
button {
  padding: 12px 18px;
  border: none;
  border-radius: 6px;
  font-size: 14px;
  font-weight: 500;
  cursor: pointer;
  background-color: #0056b3;
  color: white;
  transition: background-color 0.3s ease;
}

button:hover {
  background-color: #004a99;
}

button:disabled {
  background-color: #c0c0c0;
  cursor: not-allowed;
}

/* Popup Styling */
.popup {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  padding: 20px;
  background-color: white;
  border-radius: 10px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
  z-index: 1000;
}

/* Media Queries for Responsiveness */
@media (max-width: 768px) {
  .content-container {
    padding: 15px;
  }

  .detail {
    flex-direction: column;
    align-items: flex-start;
    gap: 4px;
  }

  .button-container,
  .form-actions {
    flex-direction: column;
    align-items: stretch;
  }

  button {
    width: 100%;
  }
}
</style>

