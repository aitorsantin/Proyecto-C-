using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CapaIntermedia;

namespace CapaCliente
{
    public partial class BuscarArticulos : Form
    {
        BL_Articulos blarticulo = new BL_Articulos();
        string valor;
        DataSet dsArticulos = new DataSet();
        public static string codarticulo;
        public static string descripcion;
        public static decimal precio;
        public BuscarArticulos()
        {
            InitializeComponent();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            valor = txtcodigo.Text;
            dsArticulos = blarticulo.BL_BuscarArticulo(valor);
            datagridarticulos.DataSource = dsArticulos.Tables[0];
            btnbuscar.Enabled = false;
        }

        private void datagridarticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            codarticulo = datagridarticulos.Rows[datagridarticulos.CurrentRow.Index].Cells[0].Value.ToString();
            precio = decimal.Parse(datagridarticulos.Rows[datagridarticulos.CurrentRow.Index].Cells[2].Value.ToString());
            descripcion = datagridarticulos.Rows[datagridarticulos.CurrentRow.Index].Cells[1].Value.ToString();
            this.Close();
            btnbuscar.Enabled = true;
        }
    }
}
