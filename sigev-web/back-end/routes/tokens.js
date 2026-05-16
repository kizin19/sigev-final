const express = require('express')
const router  = express.Router()
const pool    = require('../db/connection')

router.post('/validar', async (req, res) => {
    const { token, casilla_id } = req.body

    if (!token || !casilla_id) {
        return res.status(400).json({ 
            valido: false, 
            mensaje: 'Token y casilla son requeridos' 
        })
    }

    try {
        const result = await pool.query(
            'SELECT valido, mensaje FROM validar_token($1, $2)',
            [token, parseInt(casilla_id)]
        )
        const { valido, mensaje } = result.rows[0]
        res.json({ valido, mensaje })

    } catch (err) {
        console.error('Error validando token:', err.message)
        res.status(500).json({ valido: false, mensaje: 'Error interno' })
    }
})

router.get('/panel', async (req, res) => {
    try {
        const result = await pool.query('SELECT * FROM panel_iee ORDER BY municipio, nombre')
        res.json(result.rows)
    } catch (err) {
        res.status(500).json({ error: err.message })
    }
})

module.exports = router