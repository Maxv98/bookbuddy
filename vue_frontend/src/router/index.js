import { createRouter, createWebHistory } from 'vue-router'
import BookBuddyRegisterView from '../views/bookbuddy/Register.vue'
import BookBuddyDetailsView from '../views/bookbuddy/Details.vue'
import BookbuddyList from '@/views/bookbuddy/BookbuddyList.vue'

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
    }
  ]
})

export default router
