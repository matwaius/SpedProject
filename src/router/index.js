import { createRouter, createWebHistory } from 'vue-router'
import Login from '../views/Signin.vue'
import Register from '../views/Register.vue'

const routes = [
  {
    path: '/',
    name: 'Home'
  },
  {
    path: '/signin',
    name: 'Signin',
    component: () => import('../views/Signin.vue')
  },
  {
    path: '/register',
    name: 'Register',
    component: () => import('../views/Register.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
