using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
   public class DA_NuevoPedido
    {
        DA_Conexion cn = new DA_Conexion();
        SqlConnection conexion = new SqlConnection();
        SqlCommand cmd;
        DataSet ds_nuevoPedido = new DataSet();
        SqlTransaction transaction;

        //En esta funcion llenamos un datareader con un nuevo codigo de pedido y la enviamos a la capa intermedia
        public string DA_NumeroPedido()
        {
            string nuevo;
            cmd = new SqlCommand();
            cmd.CommandText = "pr_CodigoPedido";
            cmd.CommandType = CommandType.StoredProcedure;
            conexion = cn.DA_Conectar();
            cmd.Connection = conexion;

            SqlParameter p_salida = new SqlParameter();
            p_salida.ParameterName = "@p_salida";
            p_salida.SqlDbType = SqlDbType.Char;
            p_salida.Direction = ParameterDirection.Output;
            p_salida.Size = 9;
            cmd.Parameters.Add(p_salida);
            conexion.Open();

            cmd.ExecuteNonQuery();
            nuevo = cmd.Parameters["@p_salida"].Value.ToString();
            return nuevo;
        }

        //En esta funcion llenamos un dataset con los datos de todos los empleados y se lo enviamos a la capa intermedia
        public DataSet DA_ComboComercial()
        {
            cmd = new SqlCommand();
            //Vamos a llenar el combobox de Comerciales con todos los comerciales de la empresa
            cmd.CommandText = ("pr_P_Comercial");
            cmd.CommandType = CommandType.StoredProcedure;
            conexion = cn.DA_Conectar();
            cmd.Connection = conexion;
            //Creamos un adaptador de solo lectura y le pasamos el comand
            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            //Llenamos el Dataset con el metodo fill del adaptador.
            adaptador.Fill(ds_nuevoPedido, "Empleados");
            return ds_nuevoPedido;

        }

        //En esta funcion llenamos un dataset con los datos de un empleado y se lo enviamos a la capa intermedia
        public DataSet DA_ComboUnicoComercial(int codigo)
        {
            cmd = new SqlCommand();
            //Para ooder identificar al comercial le pasaremos el iddelcomercial obtenido en form1
            int id = codigo;
            cmd.CommandText = ("pr_P_empleado");
            cmd.CommandType = CommandType.StoredProcedure;
            conexion = cn.DA_Conectar();
            cmd.Connection = conexion;
            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            adaptador.SelectCommand.Parameters.AddWithValue("@p_codigo", id);
            adaptador.Fill(ds_nuevoPedido, "Empleados");
            return ds_nuevoPedido;
        }
        //Esta funcion se utiliza para cargar los clientes asignados al empleado
        public SqlDataReader DA_ComboClientes(String codigo)
        {
            //Vamos a llenar un dataset con el codigo del Cliente y el Nombre y CIF
            //Le pasaremos el codigo del empleado
            cmd = new SqlCommand();
            cmd.CommandText = ("pr_P_Clientes");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_codEmpleado", codigo);
            conexion = cn.DA_Conectar();
            cmd.Connection = conexion;
            conexion.Open();
            SqlDataReader drclientes = cmd.ExecuteReader();
            return drclientes;
        }
        //Esta funcion enviara a la capa intermedia un datareader con el numero de unidades del articulo
        public SqlDataReader DA_ComprobarUnidades(string codarticulo)
        {
            cmd = new SqlCommand();
            conexion = cn.DA_Conectar();
            cmd.CommandText = ("select Stock from proyecto.Articulos WHERE CodArticulo = @codigo");
            cmd.Parameters.AddWithValue("@codigo", codarticulo);
            cmd.Connection = conexion;
            conexion.Open();
            SqlDataReader dr_Unidades = cmd.ExecuteReader();
            return dr_Unidades;
        }

        public SqlDataReader DA_CargarCodCliente(string cif)
        {
            cmd = new SqlCommand();
            cmd.CommandText = ("PR_P_CodCliente");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_cif", cif);
            conexion = cn.DA_Conectar();
            cmd.Connection = conexion;
            conexion.Open();
            SqlDataReader drcodcliente = cmd.ExecuteReader();
            return drcodcliente;
        }

        //Funcion que devuelve un resultado booleano si se ha realizado correctamente o no la insert de la cabecera de pedido
        public Boolean DA_GuardarCabecera_Pedido( EN_Pedidos en_Pedidos /*string codpedido, string codcomercial, string codcliente, string fecha, string estado, string pagado*/)
        {
            EN_Pedidos pedidos = new EN_Pedidos();
            pedidos.codcliente = en_Pedidos.codcliente;
            pedidos.codcomercial = en_Pedidos.codcomercial;
            pedidos.codpedido = en_Pedidos.codpedido;
            pedidos.estado = en_Pedidos.estado;
            pedidos.fecha = en_Pedidos.fecha;
            pedidos.pagado = en_Pedidos.pagado;

            Boolean resultado;
            try
            {
                cmd = new SqlCommand();
                conexion = cn.DA_Conectar();
                cmd.CommandText = ("INSERT INTO proyecto.CabeceraPedido (CodPedido, CodEmpleado, CodCliente, Fecha, Estado, Pagado)" +
                    "VALUES (@cpc, @ccc, @cclic, @fc, @ec, @pc )");

                //cmd.Parameters.AddWithValue("@cpc", codpedido);
                //cmd.Parameters.AddWithValue("@ccc", int.Parse(codcomercial));
                //cmd.Parameters.AddWithValue("@cclic", codcliente);
                //cmd.Parameters.AddWithValue("@fc", DateTime.Parse(fecha.ToString()));
                //cmd.Parameters.AddWithValue("@ec", estado);
                //cmd.Parameters.AddWithValue("@pc", pagado);

                cmd.Parameters.AddWithValue("@cpc", pedidos.codpedido);
                cmd.Parameters.AddWithValue("@ccc", int.Parse(pedidos.codcomercial));
                cmd.Parameters.AddWithValue("@cclic", pedidos.codcliente);
                cmd.Parameters.AddWithValue("@fc", DateTime.Parse(pedidos.fecha.ToString()));
                cmd.Parameters.AddWithValue("@ec", pedidos.estado);
                cmd.Parameters.AddWithValue("@pc", pedidos.pagado);

                cmd.Connection = conexion;

                conexion.Open();
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (SqlException error)
            {
                string mensaje = error.Number.ToString();
                resultado = false;
            }
            finally
            {
                conexion.Close();
            }
            return resultado;

        }
        //Funcion que devuelve un resultado booleano si se ha realizado correctamente o no la insert de las lineas de pedido
        public Boolean DA_GuardarLineasPedido(List<EN_LineasPedido> list_lineasPedido )
        {
            
            Boolean resultado;

            try
            {
                foreach (var item in list_lineasPedido)
                {
                    cmd = new SqlCommand();
                    conexion = cn.DA_Conectar();
                    cmd.CommandText = "INSERT INTO proyecto.DetallePedido (CodPedido, N_Lineas, CodArticulo, Cantidad, PrecioUnitario) " +
                                "VALUES (@cp, @num, @ca, @cant, @pu)";

                    cmd.Connection = conexion;

                    cmd.Parameters.AddWithValue("@cp", item.codpedido);
                    cmd.Parameters.AddWithValue("@num", item.nlinea);
                    cmd.Parameters.AddWithValue("@ca", item.codarticulo);
                    cmd.Parameters.AddWithValue("@cant", item.cantidad);
                    cmd.Parameters.AddWithValue("@pu", item.precio);

                    conexion.Open();
                    transaction = conexion.BeginTransaction();
                    cmd.Transaction = transaction;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    conexion.Close();
                }

                resultado = true;
            }
            catch (SqlException error)
            {

                transaction.Rollback();
                string mensaje = error.Number.ToString();
                resultado = false;
            }
            return resultado;
            
        }

        /*Metodo que ejecuta un procedimiento almnacenado para contar cuantos pedidos tiene el cliente y */
        /*guardarlo en la tabla ContadorPedidos*/

        public void DA_ActualizarContador(string codcliente)
        {
            cmd = new SqlCommand();
            conexion = cn.DA_Conectar();
            cmd.CommandText = "pr_ContarPedidos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conexion;
            cmd.Parameters.AddWithValue("@p_codcliente", codcliente);
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();

        }

        /*metodo que ejecuta un procedimiento para contar los pedidos de la tabla ContadorPedidos y*/
        /* actuar si es necesario el estado del cliente*/

        public void DA_ActualizarEstado(string codcliente)
        {
            cmd = new SqlCommand();
            conexion = cn.DA_Conectar();
            cmd.CommandText = "pr_ActualizarEstadoCliente";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conexion;
            cmd.Parameters.AddWithValue("@p_codcliente", codcliente);
            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        //Funcion que elimina todo el pedido en caso de que a la hora de darlo de alta falle
        public int DA_EliminarPedido(string codpedido)
        {
            int resultado;
            cmd = new SqlCommand();
            conexion = cn.DA_Conectar();
            cmd.CommandText = "PR_EliminarPedido";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conexion;
            cmd.Parameters.AddWithValue("@p_codpedido", codpedido);

            SqlParameter p_salida = new SqlParameter();
            p_salida.ParameterName = "@p_salida";
            p_salida.SqlDbType = SqlDbType.SmallInt;
            p_salida.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p_salida);

            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();

            resultado = int.Parse(cmd.Parameters["@p_salida"].Value.ToString());
            return resultado;
        }











    }
}
