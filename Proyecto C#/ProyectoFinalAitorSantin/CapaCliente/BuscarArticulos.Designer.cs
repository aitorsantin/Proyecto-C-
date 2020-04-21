namespace CapaCliente
{
    partial class BuscarArticulos
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
            this.datagridarticulos = new System.Windows.Forms.DataGridView();
            this.btnbuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtcodigo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.datagridarticulos)).BeginInit();
            this.SuspendLayout();
            // 
            // datagridarticulos
            // 
            this.datagridarticulos.AllowUserToAddRows = false;
            this.datagridarticulos.AllowUserToDeleteRows = false;
            this.datagridarticulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridarticulos.Location = new System.Drawing.Point(23, 103);
            this.datagridarticulos.Name = "datagridarticulos";
            this.datagridarticulos.ReadOnly = true;
            this.datagridarticulos.Size = new System.Drawing.Size(739, 332);
            this.datagridarticulos.TabIndex = 11;
            this.datagridarticulos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridarticulos_CellDoubleClick);
            // 
            // btnbuscar
            // 
            this.btnbuscar.Location = new System.Drawing.Point(270, 39);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(75, 23);
            this.btnbuscar.TabIndex = 10;
            this.btnbuscar.Text = "Buscar";
            this.btnbuscar.UseVisualStyleBackColor = true;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Introduzca el codigo o el mombre";
            // 
            // txtcodigo
            // 
            this.txtcodigo.Location = new System.Drawing.Point(20, 42);
            this.txtcodigo.Name = "txtcodigo";
            this.txtcodigo.Size = new System.Drawing.Size(163, 20);
            this.txtcodigo.TabIndex = 8;
            // 
            // BuscarArticulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 459);
            this.Controls.Add(this.datagridarticulos);
            this.Controls.Add(this.btnbuscar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtcodigo);
            this.Name = "BuscarArticulos";
            this.Text = "BuscarArticulos";
            ((System.ComponentModel.ISupportInitialize)(this.datagridarticulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView datagridarticulos;
        private System.Windows.Forms.Button btnbuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtcodigo;
    }
}