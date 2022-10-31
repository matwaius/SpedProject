import Vue from 'vue'
import VueRouter from 'vue-router'
import EnsureIsAuthenticated from '../middlewares/ensureIsAuthenticated'
Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    redirect: '/home'
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
    meta:{
      middleware: [EnsureIsAuthenticated]
    },
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
    path: '/ReportNFDayByUF',
    name: 'ReportNFDayByUF',
    component: () => import('../reports/ReportNFDayByUF.vue')
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

function nextFactory(context, middleware, index) {
  const subsequentMiddleware = middleware[index];
  // If no subsequent Middleware exists,
  // the default `next()` callback is returned.
  if (!subsequentMiddleware) return context.next;
  return (...parameters) => {
    // Run the default Vue Router `next()` callback first.
    context.next(...parameters);
    // Then run the subsequent Middleware with a new
    // `nextMiddleware()` callback.
    const nextMiddleware = nextFactory(context, middleware, index + 1);
    subsequentMiddleware({ ...context, next: nextMiddleware });
  };
  }
  
  router.beforeEach((to, from, next) => {
  if (to.meta.middleware) {
    const middleware = Array.isArray(to.meta.middleware)
      ? to.meta.middleware
      : [to.meta.middleware];

    const context = {
      from,
      next,
      router,
      to,
    };
    const nextMiddleware = nextFactory(context, middleware, 1);
    return middleware[0]({ ...context, next: nextMiddleware });
  }
  
  return next();
  });
  

export default router
