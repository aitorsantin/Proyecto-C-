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
    public partial class NuevoPedido : Form
    {
        BL_NuevoPedido nuevopedido = new BL_NuevoPedido();
       
        
        SqlDataReader dr_nuevoPedido;
        string parametro;
        string codempleado;
        int codigo = Login.id;
        string codarticulo;
        int idempleo = Login.idempleo;
        DataSet dsnuevoP = new DataSet();
        float precioventa;
        float porcentaje = 15;
        float precio;
        int num_fila = 0;
        int cont = 1;
        Boolean resultado;
        string codPedido, codCliente, cif, es, p, fecha;
        ErrorProvider error = new ErrorProvider();

        int unidades;
        decimal price;

        public static string pagado = "N";
        public static string estado = "P";

        public NuevoPedido()
        {
            InitializeComponent();
        }

        private void NuevoPedido_Load(object sender, EventArgs e)
        {
            //recogemos la fecha actual que nos la envian desde la capa intermedia
            txtfecha.Text = nuevopedido.BL_FechaActual();

            //Por defecto el estado sera pendiente igual que el estado del pago
            txtestado.Text = "Pendiente";
            txtpagado.Text = "Pendiente de pago";

            //Cargamos el codigo de pedido
            Cargar_NumeroPedido();

            //Cargamos el combo de los empleados
            CargarComboEmpleados();
        }

       
        private void CargarComboEmpleados()
        {
            //Para cargar los empleados utilizamos el objeto pedidos para que llame a la funcion BL_ComprobarCargo
            //En la capa Logica de Negocio se revisara que estatus tiene el empelado
            dsnuevoP = nuevopedido.BL_ComprobarCargo(idempleo, codigo);
            DataRow r = dsnuevoP.Tables["Empleados"].NewRow();

            r["IdEmpleado"] = 0;
            r["nombre"] = "Seleccione al Empleado";
            dsnuevoP.Tables["Empleados"].Rows.Add(r);

            cbcomercial.DataSource = dsnuevoP.Tables["Empleados"];
            cbcomercial.DisplayMember = "nombre";
            cbcomercial.ValueMember = "IdEmpleado";

            cbcomercial.SelectedValue = 0;
        }

        private void Cargar_NumeroPedido()
        {
            //almacenamos el el datareader dr_nuevoPedido el id de pedido que nos envian desde la capa intermedia

            string nuevo = nuevopedido.BL_NumeroPedido();
            //Recorremos una unica vez el datareader y cargamos el codigo de pedido
            txtnpedido.Text = nuevo;

        }

        private void CargarArticulos()
        {
            //parametro = txtcodarticulo.Text;
            ////Cargamos los datos del articulos
            //SqlDataReader dr_Articulos = nuevopedido.BL_BuscarArticulos(parametro);
            //Vaciamos los textboxes de los datos del articulo
            txtc.Text = BuscarArticulos.codarticulo;
            txtdes.Text = BuscarArticulos.descripcion;
            precio = float.Parse(BuscarArticulos.precio.ToString());
            precioventa = ((precio * porcentaje) / 100) + precio;
            txtpre.Text = precioventa.ToString();
        }

        private void cbcomercial_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarComboClientes();
            
        }

        private void CargarComboClientes()
        {
            codempleado = cbcomercial.SelectedItem.ToString();
            string codigo = cbcomercial.SelectedValue.ToString();
            cbcliente.Items.Clear();
            SqlDataReader drclientes;
            drclientes = nuevopedido.BL_CargaCliente(codigo);
            while (drclientes.Read())
            {
                cbcliente.Items.Add(drclientes["Nombre"]);
            }
            drclientes.Close();
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            txtc.Clear();
            txtdes.Clear();
            txtpre.Clear();
            txtcant.Clear();

            BuscarArticulos buscararticulo = new BuscarArticulos();
            buscararticulo.ShowDialog();
            CargarArticulos();
        }

        //Declaramos dos variables estaticas una para contar las filas que tenemos en el datagrid 
        //y la otra para mostrar la cantidad total del pedido;
        public static int cont_filas = 0;
        public static double total;
        public bool existe = false;

        private void btncolocar_Click(object sender, EventArgs e)
        {
            //Comprobamos que todos los campos tenga contenido
            if (txtdes.Text == "" || txtpre.Text == "" || txtcant.Text == "")
            {
                error.SetError(txtc, "Campo Requerido");
                error.SetError(txtdes, "Campo Requerido");
                error.SetError(txtpre, "Campo Requerido");
                error.SetError(txtcant, "Campo Requerido");
                txtc.Focus();
                txtdes.Focus();
                txtpre.Focus();
                txtcant.Focus();

            }
            else
            {
                int cantidad = 0;
                //Comprobamos que el contenido de txtcant sea numerico
                if (int.TryParse(txtcant.Text, out cantidad))
                {
                    //comprobamos que no sobrepase la cantidad maxima del stock
                    codarticulo = txtc.Text;
                    string unidades = txtcant.Text;

                    string mensaje = nuevopedido.BL_ComprobarUnidades(codarticulo, unidades);
                    //SqlDataReader rd = np.BL_CargarCantidad(codigoart);
                    //if (rd.Read())
                    if (mensaje == "-1")
                    {
                        string stock = nuevopedido.BL_StockMaximo();
                        string s = string.Format("La cantidad introducida excede la cantidad de stock que tenemos en el almacen, cantidad maxima: {0}", stock);
                        MessageBox.Show(s);
                    }
                    if (mensaje == "-2")
                    {
                        MessageBox.Show("La cantidad introducida no puede ser menor o igual a 0");
                    }
                    else
                    {
                        error.Clear();
                        if (cont_filas == 0)
                        {
                            datagridlineas.Rows.Add(txtc.Text, cont_filas + 1, txtdes.Text, txtpre.Text, txtcant.Text);
                            //el importe es la multiplicacion de la cantidad por el precio
                            //double importe = Convert.ToDouble(datagridlineas.Rows[cont_filas].Cells[3].Value) * Convert.ToDouble(datagridlineas.Rows[cont_filas].Cells[4].Value);
                            decimal importe = 0;
                            importe = Convert.ToInt32(txtcant.Text.ToString()) * Convert.ToDecimal(txtpre.Text.ToString());
                            datagridlineas.Rows[cont_filas].Cells[5].Value = importe;
                            cont++;
                            cont_filas++;
                            importe = 0;
                        }
                        else
                        {
                            // Recorremos todas las filas para comprobar si ya hemos introducido un articulo
                            datagridlineas.Refresh();

                            ComprobarDuplicados();
                            //Si el articulo no esta en el datagridview
                        }

                    }
                    //Calcular total
                    total = 0;
                    foreach (DataGridViewRow fila in datagridlineas.Rows)
                    {
                        total += Convert.ToDouble(fila.Cells[5].Value);
                    }
                    lbltotal.Text = total.ToString() + " €";
                }
                else
                {
                    MessageBox.Show("Se deve introducir un numero");
                }

            }
            txtc.Text = "";
            txtdes.Text = "";
            txtpre.Text = "";
            txtcant.Text = "";
        }

        private void btnpagado_Click(object sender, EventArgs e)
        {
            pagado = "S";
            txtpagado.Text = "Pagado";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Principal p = new Principal();
            p.Show();
            Limpiar();
            this.Hide();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (cont_filas > 0)
            {
                total = total - (Convert.ToDouble(datagridlineas.Rows[datagridlineas.CurrentRow.Index].Cells[4].Value));
                lbltotal.Text = total.ToString() + "€";

                datagridlineas.Rows.RemoveAt(datagridlineas.CurrentRow.Index);
                cont_filas--;
            }
            if (cont_filas == 0)
            {
                MessageBox.Show("No hay articulos para eliminar");
            }
        }

        private void ComprobarDuplicados()
        {
            for (int i = 0; i < datagridlineas.Rows.Count; i++)
            {
                if (i == datagridlineas.Rows.Count - 1)
                {
                    existe = false;
                }
                if (i < datagridlineas.Rows.Count)
                {
                    if (txtc.Text.ToString() == datagridlineas.Rows[i].Cells[0].Value.ToString())
                    {

                        //Si ese articulo existe en el datagridview
                        existe = true;
                        //sacaremos en que fila se encuentra el producto a agregrar
                        num_fila = datagridlineas.Rows[i].Index;

                        int gridcant, units;
                        gridcant = Convert.ToInt32(datagridlineas.Rows[num_fila].Cells[4].Value);
                        units = Convert.ToInt32(txtcant.Text);
                        datagridlineas.Rows[num_fila].Cells[4].Value = gridcant + units;

                        unidades = Convert.ToInt32(datagridlineas.Rows[num_fila].Cells[4].Value);
                        price = Convert.ToDecimal(datagridlineas.Rows[num_fila].Cells[3].Value);

                        decimal importe = price * unidades;

                        //double importe = Convert.ToDouble(datagridlineas.Rows[num_fila].Cells[3].Value) * Convert.ToDouble(datagridlineas.Rows[num_fila].Cells[4].Value);

                        datagridlineas.Rows[num_fila].Cells[5].Value = importe;
                        break;
                    }
                }
            }
            if (existe == false)
            {

                datagridlineas.Rows.Add(txtc.Text, cont_filas + 1, txtdes.Text, txtpre.Text, txtcant.Text);
                //el importe es la multiplicacion de la cantidad por el precio

                unidades = int.Parse(txtcant.Text.ToString());
                price = Convert.ToDecimal(txtpre.Text.ToString());
                decimal imp = unidades * price;

                //double imp = Convert.ToDouble(datagridlineas.Rows[cont_filas].Cells[3].Value) * Convert.ToDouble(datagridlineas.Rows[cont_filas].Cells[4].Value);
                datagridlineas.Rows[cont_filas].Cells[5].Value = imp;
                cont_filas++;
                cont++;
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            EN_Pedidos en_pedidos = new EN_Pedidos();
            //codPedido = txtnpedido.Text.ToString();
            //codempleado = cbcomercial.SelectedValue.ToString();
            cif = cbcliente.SelectedItem.ToString();

            SacarCodCliente();

            //fecha = txtfecha.Text.ToString();
            //es = estado;
            //p = pagado;

            en_pedidos.codpedido = txtnpedido.Text.ToString();
            en_pedidos.codcomercial = cbcomercial.SelectedValue.ToString();
            en_pedidos.codcliente = codCliente;
            en_pedidos.fecha = txtfecha.Text.ToString();
            en_pedidos.estado = estado;
            en_pedidos.pagado = pagado;


            resultado = nuevopedido.BL_GuardarCabeceraPedido(en_pedidos /*codPedido, codempleado, codCliente, fecha, es, p*/);
            if (resultado == true)
            {
                

                //MessageBox.Show("La cabecera del pedido se dio de alta correctamente");
                GuardarLineasPedido();
                ActualizarEstado(codCliente);
            }
            if (resultado == false)
            {
                MessageBox.Show("No se pudo guardar la cabecera del pedido");

            }
            Limpiar();
        }

        /*En este metodo mandamos el codigo del cliente para contar los pedidos realizados y*/
        /*actualizar si es necesario el estado del cliente*/

        private void ActualizarEstado(string codCliente)
        {
            nuevopedido.BL_ActualizarContador(codCliente);
            nuevopedido.BL_ActualizarEstado(codCliente);
        }

        private void SacarCodCliente()
        {
            SqlDataReader drcodCliente = nuevopedido.BL_CargarCodCliente(cif);

            if (drcodCliente.Read())
            {
                codCliente = drcodCliente["CodCliente"].ToString();
            }
        }

        private void GuardarLineasPedido()
        {

            List<EN_LineasPedido> list_lineasPedido = new List<EN_LineasPedido>();
            int contador = 1;

            foreach (DataGridViewRow row in datagridlineas.Rows)
            {
                EN_LineasPedido lineasPedido = new EN_LineasPedido();
                lineasPedido.codpedido= txtnpedido.Text;
                lineasPedido.codarticulo = row.Cells[0].Value.ToString();
                lineasPedido.cantidad = Convert.ToByte(row.Cells[4].Value.ToString());
                lineasPedido.precio = Convert.ToDecimal(row.Cells[3].Value.ToString());
                lineasPedido.nlinea = contador;

                list_lineasPedido.Add(lineasPedido);
                contador++;
            }

            resultado = nuevopedido.BL_GuardarLineasPedido(list_lineasPedido);

            if (resultado==true)
            {
                MessageBox.Show("El pedido y las lineas se han dado de alta correctamente", "Mensaje informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resultado==false)
            {
                MessageBox.Show("Ha ocurrido un error al insertar las lineas de pedido, este pedido no se va a guardar", "Error de Programa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Eliminamos las lineas y el pedido
                int msn = nuevopedido.BL_EliminarPedido(codPedido);

                if (msn == 1)
                {
                    MessageBox.Show("El pedido entero fue eliminado", "Mensaje informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            Limpiar();
        }

        private void Limpiar()
        {
            Cargar_NumeroPedido();
            cbcliente.Items.Clear();
            dsnuevoP.Tables["Empleados"].Clear();
            CargarComboEmpleados();
            CargarComboClientes();
            txtc.Clear();
            txtdes.Clear();
            txtpre.Clear();
            txtcant.Text = "0";
            datagridlineas.Rows.Clear();
            cont_filas = 0;

        }

    }
}
