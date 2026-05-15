import { createRouter, createWebHistory } from 'vue-router'
import RegistroView from '../views/RegistroView.vue'
import ConfirmacionView from '../views/ConfirmacionView.vue'

const routes = [
  { path: '/', component: RegistroView },
  { path: '/confirmacion', component: ConfirmacionView }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

export default router