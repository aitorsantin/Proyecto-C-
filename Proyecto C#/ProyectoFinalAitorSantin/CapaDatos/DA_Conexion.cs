using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
  public class DA_Conexion
    {
        public SqlConnection DA_Conectar()
        {
            SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-3CI1724;Initial Catalog=Dam_AitorSantin;Integrated Security=True");
            //SqlConnection cn = new SqlConnection(@"Data Source=SEGUNDO150\SEGUNDO150;Initial Catalog=Dam_AitorSantin;Integrated Security=True");
            return cn;
        }
    }
}
