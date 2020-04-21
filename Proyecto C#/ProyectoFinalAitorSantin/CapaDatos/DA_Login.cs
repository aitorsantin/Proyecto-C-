using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DA_Login
    {
        DA_Conexion conexion = new DA_Conexion();
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd;
        SqlDataReader r;
        //Metodo para comprobar si se tiene acceso al programa
        public SqlDataReader DA_Acces(string nombre, string pass)
        {
            cmd = new SqlCommand();
            cn = conexion.DA_Conectar();
            cmd.CommandText = ("pr_Login");
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@p_user", nombre);
            cmd.Parameters.AddWithValue("@p_pass", pass);
            cmd.Connection = cn;
            cn.Open();

            r = cmd.ExecuteReader();
            return r;
        }
        //Funcion que carga un datareader con los datos personales
        public SqlDataReader DA_InformacionPer(string user, string pass)
        {
            cmd = new SqlCommand();
            cmd.CommandText = ("PR_L_DatosPersonales");
            cmd.CommandType = CommandType.StoredProcedure;
            cn = conexion.DA_Conectar();
            cmd.Parameters.AddWithValue("@p_user", user);
            cmd.Parameters.AddWithValue("@p_pass", pass);
            cmd.Connection = cn;
            cn.Open();
            r = cmd.ExecuteReader();
            return r;
        }
    }
}
