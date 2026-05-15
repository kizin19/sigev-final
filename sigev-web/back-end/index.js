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