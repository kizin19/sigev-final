using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIGEV
{
    public partial class Form1 : Form
    {
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
    }
}
