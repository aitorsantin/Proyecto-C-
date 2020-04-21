namespace CapaCliente
{
    partial class Almacen
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.datagridmovialmacen = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.cbEntradaSalida = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.articulosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dAM_AitorSantinDataSet = new CapaCliente.DAM_AitorSantinDataSet();
            this.btnsolicitar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtcodarticulo = new System.Windows.Forms.TextBox();
            this.txtdescripcion = new System.Windows.Forms.TextBox();
            this.txtcantidad = new System.Windows.Forms.TextBox();
            this.btnvolver = new System.Windows.Forms.Button();
            this.btnrecargar = new System.Windows.Forms.Button();
            this.pbArticulo = new System.Windows.Forms.PictureBox();
            this.articulosTableAdapter = new CapaCliente.DAM_AitorSantinDataSetTableAdapters.ArticulosTableAdapter();
            this.lblinfor = new System.Windows.Forms.Label();
            this.datagridarticulos = new System.Windows.Forms.DataGridView();
            this.codArticuloDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioCosteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockMedidoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockMaximoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stockMinimoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fotoDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.datagridmovialmacen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.articulosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dAM_AitorSantinDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbArticulo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagridarticulos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(505, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Movimientos Almacen";
            // 
            // datagridmovialmacen
            // 
            this.datagridmovialmacen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridmovialmacen.Location = new System.Drawing.Point(76, 105);
            this.datagridmovialmacen.Name = "datagridmovialmacen";
            this.datagridmovialmacen.Size = new System.Drawing.Size(977, 207);
            this.datagridmovialmacen.TabIndex = 1;
            this.datagridmovialmacen.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridmovialmacen_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 743);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Codigo Articulo";
            // 
            // cbEntradaSalida
            // 
            this.cbEntradaSalida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEntradaSalida.FormattingEnabled = true;
            this.cbEntradaSalida.Location = new System.Drawing.Point(71, 69);
            this.cbEntradaSalida.Name = "cbEntradaSalida";
            this.cbEntradaSalida.Size = new System.Drawing.Size(121, 21);
            this.cbEntradaSalida.TabIndex = 3;
            this.cbEntradaSalida.SelectedIndexChanged += new System.EventHandler(this.cbEntradaSalida_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Entradas/Salidas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(381, 743);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Descripcion";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(610, 748);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Cantidad";
            // 
            // articulosBindingSource
            // 
            this.articulosBindingSource.DataMember = "Articulos";
            this.articulosBindingSource.DataSource = this.dAM_AitorSantinDataSet;
            // 
            // dAM_AitorSantinDataSet
            // 
            this.dAM_AitorSantinDataSet.DataSetName = "DAM_AitorSantinDataSet";
            this.dAM_AitorSantinDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnsolicitar
            // 
            this.btnsolicitar.Location = new System.Drawing.Point(762, 775);
            this.btnsolicitar.Name = "btnsolicitar";
            this.btnsolicitar.Size = new System.Drawing.Size(75, 23);
            this.btnsolicitar.TabIndex = 9;
            this.btnsolicitar.Text = "Solicitar Articulo";
            this.btnsolicitar.UseVisualStyleBackColor = true;
            this.btnsolicitar.Click += new System.EventHandler(this.btnsolicitar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(505, 403);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(221, 25);
            this.label6.TabIndex = 10;
            this.label6.Text = "Listado de Articulos";
            // 
            // txtcodarticulo
            // 
            this.txtcodarticulo.Location = new System.Drawing.Point(285, 775);
            this.txtcodarticulo.Name = "txtcodarticulo";
            this.txtcodarticulo.ReadOnly = true;
            this.txtcodarticulo.Size = new System.Drawing.Size(75, 20);
            this.txtcodarticulo.TabIndex = 11;
            // 
            // txtdescripcion
            // 
            this.txtdescripcion.Location = new System.Drawing.Point(384, 775);
            this.txtdescripcion.Name = "txtdescripcion";
            this.txtdescripcion.ReadOnly = true;
            this.txtdescripcion.Size = new System.Drawing.Size(204, 20);
            this.txtdescripcion.TabIndex = 12;
            // 
            // txtcantidad
            // 
            this.txtcantidad.Location = new System.Drawing.Point(613, 775);
            this.txtcantidad.Name = "txtcantidad";
            this.txtcantidad.ReadOnly = true;
            this.txtcantidad.Size = new System.Drawing.Size(100, 20);
            this.txtcantidad.TabIndex = 13;
            this.txtcantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcantidad_KeyPress);
            // 
            // btnvolver
            // 
            this.btnvolver.Location = new System.Drawing.Point(998, 772);
            this.btnvolver.Name = "btnvolver";
            this.btnvolver.Size = new System.Drawing.Size(75, 23);
            this.btnvolver.TabIndex = 14;
            this.btnvolver.Text = "Salir";
            this.btnvolver.UseVisualStyleBackColor = true;
            this.btnvolver.Click += new System.EventHandler(this.btnvolver_Click);
            // 
            // btnrecargar
            // 
            this.btnrecargar.Location = new System.Drawing.Point(76, 457);
            this.btnrecargar.Name = "btnrecargar";
            this.btnrecargar.Size = new System.Drawing.Size(103, 23);
            this.btnrecargar.TabIndex = 15;
            this.btnrecargar.Text = "Volver a cargar";
            this.btnrecargar.UseVisualStyleBackColor = true;
            // 
            // pbArticulo
            // 
            this.pbArticulo.Location = new System.Drawing.Point(109, 700);
            this.pbArticulo.Name = "pbArticulo";
            this.pbArticulo.Size = new System.Drawing.Size(137, 113);
            this.pbArticulo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbArticulo.TabIndex = 16;
            this.pbArticulo.TabStop = false;
            // 
            // articulosTableAdapter
            // 
            this.articulosTableAdapter.ClearBeforeFill = true;
            // 
            // lblinfor
            // 
            this.lblinfor.AutoSize = true;
            this.lblinfor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblinfor.ForeColor = System.Drawing.Color.Blue;
            this.lblinfor.Location = new System.Drawing.Point(71, 334);
            this.lblinfor.Name = "lblinfor";
            this.lblinfor.Size = new System.Drawing.Size(46, 18);
            this.lblinfor.TabIndex = 17;
            this.lblinfor.Text = "label7";
            this.lblinfor.Visible = false;
            // 
            // datagridarticulos
            // 
            this.datagridarticulos.AllowUserToAddRows = false;
            this.datagridarticulos.AllowUserToDeleteRows = false;
            this.datagridarticulos.AutoGenerateColumns = false;
            this.datagridarticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridarticulos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codArticuloDataGridViewTextBoxColumn,
            this.descripcionDataGridViewTextBoxColumn,
            this.precioCosteDataGridViewTextBoxColumn,
            this.stockDataGridViewTextBoxColumn,
            this.stockMedidoDataGridViewTextBoxColumn,
            this.stockMaximoDataGridViewTextBoxColumn,
            this.stockMinimoDataGridViewTextBoxColumn,
            this.fotoDataGridViewImageColumn});
            this.datagridarticulos.DataSource = this.articulosBindingSource;
            this.datagridarticulos.Location = new System.Drawing.Point(197, 457);
            this.datagridarticulos.Name = "datagridarticulos";
            this.datagridarticulos.ReadOnly = true;
            this.datagridarticulos.Size = new System.Drawing.Size(837, 237);
            this.datagridarticulos.TabIndex = 18;
            this.datagridarticulos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridarticulos_CellClick);
            // 
            // codArticuloDataGridViewTextBoxColumn
            // 
            this.codArticuloDataGridViewTextBoxColumn.DataPropertyName = "CodArticulo";
            this.codArticuloDataGridViewTextBoxColumn.HeaderText = "CodArticulo";
            this.codArticuloDataGridViewTextBoxColumn.Name = "codArticuloDataGridViewTextBoxColumn";
            this.codArticuloDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descripcionDataGridViewTextBoxColumn
            // 
            this.descripcionDataGridViewTextBoxColumn.DataPropertyName = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn.HeaderText = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn.Name = "descripcionDataGridViewTextBoxColumn";
            this.descripcionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // precioCosteDataGridViewTextBoxColumn
            // 
            this.precioCosteDataGridViewTextBoxColumn.DataPropertyName = "PrecioCoste";
            this.precioCosteDataGridViewTextBoxColumn.HeaderText = "PrecioCoste";
            this.precioCosteDataGridViewTextBoxColumn.Name = "precioCosteDataGridViewTextBoxColumn";
            this.precioCosteDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stockDataGridViewTextBoxColumn
            // 
            this.stockDataGridViewTextBoxColumn.DataPropertyName = "Stock";
            this.stockDataGridViewTextBoxColumn.HeaderText = "Stock";
            this.stockDataGridViewTextBoxColumn.Name = "stockDataGridViewTextBoxColumn";
            this.stockDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stockMedidoDataGridViewTextBoxColumn
            // 
            this.stockMedidoDataGridViewTextBoxColumn.DataPropertyName = "Stock_Medido";
            this.stockMedidoDataGridViewTextBoxColumn.HeaderText = "Stock_Medido";
            this.stockMedidoDataGridViewTextBoxColumn.Name = "stockMedidoDataGridViewTextBoxColumn";
            this.stockMedidoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stockMaximoDataGridViewTextBoxColumn
            // 
            this.stockMaximoDataGridViewTextBoxColumn.DataPropertyName = "Stock_Maximo";
            this.stockMaximoDataGridViewTextBoxColumn.HeaderText = "Stock_Maximo";
            this.stockMaximoDataGridViewTextBoxColumn.Name = "stockMaximoDataGridViewTextBoxColumn";
            this.stockMaximoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stockMinimoDataGridViewTextBoxColumn
            // 
            this.stockMinimoDataGridViewTextBoxColumn.DataPropertyName = "Stock_Minimo";
            this.stockMinimoDataGridViewTextBoxColumn.HeaderText = "Stock_Minimo";
            this.stockMinimoDataGridViewTextBoxColumn.Name = "stockMinimoDataGridViewTextBoxColumn";
            this.stockMinimoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fotoDataGridViewImageColumn
            // 
            this.fotoDataGridViewImageColumn.DataPropertyName = "Foto";
            this.fotoDataGridViewImageColumn.HeaderText = "Foto";
            this.fotoDataGridViewImageColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.fotoDataGridViewImageColumn.Name = "fotoDataGridViewImageColumn";
            this.fotoDataGridViewImageColumn.ReadOnly = true;
            // 
            // Almacen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 838);
            this.ControlBox = false;
            this.Controls.Add(this.datagridarticulos);
            this.Controls.Add(this.lblinfor);
            this.Controls.Add(this.pbArticulo);
            this.Controls.Add(this.btnrecargar);
            this.Controls.Add(this.btnvolver);
            this.Controls.Add(this.txtcantidad);
            this.Controls.Add(this.txtdescripcion);
            this.Controls.Add(this.txtcodarticulo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnsolicitar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbEntradaSalida);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.datagridmovialmacen);
            this.Controls.Add(this.label1);
            this.Name = "Almacen";
            this.Text = "Almacen";
            this.Load += new System.EventHandler(this.Almacen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagridmovialmacen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.articulosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dAM_AitorSantinDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbArticulo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagridarticulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView datagridmovialmacen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbEntradaSalida;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnsolicitar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtcodarticulo;
        private System.Windows.Forms.TextBox txtdescripcion;
        private System.Windows.Forms.TextBox txtcantidad;
        private System.Windows.Forms.Button btnvolver;
        private System.Windows.Forms.Button btnrecargar;
        private System.Windows.Forms.PictureBox pbArticulo;
        private DAM_AitorSantinDataSet dAM_AitorSantinDataSet;
        private System.Windows.Forms.BindingSource articulosBindingSource;
        private DAM_AitorSantinDataSetTableAdapters.ArticulosTableAdapter articulosTableAdapter;
        private System.Windows.Forms.Label lblinfor;
        private System.Windows.Forms.DataGridView datagridarticulos;
        private System.Windows.Forms.DataGridViewTextBoxColumn codArticuloDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioCosteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockMedidoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockMaximoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stockMinimoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn fotoDataGridViewImageColumn;
    }
}