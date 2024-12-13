<template>
    <div class="post">
        <h1>{{ post.title }}</h1>
        <p>{{ post.body }}</p>
    </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
    name: 'Post',
    props: {
        post: {
            type: Object as () => { title: string; body: string },
            required: true
        }
    }
});
</script>

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