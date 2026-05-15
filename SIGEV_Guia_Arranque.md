# SIGEV — Guía de Arranque
## Todo lo que necesitas para empezar a programar mañana

---

## 1. ANTES DE EMPEZAR — Verifica que tienes todo instalado

Abre una terminal y ejecuta cada comando. Todos deben mostrar un número de versión:

```bash
node --version        # debe decir v18 o superior
npm --version         # debe decir 9 o superior
psql --version        # debe decir PostgreSQL 14 o superior
```

Si Node.js no está instalado: https://nodejs.org (descarga LTS)

---

## 2. ESTRUCTURA COMPLETA DEL PROYECTO

```
sigev/
├── sigev-backend/
│   ├── db/
│   │   └── connection.js
│   ├── routes/
│   │   ├── registro.js
│   │   ├── tokens.js
│   │   └── casillas.js
│   ├── index.js
│   ├── .env
│   └── package.json
│
└── sigev-frontend/
    ├── src/
    │   ├── views/
    │   │   ├── Registro.vue
    │   │   └── PanelIEE.vue
    │   ├── router/
    │   │   └── index.js
    │   ├── App.vue
    │   └── main.js
    └── package.json
```

---

## 3. CREAR LA BASE DE DATOS

Abre pgAdmin o la terminal de PostgreSQL y ejecuta:

```sql
CREATE DATABASE sigev;
```

Luego conecta a esa base de datos y ejecuta el schema completo
(usa el archivo sigev_schema.sql que ya tienes).

---

## 4. CREAR EL BACKEND — Paso a paso

### 4.1 Crear carpeta e instalar dependencias

```bash
mkdir sigev
cd sigev
mkdir sigev-backend
cd sigev-backend
npm init -y
npm install express pg dotenv cors uuid
```

### 4.2 Crear archivo .env

Crea el archivo `.env` en la raíz de sigev-backend:

```
DB_HOST=localhost
DB_PORT=5432
DB_NAME=sigev
DB_USER=postgres
DB_PASSWORD=tu_contraseña_aqui
PORT=3000
TWILIO_ACCOUNT_SID=tu_sid_de_twilio
TWILIO_AUTH_TOKEN=tu_token_de_twilio
TWILIO_PHONE=+1XXXXXXXXXX
```

### 4.3 Crear db/connection.js

```javascript
const { Pool } = require('pg')
require('dotenv').config()

const pool = new Pool({
    host:     process.env.DB_HOST,
    port:     process.env.DB_PORT,
    database: process.env.DB_NAME,
    user:     process.env.DB_USER,
    password: process.env.DB_PASSWORD,
})

pool.connect((err) => {
    if (err) {
        console.error('Error conectando a PostgreSQL:', err.message)
    } else {
        console.log('✅ Conectado a PostgreSQL correctamente')
    }
})

module.exports = pool
```

### 4.4 Crear routes/registro.js

```javascript
const express  = require('express')
const router   = express.Router()
const pool     = require('../db/connection')
const { v4: uuidv4 } = require('uuid')
const twilio   = require('twilio')
require('dotenv').config()

const client = twilio(
    process.env.TWILIO_ACCOUNT_SID,
    process.env.TWILIO_AUTH_TOKEN
)

// POST /registro — registra un ciudadano y genera su token
router.post('/', async (req, res) => {
    const { curp, telefono, casilla_id, canal } = req.body

    // Validación básica
    if (!curp || !casilla_id) {
        return res.status(400).json({ error: 'CURP y casilla son requeridos' })
    }

    try {
        // 1. Verificar que el CURP no esté ya registrado
        const existe = await pool.query(
            'SELECT id FROM ciudadanos WHERE curp = $1',
            [curp]
        )
        if (existe.rows.length > 0) {
            return res.status(409).json({ error: 'Este CURP ya está registrado' })
        }

        // 2. Registrar al ciudadano
        const ciudadano = await pool.query(
            `INSERT INTO ciudadanos (curp, telefono, canal)
             VALUES ($1, $2, $3) RETURNING id`,
            [curp, telefono || null, canal || 'web']
        )
        const ciudadano_id = ciudadano.rows[0].id

        // 3. Generar token único
        const token = uuidv4()

        // 4. Calcular hora asignada (próximo slot disponible)
        const hora_asignada = new Date()
        hora_asignada.setMinutes(hora_asignada.getMinutes() + 30)

        // 5. Crear cita
        await pool.query(
            `INSERT INTO citas (ciudadano_id, casilla_id, hora_asignada)
             VALUES ($1, $2, $3)`,
            [ciudadano_id, casilla_id, hora_asignada]
        )

        // 6. Guardar token en base de datos
        await pool.query(
            `INSERT INTO tokens (token, ciudadano_id, casilla_id, tipo)
             VALUES ($1, $2, $3, 'cita')`,
            [token, ciudadano_id, casilla_id]
        )

        // 7. Enviar SMS si tiene teléfono
        if (telefono) {
            const hora_formato = hora_asignada.toLocaleTimeString('es-MX', {
                hour: '2-digit', minute: '2-digit'
            })
            await client.messages.create({
                body: `SIGEV: Tu registro fue exitoso.\nToken: ${token}\nHora asignada: ${hora_formato}\nPreséntate 5 min antes en tu casilla.`,
                from: process.env.TWILIO_PHONE,
                to:   `+52${telefono}`
            })

            // Registrar notificación enviada
            await pool.query(
                `INSERT INTO notificaciones (ciudadano_id, tipo, canal, mensaje, enviado, enviado_at)
                 VALUES ($1, 'confirmacion', 'sms', $2, true, NOW())`,
                [ciudadano_id, `Token ${token} enviado`]
            )
        }

        // 8. Respuesta al frontend
        res.status(201).json({
            mensaje:       'Registro exitoso',
            token:         token,
            hora_asignada: hora_asignada,
            ciudadano_id:  ciudadano_id
        })

    } catch (err) {
        console.error('Error en registro:', err.message)
        res.status(500).json({ error: 'Error interno del servidor' })
    }
})

module.exports = router
```

### 4.5 Crear routes/tokens.js

```javascript
const express = require('express')
const router  = express.Router()
const pool    = require('../db/connection')

// POST /tokens/validar — valida y quema un token (lo usa la app C#)
router.post('/validar', async (req, res) => {
    const { token, casilla_id } = req.body

    if (!token || !casilla_id) {
        return res.status(400).json({ 
            valido: false, 
            mensaje: 'Token y casilla son requeridos' 
        })
    }

    try {
        // Llama a la función atómica de PostgreSQL
        const result = await pool.query(
            'SELECT valido, mensaje FROM validar_token($1, $2)',
            [token, casilla_id]
        )
        const { valido, mensaje } = result.rows[0]
        res.json({ valido, mensaje })

    } catch (err) {
        console.error('Error validando token:', err.message)
        res.status(500).json({ valido: false, mensaje: 'Error interno' })
    }
})

// GET /tokens/panel — datos para el panel IEE
router.get('/panel', async (req, res) => {
    try {
        const result = await pool.query('SELECT * FROM panel_iee ORDER BY municipio, nombre')
        res.json(result.rows)
    } catch (err) {
        res.status(500).json({ error: err.message })
    }
})

module.exports = router
```

### 4.6 Crear routes/casillas.js

```javascript
const express = require('express')
const router  = express.Router()
const pool    = require('../db/connection')

// GET /casillas — lista todas las casillas activas
router.get('/', async (req, res) => {
    try {
        const result = await pool.query(
            'SELECT id, clave, nombre, municipio, estado FROM casillas WHERE activa = true ORDER BY municipio, nombre'
        )
        res.json(result.rows)
    } catch (err) {
        res.status(500).json({ error: err.message })
    }
})

module.exports = router
```

### 4.7 Crear index.js principal

```javascript
const express = require('express')
const cors    = require('cors')
require('dotenv').config()

const app = express()

// Middlewares
app.use(cors())
app.use(express.json())

// Rutas
app.use('/registro', require('./routes/registro'))
app.use('/tokens',   require('./routes/tokens'))
app.use('/casillas', require('./routes/casillas'))

// Ruta de prueba
app.get('/', (req, res) => {
    res.json({ mensaje: 'SIGEV API funcionando ✅', version: '1.0' })
})

// Arrancar servidor
app.listen(process.env.PORT, () => {
    console.log(`🚀 Servidor corriendo en puerto ${process.env.PORT}`)
})
```

---

## 5. CREAR EL FRONTEND — Paso a paso

### 5.1 Crear proyecto Vue

Desde la carpeta `sigev/`:

```bash
npm install -g @vue/cli
vue create sigev-frontend
# Selecciona: Manually select features
# Selecciona: Router
# Vue version: 3
# History mode: Yes
cd sigev-frontend
npm install axios
npm run serve
```

### 5.2 Crear src/views/Registro.vue

```vue
<template>
  <div class="registro">
    <h1>Registro SIGEV</h1>
    <p>Registra tu turno para votar sin hacer fila</p>

    <form @submit.prevent="registrar">
      <div class="campo">
        <label>CURP *</label>
        <input v-model="form.curp" type="text" maxlength="18"
               placeholder="Ej: ABCD123456HDFXXX00" required />
      </div>

      <div class="campo">
        <label>Teléfono (para recibir tu token por SMS)</label>
        <input v-model="form.telefono" type="tel"
               placeholder="10 dígitos sin espacios" />
      </div>

      <div class="campo">
        <label>Casilla *</label>
        <select v-model="form.casilla_id" required>
          <option value="">Selecciona tu casilla</option>
          <option v-for="c in casillas" :key="c.id" :value="c.id">
            {{ c.nombre }} — {{ c.municipio }}
          </option>
        </select>
      </div>

      <button type="submit" :disabled="cargando">
        {{ cargando ? 'Registrando...' : 'Registrarme' }}
      </button>
    </form>

    <!-- Resultado exitoso -->
    <div v-if="resultado" class="resultado-ok">
      <h2>✅ Registro exitoso</h2>
      <p><strong>Tu token:</strong> {{ resultado.token }}</p>
      <p><strong>Hora asignada:</strong> {{ formatHora(resultado.hora_asignada) }}</p>
      <p v-if="form.telefono">📱 Te enviamos un SMS con esta información</p>
      <p>Preséntate en tu casilla a la hora indicada</p>
    </div>

    <!-- Error -->
    <div v-if="error" class="resultado-error">
      <p>❌ {{ error }}</p>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

const API = 'http://localhost:3000'

export default {
  name: 'Registro',
  data() {
    return {
      form: { curp: '', telefono: '', casilla_id: '' },
      casillas: [],
      resultado: null,
      error:     null,
      cargando:  false,
    }
  },
  async mounted() {
    // Carga la lista de casillas al abrir la página
    try {
      const res = await axios.get(`${API}/casillas`)
      this.casillas = res.data
    } catch (err) {
      console.error('Error cargando casillas:', err)
    }
  },
  methods: {
    async registrar() {
      this.cargando  = true
      this.resultado = null
      this.error     = null
      try {
        const res = await axios.post(`${API}/registro`, {
          curp:       this.form.curp.toUpperCase(),
          telefono:   this.form.telefono || null,
          casilla_id: this.form.casilla_id,
          canal:      'web'
        })
        this.resultado = res.data
      } catch (err) {
        this.error = err.response?.data?.error || 'Error al registrar'
      } finally {
        this.cargando = false
      }
    },
    formatHora(fecha) {
      return new Date(fecha).toLocaleTimeString('es-MX', {
        hour: '2-digit', minute: '2-digit'
      })
    }
  }
}
</script>
```

### 5.3 Crear src/views/PanelIEE.vue

```vue
<template>
  <div class="panel">
    <h1>Panel IEE — Monitoreo en tiempo real</h1>
    <p>Actualización automática cada 30 segundos</p>

    <div class="casillas-grid">
      <div v-for="c in casillas" :key="c.casilla_id" class="casilla-card">
        <h3>{{ c.nombre }}</h3>
        <p>{{ c.municipio }}, {{ c.estado }}</p>
        <div class="stats">
          <div class="stat">
            <span class="numero">{{ c.votos_emitidos }}</span>
            <span class="label">Votos emitidos</span>
          </div>
          <div class="stat">
            <span class="numero">{{ c.tokens_activos }}</span>
            <span class="label">En espera</span>
          </div>
          <div class="stat">
            <span class="numero">{{ c.porcentaje_participacion }}%</span>
            <span class="label">Participación</span>
          </div>
        </div>
      </div>
    </div>

    <p v-if="casillas.length === 0">Cargando datos...</p>
  </div>
</template>

<script>
import axios from 'axios'

const API = 'http://localhost:3000'

export default {
  name: 'PanelIEE',
  data() {
    return {
      casillas:  [],
      intervalo: null,
    }
  },
  async mounted() {
    await this.cargarDatos()
    // Actualiza cada 30 segundos automáticamente
    this.intervalo = setInterval(this.cargarDatos, 30000)
  },
  beforeUnmount() {
    clearInterval(this.intervalo)
  },
  methods: {
    async cargarDatos() {
      try {
        const res = await axios.get(`${API}/tokens/panel`)
        this.casillas = res.data
      } catch (err) {
        console.error('Error cargando panel:', err)
      }
    }
  }
}
</script>
```

### 5.4 Actualizar src/router/index.js

```javascript
import { createRouter, createWebHistory } from 'vue-router'
import Registro  from '../views/Registro.vue'
import PanelIEE  from '../views/PanelIEE.vue'

const routes = [
  { path: '/',      component: Registro  },
  { path: '/panel', component: PanelIEE  },
]

export default createRouter({
  history: createWebHistory(),
  routes,
})
```

---

## 6. ENDPOINTS DEL API — Referencia rápida

| Método | Endpoint | Qué hace | Lo usa |
|--------|----------|----------|--------|
| GET | `/` | Prueba que el servidor vive | Navegador |
| GET | `/casillas` | Lista casillas activas | Vue.js |
| POST | `/registro` | Registra ciudadano y genera token | Vue.js |
| POST | `/tokens/validar` | Valida y quema un token | App C# |
| GET | `/tokens/panel` | Datos para el panel IEE | Vue.js |

---

## 7. ORDEN DE TRABAJO PARA MAÑANA

```
1. Verificar instalaciones (node, npm, psql)     ~10 min
2. Crear carpeta sigev/ y sigev-backend/         ~5 min
3. npm init + instalar dependencias backend      ~5 min
4. Crear .env con tus credenciales               ~5 min
5. Copiar connection.js, routes/, index.js       ~15 min
6. Ejecutar sigev_schema.sql en PostgreSQL       ~10 min
7. Probar node index.js + endpoints en navegador ~15 min
8. Crear proyecto Vue + instalar axios           ~10 min
9. Copiar Registro.vue y PanelIEE.vue            ~15 min
10. Probar flujo completo registro → token       ~20 min
```

**Total estimado: ~2 horas para tener todo funcionando**

---

## 8. COMANDOS DE USO DIARIO

```bash
# Arrancar backend
cd sigev/sigev-backend
node index.js

# Arrancar frontend (en otra terminal)
cd sigev/sigev-frontend
npm run serve

# Abrir en navegador
# Registro ciudadano:  http://localhost:8080
# Panel IEE:           http://localhost:8080/panel
# API directa:         http://localhost:3000
```

---

## 9. INSERTAR CASILLA DE PRUEBA

Antes de probar el registro, inserta al menos una casilla en la base de datos:

```sql
INSERT INTO casillas (clave, nombre, municipio, estado, capacidad_hora)
VALUES ('CHI-001', 'Casilla Centro', 'Chihuahua', 'Chihuahua', 20);
```

---

## 10. SI ALGO FALLA — Checklist

- ¿El servidor Node.js está corriendo? → `node index.js`
- ¿PostgreSQL está corriendo? → Abre pgAdmin y verifica
- ¿Las credenciales del .env son correctas?
- ¿Ejecutaste el sigev_schema.sql completo?
- ¿Insertaste al menos una casilla de prueba?
- ¿El frontend apunta a `http://localhost:3000`?

---

*SIGEV — Guía de Arranque v1.0*
