using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Windows.Forms;

namespace CapaDatos
{
   public class DA_Pedidos
    {
        DA_Conexion cn = new DA_Conexion();
        SqlConnection connection = new SqlConnection();
        SqlCommand cmd;
        DataSet dspedidos = new DataSet();
        SqlDataAdapter adapCabeceraPedido;
        SqlDataAdapter adapLineasPedido;


        public DataSet DA_ComboComercial()
        {
            cmd = new SqlCommand();
            //Vamos a llenar el combobox de Comerciales con todos los comerciales de la empresa
            cmd.CommandText = ("pr_P_Comercial");
            cmd.CommandType = CommandType.StoredProcedure;
            connection = cn.DA_Conectar();
            cmd.Connection = connection;
            //Creamos un adaptador de solo lectura y le pasamos el comand
            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            //Llenamos el Dataset con el metodo fill del adaptador.
            adaptador.Fill(dspedidos, "Empleados");
            return dspedidos;

        }
        //Vamos a llenar un dataset en el caso de que la conexion lo realice un comercial y no un jefe de equipo o gerente
        public DataSet DA_ComboUnicoComercial(int codigo)
        {
            cmd = new SqlCommand();
            //Para ooder identificar al comercial le pasaremos el iddelcomercial obtenido en form1
            int id = codigo;
            cmd.CommandText = ("pr_P_empleado");
            cmd.CommandType = CommandType.StoredProcedure;
            connection = cn.DA_Conectar();
            cmd.Connection = connection;
            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            adaptador.SelectCommand.Parameters.AddWithValue("@p_codigo", id);
            adaptador.Fill(dspedidos, "Empleados");
            return dspedidos;
        }

        public SqlDataReader DA_ComboClientes(string codigo)
        {

            //Vamos a llenar un dataset con el codigo del Cliente y el Nombre y CIF
            //Le pasaremos el codigo del empleado
            cmd = new SqlCommand();
            cmd.CommandText = ("pr_P_Clientes");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_codEmpleado", int.Parse(codigo.ToString()));
            connection = cn.DA_Conectar();
            cmd.Connection = connection;
            connection.Open();
            SqlDataReader drclientes = cmd.ExecuteReader();
            return drclientes;
            //SqlDataAdapter adapclientes = new SqlDataAdapter(cmd);
            //adapclientes.Fill(dspedidos, "Clientes");
            //return dspedidos;
        }

        public SqlDataReader DA_CargarCodCliente(string cif)
        {
            cmd = new SqlCommand();
            cmd.CommandText = ("PR_P_CodCliente");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_cif", cif);
            connection = cn.DA_Conectar();
            cmd.Connection = connection;
            connection.Open();
            SqlDataReader drcodcliente = cmd.ExecuteReader();
            return drcodcliente;
        }

        //Con este metodo cargaremos el tipo de cliente
        public SqlDataReader DA_CargarTipoCliente(string codigo)
        {
            //Obtenemos el tipo cliente de la capa intermedia
            cmd = new SqlCommand();
            string id = codigo;
            cmd.CommandText = ("select TipoCliente from proyecto.Clientes Where CodCliente = @codigo");
            cmd.Parameters.AddWithValue("@codigo", id);
            connection = cn.DA_Conectar();
            cmd.Connection = connection;
            connection.Open();
            //cargarmos un datareader con la consulta y lo enviamos a la capa intermedia
            SqlDataReader drTipo = cmd.ExecuteReader();
            // connection.Close();
            return drTipo;

        }
        //Creamos esta funcion para cargar la cabecera del pedido dependiendo del empleado y el cliente seleccionado
        public DataSet DA_CargarCabeceraPedido(string codcliente, String codempleado)
        {
            string codcli, codemp;
            codcli = codcliente;
            codemp = codempleado;
            cmd = new SqlCommand();
            connection = cn.DA_Conectar();
            cmd.CommandText = ("pr_P_CabeceraPedido");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_codcli", codcli);
            cmd.Parameters.AddWithValue("@p_codempleado", int.Parse(codemp.ToString()));
            cmd.Connection = connection;
            adapCabeceraPedido = new SqlDataAdapter(cmd);
            adapCabeceraPedido.Fill(dspedidos, "CabeceraPedido");
            return dspedidos;


        }
        public DataSet DA_CargarLineasPedido(string codpedido)
        {
            string idpedido = codpedido;
            cmd = new SqlCommand();
            cmd.CommandText = ("select CodPedido ,N_Lineas, CodArticulo, Cantidad, PrecioUnitario from proyecto.DetallePedido where CodPedido = @p_codpedido");
            cmd.Connection = cn.DA_Conectar();
            adapLineasPedido = new SqlDataAdapter(cmd);
            adapLineasPedido.SelectCommand.Parameters.AddWithValue("@p_codpedido", idpedido);
            adapLineasPedido.Fill(dspedidos, "DetallePedido");
            return dspedidos;
        }

        public SqlDataReader DA_ComprobarPago(string codpedido)
        {
            cmd.CommandText = ("select Pagado from proyecto.CabeceraPedido Where CodPedido = @codped");
            connection = cn.DA_Conectar();
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@codped", codpedido);
            connection.Open();
            SqlDataReader dtpagado = cmd.ExecuteReader();
            return dtpagado;
        }
        public string DA_ActualizarEstadoPagado(string codpedido)
        {
            string resultado = "";
            try
            {
                cmd.CommandText = ("UPDATE proyecto.CabeceraPedido SET Pagado = 'S' WHERE CodPedido = @p_cp");
                connection = cn.DA_Conectar();
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@p_cp", codpedido);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                return resultado = "Pago realizado";
            }
            catch (SqlException ex)
            {

                return resultado = ex.Message.ToString();


            }
        }
        public string DA_EliminarLineasPedido(string codpedido, int numfila, string codArticulo, int cantidad)
        {
            string c = codpedido;
            int n = numfila;
            string carticulo = codArticulo;
            int cant = cantidad;
            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = "delete from proyecto.DetallePedido where CodPedido = @cp AND N_Lineas=@n AND CodArticulo = @coa AND Cantidad = @cant";
                connection = cn.DA_Conectar();
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@cp", c);
                cmd.Parameters.AddWithValue("@n", n);
                cmd.Parameters.AddWithValue("@coa", carticulo);
                cmd.Parameters.AddWithValue("@cant", cant);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                return "0";
            }
            catch (SqlException error)
            {

                return error.ErrorCode.ToString();
            }



        }

        public string DA_EliminarCabecera(string codPedido)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "DELETE FROM proyecto.CabeceraPedido Where CodPedido = @codigo";
                connection = cn.DA_Conectar();
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@codigo", codPedido);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return "0";
            }
            catch (SqlException error)
            {

                return error.Number.ToString();
            }

        }

        public string DA_ActualizarPedido( EN_LineasPedido en_LineasPedido/*string codigo, int numfila, string codarticulo, int cantidad, decimal precio*/)
        {
            EN_LineasPedido lineasPedido = new EN_LineasPedido();
            lineasPedido.codpedido = en_LineasPedido.codpedido;
            lineasPedido.cantidad = en_LineasPedido.cantidad;
            lineasPedido.codarticulo = en_LineasPedido.codarticulo;
            lineasPedido.nlinea = en_LineasPedido.nlinea;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = ("UPDATE proyecto.DetallePedido SET CodArticulo = @CodArticulo, Cantidad = @cantidad, PrecioUnitario=@p WHERE CodPedido = @codpedido AND N_Lineas = @n");
                connection = cn.DA_Conectar();
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@codpedido", lineasPedido.codpedido);
                cmd.Parameters.AddWithValue("@n", lineasPedido.nlinea);
                cmd.Parameters.AddWithValue("@CodArticulo", lineasPedido.codarticulo);
                cmd.Parameters.AddWithValue("@cantidad", lineasPedido.cantidad);
                cmd.Parameters.AddWithValue("@p", lineasPedido.precio);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return "1";
            }
            catch (SqlException error)
            {

                return error.Number.ToString();
            }
        }

        public bool DA_CancelarPedido(string codPedido)
        {
            bool resultado = false;
            
            try
            {
                cmd = new SqlCommand("UPDATE proyecto.CabeceraPedido SET Estado = 'K' WHERE CodPedido = @p_codpedido");
                connection = cn.DA_Conectar();
                cmd.Parameters.AddWithValue("@p_codpedido", codPedido);
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                resultado = true;


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message);
                resultado = false;
               
            }
            finally
            {
                connection.Close();
            }
            return resultado;
        }
    }
}
