import { createRouter, createWebHistory } from 'vue-router'
import BookBuddyRegisterView from '../views/bookbuddy/Register.vue'
import BookBuddyDetailsView from '../views/bookbuddy/Details.vue'
import BookbuddyList from '@/views/bookbuddy/BookbuddyList.vue'
import SavedPosts from '@/views/bookbuddy/SavedPosts.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: '/bookbuddy/index'
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
      path: '/bookbuddy/savedposts/:id',
      name: 'bookbuddy-savedposts',
      component: SavedPosts  
    },
    {
      path: `/bookbuddy/index`,
      name: 'bookbuddy-index',
      component: BookbuddyList
    },
  ]
})

export default router
