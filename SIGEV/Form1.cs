using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SIGEV
{
    public partial class Form1 : Form
    {
        // Importamos la función de la API de Windows para modificar atributos de la ventana
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int pvAttribute, int cbAttribute);

        // Constante que le dice a Windows que queremos cambiar el color del borde/barra de título
        private const int DWMWA_CAPTION_COLOR = 35;
        private const string API_URL = "https://swimmer-frenzied-crouch.ngrok-free.dev";
        //private const string API_URL = "http://10.33.14.109:3000";
        private const int CASILLA_ID = 1;
        private static readonly HttpClient http = new HttpClient();

        public Form1()
        {
            InitializeComponent();
            http.DefaultRequestHeaders.Add("ngrok-skip-browser-warning", "true");
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

        private async void btnValidar_Click(object sender, EventArgs e)
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
                    Autorizado();
                    lblResultado.ForeColor = Color.DarkGreen;
                    lblResultado.Text = "✅ " + mensaje;
                }
                else
                {
                    Rechazado();
                    lblResultado.ForeColor = Color.DarkRed;
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

        private async void btnEmergencia_Click(object sender, EventArgs e)
        {
            int horas = (int)nudHoras.Value;

            if (horas <= 0)
            {
                MessageBox.Show("Selecciona las horas de retraso", "Error");
                return;
            }

            var confirmacion = MessageBox.Show(
                $"¿Activar modo emergencia con {horas} hora(s) de retraso?\nSe notificará a todos los ciudadanos afectados por SMS.",
                "⚠️ Modo Emergencia",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion != DialogResult.Yes) return;

            btnEmergencia.Enabled = false;
            btnEmergencia.Text = "Procesando...";

            try
            {
                var body = new
                {
                    casilla_id = 1,
                    horas_retraso = horas
                };

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await http.PostAsync($"{API_URL}/emergencia", content);
                var responseText = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<dynamic>(responseText);

                MessageBox.Show(
                    $"Emergencia procesada.\nCiudadanos afectados: {resultado.afectados}\nNotificados por SMS: {resultado.notificados}",
                    "✅ Emergencia activada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error de conexión");
            }
            finally
            {
                btnEmergencia.Enabled = true;
                btnEmergencia.Text = "⚠️ Emergencia";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // El color debe estar en formato BGR (Blue, Green, Red) en lugar de RGB.
            // Por ejemplo, para un Azul Marino (como el de tu diseño): #143278 -> BGR sería 0x783214
            int colorBGR = 0xEC3A7B;

            // Aplicamos el color al marco de este Form
            DwmSetWindowAttribute(this.Handle, DWMWA_CAPTION_COLOR, ref colorBGR, sizeof(int));
        }
    }
}