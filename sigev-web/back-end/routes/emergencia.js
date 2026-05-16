const express = require('express')
const router  = express.Router()
const pool    = require('../db/connection')
const twilio  = require('twilio')
require('dotenv').config()

const client = twilio(
    process.env.TWILIO_ACCOUNT_SID,
    process.env.TWILIO_AUTH_TOKEN
)

// POST /emergencia — recorre citas y notifica por SMS
router.post('/', async (req, res) => {
    const { casilla_id, horas_retraso } = req.body

    if (!casilla_id || !horas_retraso) {
        return res.status(400).json({ error: 'casilla_id y horas_retraso son requeridos' })
    }

    try {
        // 1. Obtener citas afectadas con teléfono
        const afectados = await pool.query(
            `SELECT ci.id, ci.hora_asignada, c.nombre, c.telefono
             FROM citas ci
             JOIN ciudadanos c ON c.id = ci.ciudadano_id
             WHERE ci.casilla_id = $1
             AND ci.estado = 'pendiente'
             AND ci.hora_asignada < NOW()`,
            [casilla_id]
        )

        if (afectados.rows.length === 0) {
            return res.json({ mensaje: 'No hay citas afectadas', afectados: 0 })
        }

        // 2. Recorrer citas y notificar
        let notificados = 0
        for (const cita of afectados.rows) {
            // Recorrer hora
            const nueva_hora = new Date(cita.hora_asignada)
            nueva_hora.setHours(nueva_hora.getHours() + parseInt(horas_retraso))

            await pool.query(
                `UPDATE citas SET hora_asignada = $1 WHERE id = $2`,
                [nueva_hora, cita.id]
            )

            // Notificar por SMS si tiene teléfono
            if (cita.telefono) {
                const hora_formato = nueva_hora.toLocaleTimeString('es-MX', {
                    hour: '2-digit', minute: '2-digit'
                })
                try {
                    await client.messages.create({
                        body: `SIGEV AVISO: Tu casilla abrió tarde.\nTu nueva hora asignada es: ${hora_formato}\nPreséntate 5 min antes.\nDisculpa el inconveniente.`,
                        from: '+17752957554',
                        to:   `+52${cita.telefono}`
                    })
                    notificados++
                } catch (smsErr) {
                    console.error('Error SMS:', smsErr.message)
                }
            }
        }

        res.json({
            mensaje:     `Emergencia procesada correctamente`,
            afectados:   afectados.rows.length,
            notificados: notificados
        })

    } catch (err) {
        console.error('Error emergencia:', err.message)
        res.status(500).json({ error: 'Error interno del servidor' })
    }
})

module.exports = router