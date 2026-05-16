<script setup>
import { reactive } from 'vue'
import { useRouter } from 'vue-router'
import { useRegistroStore } from '../stores/registroStore'
import api from '../api'

const router = useRouter()
const store  = useRegistroStore()

const form = reactive({
  nombre: '',
  apellidos: '',
  curp: '',
  telefono: '',
  direccion: ''
})

const regexCURP = /^[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}[A-Z]{2}[B-DF-HJ-NP-TV-Z]{3}[A-Z0-9]{1}[0-9]{1}$/

const enviar = async () => {
  if (!form.nombre || !form.apellidos || !form.curp || !form.telefono || !form.direccion) {
    alert('Por favor completa todos los campos')
    return
  }

  if (!regexCURP.test(form.curp.toUpperCase())) {
    alert('La CURP no tiene un formato válido')
    return
  }

  if (!/^\d{10}$/.test(form.telefono)) {
    alert('El teléfono debe tener 10 dígitos')
    return
  }

  try {
    const res = await api.post('/registro', {
      curp:       form.curp.toUpperCase(),
      telefono:   form.telefono,
      nombre:     form.nombre,
      apellidos:  form.apellidos,
      direccion:  form.direccion,
      casilla_id: 1
    })
    store.agregarRegistro({
      folio:     res.data.token,
      nombre:    form.nombre,
      apellidos: form.apellidos,
      curp:      form.curp.toUpperCase(),
      telefono:  form.telefono,
      direccion: form.direccion
    })
    router.push('/confirmacion')
  } catch (err) {
    alert(err.response?.data?.error || 'Error al registrar, intenta de nuevo')
  }
}
</script>

<template>
  <div class="container">
    <div class="card">
      <div class="header">
        <h1>SIGEV</h1>
        <p>Registro de Votante</p>
      </div>

      <div class="form">
        <div class="field">
          <label>Nombre(s)</label>
          <input v-model="form.nombre" type="text" placeholder="Ej. Juan Carlos" />
        </div>

        <div class="field">
          <label>Apellidos</label>
          <input v-model="form.apellidos" type="text" placeholder="Ej. Pérez López" />
        </div>

        <div class="field">
          <label>CURP</label>
          <input v-model="form.curp" type="text" placeholder="18 caracteres" maxlength="18" />
          <span class="hint">{{ form.curp.length }}/18</span>
        </div>

        <div class="field">
          <label>Teléfono</label>
          <input v-model="form.telefono" type="tel" placeholder="10 dígitos" maxlength="10" />
        </div>

        <div class="field">
          <label>Dirección</label>
          <input v-model="form.direccion" type="text" placeholder="Calle, número, colonia" />
        </div>

        <button class="btn-registrar" @click="enviar">
          Registrarme
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.container {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 2rem;
}

.card {
  background: #ffffff;
  border-radius: 20px;
  padding: 2.5rem;
  width: 100%;
  max-width: 480px;
  border: 1px solid #ddd;
  box-shadow: 0 4px 24px rgba(0,0,0,0.08);
}

.header {
  text-align: center;
  margin-bottom: 2rem;
}

.header h1 {
  font-size: 2rem;
  color: #7c3aed;
  letter-spacing: 4px;
}

.header p {
  color: #888;
  font-size: 0.9rem;
  margin-top: 0.3rem;
}

.field {
  margin-bottom: 1.2rem;
  display: flex;
  flex-direction: column;
  gap: 0.4rem;
}

.field label {
  font-size: 0.85rem;
  color: #888;
  text-transform: uppercase;
  letter-spacing: 1px;
}

.field input {
  background: #f5f5f5;
  border: 1px solid #ddd;
  border-radius: 10px;
  padding: 0.7rem 1rem;
  color: #333;
  font-size: 0.95rem;
  outline: none;
  transition: border 0.2s;
}

.field input:focus {
  border-color: #7c3aed;
}

.hint {
  font-size: 0.75rem;
  color: #7c3aed;
  text-align: right;
}

.btn-registrar {
  width: 100%;
  padding: 0.9rem;
  background: #7c3aed;
  color: #fff;
  border: none;
  border-radius: 12px;
  font-size: 1rem;
  font-weight: bold;
  cursor: pointer;
  margin-top: 0.5rem;
  transition: all 0.2s;
}

.btn-registrar:hover {
  background: #6d28d9;
  transform: scale(1.02);
}
</style>