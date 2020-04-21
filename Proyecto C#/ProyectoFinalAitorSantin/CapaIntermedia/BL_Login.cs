using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaDatos;

namespace CapaIntermedia
{
    public class BL_Login
    {
        //Envia los datos de la capa cliente a la capa de datos para comprobar si los datos existen
        DA_Login daLogin = new DA_Login();
        SqlDataReader drLogin;
        public SqlDataReader BL_Access(string n, string p)
        {
            return daLogin.DA_Acces(n, p);


        }

        public SqlDataReader BL_DatosPersonales(string user, string pass)
        {
            drLogin = daLogin.DA_InformacionPer(user, pass);
            return drLogin;

        }


    }
}