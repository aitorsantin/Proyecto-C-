namespace CapaCliente
{
    partial class GestionPedidos
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
            this.btnenviarpedido = new System.Windows.Forms.Button();
            this.btnfactura = new System.Windows.Forms.Button();
            this.btnsalir = new System.Windows.Forms.Button();
            this.btnalmacen = new System.Windows.Forms.Button();
            this.btnalbaran = new System.Windows.Forms.Button();
            this.btnevaluar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.datagridcabecera = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.datagridcabecera)).BeginInit();
            this.SuspendLayout();
            // 
            // btnenviarpedido
            // 
            this.btnenviarpedido.Enabled = false;
            this.btnenviarpedido.Location = new System.Drawing.Point(53, 653);
            this.btnenviarpedido.Name = "btnenviarpedido";
            this.btnenviarpedido.Size = new System.Drawing.Size(118, 31);
            this.btnenviarpedido.TabIndex = 45;
            this.btnenviarpedido.Text = "EnviarPedido";
            this.btnenviarpedido.UseVisualStyleBackColor = true;
            this.btnenviarpedido.Click += new System.EventHandler(this.btnenviarpedido_Click);
            // 
            // btnfactura
            // 
            this.btnfactura.Enabled = false;
            this.btnfactura.Location = new System.Drawing.Point(187, 599);
            this.btnfactura.Name = "btnfactura";
            this.btnfactura.Size = new System.Drawing.Size(118, 31);
            this.btnfactura.TabIndex = 44;
            this.btnfactura.Text = "Generar Factura";
            this.btnfactura.UseVisualStyleBackColor = true;
            this.btnfactura.Click += new System.EventHandler(this.btnfactura_Click);
            // 
            // btnsalir
            // 
            this.btnsalir.Location = new System.Drawing.Point(325, 653);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(118, 31);
            this.btnsalir.TabIndex = 43;
            this.btnsalir.Text = "Salir";
            this.btnsalir.UseVisualStyleBackColor = true;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // btnalmacen
            // 
            this.btnalmacen.Location = new System.Drawing.Point(187, 653);
            this.btnalmacen.Name = "btnalmacen";
            this.btnalmacen.Size = new System.Drawing.Size(118, 31);
            this.btnalmacen.TabIndex = 42;
            this.btnalmacen.Text = "Almacen";
            this.btnalmacen.UseVisualStyleBackColor = true;
            this.btnalmacen.Click += new System.EventHandler(this.btnalmacen_Click);
            // 
            // btnalbaran
            // 
            this.btnalbaran.Enabled = false;
            this.btnalbaran.Location = new System.Drawing.Point(53, 599);
            this.btnalbaran.Name = "btnalbaran";
            this.btnalbaran.Size = new System.Drawing.Size(118, 31);
            this.btnalbaran.TabIndex = 41;
            this.btnalbaran.Text = "Crear Albaran";
            this.btnalbaran.UseVisualStyleBackColor = true;
            this.btnalbaran.Click += new System.EventHandler(this.btnalbaran_Click);
            // 
            // btnevaluar
            // 
            this.btnevaluar.Enabled = false;
            this.btnevaluar.Location = new System.Drawing.Point(325, 599);
            this.btnevaluar.Name = "btnevaluar";
            this.btnevaluar.Size = new System.Drawing.Size(118, 31);
            this.btnevaluar.TabIndex = 40;
            this.btnevaluar.Text = "Evaluar Pedido";
            this.btnevaluar.UseVisualStyleBackColor = true;
            this.btnevaluar.Click += new System.EventHandler(this.btnevaluar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(221, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 20);
            this.label1.TabIndex = 39;
            this.label1.Text = "Pedidos pedientes:";
            // 
            // datagridcabecera
            // 
            this.datagridcabecera.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridcabecera.Location = new System.Drawing.Point(53, 53);
            this.datagridcabecera.Name = "datagridcabecera";
            this.datagridcabecera.ReadOnly = true;
            this.datagridcabecera.Size = new System.Drawing.Size(525, 510);
            this.datagridcabecera.TabIndex = 38;
            this.datagridcabecera.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridcabecera_CellClick);
            // 
            // GestionPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 693);
            this.ControlBox = false;
            this.Controls.Add(this.btnenviarpedido);
            this.Controls.Add(this.btnfactura);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.btnalmacen);
            this.Controls.Add(this.btnalbaran);
            this.Controls.Add(this.btnevaluar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.datagridcabecera);
            this.Name = "GestionPedidos";
            this.Text = "GestionPedidos";
            this.Load += new System.EventHandler(this.GestionPedidos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagridcabecera)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnenviarpedido;
        private System.Windows.Forms.Button btnfactura;
        private System.Windows.Forms.Button btnsalir;
        private System.Windows.Forms.Button btnalmacen;
        private System.Windows.Forms.Button btnalbaran;
        private System.Windows.Forms.Button btnevaluar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView datagridcabecera;
    }
}