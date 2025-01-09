<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { type Bookbuddy, useBookbuddy } from '../composables/useBookbuddy';
import { type Post } from '../composables/usePost';

const { fetchBookbuddyById } = useBookbuddy();
const bookbuddy = ref<Bookbuddy | null>(null);

const props = defineProps({
  post: {
    type: Object as () => Post,
    required: true,
  },
});


onMounted(async () => {
  try {
    bookbuddy.value = await fetchBookbuddyById(props.post.bookbuddyId);
  } catch (error) {
    console.error('Fetch error:', error);
  }
});

</script>

<template>
    <div class="post">
        <h1>{{post.title}}</h1>
        <h2>By: {{ bookbuddy.username }}</h2>
        <p>{{ post.text }}</p>
    </div>
</template>