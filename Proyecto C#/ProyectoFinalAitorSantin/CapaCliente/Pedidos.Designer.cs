namespace CapaCliente
{
    partial class Pedidos
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
            this.cbcomercial = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtpagado = new System.Windows.Forms.TextBox();
            this.btnpagar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnsalir = new System.Windows.Forms.Button();
            this.btnelimiar = new System.Windows.Forms.Button();
            this.btnmodificar = new System.Windows.Forms.Button();
            this.btnnuevo = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txttipo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.datagridLinPedidos = new System.Windows.Forms.DataGridView();
            this.gridcpedidos = new System.Windows.Forms.DataGridView();
            this.cbclientes = new System.Windows.Forms.ComboBox();
            this.btncancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.datagridLinPedidos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridcpedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 80;
            this.label6.Text = "Comercial";
            // 
            // cbcomercial
            // 
            this.cbcomercial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcomercial.FormattingEnabled = true;
            this.cbcomercial.Location = new System.Drawing.Point(19, 94);
            this.cbcomercial.Name = "cbcomercial";
            this.cbcomercial.Size = new System.Drawing.Size(219, 21);
            this.cbcomercial.TabIndex = 79;
            this.cbcomercial.SelectionChangeCommitted += new System.EventHandler(this.cbcomercial_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(685, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 78;
            this.label7.Text = "Estado del Pago";
            // 
            // txtpagado
            // 
            this.txtpagado.Location = new System.Drawing.Point(688, 98);
            this.txtpagado.Name = "txtpagado";
            this.txtpagado.ReadOnly = true;
            this.txtpagado.Size = new System.Drawing.Size(100, 20);
            this.txtpagado.TabIndex = 77;
            // 
            // btnpagar
            // 
            this.btnpagar.Enabled = false;
            this.btnpagar.Location = new System.Drawing.Point(807, 95);
            this.btnpagar.Name = "btnpagar";
            this.btnpagar.Size = new System.Drawing.Size(112, 23);
            this.btnpagar.TabIndex = 76;
            this.btnpagar.Text = "Pagar";
            this.btnpagar.UseVisualStyleBackColor = true;
            this.btnpagar.Click += new System.EventHandler(this.btnpagar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(394, 324);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 75;
            this.label5.Text = "Detalle de Pedidos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(445, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 74;
            this.label4.Text = "Pedidos";
            // 
            // btnsalir
            // 
            this.btnsalir.Location = new System.Drawing.Point(807, 566);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Size = new System.Drawing.Size(112, 23);
            this.btnsalir.TabIndex = 73;
            this.btnsalir.Text = "Salir";
            this.btnsalir.UseVisualStyleBackColor = true;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // btnelimiar
            // 
            this.btnelimiar.Location = new System.Drawing.Point(807, 426);
            this.btnelimiar.Name = "btnelimiar";
            this.btnelimiar.Size = new System.Drawing.Size(112, 23);
            this.btnelimiar.TabIndex = 72;
            this.btnelimiar.Text = "Eliminar";
            this.btnelimiar.UseVisualStyleBackColor = true;
            this.btnelimiar.Click += new System.EventHandler(this.btnelimiar_Click);
            // 
            // btnmodificar
            // 
            this.btnmodificar.Location = new System.Drawing.Point(807, 351);
            this.btnmodificar.Name = "btnmodificar";
            this.btnmodificar.Size = new System.Drawing.Size(112, 23);
            this.btnmodificar.TabIndex = 71;
            this.btnmodificar.Text = "Modificar";
            this.btnmodificar.UseVisualStyleBackColor = true;
            this.btnmodificar.Click += new System.EventHandler(this.btnmodificar_Click);
            // 
            // btnnuevo
            // 
            this.btnnuevo.Location = new System.Drawing.Point(33, 172);
            this.btnnuevo.Name = "btnnuevo";
            this.btnnuevo.Size = new System.Drawing.Size(112, 23);
            this.btnnuevo.TabIndex = 70;
            this.btnnuevo.Text = "Nuevo Pedido";
            this.btnnuevo.UseVisualStyleBackColor = true;
            this.btnnuevo.Click += new System.EventHandler(this.btnnuevo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(358, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 25);
            this.label3.TabIndex = 69;
            this.label3.Text = "Pedidos";
            // 
            // txttipo
            // 
            this.txttipo.Location = new System.Drawing.Point(538, 95);
            this.txttipo.Name = "txttipo";
            this.txttipo.ReadOnly = true;
            this.txttipo.Size = new System.Drawing.Size(100, 20);
            this.txttipo.TabIndex = 68;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(535, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "Tipo de Cliente";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Clientes";
            // 
            // datagridLinPedidos
            // 
            this.datagridLinPedidos.AllowUserToAddRows = false;
            this.datagridLinPedidos.AllowUserToDeleteRows = false;
            this.datagridLinPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridLinPedidos.Location = new System.Drawing.Point(199, 351);
            this.datagridLinPedidos.Name = "datagridLinPedidos";
            this.datagridLinPedidos.Size = new System.Drawing.Size(589, 238);
            this.datagridLinPedidos.TabIndex = 65;
            this.datagridLinPedidos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridLinPedidos_CellContentClick);
            this.datagridLinPedidos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridLinPedidos_CellDoubleClick);
            // 
            // gridcpedidos
            // 
            this.gridcpedidos.AllowUserToAddRows = false;
            this.gridcpedidos.AllowUserToDeleteRows = false;
            this.gridcpedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridcpedidos.Location = new System.Drawing.Point(187, 172);
            this.gridcpedidos.Name = "gridcpedidos";
            this.gridcpedidos.ReadOnly = true;
            this.gridcpedidos.Size = new System.Drawing.Size(601, 136);
            this.gridcpedidos.TabIndex = 64;
            this.gridcpedidos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridcpedidos_CellDoubleClick);
            // 
            // cbclientes
            // 
            this.cbclientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbclientes.FormattingEnabled = true;
            this.cbclientes.IntegralHeight = false;
            this.cbclientes.Location = new System.Drawing.Point(253, 95);
            this.cbclientes.Name = "cbclientes";
            this.cbclientes.Size = new System.Drawing.Size(262, 21);
            this.cbclientes.TabIndex = 63;
            this.cbclientes.SelectedIndexChanged += new System.EventHandler(this.cbclientes_SelectedIndexChanged);
            // 
            // btncancelar
            // 
            this.btncancelar.Location = new System.Drawing.Point(807, 493);
            this.btncancelar.Name = "btncancelar";
            this.btncancelar.Size = new System.Drawing.Size(112, 23);
            this.btncancelar.TabIndex = 81;
            this.btncancelar.Text = "Cancelar";
            this.btncancelar.UseVisualStyleBackColor = true;
            this.btncancelar.Click += new System.EventHandler(this.btncancelar_Click);
            // 
            // Pedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 674);
            this.ControlBox = false;
            this.Controls.Add(this.btncancelar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbcomercial);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtpagado);
            this.Controls.Add(this.btnpagar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnsalir);
            this.Controls.Add(this.btnelimiar);
            this.Controls.Add(this.btnmodificar);
            this.Controls.Add(this.btnnuevo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txttipo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.datagridLinPedidos);
            this.Controls.Add(this.gridcpedidos);
            this.Controls.Add(this.cbclientes);
            this.Name = "Pedidos";
            this.Text = "Pedidos";
            this.Load += new System.EventHandler(this.Pedidos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagridLinPedidos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridcpedidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbcomercial;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtpagado;
        private System.Windows.Forms.Button btnpagar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnsalir;
        private System.Windows.Forms.Button btnelimiar;
        private System.Windows.Forms.Button btnmodificar;
        private System.Windows.Forms.Button btnnuevo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txttipo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView datagridLinPedidos;
        private System.Windows.Forms.DataGridView gridcpedidos;
        private System.Windows.Forms.ComboBox cbclientes;
        private System.Windows.Forms.Button btncancelar;
    }
}