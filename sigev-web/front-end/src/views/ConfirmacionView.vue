<script setup>
import { useRegistroStore } from '../stores/registroStore'
import { useRouter } from 'vue-router'

const store = useRegistroStore()
const router = useRouter()
const r = store.ultimoRegistro
</script>

<template>
  <div class="container">
    <div class="card" v-if="r">
      <div class="icono">✅</div>
      <h1>¡Registro exitoso!</h1>
      <p class="sub">Guarda tu folio para el día de la votación</p>

      <div class="folio">{{ r.folio }}</div>

      <div class="info">
        <div class="fila">
          <span class="etiqueta">Nombre</span>
          <span>{{ r.nombre }} {{ r.apellidos }}</span>
        </div>
        <div class="fila">
          <span class="etiqueta">CURP</span>
          <span>{{ r.curp }}</span>
        </div>
        <div class="fila">
          <span class="etiqueta">Teléfono</span>
          <span>{{ r.telefono }}</span>
        </div>
        <div class="fila">
          <span class="etiqueta">Dirección</span>
          <span>{{ r.direccion }}</span>
        </div>
      </div>

      <div class="botones">
  <button class="btn-nuevo" @click="router.push('/')">
    ← Nuevo registro
  </button>
  <button class="btn-informate" @click="router.push('/informate')">
    Infórmate
  </button>
</div>
    </div>

    <div v-else class="card">
      <p>No hay registro reciente.</p>
      <button @click="router.push('/')">Ir al registro</button>
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
  text-align: center;
}

.icono { font-size: 3rem; margin-bottom: 1rem; }

h1 { font-size: 1.8rem; color: #7c3aed; }

.sub {
  color: #888;
  margin: 0.5rem 0 1.5rem;
  font-size: 0.9rem;
}

.folio {
  background: #f5f5f5;
  border: 2px dashed #7c3aed;
  border-radius: 12px;
  padding: 1rem;
  font-size: 1.2rem;
  font-weight: bold;
  color: #7c3aed;
  letter-spacing: 2px;
  margin-bottom: 1.5rem;
}

.info {
  text-align: left;
  margin-bottom: 1.5rem;
}

.fila {
  display: flex;
  justify-content: space-between;
  padding: 0.6rem 0;
  border-bottom: 1px solid #eee;
  font-size: 0.9rem;
  gap: 1rem;
}

.etiqueta {
  color: #888;
  text-transform: uppercase;
  font-size: 0.8rem;
  letter-spacing: 1px;
  white-space: nowrap;
}

.btn-nuevo {
  width: 100%;
  padding: 0.8rem;
  background: #f5f5f5;
  color: #333;
  border: 1px solid #ddd;
  border-radius: 10px;
  cursor: pointer;
  font-weight: bold;
  transition: all 0.2s;
}

.btn-nuevo:hover {
  border-color: #7c3aed;
  color: #7c3aed;
  transform: scale(1.03);
}

.botones {
  display: flex;
  gap: 1rem;
}

.btn-nuevo {
  flex: 1;
  padding: 0.8rem;
  background: #f5f5f5;
  color: #333;
  border: 1px solid #ddd;
  border-radius: 10px;
  cursor: pointer;
  font-weight: bold;
  transition: all 0.2s;
}

.btn-nuevo:hover {
  border-color: #7c3aed;
  color: #7c3aed;
}

.btn-informate {
  flex: 1;
  padding: 0.8rem;
  background: #7c3aed;
  color: #fff;
  border: none;
  border-radius: 10px;
  cursor: pointer;
  font-weight: bold;
  transition: all 0.2s;
}

.btn-informate:hover {
  background: #6d28d9;
  transform: scale(1.03);
}
</style>