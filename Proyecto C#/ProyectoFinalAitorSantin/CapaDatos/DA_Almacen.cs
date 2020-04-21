using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;
using System.Windows.Forms;
using System.IO;

namespace CapaDatos
{
    public class DA_Almacen
    {
        DA_Conexion conexion = new DA_Conexion();
        
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataSet dsMovimientosAlmacen = new DataSet();
        DataSet dsArticulos = new DataSet();

        //Cargamos un dataset para poder cargar con sus datos el datagridview, este dataset se pasa a la capa de negocio
        public DataSet DA_CargarGridMovimientosAlmacen()
        {
            cmd.CommandText= "SELECT CodArticulo, FechaOperacion, NumeroEntradas, Cantidad, CantidadInicial, " +
                "Precio, EntradaSalida, Agotados, EstadoArticulo FROM proyecto.MoviAlmacen";

            cn = conexion.DA_Conectar();
            cmd.Connection = cn;
            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            adaptador.Fill(dsMovimientosAlmacen, "MoviAlmacen");
            return dsMovimientosAlmacen;
        }


        public List<EN_MoviAlmacen> LlenarObjetoAlmacen()
        {
            List<EN_MoviAlmacen> listmovialmacen = new List<EN_MoviAlmacen>();
            
            cmd.CommandText = "SELECT CodArticulo, FechaOperacion, NumeroEntradas, Cantidad, CantidadInicial, " +
               "Precio, EntradaSalida, Agotados, EstadoArticulo FROM proyecto.MoviAlmacen";

            cn = conexion.DA_Conectar();
            cmd.Connection = cn;
            cn.Open();
            SqlDataReader drMoviAlmacen = cmd.ExecuteReader();
            
            while (drMoviAlmacen.Read())
            {
                EN_MoviAlmacen moviAlmacen = new EN_MoviAlmacen();
                moviAlmacen.CodArticulo = drMoviAlmacen["CodArticulo"].ToString();
                moviAlmacen.Cantidad = int.Parse(drMoviAlmacen["Cantidad"].ToString());
                moviAlmacen.EntradaSalida = drMoviAlmacen["EntradaSalida"].ToString();

                listmovialmacen.Add(moviAlmacen);
            }

            cn.Close();
            drMoviAlmacen.Close();

            return listmovialmacen;
        }
        
        public DataSet DA_CargarGridArticulos()
        {
            try
            {
                cn = conexion.DA_Conectar();
                cmd = new SqlCommand("SELECT CodArticulo, Descripcion, PrecioCoste, Stock, Stock_Medido, Stock_Maximo, Stock_Minimo, Foto FROM proyecto.Articulos");
                cmd.Connection = cn;
                SqlDataAdapter adpArticulos = new SqlDataAdapter(cmd);
                adpArticulos.Fill(dsArticulos, "Articulos");
                
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Ha ocurrido un error: " + ex.Message);
            }
            return dsArticulos;
        }

        public EN_Articulo DA_MostrarArticulo(EN_Articulo articulo)
        {
            EN_Articulo en_articulo = new EN_Articulo();
            try
            {
                cn = conexion.DA_Conectar();
                cmd = new SqlCommand("SELECT CodArticulo, Descripcion, Foto FROM proyecto.Articulos where CodArticulo = @p_codArticulo");
                cmd.Parameters.AddWithValue("@p_codArticulo", articulo.codArticulo);
                cmd.Connection = cn;
                cn.Open();
                SqlDataReader drArticulo = cmd.ExecuteReader();
                if (drArticulo.Read())
                {
                    en_articulo.descripcion = drArticulo["Descripcion"].ToString();
                    if (drArticulo["Foto"].ToString()!="")
                    {
                        en_articulo.foto = (byte[])drArticulo["Foto"];
                    }

                   
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ha ocurrido un errror: " + ex.Message);
                
            }
            finally
            {
                cn.Close();
            }
            return en_articulo;
        }

        public Boolean DA_ComprarArticulos(string codArticulo, int cantidad)
        {
            bool resultado = false;
            cn = conexion.DA_Conectar();
            try
            {
                cmd = new SqlCommand("PR_SolicitudDeArticulos");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_codArticulo", codArticulo);
                cmd.Parameters.AddWithValue("@p_cantidad", cantidad);
                cmd.Connection = cn;
                cn.Open();
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: "+ex.Message);
                resultado = false;
            }
            finally
            {
                cn.Close();
            }
            return resultado;
           
           
            
        }

        
        

    }
}
