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

namespace CapaCliente
{
    public partial class Pedidos : Form
    {

        BL_Pedidos pedidos = new BL_Pedidos();
        DataSet dspedidos = new DataSet();
        SqlDataReader dtPedidos;
        int idempleo = Login.idempleo;
        //Obtenemos el id del comercial para poder llenar el dataset
        int codigo = Login.id;
        String codpedido, codempleado, codcliente, cif;
        decimal precioventa;
        decimal porcentaje = 15;
        BindingSource acoplador = new BindingSource();

        public Pedidos()
        {
            InitializeComponent();
        }

        private void Pedidos_Load(object sender, EventArgs e)
        {
            CargarComboEmpleados();
        }

        private void CargarComboEmpleados()
        {
            //Para cargar los empleados utilizamos el objeto pedidos para que llame a la funcion BL_ComprobarCargo
            //En la capa Logica de Negocio se revisara que estatus tiene el empelado
            dspedidos = pedidos.BL_ComprobarCargo(idempleo, codigo);
            DataRow r = dspedidos.Tables["Empleados"].NewRow();

            r["IdEmpleado"] = 0;
            r["nombre"] = "Seleccione al Empleado";
            dspedidos.Tables["Empleados"].Rows.Add(r);

            cbcomercial.DataSource = dspedidos.Tables["Empleados"];
            cbcomercial.DisplayMember = "nombre";
            cbcomercial.ValueMember = "IdEmpleado";

            cbcomercial.SelectedValue = 0;
        }

        private void cbcomercial_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //Una vez elegido el comercial cargaremos el combo de los clientes los cuales esta manejando
            codempleado = cbcomercial.SelectedValue.ToString();
            LlenarComboClientes(codempleado);
        }

        private void LlenarComboClientes(String cbcomercial)
        {
            //Antes de llenar el combobox lo limpiamos
            cbclientes.Items.Clear();
            //Vamos a añadir al dataset una nueva tabla que sera la de clientes para cargar sus datos en el combobox
            SqlDataReader drclientes;
            drclientes = pedidos.BL_CargarClientes(cbcomercial);
            while (drclientes.Read())
            {
                cbclientes.Items.Add(drclientes["Nombre"]);
            }
            drclientes.Close();
        }

        private void cbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cif = cbclientes.SelectedItem.ToString();
            SqlDataReader drcodcliente;
            drcodcliente = pedidos.BL_CargarCodCliente(cif);
            if (drcodcliente.Read())
            {
                codcliente = drcodcliente["CodCliente"].ToString();
            }
            drcodcliente.Close();

            //Cada vez que cambiemos de cliente limpiearemos el grid de cabecera de pedidos y lineas
            if (gridcpedidos.Rows.Count > 0)
            {
                dspedidos.Tables["CabeceraPedido"].Clear();
                gridcpedidos.DataSource = null;
            }

            if (datagridLinPedidos.Rows.Count > 0)
            {
                dspedidos.Tables["DetallePedido"].Clear();
                datagridLinPedidos.DataSource = null;

            }

            CargarTipoCliente(codcliente);
            CargarCabeceraPedido(codcliente, codempleado);
        }

        private void CargarTipoCliente(string codcliente)
        {
            txttipo.Text = "";
            dtPedidos = pedidos.BL_CargarTipo(codcliente);
            //Llenamos el datareader y lo recorreomos para que nos carge el tipo de cliente
            while (dtPedidos.Read())
            {
                txttipo.Text = dtPedidos["TipoCliente"].ToString();
            }
            dtPedidos.Close();
            //Una vez cargado los tipos de clientes cargaremos el grid de pedidos
        }
        private void CargarCabeceraPedido(string codcliente, string codempleado)
        {
            //Para poder vaciar el grid si ya hemos seleccionado anteriormente otro cliente
            //comprobamos que el gridview contenga filas

            //si contiene datos eliminaremos todas las filas del gridview
            for (int i = 0; i < gridcpedidos.Rows.Count; i++)
            {
                gridcpedidos.Rows.RemoveAt(i);
            }

            //codcliente = cbclientes.SelectedValue.ToString();
            codempleado = cbcomercial.SelectedValue.ToString();
            dspedidos = pedidos.BL_CargarPedidos(codcliente, codempleado);
            gridcpedidos.DataSource = dspedidos.Tables["CabeceraPedido"];
        }

        //Declaramos una variable estatica que comprobar que el pago esta realizado y en caso de que en el pedido indique que no
        //poder pagarlo
        public static string valor;

        private void gridcpedidos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (datagridLinPedidos.Rows.Count > 0)
            {
                dspedidos.Tables["DetallePedido"].Clear();
                datagridLinPedidos.DataSource = null;


            }
            if (e.ColumnIndex == 0)
            {

                //si seleccionamos la celda con del codigo del pedido y hacemos doble click cargaran las lineas de pedido
                //Para ello almacenado el codigo del pedido seleecionado
                codpedido = gridcpedidos.SelectedCells[0].Value.ToString();
                CargarLineasPedido(codpedido);
                string c = gridcpedidos.Rows[gridcpedidos.CurrentRow.Index].Cells[0].Value.ToString();
                Pagado(c);
            }
            else
            {
                MessageBox.Show("Seleccione un codigo de pedido");
            }
            //Vamos a mostrar en el texbox de pagado si el pedido seleccionado esta pagado o no

        }

        private void Pagado(string c)
        {
            SqlDataReader dtpagado;
            dtpagado = pedidos.BL_ComprobarPago(c);
            if (dtpagado.Read())
            {
                valor = dtpagado["Pagado"].ToString();
                if (valor == "S")
                {
                    txtpagado.Text = "Pedido Pagado";
                }
                if (valor == "N")
                {
                    txtpagado.Text = "Pendiente de pago";
                    btnpagar.Enabled = true;
                }
            }
            dtpagado.Close();
        }

        private void CargarLineasPedido(string codpedido)
        {
            for (int i = 0; i < datagridLinPedidos.Rows.Count; i++)
            {
                datagridLinPedidos.Rows.RemoveAt(i);
            }
            dspedidos = pedidos.BL_CargarLineasPedido(codpedido);
            acoplador.DataSource = dspedidos.Tables["DetallePedido"];
            datagridLinPedidos.DataSource = acoplador;
            datagridLinPedidos.ReadOnly = true;
        }
        private void OnRowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {

            MessageBox.Show(e.Status.ToString());
            if (e.Status == UpdateStatus.ErrorsOccurred)
            {
                MessageBox.Show("Alguien modifico los datos desde la ultima descarga de datos");
            }

        }

        private void btnpagar_Click(object sender, EventArgs e)
        {
            //Actualizamos el pago y volvemos a llamar al evento para que compruebe los cambios           
            string c = gridcpedidos.Rows[gridcpedidos.CurrentRow.Index].Cells[0].Value.ToString();
            ActualizarPago(c);
            //Pagado(c);
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Principal f = new Principal();
            f.Show();
            this.Close();
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            NuevoPedido f = new NuevoPedido();
            f.Show();
            this.Close();
        }

        //Este metodo actualizara el estado en la base de datos
        private void ActualizarPago(string c)
        {
            string resultado;
            resultado = pedidos.BL_ActualizarEstadoPagado(c);

            if (resultado == "Pago realizado")
            {
                txtpagado.Text = "Pedido Pagado";
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error: " + resultado.ToString());
            }

        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            codpedido= datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[0].Value.ToString();
            codempleado = cbcomercial.SelectedValue.ToString();
            bool resultado = pedidos.BL_CancelarPedido(codpedido);
            if (resultado==true)
            {
                MessageBox.Show("Se ha cancelado el pedido", "Informacion de la aplicacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarCabeceraPedido(codempleado, codcliente);

                //Cada vez que cambiemos de cliente limpiearemos el grid de cabecera de pedidos y lineas
                if (gridcpedidos.Rows.Count > 0)
                {
                    dspedidos.Tables["CabeceraPedido"].Clear();
                    gridcpedidos.DataSource = null;
                }

                if (datagridLinPedidos.Rows.Count > 0)
                {
                    dspedidos.Tables["DetallePedido"].Clear();
                    datagridLinPedidos.DataSource = null;

                }

                CargarCabeceraPedido(codcliente, codempleado);

            }
        }

        private void btnelimiar_Click(object sender, EventArgs e)
        {
            //Almacenamos el estado de un articulo en una variable
            string estado = gridcpedidos.Rows[gridcpedidos.CurrentRow.Index].Cells[2].Value.ToString();

            if (estado == "P")
            {
                string cellpedido;
                cellpedido = gridcpedidos.Rows[gridcpedidos.CurrentRow.Index].Cells[0].Value.ToString();
                

                int cont_filas = 0;
                //Cogemos el numero maximo de filas del datagridview y lo almacenamos en la variable cont_filas
                cont_filas = int.Parse(datagridLinPedidos.Rows.Count.ToString());

                //Cada vez que se pulse en el boton eliminar si todavia quedan filas
                if (cont_filas > 0)
                {
                    //Ejecutar sentencia deleted

                    //Almacenamos en las siguientes variables los campos que se utilizan para comparar los parametros
                    codpedido = datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[0].Value.ToString();
                    int n = int.Parse(datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[1].Value.ToString());
                    string coa = datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[2].Value.ToString();
                    int cant = int.Parse(datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[3].Value.ToString());
                    //llamamos a la clase BL_EliminarLineasPedido y le pasamos las variables para realizar la eliminacion de las filas
                    string resultado = pedidos.BL_EliminarLineaPedido(codpedido, n, coa, cant);

                    if (resultado == "0")
                    {
                        MessageBox.Show("Eliminacion completada");

                        //Eliminar filas del datagridview
                        datagridLinPedidos.Rows.RemoveAt(datagridLinPedidos.CurrentRow.Index);
                        cont_filas--;
                        datagridLinPedidos.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error: " + resultado);
                    }


                }
                //Si no disponemos de mas lineas de pedido
                if (cont_filas == 0)
                {
                    //Almacenamos el valor del codigo del pedido
                    //El valor del codigo de empleado
                    //y el valor del codigo del cliente
                    SqlDataReader drcodcliente;
                    drcodcliente = pedidos.BL_CargarCodCliente(cif);
                    if (drcodcliente.Read())
                    {
                        codcliente = drcodcliente["CodCliente"].ToString();
                    }
                    drcodcliente.Close();

                    codpedido = gridcpedidos.Rows[gridcpedidos.CurrentRow.Index].Cells[0].Value.ToString();
                    codempleado=Convert.ToString(codigo);


                    //Llamamos a la funcion y pasandole como parametros estas variables
                    string resultado = pedidos.BL_EliminarCabeceraPedido(codpedido);

                    if (resultado == "0")
                    {
                        MessageBox.Show("Eliminacion completada");

                        //Eliminar lineas del datagridview de cabecera pedido
                        gridcpedidos.Rows.RemoveAt(gridcpedidos.CurrentRow.Index);
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error: " + resultado);
                    }
                }
            }

            if (estado != "P")
            {
                MessageBox.Show("Este pedido no se puede eliminar tiene ya que se encuentra en estado Activo");
            }
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            //codpedido = datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[0].Value.ToString();
            //nfila = int.Parse(datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[1].Value.ToString());
            //codarticulo = datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[2].Value.ToString();
            //cant = int.Parse(datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[3].Value.ToString());
            //precio = Decimal.Parse(datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[4].Value.ToString());
            EN_LineasPedido en_LineasPedido = new EN_LineasPedido();

            en_LineasPedido.codpedido = datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[0].Value.ToString();
            en_LineasPedido.nlinea = int.Parse(datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[1].Value.ToString());
            en_LineasPedido.codarticulo = datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[2].Value.ToString();
            en_LineasPedido.cantidad = int.Parse(datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[3].Value.ToString());
            en_LineasPedido.precio = Decimal.Parse(datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[4].Value.ToString());

            string mensaje = pedidos.BL_ActualizarPedido(en_LineasPedido /*codpedido, nfila, codarticulo, cant, precio*/);

            if (mensaje == "1")
            {
                MessageBox.Show("Se han actualizado los datos");
                datagridLinPedidos.Refresh();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error: " + mensaje);
            }
            VaciarObjeto(en_LineasPedido);
        }

        private void datagridLinPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void VaciarObjeto(EN_LineasPedido en_LineasPedido)
        {
            en_LineasPedido.codpedido = "";
            en_LineasPedido.nlinea = 0;
            en_LineasPedido.codarticulo = "";
            en_LineasPedido.cantidad = 0;
            en_LineasPedido.precio = 0;
        }

        private void datagridLinPedidos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string valor = "P";
            string estado = gridcpedidos.Rows[gridcpedidos.CurrentRow.Index].Cells[2].Value.ToString();
            //Llamamos a la funcion para comprobar el estado
            string resultado = pedidos.BL_ComprobarEstado(valor, estado);
            //Si el pedido esta activo
            if (resultado == "-1")
            {
                MessageBox.Show("Este pedido no se puede modificar ya que ya se encuentra activo");
            }
            else
            {
                datagridLinPedidos.ReadOnly = false;
                datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[0].ReadOnly = true;
                datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[1].ReadOnly = true;
                datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[2].ReadOnly = true;
                datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[4].ReadOnly = true;

                if (e.ColumnIndex == 0)
                {
                    MessageBox.Show("No puede modificar el codigo de pedido");
                }
                if (e.ColumnIndex == 1)
                {
                    MessageBox.Show("No puede modificar el Numero de linea");
                }
                if (e.ColumnIndex == 2)
                {
                    //Vamos a abrir el formulario BuscarArticulos para buscar el articulo que queremos modificar
                    BuscarArticulos articulos = new BuscarArticulos();
                    articulos.ShowDialog();

                    CagarArticulo();


                }
                if (e.ColumnIndex == 4)
                {
                    MessageBox.Show("No se puede modificar el precio unitario");
                }
            }
        }

        private void CagarArticulo()
        {
            //Damos de alta dos variables y les damos el valor de las variables staticas declaradas en el formulario BuscarArticulos
            string codigo = BuscarArticulos.codarticulo;
            decimal precio = BuscarArticulos.precio;
            int cantidad = 1;
            precioventa = ((precio * porcentaje) / 100) + precio;

            //Modificamos los datos en el datagridview
            datagridLinPedidos.Refresh();
            datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[2].Value = codigo;
            datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[3].Value = cantidad;
            datagridLinPedidos.Rows[datagridLinPedidos.CurrentRow.Index].Cells[4].Value = precioventa;
        }

    }
}
