using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace CapaDatos
{
  public  class DA_Articulos
    {
        DA_Conexion conexion = new DA_Conexion();
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adaptador;
        DataSet dsArticulos = new DataSet();



        public DataSet DA_BuscarArticulos(string valor)
        {
            cmd.CommandText = ("P_BuscarArticulos");
            cmd.CommandType = CommandType.StoredProcedure;
            cn = conexion.DA_Conectar();
            cmd.Parameters.AddWithValue("@p_parametro", valor);
            cmd.Connection = cn;
            adaptador = new SqlDataAdapter(cmd);
            adaptador.Fill(dsArticulos, "Articulos");
            return dsArticulos;
        }
    }
}
