import { defineStore } from 'pinia'

const casillas = [
  { id: 1, nombre: 'Casilla 01 - Centro', direccion: 'Escuela Primaria Benito Juárez, Av. Juárez 100' },
  { id: 2, nombre: 'Casilla 02 - Norte', direccion: 'Jardín de Niños Revolución, Calle Norte 45' },
  { id: 3, nombre: 'Casilla 03 - Sur', direccion: 'Secundaria Técnica No. 12, Blvd. Sur 230' },
  { id: 4, nombre: 'Casilla 04 - Oriente', direccion: 'Centro Comunitario Oriente, Calle 5 de Mayo 88' },
  { id: 5, nombre: 'Casilla 05 - Poniente', direccion: 'Auditorio Municipal Poniente, Av. Hidalgo 310' },
  { id: 6, nombre: 'Casilla 06 - Noreste', direccion: 'Preparatoria No. 3, Blvd. Tecnológico 540' },
  { id: 7, nombre: 'Casilla 07 - Suroeste', direccion: 'Casa de Cultura Suroeste, Calle Morelos 78' },
  { id: 8, nombre: 'Casilla 08 - Valle', direccion: 'Unidad Deportiva Valle Verde, Av. del Valle 990' }
]

const asignarCasilla = () => {
  const index = Math.floor(Math.random() * casillas.length)
  return casillas[index]
}

export const useRegistroStore = defineStore('registro', {
  state: () => ({
    registros: [],
    ultimoRegistro: null
  }),

  actions: {
    agregarRegistro(datos) {
      const folio = 'SIGEV-' + Date.now()
      const casilla = asignarCasilla()

      const registro = {
        ...datos,
        folio,
        casilla: datos.casilla || casilla.nombre,
        direccionCasilla: datos.direccionCasilla || casilla.direccion,
        estado: 'pendiente'
      }

      this.registros.push(registro)
      this.ultimoRegistro = registro
    }
  },

  persist: true
})