import { defineStore } from 'pinia'

export const useRegistroStore = defineStore('registro', {
  state: () => ({
    registros: [],
    ultimoRegistro: null
  }),

  actions: {
    agregarRegistro(datos) {
      const folio = 'SIGEV-' + Date.now()

      const registro = {
        ...datos,
        folio,
        estado: 'pendiente'
      }

      this.registros.push(registro)
      this.ultimoRegistro = registro
    }
  },

  persist: true
})