import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

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
  },
  {
    path: '/sobre',
    name: 'About',
    component: () => import('../views/About.vue')
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: () => import('../views/Dashboard.vue')
  },
  {
    path: '/usuarios',
    name: 'Users',
    component: () => import('../views/Users.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  routes
})

export default router
