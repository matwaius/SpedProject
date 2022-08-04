import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    name: 'Begin'
  },
  {
    path: '/login',
    name: 'Signin',
    component: () => import('../views/Signin.vue')
  },
  {
    path: '/registrar',
    name: 'Register',
    component: () => import('../views/Register.vue')
  },
  {
    path: '/home',
    name: 'Home',
    component: () => import('../views/Home.vue')
  }

]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
