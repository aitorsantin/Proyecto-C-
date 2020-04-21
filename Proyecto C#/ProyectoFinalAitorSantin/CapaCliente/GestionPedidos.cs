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
    public partial class GestionPedidos : Form
    {
        BL_GestionPedidos gestionpedidos = new BL_GestionPedidos();
        DataSet ds_gestionPedidos = new DataSet();
        int codempleado = Login.id;
        string codpedido;
        int estado;
        public GestionPedidos()
        {
            InitializeComponent();
        }

        private void GestionPedidos_Load(object sender, EventArgs e)
        {
            CargarGridPedidos();
        }

        public void CargarGridPedidos()
        {
            //Llenamos el dataset con los datos que nos pasan desde la capa intermedia
            ds_gestionPedidos = gestionpedidos.BL_PedidosPendientes();
            //llenamos el datagrid con los datos del dataset
            datagridcabecera.DataSource = ds_gestionPedidos.Tables["CabeceraPedido"];

            datagridcabecera.Rows[0].Selected = true;
            datagridcabecera.CurrentCell = datagridcabecera.Rows[0].Cells[0];
        }

        private void datagridcabecera_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Si seleccionamos la primera fila cargaremos las lineas del primer pedido
            if (datagridcabecera.CurrentRow == datagridcabecera.Rows[0])
            {
                MessageBox.Show("Pedido Seleccionado");

                //Vamos a revisar si la operacion se quedo pendiente de realizar alguna cosa
                codpedido = datagridcabecera.Rows[datagridcabecera.CurrentRow.Index].Cells[0].Value.ToString();
                estado = gestionpedidos.BL_ConsultarEstadoGestion(codpedido);

                //si estado es =0 no hemos realizado ninguna de las gestiones
                if (estado == 0)
                {
                    btnalbaran.Enabled = true;
                    btnfactura.Enabled = false;
                    btnevaluar.Enabled = false;
                    btnenviarpedido.Enabled = false;
                }
                //si estado es =1 ya tenemos el albaran creado
                else if (estado == 1)
                {
                    btnalbaran.Enabled = false;
                    btnfactura.Enabled = true;
                    btnevaluar.Enabled = false;
                    btnenviarpedido.Enabled = false;
                }
                //si estado es =2 tanto el albaran como la factura estan generados
                else if (estado == 2)
                {
                    btnalbaran.Enabled = false;
                    btnfactura.Enabled = false;
                    btnevaluar.Enabled = true;
                    btnenviarpedido.Enabled = false;
                }
                //si estado es =3 solo nos queda enviar el pedido
                else if (estado == 3)
                {
                    btnalbaran.Enabled = false;
                    btnfactura.Enabled = false;
                    btnevaluar.Enabled = false;
                    btnenviarpedido.Enabled = true;
                }

            }

            else
            {
                MessageBox.Show("Debe evaluar el primer pedido");
            }
        }

        private void btnalbaran_Click(object sender, EventArgs e)
        {
            //Almacenamos el codigo del pedido en la variable y se lo mandamos a la capa intermedia
            codpedido = datagridcabecera.Rows[datagridcabecera.CurrentRow.Index].Cells[0].Value.ToString();
            string mensaje = gestionpedidos.BL_GenerarAlbaran(codpedido);
            MessageBox.Show(mensaje);

            //Podemos el estado a 1
            estado = 1;
            gestionpedidos.BL_RegistrarEstadoGestion(codpedido, estado);

            btnalbaran.Enabled = false;
            btnfactura.Enabled = true;
            btnalbaran.Enabled = false;
        }

        private void btnfactura_Click(object sender, EventArgs e)
        {
            //Almacenamos el codigo del pedido en la variable y se lo mandamos a la capa intermedia
            codpedido = datagridcabecera.Rows[datagridcabecera.CurrentRow.Index].Cells[0].Value.ToString();
            string mensaje = gestionpedidos.BL_GenerarFactura(codpedido);
            MessageBox.Show(mensaje);

            estado = 2;
            gestionpedidos.BL_ActualizarEstadoGestion(codpedido, estado);

            btnfactura.Enabled = false;
            btnevaluar.Enabled = true;
        }

        private void btnevaluar_Click(object sender, EventArgs e)
        {
            codpedido = datagridcabecera.Rows[datagridcabecera.CurrentRow.Index].Cells[0].Value.ToString();
            string mensaje = gestionpedidos.BL_ComprobarPedido(codpedido);
            MessageBox.Show(mensaje);

            estado = 3;
            gestionpedidos.BL_ActualizarEstadoGestion(codpedido, estado);

            btnevaluar.Enabled = false;
            btnenviarpedido.Enabled = true;
        }

        private void btnenviarpedido_Click(object sender, EventArgs e)
        {
            codpedido = datagridcabecera.Rows[datagridcabecera.CurrentRow.Index].Cells[0].Value.ToString();
            string mensaje = gestionpedidos.BL_EnviarPedido(codpedido);
            MessageBox.Show(mensaje);

            estado = 4;
            gestionpedidos.BL_ActualizarEstadoGestion(codpedido, estado);

            btnalmacen.Enabled = true;
            btnenviarpedido.Enabled = false;
            //Limpiar el grid
            ds_gestionPedidos.Tables[0].Rows.Clear();
            datagridcabecera.DataSource = null;
            CargarGridPedidos();
        }

        private void btnalmacen_Click(object sender, EventArgs e)
        {
            Almacen almacen = new Almacen();
            almacen.Show();
            this.Close();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Principal P = new Principal();
            P.Show();
            this.Close();
        }
    }
}
