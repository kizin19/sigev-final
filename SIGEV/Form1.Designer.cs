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
            this.pbIcono = new System.Windows.Forms.PictureBox();
            this.lblResultado = new System.Windows.Forms.Label();
            this.gbToken.SuspendLayout();
            this.gbValidar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcono)).BeginInit();
            this.SuspendLayout();
            // 
            // gbToken
            // 
            this.gbToken.Controls.Add(this.btnValidar);
            this.gbToken.Controls.Add(this.btnLimpiar);
            this.gbToken.Controls.Add(this.txtToken);
            this.gbToken.Controls.Add(this.lbToken);
            this.gbToken.Font = new System.Drawing.Font("Dubai", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbToken.ForeColor = System.Drawing.Color.MidnightBlue;
            this.gbToken.Location = new System.Drawing.Point(32, 32);
            this.gbToken.Name = "gbToken";
            this.gbToken.Size = new System.Drawing.Size(432, 224);
            this.gbToken.TabIndex = 0;
            this.gbToken.TabStop = false;
            this.gbToken.Text = "Token del ciudadano";
            // 
            // btnValidar
            // 
            this.btnValidar.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnValidar.Location = new System.Drawing.Point(272, 160);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(136, 48);
            this.btnValidar.TabIndex = 3;
            this.btnValidar.Text = "Validar";
            this.btnValidar.UseVisualStyleBackColor = true;
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLimpiar.Location = new System.Drawing.Point(104, 160);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(136, 48);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // txtToken
            // 
            this.txtToken.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.txtToken.Font = new System.Drawing.Font("Dubai", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToken.ForeColor = System.Drawing.Color.White;
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
            this.gbValidar.Controls.Add(this.lbSub);
            this.gbValidar.Controls.Add(this.lbEstado);
            this.gbValidar.Controls.Add(this.pbIcono);
            this.gbValidar.Font = new System.Drawing.Font("Dubai", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbValidar.Location = new System.Drawing.Point(32, 272);
            this.gbValidar.Name = "gbValidar";
            this.gbValidar.Size = new System.Drawing.Size(432, 144);
            this.gbValidar.TabIndex = 1;
            this.gbValidar.TabStop = false;
            // 
            // lbSub
            // 
            this.lbSub.AutoSize = true;
            this.lbSub.BackColor = System.Drawing.Color.Transparent;
            this.lbSub.Font = new System.Drawing.Font("Dubai", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSub.Location = new System.Drawing.Point(104, 96);
            this.lbSub.Name = "lbSub";
            this.lbSub.Size = new System.Drawing.Size(237, 29);
            this.lbSub.TabIndex = 2;
            this.lbSub.Text = "Ingrese token y presione Validar";
            // 
            // lbEstado
            // 
            this.lbEstado.AutoSize = true;
            this.lbEstado.BackColor = System.Drawing.Color.Transparent;
            this.lbEstado.Location = new System.Drawing.Point(104, 64);
            this.lbEstado.Name = "lbEstado";
            this.lbEstado.Size = new System.Drawing.Size(110, 39);
            this.lbEstado.TabIndex = 1;
            this.lbEstado.Text = "En espera";
            // 
            // pbIcono
            // 
            this.pbIcono.Location = new System.Drawing.Point(24, 64);
            this.pbIcono.Name = "pbIcono";
            this.pbIcono.Size = new System.Drawing.Size(64, 64);
            this.pbIcono.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIcono.TabIndex = 0;
            this.pbIcono.TabStop = false;
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(587, 176);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(48, 16);
            this.lblResultado.TabIndex = 2;
            this.lblResultado.Text = "validar";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.gbValidar);
            this.Controls.Add(this.gbToken);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = ".";
            this.gbToken.ResumeLayout(false);
            this.gbToken.PerformLayout();
            this.gbValidar.ResumeLayout(false);
            this.gbValidar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcono)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

