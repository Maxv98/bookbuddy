<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import PostComponent from '../../components/UI/Post.vue';
import { type Post, usePost } from '../../composables/usePost';

const { fetchPostById } = usePost();
const route = useRoute();
const postId = Number(route.params.id);

const post = ref<Post>(null);
const error = ref<string | null>(null);

onMounted(async () => {
  try {
    console.log(`Fetching post with id: ${postId}`);
    post.value = await fetchPostById(postId);
    console.log(`${post.value.title}`);
  } catch (err: any) {
    error.value = err.message;
    console.error('Fetch error:', err);
  }
});
</script>

<template>
  <div class="details">
    <div v-if="post">
      <PostComponent :post="post" />
    </div>
    <div v-else>
      <p>"loading"</p>
  </div>
  </div>
</template>

<style scoped>
.details {
  padding: 20px;
}
</style>