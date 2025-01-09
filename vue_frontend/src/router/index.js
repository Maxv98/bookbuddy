import { createRouter, createWebHistory } from 'vue-router'
import BookBuddyRegisterView from '../views/bookbuddy/Register.vue'
import BookBuddyDetailsView from '../views/bookbuddy/Details.vue'
import BookbuddyList from '@/views/bookbuddy/BookbuddyList.vue'
import PostsGet from '@/views/posts/Details.vue'
import PostsCreate from '@/views/posts/Create.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: '/bookbuddy/register'
    },
    {
      path: '/bookbuddy/register',
      name: 'bookbuddy-register',
      component: BookBuddyRegisterView
    },
    {
      path: '/bookbuddy/:id',
      name: 'bookbuddy-get',
      component: BookBuddyDetailsView
    },
    {
      path: `/bookbuddy/index`,
      name: 'bookbuddy-index',
      component: BookbuddyList
    },
    {
      path: `/posts/index`,
      name: 'posts-index',
    },
    {
      path: `/posts/:id`,
      name: 'posts-get',
      component: PostsGet
    },
    {
      path: `/posts/create`,
      name: 'posts-create',
      component: PostsCreate
    }
  ]
})

export default router
