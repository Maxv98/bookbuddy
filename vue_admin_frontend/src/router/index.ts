
import { createRouter, createWebHistory } from 'vue-router'

import CreatePost from '../../src/CreatePost.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'Home',
      component: CreatePost
    }
  ]
})

export default router
