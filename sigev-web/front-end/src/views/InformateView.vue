<script setup>
import { useRouter } from 'vue-router'
const router = useRouter()

const partidos = [
  {
    siglas: 'PHV',
    nombre: 'Partido Horizonte Verde',
    ideologia: 'Ecologismo progresista',
    lema: 'Progreso sin destruir el futuro.',
    color: '#16a34a',
    colorClaro: '#dcfce7',
    emoji: '🌿',
    propuestas: [
      'Energía 100% renovable en 20 años',
      'Transporte público eléctrico y gratuito para estudiantes',
      'Impuestos a empresas altamente contaminantes',
      'Reforestación masiva y protección de reservas naturales',
      'Programas de reciclaje inteligente en ciudades'
    ]
  },
  {
    siglas: 'UNT',
    nombre: 'Unión Nacional Tradicionalista',
    ideologia: 'Conservadurismo nacionalista',
    lema: 'Orden, patria y estabilidad.',
    color: '#b45309',
    colorClaro: '#fef3c7',
    emoji: '🦅',
    propuestas: [
      'Mayor inversión en fuerzas de seguridad',
      'Educación basada en civismo y cultura nacional',
      'Restricciones más fuertes a la inmigración ilegal',
      'Incentivos económicos para familias',
      'Protección de industrias nacionales'
    ]
  },
  {
    siglas: 'MFD',
    nombre: 'Movimiento Futuro Digital',
    ideologia: 'Tecnoliberalismo e innovación',
    lema: 'El futuro empieza hoy.',
    color: '#2563eb',
    colorClaro: '#dbeafe',
    emoji: '⚡',
    propuestas: [
      'Inteligencia artificial en servicios públicos',
      'Educación obligatoria en programación desde primaria',
      'Ciudades inteligentes con automatización',
      'Internet gratuito en espacios públicos',
      'Eliminación gradual del uso de papel en gobierno'
    ]
  },
  {
    siglas: 'PJSP',
    nombre: 'Partido Justicia Social Popular',
    ideologia: 'Socialdemocracia y justicia social',
    lema: 'Nadie debe quedarse atrás.',
    color: '#dc2626',
    colorClaro: '#fee2e2',
    emoji: '✊',
    propuestas: [
      'Salud y educación pública universal',
      'Aumento al salario mínimo',
      'Programas de vivienda accesible',
      'Becas para estudiantes de bajos recursos',
      'Impuestos más altos a grandes fortunas'
    ]
  },
  {
    siglas: 'ALE',
    nombre: 'Alianza Libertad Económica',
    ideologia: 'Liberalismo económico',
    lema: 'Más libertad, más oportunidades.',
    color: '#7c3aed',
    colorClaro: '#ede9fe',
    emoji: '📈',
    propuestas: [
      'Reducción de impuestos',
      'Apoyo a pequeñas y medianas empresas',
      'Privatización parcial de empresas estatales',
      'Simplificación de trámites gubernamentales',
      'Fomento de inversión extranjera'
    ]
  }
]
</script>

<template>
  <div class="page">
    <div class="hero">
      <button class="btn-volver" @click="router.push('/')">← Volver</button>
      <h1>Conoce a los partidos</h1>
      <p>Infórmate antes de votar. Aquí están las propuestas de cada partido participante.</p>
    </div>
  </div>

    <div class="partidos">
      <div class="partido-card" v-for="p in partidos" :key="p.siglas">

        <div class="partido-header" :style="{ backgroundColor: p.color }">
          <div class="logo">
            <span class="emoji">{{ p.emoji }}</span>
            <span class="siglas">{{ p.siglas }}</span>
          </div>
          <div class="header-info">
            <h2>{{ p.nombre }}</h2>
            <span class="ideologia">{{ p.ideologia }}</span>
          </div>
        </div>

        <div class="partido-body">
          <div class="lema" :style="{ backgroundColor: p.colorClaro, borderLeft: `4px solid ${p.color}` }">
            <span class="lema-label">Lema</span>
            <p :style="{ color: p.color }">"{{ p.lema }}"</p>
          </div>

          <div class="seccion">
            <h3>Propuestas principales</h3>
            <ul>
              <li v-for="prop in p.propuestas" :key="prop">
                <span class="bullet" :style="{ backgroundColor: p.color }"></span>
                {{ prop }}
              </li>
            </ul>
          </div>

      </div>
    </div>

    <div class="footer">
      <button class="btn-volver-abajo" @click="router.push('/')">← Volver al registro</button>
    </div>
  </div>
</template>

<style scoped>
.page {
  max-width: 860px;
  margin: 0 auto;
  padding: 2rem 1.5rem 4rem;
}

.hero {
  text-align: center;
  margin-bottom: 3rem;
  position: relative;
}

.btn-volver {
  position: absolute;
  left: 0;
  top: 0;
  padding: 0.5rem 1rem;
  background: #f5f5f5;
  color: #333;
  border: 1px solid #ddd;
  border-radius: 8px;
  cursor: pointer;
  font-size: 0.85rem;
  font-weight: bold;
  transition: all 0.2s;
}

.btn-volver:hover {
  border-color: #7c3aed;
  color: #7c3aed;
}

.hero h1 {
  font-size: 2.2rem;
  color: #1a1a2e;
  margin-bottom: 0.5rem;
}

.hero p {
  color: #888;
  font-size: 1rem;
}

.partidos {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.partido-card {
  border-radius: 16px;
  overflow: hidden;
  box-shadow: 0 4px 20px rgba(0,0,0,0.08);
  border: 1px solid #eee;
  background: #fff;
}

.partido-header {
  display: flex;
  align-items: center;
  gap: 1.2rem;
  padding: 1.2rem 1.5rem;
  color: #fff;
}

.logo {
  display: flex;
  flex-direction: column;
  align-items: center;
  background: rgba(255,255,255,0.2);
  border-radius: 12px;
  padding: 0.6rem 0.9rem;
  min-width: 64px;
}

.emoji {
  font-size: 1.8rem;
  line-height: 1;
}

.siglas {
  font-size: 0.7rem;
  font-weight: bold;
  letter-spacing: 1px;
  margin-top: 0.2rem;
  opacity: 0.9;
}

.header-info h2 {
  font-size: 1.2rem;
  font-weight: bold;
  margin-bottom: 0.2rem;
}

.ideologia {
  font-size: 0.85rem;
  opacity: 0.85;
}

.partido-body {
  padding: 1.5rem;
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
}

.lema {
  border-radius: 8px;
  padding: 0.8rem 1rem;
}

.lema-label {
  font-size: 0.7rem;
  text-transform: uppercase;
  letter-spacing: 1px;
  color: #888;
  display: block;
  margin-bottom: 0.3rem;
}

.lema p {
  font-size: 1rem;
  font-weight: bold;
  font-style: italic;
}

.seccion h3 {
  font-size: 0.8rem;
  text-transform: uppercase;
  letter-spacing: 1px;
  color: #888;
  margin-bottom: 0.8rem;
}

.seccion ul {
  list-style: none;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.seccion li {
  display: flex;
  align-items: center;
  gap: 0.7rem;
  font-size: 0.92rem;
  color: #333;
}

.bullet {
  width: 8px;
  height: 8px;
  border-radius: 50%;
  flex-shrink: 0;
}

.votantes {
  background: #f9f9f9;
  border-radius: 8px;
  padding: 0.8rem 1rem;
}

.votantes-label {
  font-size: 0.75rem;
  text-transform: uppercase;
  letter-spacing: 1px;
  color: #888;
  display: block;
  margin-bottom: 0.3rem;
}

.votantes p {
  font-size: 0.88rem;
  color: #555;
}

.footer {
  text-align: center;
  margin-top: 3rem;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 1rem;
}

.footer p {
  font-size: 0.8rem;
  color: #aaa;
}

.btn-volver-abajo {
  padding: 0.7rem 1.5rem;
  background: #7c3aed;
  color: #fff;
  border: none;
  border-radius: 10px;
  cursor: pointer;
  font-weight: bold;
  transition: all 0.2s;
}

.btn-volver-abajo:hover {
  background: #6d28d9;
  transform: scale(1.03);
}
</style>