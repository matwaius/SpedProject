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
    name: 'HomePage',
    component: () => import('../views/Home.vue')
  },
  {
    path: '/sobre',
    name: 'About',
    component: () => import('../views/About.vue')
  },
  {
    path: '/reports',
    name: 'Reports',
    component: () => import('../views/Reports.vue')
  },
  {
    path: '/Users',
    name: 'Users',
    component: () => import('../views/Users.vue')
  },
  {
    path: '/Users-Insert',
    name: 'Users-Insert',
    component: () => import('../views/Users-Register.vue')
  },
  {
    path: '/Users-Edit/:id',
    name: 'Users-Edit',
    component: () => import('@/views/Users-Register.vue'),
    props: true //  router => router.params
  },
  {
    path: '/ReportCFDay',
    name: 'ReportCFDay',
    component: () => import('../reports/ReportCFDay.vue')
  },
  {
    path: '/ReportNFDay',
    name: 'ReportNFDay',
    component: () => import('../reports/ReportNFDay.vue')
  },
  {
    path: '/ReportNFDayByICMS',
    name: 'ReportNFDayByICMS',
    component: () => import('../reports/ReportNFDayByICMS.vue')
  },
  {
    path: '/ReportNFDayByICMSST',
    name: 'ReportNFDayByICMSST',
    component: () => import('../reports/ReportNFDayByICMSST.vue')
  },
  {
    path: '/ReportNFDepartureDayByRedZ',
    name: 'ReportNFDepartureDayByRedZ',
    component: () => import('../reports/ReportNFDepartureDayByRedZ.vue')
  },
  {
    path: '/ReportNFDepartureDayByUF',
    name: 'ReportNFDepartureDayByUF',
    component: () => import('../reports/ReportNFDepartureDayByUF.vue')
  },
  {
    path: '/ReportNFQtdDay',
    name: 'ReportNFQtdDay',
    component: () => import('../reports/ReportNFQtdDay.vue')
  },
  {
    path: '/ReportNFQtdDayByUF',
    name: 'ReportNFQtdDayByUF',
    component: () => import('../reports/ReportNFQtdDayByUF.vue')
  },
  {
    path: '/ReportTotalizersDay',
    name: 'ReportTotalizersDay',
    component: () => import('../reports/ReportTotalizersDay.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  routes
})

export default router
