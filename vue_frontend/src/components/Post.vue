<script lang="ts">
import { defineComponent } from 'vue';
import { useBookbuddy } from './composables/useBookbuddy';


export default defineComponent({
    props: {
        bookbuddyId:{
            type: Number,
            required: true
        },
        title: {
            type: String,
            required: true
        },
        body: {
            type: String,
            required: true
        }
    }
});


</script>

<template>
    <div class="post">
        <h1>{{ title }}</h1>
        <p>{{ body }}</p>
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