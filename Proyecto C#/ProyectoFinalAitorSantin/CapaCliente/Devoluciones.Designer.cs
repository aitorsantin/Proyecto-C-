namespace CapaCliente
{
    partial class Devoluciones
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
            this.cbpedidosdevueltos = new System.Windows.Forms.ComboBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnEnviarAlmacen = new System.Windows.Forms.Button();
            this.btnDesechar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.datagridLineasPedido = new System.Windows.Forms.DataGridView();
            this.richTxtMotivo = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.datagridLineasPedido)).BeginInit();
            this.SuspendLayout();
            // 
            // cbpedidosdevueltos
            // 
            this.cbpedidosdevueltos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbpedidosdevueltos.FormattingEnabled = true;
            this.cbpedidosdevueltos.Location = new System.Drawing.Point(156, 58);
            this.cbpedidosdevueltos.Name = "cbpedidosdevueltos";
            this.cbpedidosdevueltos.Size = new System.Drawing.Size(121, 21);
            this.cbpedidosdevueltos.TabIndex = 57;
            this.cbpedidosdevueltos.SelectionChangeCommitted += new System.EventHandler(this.cbpedidosdevueltos_SelectionChangeCommitted);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(700, 440);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(59, 32);
            this.btnSalir.TabIndex = 56;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnEnviarAlmacen
            // 
            this.btnEnviarAlmacen.Enabled = false;
            this.btnEnviarAlmacen.Location = new System.Drawing.Point(647, 218);
            this.btnEnviarAlmacen.Name = "btnEnviarAlmacen";
            this.btnEnviarAlmacen.Size = new System.Drawing.Size(112, 40);
            this.btnEnviarAlmacen.TabIndex = 55;
            this.btnEnviarAlmacen.Text = "Envviar al Almacen";
            this.btnEnviarAlmacen.UseVisualStyleBackColor = true;
            this.btnEnviarAlmacen.Click += new System.EventHandler(this.btnEnviarAlmacen_Click);
            // 
            // btnDesechar
            // 
            this.btnDesechar.Enabled = false;
            this.btnDesechar.Location = new System.Drawing.Point(647, 156);
            this.btnDesechar.Name = "btnDesechar";
            this.btnDesechar.Size = new System.Drawing.Size(112, 42);
            this.btnDesechar.TabIndex = 54;
            this.btnDesechar.Text = "Desechar Articulo";
            this.btnDesechar.UseVisualStyleBackColor = true;
            this.btnDesechar.Click += new System.EventHandler(this.btnDesechar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "Numero de devolucion:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(235, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Articulos Devueltos";
            // 
            // datagridLineasPedido
            // 
            this.datagridLineasPedido.AllowUserToAddRows = false;
            this.datagridLineasPedido.AllowUserToDeleteRows = false;
            this.datagridLineasPedido.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagridLineasPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridLineasPedido.Location = new System.Drawing.Point(26, 139);
            this.datagridLineasPedido.Name = "datagridLineasPedido";
            this.datagridLineasPedido.ReadOnly = true;
            this.datagridLineasPedido.Size = new System.Drawing.Size(593, 188);
            this.datagridLineasPedido.TabIndex = 51;
            this.datagridLineasPedido.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridLineasPedido_CellClick);
            // 
            // richTxtMotivo
            // 
            this.richTxtMotivo.Location = new System.Drawing.Point(26, 376);
            this.richTxtMotivo.Name = "richTxtMotivo";
            this.richTxtMotivo.ReadOnly = true;
            this.richTxtMotivo.Size = new System.Drawing.Size(593, 96);
            this.richTxtMotivo.TabIndex = 50;
            this.richTxtMotivo.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 339);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "Motivo de la devolucion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(233, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 25);
            this.label2.TabIndex = 48;
            this.label2.Text = "Devoluciones";
            // 
            // Devoluciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 505);
            this.ControlBox = false;
            this.Controls.Add(this.cbpedidosdevueltos);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnEnviarAlmacen);
            this.Controls.Add(this.btnDesechar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.datagridLineasPedido);
            this.Controls.Add(this.richTxtMotivo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Name = "Devoluciones";
            this.Text = "Devoluciones";
            this.Load += new System.EventHandler(this.Devoluciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagridLineasPedido)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbpedidosdevueltos;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnEnviarAlmacen;
        private System.Windows.Forms.Button btnDesechar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView datagridLineasPedido;
        private System.Windows.Forms.RichTextBox richTxtMotivo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
    }
}