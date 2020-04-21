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
using CapaEntidad;
using System.IO;

namespace CapaCliente
{
    public partial class Almacen : Form
    {
        BL_Almacen almacen = new BL_Almacen();
        DataSet dsMovimientosAlmacen = new DataSet();
        
        string codarticulo;
        string descripcion;

        public Almacen()
        {
            InitializeComponent();
        }

        private void Almacen_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dAM_AitorSantinDataSet.Articulos' Puede moverla o quitarla según sea necesario.
            this.articulosTableAdapter.Fill(this.dAM_AitorSantinDataSet.Articulos);
            dsMovimientosAlmacen = almacen.BL_CargarGridMovimientosAlmacen();
            CargarGridMovimientosAlmacen(dsMovimientosAlmacen);
           // CargarGridArticulos();
            LlenarCombobox();
        }

        private void CargarGridArticulos()
        {
            DataSet dsArticulos = new DataSet();
            dsArticulos = almacen.BL_CargarGridArticulos();
            datagridarticulos.DataSource = dsArticulos.Tables["Articulos"];

            if (datagridarticulos.CurrentRow.Cells["Foto"].Value == null)
            {
                datagridarticulos.CurrentRow.Cells["Foto"].Value = CapaCliente.Properties.Resources.nodisponible;
            }

        }

        private void LlenarCombobox()
        {
            cbEntradaSalida.Items.Add("Sin relebancia");
            cbEntradaSalida.Items.Add("Entrada");
            cbEntradaSalida.Items.Add("Salida");
            cbEntradaSalida.SelectedIndex = 0;
        }

        private void CargarGridMovimientosAlmacen(DataSet dsMovimientosAlmacen)
        {
            datagridmovialmacen.DataSource = dsMovimientosAlmacen.Tables["MoviAlmacen"];
        }

        private void cbEntradaSalida_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEntradaSalida.SelectedIndex==0)
            {
                //Recargar Dataset
                dsMovimientosAlmacen.Tables["MoviAlmacen"].Rows.Clear();
                dsMovimientosAlmacen = almacen.BL_CargarGridMovimientosAlmacen();
                CargarGridMovimientosAlmacen(dsMovimientosAlmacen);

            }
            if (cbEntradaSalida.SelectedIndex==1)
            {
                dsMovimientosAlmacen.Tables["MoviAlmacen"].Rows.Clear();
                dsMovimientosAlmacen = almacen.BL_CargarGridMovimientosAlmacen();
                CargarGridMovimientosAlmacen(dsMovimientosAlmacen);

                foreach (DataRow dr in dsMovimientosAlmacen.Tables["MoviAlmacen"].Rows)
                {
                    if (dr["EntradaSalida"].ToString()=="S")
                    {
                        dr.Delete();
                    }
                }
                CargarGridMovimientosAlmacen(dsMovimientosAlmacen);

            }
            if (cbEntradaSalida.SelectedIndex==2)
            {
                dsMovimientosAlmacen.Tables["MoviAlmacen"].Rows.Clear();
                dsMovimientosAlmacen = almacen.BL_CargarGridMovimientosAlmacen();
                CargarGridMovimientosAlmacen(dsMovimientosAlmacen);

                foreach (DataRow dr in dsMovimientosAlmacen.Tables["MoviAlmacen"].Rows)
                {
                    if (dr["EntradaSalida"].ToString() == "E")
                    {
                        dr.Delete();
                    }
                }
                CargarGridMovimientosAlmacen(dsMovimientosAlmacen);
            }
        }

        private void datagridmovialmacen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblinfor.Text = "";
            codarticulo = datagridmovialmacen.Rows[datagridmovialmacen.CurrentRow.Index].Cells[0].Value.ToString();
            
            int entrada, salida;
            entrada = 0;
            salida = 0;
            

            List<EN_MoviAlmacen> listmovialmacen = new List<EN_MoviAlmacen>();
            listmovialmacen = almacen.BL_LlenarObjetoAlmacen();

            foreach (var item in listmovialmacen)
            {
                if (codarticulo==item.CodArticulo)
                {
                    if (item.EntradaSalida=="E")
                    {
                        entrada += item.Cantidad;
                    }
                    if (item.EntradaSalida == "S")
                    {
                        salida += item.Cantidad;
                    }
                }
            }
            DataRow dr = dAM_AitorSantinDataSet.Tables["Articulos"].Rows.Find(codarticulo);

            if (dr!=null)
            {
                descripcion = dr["Descripcion"].ToString();
            }
            lblinfor.Visible = true;
            lblinfor.Text= "El articulo:" + " " + descripcion + " " + "con Codigo: " + " " + codarticulo + " "
                + "Se han comprado un total de: " + " " + entrada + " " + "unidades" + " " +
                "y han sido vendidas un total de: " +
                " " + salida + " " + "unidades";

        }

        private void datagridarticulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EN_Articulo articulo = new EN_Articulo();
            articulo.codArticulo = datagridarticulos.SelectedCells[0].Value.ToString();
            txtcodarticulo.Text = articulo.codArticulo;
            articulo = almacen.BL_MostrarArticulo(articulo);
            txtdescripcion.Text = articulo.descripcion;
            if (articulo.foto==null)
            {
                pbArticulo.Image = CapaCliente.Properties.Resources.nodisponible;
            }
            else
            {
                MemoryStream ms = new MemoryStream(articulo.foto);
                pbArticulo.Image = Image.FromStream(ms);
                txtcantidad.ReadOnly = false;
            }
            
        }

        private void btnsolicitar_Click(object sender, EventArgs e)
        {
            int stock = int.Parse(datagridarticulos.Rows[datagridarticulos.CurrentRow.Index].Cells[3].Value.ToString());
            int maximo=int.Parse(datagridarticulos.Rows[datagridarticulos.CurrentRow.Index].Cells[5].Value.ToString());
            int cantidad = int.Parse(txtcantidad.Text.ToString());
            int sumCantidad = cantidad + stock;

            int resultado = almacen.BL_EvaluarCantidadaSolicitar(sumCantidad, maximo);

            codarticulo = txtcodarticulo.Text;

            bool result=false;

            DialogResult dResult;

            if (resultado==1)
            {
                dResult = MessageBox.Show("El Stock Maximo esta a 0 eso significa que no se han realizado compras de " +
                    "ese articulo o que todavia no se han realizado los calculos," +
                    " ¿Esta seguro de que quiere comprarlo?", "Advertencia de la aplicacion", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dResult==DialogResult.Yes)
                {
                    result = almacen.BL_ComprarArticulos(codarticulo, cantidad);
                    if (result==true)
                    {
                        MessageBox.Show("Articulos solicitados al proveedor","Informacion de la aplicacion",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        VaciarCamposSolicitados();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error y no se ha podido realizar la solicitud", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (dResult==DialogResult.No)
                {
                    VaciarCamposSolicitados();
                }
            }
            else if (resultado==2)
            {
                dResult = MessageBox.Show("Va a realizar una comprar que superar el Stock Maximo " +
                   "¿Esta seguro de que quiere comprar?", "Advertencia de la aplicacion",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dResult == DialogResult.Yes)
                {
                    result = almacen.BL_ComprarArticulos(codarticulo, cantidad);
                    if (result == true)
                    {
                        MessageBox.Show("Articulos solicitados al proveedor", "Informacion de la aplicacion",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        VaciarCamposSolicitados();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error y no se ha podido realizar la solicitud", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (dResult == DialogResult.No)
                {
                    VaciarCamposSolicitados();
                }
            }
            else if (resultado == 3)
            {
                result = almacen.BL_ComprarArticulos(codarticulo, cantidad);
                if (result == true)
                {
                    MessageBox.Show("Articulos solicitados al proveedor", "Informacion de la aplicacion",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    VaciarCamposSolicitados();
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error y no se ha podido realizar la solicitud", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void VaciarCamposSolicitados()
        {
            txtcodarticulo.Text = "";
            txtcantidad.Text = "";
            txtdescripcion.Text = "";
            pbArticulo.Image = null;
        }

        private void txtcantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                
                e.Handled = false;
            }
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            
        }

        private void btnvolver_Click(object sender, EventArgs e)
        {
            Principal frmPrincipal = new Principal();
            frmPrincipal.Show();
            this.Close();
        }
    }
}
