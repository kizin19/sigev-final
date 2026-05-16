using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIGEV
{
    public partial class Form1 : Form
    {
        // Cambia esta IP por la de tu laptop el día del hackathon
        private const string API_URL = "http://10.33.14.109:3000";
        private const int CASILLA_ID = 1;
        private static readonly HttpClient http = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtToken.Clear();
            lbEstado.Text = "En espera";
            lbSub.Text = "Ingrese token y presione Validar";
            lbEstado.ForeColor = Color.MidnightBlue;
            pbIcono.Image = Properties.Resources.buscar;
        }

        private void Autorizado()
        {
            lbEstado.Text = "Ciudadano autorizado";
            lbSub.Text = "Token válido para esta casilla";
            lbEstado.ForeColor = Color.Green;
            pbIcono.Image = Properties.Resources.valido;
        }

        private void Rechazado()
        {
            lbEstado.Text = "Token rechazado";
            lbSub.Text = "No válido, ya consumido o casilla incorrecta";
            lbEstado.ForeColor = Color.Red;
            pbIcono.Image = Properties.Resources.mal;
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            string token = txtToken.Text.Trim();

            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Ingresa un token", "Error");
                return;
            }

            btnValidar.Enabled = false;
            lblResultado.Text = "Validando...";

            try
            {
                var body = new
                {
                    token = token,
                    casilla_id = CASILLA_ID
                };

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await http.PostAsync($"{API_URL}/tokens/validar", content);
                var responseText = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<dynamic>(responseText);

                bool valido = resultado.valido;
                string mensaje = resultado.mensaje;

                if (valido)
                {
                    
                    lblResultado.ForeColor = System.Drawing.Color.DarkGreen;
                    lblResultado.Text = "✅ " + mensaje;
                }
                else
                {
                   
                    lblResultado.ForeColor = System.Drawing.Color.DarkRed;
                    lblResultado.Text = "❌ " + mensaje;
                }
            }
            catch (Exception ex)
            {
                lblResultado.Text = "Error de conexión: " + ex.Message;
            }
            finally
            {
                btnValidar.Enabled = true;
                txtToken.Clear();
                txtToken.Focus();
            }
        }

    }
}
