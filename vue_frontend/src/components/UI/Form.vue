<script setup lang="ts">
import ButtonStandard from '@/components/UI/Button.vue';


const props = defineProps({
    title: {
        type: String,
        default: 'Placeholder'
    },
    buttonText: {
        type: String,
        default: 'Placeholder'
    },
    onSubmit: {
        type: Function,
        required: true
    }
})

const handleSubmit = () => {
    props.onSubmit();
}
</script>

<template>
    <form class="window" @submit.prevent="handleSubmit">
        <h1>{{ title }}</h1>
        <div class="profile_data">
            <slot name="input-fields"></slot>
        </div>
        <div class="register-button-container">
            <ButtonStandard :content="buttonText" />
        </div>
    </form>
</template>

<style scoped>
.window {
    background-color: #fff;
    padding: 2rem;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    max-width: 500px;
    margin: 0 auto;
    font-family: 'Arial', sans-serif;
}

h1 {
    font-size: 1.5rem;
    margin-bottom: 0.5rem;
    color: #333;
    text-align: center;
}

.profile_data {
    display: flex;
    flex-direction: column;
    gap: 1rem;
    margin-top: 0%;
}

input {
    width: 100%;
    padding: 0.75rem;
    border: 1px solid #ccc;
    border-radius: 4px;
    font-size: 1rem;
    transition: border-color 0.3s ease;
}

input:focus {
    border-color: #007BFF;
    outline: none;
}

.register-button-container {
    margin-top: 1.5rem;
    align-self: center;
}

button {
    background-color: #007BFF;
    color: #fff;
    padding: 0.75rem 1.5rem;
    border: none;
    border-radius: 4px;
    font-size: 1rem;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

button:hover {
    background-color: #0056b3;
}
</style>