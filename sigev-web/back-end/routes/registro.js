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

// Asigna casilla según dirección
function asignarCasilla(direccion) {
    const dir = direccion.toLowerCase()

    if (dir.includes('centro'))           return 1
    if (dir.includes('rosario'))          return 2
    if (dir.includes('san felipe'))       return 3
    if (dir.includes('nombre de dios'))   return 4
    if (dir.includes('universidad'))      return 5
    if (dir.includes('cerro grande'))     return 6
    if (dir.includes('san marcos'))       return 7
    if (dir.includes('mirador'))          return 8
    if (dir.includes('altavista'))        return 9
    if (dir.includes('panamericana'))     return 10

    return 1 // casilla por defecto
}

router.post('/', async (req, res) => {
    const { curp, telefono, nombre, apellidos, direccion } = req.body

    // Asigna casilla automáticamente
    const casilla_id = asignarCasilla(direccion)

    if (!curp) {
        return res.status(400).json({ error: 'CURP es requerida' })
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

        const tokenCompleto = uuidv4()
        const tokenCorto    = tokenCompleto.split('-')[0].toUpperCase()

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
            [tokenCorto, ciudadano_id, casilla_id]
        )

        // Obtener nombre de la casilla
        const casilla = await pool.query(
            'SELECT nombre, municipio FROM casillas WHERE id = $1',
            [casilla_id]
        )
        const nombre_casilla   = casilla.rows[0].nombre
        const municipio_casilla = casilla.rows[0].municipio

        if (telefono) {
            const hora_formato = hora_asignada.toLocaleTimeString('es-MX', {
                hour: '2-digit', minute: '2-digit'
            })
            await client.messages.create({
                body: `SIGEV: Tu registro fue exitoso.\nToken: ${tokenCorto}\nHora: ${hora_formato}\nCasilla: ${nombre_casilla}, ${municipio_casilla}\nPreséntate 5 min antes.`,
                from: '+17752957554',
                to:   `+52${telefono}`
            })

            await pool.query(
                `INSERT INTO notificaciones (ciudadano_id, tipo, canal, mensaje, enviado, enviado_at)
                 VALUES ($1, 'confirmacion', 'sms', $2, true, NOW())`,
                [ciudadano_id, `Token ${tokenCorto} enviado`]
            )
        }

        res.status(201).json({
            mensaje:         'Registro exitoso',
            token:           tokenCorto,
            hora_asignada:   hora_asignada,
            ciudadano_id:    ciudadano_id,
            casilla:         nombre_casilla,
            municipio:       municipio_casilla
        })

    } catch (err) {
        console.error('Error en registro:', err.message)
        res.status(500).json({ error: 'Error interno del servidor' })
    }
})

module.exports = router