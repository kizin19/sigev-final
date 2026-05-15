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