<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { type Bookbuddy, useBookbuddy } from '@/composables/useBookbuddy';
import { useRouter } from 'vue-router';


const { fetchBookbuddies } = useBookbuddy();
const bookbuddies = ref<Bookbuddy[]>([]);
const router = useRouter();

onMounted(async () => {
  try {
    bookbuddies.value = await fetchBookbuddies();
  } catch (error) {
    console.error('Error fetching bookbuddies:', error);
  }
});

const navigateToDetails = (id: number) => {
  router.push({ name: 'bookbuddy-get', params: { id } });
};

</script>

<template>
  <div class="bookbuddy-list">
    <h1>Bookbuddies</h1>
    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>Username</th>
          <th>Email</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="bookbuddy in bookbuddies" :key="bookbuddy.id" @click="navigateToDetails(bookbuddy.id)" class="clickable-row">
          <td>{{ bookbuddy.id }}</td>
          <td>{{ bookbuddy.username }}</td>
          <td>{{ bookbuddy.email }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
.bookbuddy-list {
  padding: 20px;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th,
td {
  border: 1px solid #ddd;
  padding: 8px;
}

th {
  background-color: #f2f2f2;
  text-align: left;
}
</style>