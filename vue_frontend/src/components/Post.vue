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
    console.log(`${props.post.title}, ${props.post.text}`);
    try {
        if (props.post.bookbuddyId) {
            bookbuddy.value = await fetchBookbuddyById(props.post.bookbuddyId);
        }
    } catch (error) {
        console.error('Fetch error:', error);
    }
});

</script>

<template>
    <div class="post">
        <h1>{{ post.title }}</h1>
        <h2 v-if="bookbuddy">By: {{ bookbuddy.username }}</h2>
        <p>{{ post.text }}</p>
    </div>
</template>

<style scoped>
.post {
    border: 1px solid #ccc;
    padding: 16px;
    margin: 16px 0;
}
.post h1 {
    font-size: 24px;
    margin-bottom: 8px;
}
.post p {
    font-size: 16px;
}
</style>