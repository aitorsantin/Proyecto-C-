using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace CapaDatos
{
   public class DA_GestionPedidos
    {
        DA_Conexion cn = new DA_Conexion();
        SqlConnection connection = new SqlConnection();
        SqlCommand cmd;
        DataSet ds_GestionPedidos = new DataSet();
        SqlDataAdapter da_pedidosp;


        //Cargamos los pedidos que estan pendientes
        public DataSet DA_PedidosPendientes()
        {
            connection = cn.DA_Conectar();
            cmd = new SqlCommand();
            cmd.CommandText = "pr_ListarPedidosPendienes";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;
            da_pedidosp = new SqlDataAdapter(cmd);
            da_pedidosp.Fill(ds_GestionPedidos, "CabeceraPedido");
            return ds_GestionPedidos;
        }

        //Metodo que nos genera tanto la cabecera como las lineas del albaran
        public int DA_GenerarAlbaran(string codpedido)
        {
            int resultado;
            connection = cn.DA_Conectar();
            cmd = new SqlCommand();
            cmd.CommandText = "PR_AltaAlbaran";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@p_codPedido", codpedido);

            SqlParameter p_salida = new SqlParameter();
            p_salida.ParameterName = "@p_salida";
            p_salida.SqlDbType = SqlDbType.TinyInt;
            p_salida.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p_salida);
            connection.Open();

            cmd.ExecuteNonQuery();
            resultado = int.Parse(cmd.Parameters["@p_salida"].Value.ToString());

            return resultado;
        }

        //Insertamos en la tabla Gestion un  nuevo registro
        public void DA_RegistrarEstadoGestion(string codpedido, int gestion)
        {
            connection = cn.DA_Conectar();
            cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO proyecto.Gestion (CodPedido, EstadoGestion) VALUES (@p_codpedido, @p_estado)";
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@p_codpedido", codpedido);
            cmd.Parameters.AddWithValue("@p_estado", gestion);
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Advertencia de sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Actualizamos el registro de la tabla Gestion
        public void DA_ActualizarEstadoGestion(string codpedido, int gestion)
        {
            connection = cn.DA_Conectar();
            cmd = new SqlCommand();
            cmd.CommandText = "update proyecto.Gestion SET  EstadoGestion= @p_estado where CodPedido=@p_codpedido";
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@p_codpedido", codpedido);
            cmd.Parameters.AddWithValue("@p_estado", gestion);
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (SqlException ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Advertencia de sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Consultamos es estado de la gestion
        public int DA_ConsultarEstadoGestion(string codpedido)
        {
            int gestion = 0;
            connection = cn.DA_Conectar();
            cmd = new SqlCommand();
            SqlDataAdapter adapgestion = new SqlDataAdapter(cmd);
            cmd.CommandText = "SELECT EstadoGestion FROM proyecto.Gestion WHERE CodPedido = @p_codpedido";
            cmd.Connection = connection;
            cmd.Parameters.AddWithValue("@p_codpedido", codpedido);
            connection.Open();

            SqlDataReader drGestion = cmd.ExecuteReader();

            if (drGestion.Read())
            {
                gestion = Convert.ToInt32(drGestion[0]);


            }


            return gestion;

        }

        //Metodo para generar tanto la cabecera como las lineas de factura
        public int DA_GenerarFactura(string codpedido)
        {
            int resultado;
            connection = cn.DA_Conectar();
            cmd = new SqlCommand();
            cmd.CommandText = "pr_GenerarFactura";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@p_codPedido", codpedido);

            SqlParameter p_salida = new SqlParameter();
            p_salida.ParameterName = "@p_salida";
            p_salida.SqlDbType = SqlDbType.TinyInt;
            p_salida.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p_salida);
            connection.Open();

            cmd.ExecuteNonQuery();
            resultado = int.Parse(cmd.Parameters["@p_salida"].Value.ToString());

            return resultado;
        }

        //Metodo que revisa el pedido para comprobar que hay unidades suficientes para enviar el pedido
        public int DA_EvaluarPedido(string codpedido)
        {
            int resultado;

            connection = cn.DA_Conectar();
            cmd = new SqlCommand();
            cmd.CommandText = "PR_ActivarPedidos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@p_codpedido", codpedido);

            SqlParameter p_salida = new SqlParameter();
            p_salida.ParameterName = "@p_salida";
            p_salida.SqlDbType = SqlDbType.TinyInt;
            p_salida.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(p_salida);
            connection.Open();

            cmd.ExecuteNonQuery();
            resultado = int.Parse(cmd.Parameters["@p_salida"].Value.ToString());
            return resultado;
        }

        public string DA_EnviarPedido(string codpedido)
        {

            string mensaje;

            try
            {
                cmd = new SqlCommand();
                connection = cn.DA_Conectar();
                cmd.CommandText = "UPDATE proyecto.CabeceraPedido SET Estado = 'E' WHERE CodPedido = @p_codpedido";
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@p_codpedido", codpedido);
                connection.Open();
                cmd.ExecuteNonQuery();
                mensaje = "Pedido Enviado";
            }
            catch (SqlException ex)
            {

                mensaje = ex.ErrorCode.ToString();
            }

            connection.Close();
            return mensaje;
        }
    }
}
