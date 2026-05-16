namespace SIGEV
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbToken = new System.Windows.Forms.GroupBox();
            this.btnValidar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.lbToken = new System.Windows.Forms.Label();
            this.gbValidar = new System.Windows.Forms.GroupBox();
            this.lbSub = new System.Windows.Forms.Label();
            this.lbEstado = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();
            this.pbIcono = new System.Windows.Forms.PictureBox();
            this.btnEmergencia = new System.Windows.Forms.Button();
            this.nudHoras = new System.Windows.Forms.NumericUpDown();
            this.gbToken.SuspendLayout();
            this.gbValidar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcono)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHoras)).BeginInit();
            this.SuspendLayout();
            // 
            // gbToken
            // 
            this.gbToken.BackColor = System.Drawing.Color.Transparent;
            this.gbToken.Controls.Add(this.btnValidar);
            this.gbToken.Controls.Add(this.btnLimpiar);
            this.gbToken.Controls.Add(this.txtToken);
            this.gbToken.Controls.Add(this.lbToken);
            this.gbToken.Font = new System.Drawing.Font("Dubai", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbToken.ForeColor = System.Drawing.Color.MidnightBlue;
            this.gbToken.Location = new System.Drawing.Point(32, 23);
            this.gbToken.Name = "gbToken";
            this.gbToken.Size = new System.Drawing.Size(432, 224);
            this.gbToken.TabIndex = 0;
            this.gbToken.TabStop = false;
            this.gbToken.Text = "Token del ciudadano";
            // 
            // btnValidar
            // 
            this.btnValidar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(58)))), ((int)(((byte)(236)))));
            this.btnValidar.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnValidar.Location = new System.Drawing.Point(272, 160);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(136, 48);
            this.btnValidar.TabIndex = 3;
            this.btnValidar.Text = "Validar";
            this.btnValidar.UseVisualStyleBackColor = false;
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(58)))), ((int)(((byte)(236)))));
            this.btnLimpiar.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLimpiar.Location = new System.Drawing.Point(104, 160);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(136, 48);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtToken
            // 
            this.txtToken.BackColor = System.Drawing.Color.Gainsboro;
            this.txtToken.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToken.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtToken.Location = new System.Drawing.Point(104, 64);
            this.txtToken.MaxLength = 8;
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(208, 41);
            this.txtToken.TabIndex = 1;
            // 
            // lbToken
            // 
            this.lbToken.AutoSize = true;
            this.lbToken.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbToken.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbToken.Location = new System.Drawing.Point(32, 64);
            this.lbToken.Name = "lbToken";
            this.lbToken.Size = new System.Drawing.Size(74, 34);
            this.lbToken.TabIndex = 0;
            this.lbToken.Text = "Token: ";
            // 
            // gbValidar
            // 
            this.gbValidar.BackColor = System.Drawing.Color.Transparent;
            this.gbValidar.Controls.Add(this.lbSub);
            this.gbValidar.Controls.Add(this.lbEstado);
            this.gbValidar.Controls.Add(this.lblResultado);
            this.gbValidar.Controls.Add(this.pbIcono);
            this.gbValidar.Font = new System.Drawing.Font("Dubai", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbValidar.ForeColor = System.Drawing.Color.MidnightBlue;
            this.gbValidar.Location = new System.Drawing.Point(32, 263);
            this.gbValidar.Name = "gbValidar";
            this.gbValidar.Size = new System.Drawing.Size(432, 172);
            this.gbValidar.TabIndex = 1;
            this.gbValidar.TabStop = false;
            this.gbValidar.Text = "Validación";
            // 
            // lbSub
            // 
            this.lbSub.AutoSize = true;
            this.lbSub.BackColor = System.Drawing.Color.Transparent;
            this.lbSub.Font = new System.Drawing.Font("Dubai", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSub.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbSub.Location = new System.Drawing.Point(97, 84);
            this.lbSub.Name = "lbSub";
            this.lbSub.Size = new System.Drawing.Size(237, 29);
            this.lbSub.TabIndex = 2;
            this.lbSub.Text = "Ingrese token y presione Validar";
            // 
            // lbEstado
            // 
            this.lbEstado.AutoSize = true;
            this.lbEstado.BackColor = System.Drawing.Color.Transparent;
            this.lbEstado.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lbEstado.Location = new System.Drawing.Point(97, 52);
            this.lbEstado.Name = "lbEstado";
            this.lbEstado.Size = new System.Drawing.Size(110, 39);
            this.lbEstado.TabIndex = 1;
            this.lbEstado.Text = "En espera";
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Font = new System.Drawing.Font("Dubai", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultado.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblResultado.Location = new System.Drawing.Point(28, 138);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(60, 30);
            this.lblResultado.TabIndex = 2;
            this.lblResultado.Text = "validar";
            // 
            // pbIcono
            // 
            this.pbIcono.Image = global::SIGEV.Properties.Resources.buscar;
            this.pbIcono.Location = new System.Drawing.Point(17, 52);
            this.pbIcono.Name = "pbIcono";
            this.pbIcono.Size = new System.Drawing.Size(64, 64);
            this.pbIcono.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIcono.TabIndex = 0;
            this.pbIcono.TabStop = false;
            // 
            // btnEmergencia
            // 
            this.btnEmergencia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(58)))), ((int)(((byte)(236)))));
            this.btnEmergencia.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmergencia.Location = new System.Drawing.Point(644, 347);
            this.btnEmergencia.Name = "btnEmergencia";
            this.btnEmergencia.Size = new System.Drawing.Size(128, 50);
            this.btnEmergencia.TabIndex = 3;
            this.btnEmergencia.Text = "Emergencia";
            this.btnEmergencia.UseVisualStyleBackColor = false;
            this.btnEmergencia.Click += new System.EventHandler(this.btnEmergencia_Click);
            // 
            // nudHoras
            // 
            this.nudHoras.Font = new System.Drawing.Font("Dubai", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudHoras.Location = new System.Drawing.Point(648, 411);
            this.nudHoras.Name = "nudHoras";
            this.nudHoras.Size = new System.Drawing.Size(120, 33);
            this.nudHoras.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::SIGEV.Properties.Resources.f1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.nudHoras);
            this.Controls.Add(this.btnEmergencia);
            this.Controls.Add(this.gbValidar);
            this.Controls.Add(this.gbToken);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "SIGEV - Validar Token (Flujo A)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbToken.ResumeLayout(false);
            this.gbToken.PerformLayout();
            this.gbValidar.ResumeLayout(false);
            this.gbValidar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcono)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHoras)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbToken;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label lbToken;
        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.GroupBox gbValidar;
        private System.Windows.Forms.PictureBox pbIcono;
        private System.Windows.Forms.Label lbSub;
        private System.Windows.Forms.Label lbEstado;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Button btnEmergencia;
        private System.Windows.Forms.NumericUpDown nudHoras;
    }
}

