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
    public class DA_Devoluciones
    {
        SqlConnection conexion = new SqlConnection();
        SqlCommand cmd;
        DA_Conexion cn = new DA_Conexion();
        DataSet dsPedidosDevueltos = new DataSet();

        public DataSet DA_CargarComboPedidosDevueltos()
        {
            conexion = cn.DA_Conectar();
            try
            {
                cmd = new SqlCommand("SELECT c.CodDevolucion " +
                    "FROM proyecto.CabDevoluciones as c " +
                    "JOIN proyecto.DetDevoluciones as d " +
                    "on d.CodDevolucion = c.CodDevolucion " +
                    "WHERE Revisado = 0 AND Desechable = 0 group by c.CodDevolucion");

                cmd.Connection = conexion;
                SqlDataAdapter adaptador = new SqlDataAdapter(cmd);

                adaptador.Fill(dsPedidosDevueltos, "CabDevoluciones");
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message);
            }
           
            return dsPedidosDevueltos;
        }

        public DataSet DA_CargarLineasDevoluciones(string codDevolucion)
        {
            conexion = cn.DA_Conectar();
            try
            {
                cmd = new SqlCommand("SELECT CodDevolucion, CodAlbaran, CodPedido, NLineas, CodArticulo, Cantidad " +
                    "FROM proyecto.DetDevoluciones Where CodDevolucion = @p_codDevolucion AND Desechable=0");

                cmd.Connection = conexion;
                cmd.Parameters.AddWithValue("@p_codDevolucion", codDevolucion);
                SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
                adaptador.Fill(dsPedidosDevueltos, "DetDevoluciones");
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message);
            }
            
            return dsPedidosDevueltos;
        }

        public string DA_MostrarInformacion(string codDevolucion, int nLinea)
        {
            String mensaje="";
            conexion = cn.DA_Conectar();
            try
            {
                cmd = new SqlCommand("SELECT Observaciones " +
                    "FROM proyecto.DetDevoluciones where CodDevolucion = @p_codDevolucion AND NLineas = @p_nLinea");

                cmd.Connection = conexion;
                cmd.Parameters.AddWithValue("@p_codDevolucion", codDevolucion);
                cmd.Parameters.AddWithValue("@p_nLinea", nLinea);
                conexion.Open();
                SqlDataReader drLineasDevolucion = cmd.ExecuteReader();
                if (drLineasDevolucion.Read())
                {
                    mensaje = drLineasDevolucion["Observaciones"].ToString();
                }
                drLineasDevolucion.Close();
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message);
            }
            finally
            {
                
                conexion.Close();
            }
            return mensaje;
            
        }
        public bool DA_DesecharArticulo(string codDevolucion, int nLinea)
        {
            bool resultado = false;
            conexion = cn.DA_Conectar();
            try
            {
                cmd = new SqlCommand("UPDATE proyecto.DetDevoluciones " +
                    "SET Desechable = 1 " +
                    "WHERE CodDevolucion = @p_codDevolucion and NLineas = @p_nLinea");

                cmd.Connection = conexion;
                cmd.Parameters.AddWithValue("@p_codDevolucion", codDevolucion);
                cmd.Parameters.AddWithValue("@p_nLinea", nLinea);
                conexion.Open();
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

                conexion.Close();
            }
            return resultado;
        }

        public bool DA_DevolverArticuloAlmacen(string codArticulo, int cantidad)
        {
            bool resultado = false;
            conexion = cn.DA_Conectar();
            try
            {
                cmd = new SqlCommand("PR_InsertarArticuloDevueltoAlmacen");
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection = conexion;
                cmd.Parameters.AddWithValue("@p_codArticulo", codArticulo);
                cmd.Parameters.AddWithValue("@p_cantidad", cantidad);
                conexion.Open();
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

                conexion.Close();
            }
            return resultado;
        }

        public void DA_ActualizarEstadoDevuelto(string codDevolucion, int nLinea)
        {
            conexion = cn.DA_Conectar();
            try
            {
                cmd = new SqlCommand("UPDATE proyecto.DetDevoluciones " +
                    "SET Desechable = 2 " +
                    "WHERE CodDevolucion = @p_codDevolucion and NLineas = @p_nLinea");

                cmd.Connection = conexion;
                cmd.Parameters.AddWithValue("@p_codDevolucion", codDevolucion);
                cmd.Parameters.AddWithValue("@p_nLinea", nLinea);
                conexion.Open();
                cmd.ExecuteNonQuery();


            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message);
            }
            finally
            {

                conexion.Close();
            }

        }

        public bool DA_DevolucionRevisada(string codDevolucion)
        {
            bool resultado = false;
            conexion = cn.DA_Conectar();
            try
            {
                cmd = new SqlCommand("UPDATE proyecto.CabDevoluciones " +
                    "SET Revisado = 1 " +
                    "WHERE CodDevolucion = @p_codDevolucion");

                cmd.Connection = conexion;
                cmd.Parameters.AddWithValue("@p_codDevolucion", codDevolucion);
                conexion.Open();
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

                conexion.Close();
            }

            return resultado;
        }
    }
}
