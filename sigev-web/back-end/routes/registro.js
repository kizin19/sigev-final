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