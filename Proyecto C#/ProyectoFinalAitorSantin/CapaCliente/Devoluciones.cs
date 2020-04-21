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
    public partial class Devoluciones : Form
    {
        DataSet dsPedidosDevueltos = new DataSet();
        BL_Devoluciones devoluciones = new BL_Devoluciones();
        string codDevolucion, codArticulo;
        int nLinea;
        public static int contador = 0;
        public Devoluciones()
        {
            InitializeComponent();
        }

        private void Devoluciones_Load(object sender, EventArgs e)
        {
            dsPedidosDevueltos = devoluciones.BL_CargarComboPedidosDevueltos();
            cbpedidosdevueltos.DataSource = dsPedidosDevueltos.Tables["CabDevoluciones"];
            cbpedidosdevueltos.DisplayMember = "CodDevolucion";
            cbpedidosdevueltos.ValueMember = "CodDevolucion";

        }


        private void cbpedidosdevueltos_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Cargamos las lineas de las devoluciones
            codDevolucion = cbpedidosdevueltos.SelectedValue.ToString();
            if (datagridLineasPedido.Rows.Count > 0)
            {
                dsPedidosDevueltos.Tables["DetDevoluciones"].Rows.Clear();
                datagridLineasPedido.DataSource = null;
            }
            dsPedidosDevueltos = devoluciones.BL_CargarLineasDevoluciones(codDevolucion);
            datagridLineasPedido.DataSource = dsPedidosDevueltos.Tables["DetDevoluciones"];
            contador = datagridLineasPedido.Rows.Count;
            //Almacenamos el numero de filas en el contador para ir comparando si las filas estan a 0 despues de cada 
            //operacion
        }

        private void datagridLineasPedido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDesechar.Enabled = true;
            btnEnviarAlmacen.Enabled = true;
         
            //Mostramos el comentario de la linea seleccionada
            codDevolucion = datagridLineasPedido.Rows[datagridLineasPedido.CurrentRow.Index].Cells[0].Value.ToString();
            nLinea = int.Parse(datagridLineasPedido.Rows[datagridLineasPedido.CurrentRow.Index].Cells[3].Value.ToString());
            string mensaje = devoluciones.BL_MostrarInformacion(codDevolucion, nLinea);

            if (mensaje=="")
            {
                richTxtMotivo.Text = "No hay informacion";
            }
            else
            {
                richTxtMotivo.Text = mensaje;
            }                     
        }

        private void btnDesechar_Click(object sender, EventArgs e)
        {
            codDevolucion = datagridLineasPedido.Rows[datagridLineasPedido.CurrentRow.Index].Cells[0].Value.ToString();
            nLinea = int.Parse(datagridLineasPedido.Rows[datagridLineasPedido.CurrentRow.Index].Cells[3].Value.ToString());

            bool resultado = devoluciones.BL_DesecharArticulo(codDevolucion, nLinea);

            if (resultado==true)
            {
                //Eliminar linea seleccinada
                datagridLineasPedido.Rows.RemoveAt(datagridLineasPedido.CurrentRow.Index);

                MessageBox.Show("El articulo a sido marcado como desechable", "Informacion de la aplicacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                richTxtMotivo.Text = "";
                contador--;
                if (contador == 0)
                {
                    MessageBox.Show("Todo el pedido a sido revisado", 
                        "Informacion de la aplicacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Actualizar el estado
                    bool result = devoluciones.BL_DevolucionRevisada(codDevolucion);
                    if (result==true)
                    {
                        ActualizarCombobox();
                    }
                }

            }
            if (resultado==false)
            {
                MessageBox.Show("Ha ocurrido un error y no hemos podido clasificar el articulo como desechable",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarCombobox()
        {
            dsPedidosDevueltos.Tables["CabDevoluciones"].Rows.Clear();
            dsPedidosDevueltos = devoluciones.BL_CargarComboPedidosDevueltos();
            cbpedidosdevueltos.DataSource = dsPedidosDevueltos.Tables["CabDevoluciones"];
            cbpedidosdevueltos.DisplayMember = "CodDevolucion";
            cbpedidosdevueltos.ValueMember = "CodDevolucion";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Principal principal = new Principal();
            principal.Show();
            this.Close();
        }

        private void btnEnviarAlmacen_Click(object sender, EventArgs e)
        {
            codArticulo = datagridLineasPedido.Rows[datagridLineasPedido.CurrentRow.Index].Cells[4].Value.ToString();
            int cantidad = int.Parse(datagridLineasPedido.Rows[datagridLineasPedido.CurrentRow.Index].Cells[5].Value.ToString());

            bool result = devoluciones.BL_DevolverArticuloAlmacen(codArticulo, cantidad);

            if (result==true)
            {

                //Actualizar estado a devuelto
                codDevolucion = datagridLineasPedido.Rows[datagridLineasPedido.CurrentRow.Index].Cells[0].Value.ToString();
                nLinea = int.Parse(datagridLineasPedido.Rows[datagridLineasPedido.CurrentRow.Index].Cells[3].Value.ToString());

                devoluciones.BL_ActualizarEstadoDevuelto(codDevolucion, nLinea);

                //Eliminar linea seleccinada
                datagridLineasPedido.Rows.RemoveAt(datagridLineasPedido.CurrentRow.Index);

                MessageBox.Show("El articulo a sido marcado como devuelto", "Informacion de la aplicacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                richTxtMotivo.Text = "";
                contador--;
                if (contador == 0)
                {
                    MessageBox.Show("Todo el pedido a sido revisado",
                        "Informacion de la aplicacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Actualizar el estado
                    bool resultado = devoluciones.BL_DevolucionRevisada(codDevolucion);
                   
                }
            }
            if (result==false)
            {
                MessageBox.Show("Ha ocurrido un error y no hemos podido clasificar el articulo como devuelto al almacen",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
    }
}
