import { createRouter, createWebHashHistory } from 'vue-router'
import RegistroView from '../views/RegistroView.vue'
import ConfirmacionView from '../views/ConfirmacionView.vue'
import InformateView from '../views/InformateView.vue'

const routes = [
  { path: '/', component: RegistroView },
  { path: '/confirmacion', component: ConfirmacionView },
  { path: '/informate', component: InformateView }
]

const router = createRouter({
  history: createWebHashHistory(import.meta.env.BASE_URL),
  routes,
})

export default router