<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useBookbuddy, type Bookbuddy } from '../../composables/useBookbuddy';
import { useRouter } from 'vue-router';
import Popup from '../../components/UI/Popup.vue';

const { fetchBookbuddies } = useBookbuddy();
const bookbuddies = ref<Bookbuddy[]>([]);
const showPopup = ref(false);
const popupMessage = ref('');
const router = useRouter();

const getBookbuddies = async () => {
  try {
    bookbuddies.value = await fetchBookbuddies();
  } catch (error) {
    showPopup.value = true;
    popupMessage.value = 'Failed to fetch bookbuddies! \n' + error.message;
  }
};

const viewBookbuddy = (id: number) => {
  router.push(`/bookbuddy/${id}`);
};

const togglePopup = () => {
  showPopup.value = false;
};

onMounted(() => {
  getBookbuddies();
});
</script>

<template>
  <div class="bookbuddy-list-page">
    <h2>Bookbuddies</h2>
    <div v-if="bookbuddies.length" class="bookbuddies-list">
      <div v-for="buddy in bookbuddies" :key="buddy.id" class="bookbuddy-card" @click="viewBookbuddy(buddy.id)">
        <h3>{{ buddy.username }}</h3>
        <p>{{ buddy.email }}</p>
      </div>
    </div>
    <div v-else class="loading">
      <p v-if="!showPopup">Loading bookbuddies...</p>
    </div>

    <Popup v-if="showPopup" :message="popupMessage" :onClose="togglePopup" />
  </div>
</template>

<style scoped>
.bookbuddy-list-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 2rem;
  font-family: Arial, sans-serif;
  color: #333;
  background-color: #f9f9f9;
  min-height: 100vh;
}

h2 {
  font-size: 2rem;
  margin-bottom: 1.5rem;
  color: #555;
}

.bookbuddies-list {
  display: flex;
  flex-wrap: wrap;
  gap: 1.5rem;
  justify-content: center;
  width: 100%;
  max-width: 1200px;
}

.bookbuddy-card {
  background-color: #ffffff;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  border-radius: 0.5rem;
  padding: 1rem;
  text-align: center;
  width: 200px;
  cursor: pointer;
  transition: transform 0.2s, box-shadow 0.2s;
  border: 1px solid #e5e5e5;
}

.bookbuddy-card:hover {
  transform: translateY(-8px);
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
}

.bookbuddy-card h3 {
  font-size: 1.2rem;
  margin-bottom: 0.5rem;
  color: #444;
}

.bookbuddy-card p {
  font-size: 0.9rem;
  color: #666;
}

.loading {
  font-size: 1rem;
  color: #888;
  margin-top: 2rem;
}

/* Responsive Design */
@media (max-width: 768px) {
  .bookbuddies-list {
    gap: 1rem;
  }

  .bookbuddy-card {
    width: 150px;
    padding: 0.8rem;
  }

  .bookbuddy-card h3 {
    font-size: 1rem;
  }

  .bookbuddy-card p {
    font-size: 0.8rem;
  }
}

@media (max-width: 480px) {
  .bookbuddy-card {
    width: 100%;
    max-width: 300px;
  }

  .bookbuddies-list {
    flex-direction: column;
    gap: 1rem;
  }
}
</style>