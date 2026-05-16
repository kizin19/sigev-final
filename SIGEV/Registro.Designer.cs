namespace SIGEV
{
    partial class Registro
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbRegistro = new System.Windows.Forms.GroupBox();
            this.txtSeccion = new System.Windows.Forms.TextBox();
            this.lbSeccion = new System.Windows.Forms.Label();
            this.txtClaveINE = new System.Windows.Forms.TextBox();
            this.lbClaveINE = new System.Windows.Forms.Label();
            this.btnValidar = new System.Windows.Forms.Button();
            this.gbRegistro.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbRegistro
            // 
            this.gbRegistro.Controls.Add(this.btnValidar);
            this.gbRegistro.Controls.Add(this.txtSeccion);
            this.gbRegistro.Controls.Add(this.lbSeccion);
            this.gbRegistro.Controls.Add(this.txtClaveINE);
            this.gbRegistro.Controls.Add(this.lbClaveINE);
            this.gbRegistro.Font = new System.Drawing.Font("Dubai", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbRegistro.ForeColor = System.Drawing.Color.MidnightBlue;
            this.gbRegistro.Location = new System.Drawing.Point(24, 24);
            this.gbRegistro.Name = "gbRegistro";
            this.gbRegistro.Size = new System.Drawing.Size(544, 264);
            this.gbRegistro.TabIndex = 0;
            this.gbRegistro.TabStop = false;
            this.gbRegistro.Text = "Registro rápido";
            // 
            // txtSeccion
            // 
            this.txtSeccion.BackColor = System.Drawing.Color.Gainsboro;
            this.txtSeccion.ForeColor = System.Drawing.Color.White;
            this.txtSeccion.Location = new System.Drawing.Point(136, 120);
            this.txtSeccion.Name = "txtSeccion";
            this.txtSeccion.Size = new System.Drawing.Size(128, 46);
            this.txtSeccion.TabIndex = 3;
            // 
            // lbSeccion
            // 
            this.lbSeccion.AutoSize = true;
            this.lbSeccion.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSeccion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbSeccion.Location = new System.Drawing.Point(24, 128);
            this.lbSeccion.Name = "lbSeccion";
            this.lbSeccion.Size = new System.Drawing.Size(85, 34);
            this.lbSeccion.TabIndex = 2;
            this.lbSeccion.Text = "Sección: ";
            // 
            // txtClaveINE
            // 
            this.txtClaveINE.BackColor = System.Drawing.Color.Gainsboro;
            this.txtClaveINE.ForeColor = System.Drawing.Color.White;
            this.txtClaveINE.Location = new System.Drawing.Point(136, 56);
            this.txtClaveINE.Name = "txtClaveINE";
            this.txtClaveINE.Size = new System.Drawing.Size(384, 46);
            this.txtClaveINE.TabIndex = 1;
            // 
            // lbClaveINE
            // 
            this.lbClaveINE.AutoSize = true;
            this.lbClaveINE.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClaveINE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbClaveINE.Location = new System.Drawing.Point(24, 64);
            this.lbClaveINE.Name = "lbClaveINE";
            this.lbClaveINE.Size = new System.Drawing.Size(102, 34);
            this.lbClaveINE.TabIndex = 0;
            this.lbClaveINE.Text = "Clave INE: ";
            // 
            // btnValidar
            // 
            this.btnValidar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(58)))), ((int)(((byte)(236)))));
            this.btnValidar.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnValidar.Location = new System.Drawing.Point(212, 197);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(308, 48);
            this.btnValidar.TabIndex = 4;
            this.btnValidar.Text = "Registrar - Imprimir turno";
            this.btnValidar.UseVisualStyleBackColor = false;
            // 
            // Registro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbRegistro);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Registro";
            this.Text = "Registro";
            this.gbRegistro.ResumeLayout(false);
            this.gbRegistro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbRegistro;
        private System.Windows.Forms.TextBox txtClaveINE;
        private System.Windows.Forms.Label lbClaveINE;
        private System.Windows.Forms.TextBox txtSeccion;
        private System.Windows.Forms.Label lbSeccion;
        private System.Windows.Forms.Button btnValidar;
    }
}