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


router.post('/', async (req, res) => {
    const { curp, telefono, nombre, apellidos, direccion, casilla_id } = req.body

    if (!curp || !casilla_id) {
        return res.status(400).json({ error: 'CURP y casilla son requeridos' })
    }

    try {
        const existe = await pool.query(
            'SELECT id FROM ciudadanos WHERE curp = $1',
            [curp]
        )
        if (existe.rows.length > 0) {
            return res.status(409).json({ error: 'Este CURP ya está registrado' })
        }

        const ciudadano = await pool.query(
            `INSERT INTO ciudadanos (curp, telefono, nombre, apellidos, direccion)
             VALUES ($1, $2, $3, $4, $5) RETURNING id`,
            [curp, telefono || null, nombre || null, apellidos || null, direccion || null]
        )
        const ciudadano_id = ciudadano.rows[0].id

        const token = uuidv4().split('-')[0].toUpperCase()

        const hora_asignada = new Date()
        hora_asignada.setMinutes(hora_asignada.getMinutes() + 30)

        await pool.query(
            `INSERT INTO citas (ciudadano_id, casilla_id, hora_asignada)
             VALUES ($1, $2, $3)`,
            [ciudadano_id, casilla_id, hora_asignada]
        )

        await pool.query(
            `INSERT INTO tokens (token, ciudadano_id, casilla_id, tipo)
             VALUES ($1, $2, $3, 'cita')`,
            [token, ciudadano_id, casilla_id]
        )

        if (telefono) {
    const hora_formato = hora_asignada.toLocaleTimeString('es-MX', {
        hour: '2-digit', minute: '2-digit'
    })
    try {
        await client.messages.create({
            body: `SIGEV: Tu registro fue exitoso.\nToken: ${token}\nHora asignada: ${hora_formato}\nPreséntate 5 min antes en tu casilla.`,
            from: '+17752957554',
            to:   `+52${telefono}`
        })
        await pool.query(
            `INSERT INTO notificaciones (ciudadano_id, tipo, canal, mensaje, enviado, enviado_at)
             VALUES ($1, 'confirmacion', 'sms', $2, true, NOW())`,
            [ciudadano_id, `Token ${token} enviado`]
        )
    } catch (twilioErr) {
        console.error('Error Twilio completo:', twilioErr)
    }
}

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