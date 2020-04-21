namespace CapaCliente
{
    partial class NuevoPedido
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
            this.label6 = new System.Windows.Forms.Label();
            this.txtpagado = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtestado = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btncolocar = new System.Windows.Forms.Button();
            this.cbcomercial = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbcliente = new System.Windows.Forms.ComboBox();
            this.txtfecha = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Buscar = new System.Windows.Forms.Button();
            this.txtc = new System.Windows.Forms.TextBox();
            this.txtdes = new System.Windows.Forms.TextBox();
            this.txtpre = new System.Windows.Forms.TextBox();
            this.txtcant = new System.Windows.Forms.TextBox();
            this.txtnpedido = new System.Windows.Forms.TextBox();
            this.lbltotal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnpagado = new System.Windows.Forms.Button();
            this.btnguardar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btneliminar = new System.Windows.Forms.Button();
            this.datagridlineas = new System.Windows.Forms.DataGridView();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NLinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.datagridlineas)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(462, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 155;
            this.label6.Text = "Cantidad";
            // 
            // txtpagado
            // 
            this.txtpagado.Enabled = false;
            this.txtpagado.Location = new System.Drawing.Point(352, 100);
            this.txtpagado.Name = "txtpagado";
            this.txtpagado.ReadOnly = true;
            this.txtpagado.Size = new System.Drawing.Size(100, 20);
            this.txtpagado.TabIndex = 154;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(290, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 153;
            this.label3.Text = "Pagado:";
            // 
            // txtestado
            // 
            this.txtestado.Enabled = false;
            this.txtestado.Location = new System.Drawing.Point(352, 59);
            this.txtestado.Name = "txtestado";
            this.txtestado.ReadOnly = true;
            this.txtestado.Size = new System.Drawing.Size(100, 20);
            this.txtestado.TabIndex = 152;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(290, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 151;
            this.label2.Text = "Estado:";
            // 
            // btncolocar
            // 
            this.btncolocar.Location = new System.Drawing.Point(571, 192);
            this.btncolocar.Name = "btncolocar";
            this.btncolocar.Size = new System.Drawing.Size(75, 23);
            this.btncolocar.TabIndex = 150;
            this.btncolocar.Text = "Colocar";
            this.btncolocar.UseVisualStyleBackColor = true;
            this.btncolocar.Click += new System.EventHandler(this.btncolocar_Click);
            // 
            // cbcomercial
            // 
            this.cbcomercial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcomercial.FormattingEnabled = true;
            this.cbcomercial.Location = new System.Drawing.Point(81, 51);
            this.cbcomercial.Name = "cbcomercial";
            this.cbcomercial.Size = new System.Drawing.Size(140, 21);
            this.cbcomercial.TabIndex = 149;
            this.cbcomercial.SelectionChangeCommitted += new System.EventHandler(this.cbcomercial_SelectionChangeCommitted);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 13);
            this.label13.TabIndex = 148;
            this.label13.Text = "Empleados:";
            // 
            // cbcliente
            // 
            this.cbcliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcliente.FormattingEnabled = true;
            this.cbcliente.Location = new System.Drawing.Point(81, 95);
            this.cbcliente.Name = "cbcliente";
            this.cbcliente.Size = new System.Drawing.Size(140, 21);
            this.cbcliente.TabIndex = 147;
            // 
            // txtfecha
            // 
            this.txtfecha.Enabled = false;
            this.txtfecha.Location = new System.Drawing.Point(352, 19);
            this.txtfecha.Name = "txtfecha";
            this.txtfecha.ReadOnly = true;
            this.txtfecha.Size = new System.Drawing.Size(100, 20);
            this.txtfecha.TabIndex = 146;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(290, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 145;
            this.label11.Text = "Fecha:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(568, 542);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 18);
            this.label9.TabIndex = 144;
            this.label9.Text = "Total:";
            // 
            // Buscar
            // 
            this.Buscar.Location = new System.Drawing.Point(81, 129);
            this.Buscar.Name = "Buscar";
            this.Buscar.Size = new System.Drawing.Size(75, 23);
            this.Buscar.TabIndex = 143;
            this.Buscar.Text = "Buscar";
            this.Buscar.UseVisualStyleBackColor = true;
            this.Buscar.Click += new System.EventHandler(this.Buscar_Click);
            // 
            // txtc
            // 
            this.txtc.Enabled = false;
            this.txtc.Location = new System.Drawing.Point(66, 192);
            this.txtc.Name = "txtc";
            this.txtc.Size = new System.Drawing.Size(100, 20);
            this.txtc.TabIndex = 141;
            // 
            // txtdes
            // 
            this.txtdes.Enabled = false;
            this.txtdes.Location = new System.Drawing.Point(172, 192);
            this.txtdes.Name = "txtdes";
            this.txtdes.Size = new System.Drawing.Size(181, 20);
            this.txtdes.TabIndex = 140;
            // 
            // txtpre
            // 
            this.txtpre.Enabled = false;
            this.txtpre.Location = new System.Drawing.Point(359, 192);
            this.txtpre.Name = "txtpre";
            this.txtpre.Size = new System.Drawing.Size(84, 20);
            this.txtpre.TabIndex = 139;
            this.txtpre.Text = "00.00€";
            // 
            // txtcant
            // 
            this.txtcant.Location = new System.Drawing.Point(449, 192);
            this.txtcant.Name = "txtcant";
            this.txtcant.Size = new System.Drawing.Size(96, 20);
            this.txtcant.TabIndex = 138;
            // 
            // txtnpedido
            // 
            this.txtnpedido.Enabled = false;
            this.txtnpedido.Location = new System.Drawing.Point(81, 16);
            this.txtnpedido.Name = "txtnpedido";
            this.txtnpedido.ReadOnly = true;
            this.txtnpedido.Size = new System.Drawing.Size(140, 20);
            this.txtnpedido.TabIndex = 137;
            // 
            // lbltotal
            // 
            this.lbltotal.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbltotal.Location = new System.Drawing.Point(631, 527);
            this.lbltotal.Name = "lbltotal";
            this.lbltotal.Size = new System.Drawing.Size(89, 33);
            this.lbltotal.TabIndex = 136;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 135;
            this.label5.Text = "Articulo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 134;
            this.label4.Text = "Pedido:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 133;
            this.label1.Text = "Cliente:";
            // 
            // btnpagado
            // 
            this.btnpagado.Location = new System.Drawing.Point(772, 337);
            this.btnpagado.Name = "btnpagado";
            this.btnpagado.Size = new System.Drawing.Size(154, 29);
            this.btnpagado.TabIndex = 132;
            this.btnpagado.Text = "Pagar";
            this.btnpagado.UseVisualStyleBackColor = true;
            this.btnpagado.Click += new System.EventHandler(this.btnpagado_Click);
            // 
            // btnguardar
            // 
            this.btnguardar.Location = new System.Drawing.Point(772, 393);
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(154, 29);
            this.btnguardar.TabIndex = 131;
            this.btnguardar.Text = "Guardar Pedido";
            this.btnguardar.UseVisualStyleBackColor = true;
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(772, 451);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(154, 29);
            this.btnSalir.TabIndex = 130;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btneliminar
            // 
            this.btneliminar.Location = new System.Drawing.Point(772, 276);
            this.btneliminar.Name = "btneliminar";
            this.btneliminar.Size = new System.Drawing.Size(154, 29);
            this.btneliminar.TabIndex = 129;
            this.btneliminar.Text = "Eliminar";
            this.btneliminar.UseVisualStyleBackColor = true;
            this.btneliminar.Click += new System.EventHandler(this.btneliminar_Click);
            // 
            // datagridlineas
            // 
            this.datagridlineas.AllowUserToAddRows = false;
            this.datagridlineas.AllowUserToDeleteRows = false;
            this.datagridlineas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagridlineas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridlineas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.NLinea,
            this.Descripcion,
            this.Precio,
            this.Cantidad,
            this.Importe});
            this.datagridlineas.Location = new System.Drawing.Point(34, 230);
            this.datagridlineas.Name = "datagridlineas";
            this.datagridlineas.ReadOnly = true;
            this.datagridlineas.Size = new System.Drawing.Size(612, 279);
            this.datagridlineas.TabIndex = 142;
            // 
            // Codigo
            // 
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            // 
            // NLinea
            // 
            this.NLinea.HeaderText = "NLinea";
            this.NLinea.Name = "NLinea";
            this.NLinea.ReadOnly = true;
            // 
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantida";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // Importe
            // 
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            // 
            // NuevoPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 576);
            this.ControlBox = false;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtpagado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtestado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btncolocar);
            this.Controls.Add(this.cbcomercial);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cbcliente);
            this.Controls.Add(this.txtfecha);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Buscar);
            this.Controls.Add(this.txtc);
            this.Controls.Add(this.txtdes);
            this.Controls.Add(this.txtpre);
            this.Controls.Add(this.txtcant);
            this.Controls.Add(this.txtnpedido);
            this.Controls.Add(this.lbltotal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnpagado);
            this.Controls.Add(this.btnguardar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btneliminar);
            this.Controls.Add(this.datagridlineas);
            this.Name = "NuevoPedido";
            this.Text = "NuevoPedido";
            this.Load += new System.EventHandler(this.NuevoPedido_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagridlineas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtpagado;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtestado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btncolocar;
        private System.Windows.Forms.ComboBox cbcomercial;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbcliente;
        private System.Windows.Forms.TextBox txtfecha;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button Buscar;
        private System.Windows.Forms.TextBox txtc;
        private System.Windows.Forms.TextBox txtdes;
        private System.Windows.Forms.TextBox txtpre;
        private System.Windows.Forms.TextBox txtcant;
        private System.Windows.Forms.TextBox txtnpedido;
        private System.Windows.Forms.Label lbltotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnpagado;
        private System.Windows.Forms.Button btnguardar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btneliminar;
        private System.Windows.Forms.DataGridView datagridlineas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NLinea;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Importe;
    }
}