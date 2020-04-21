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
    public class BL_Articulos
    {
        
        DA_Articulos da_articulo = new DA_Articulos();
        DataSet dsArticulos = new DataSet();


        public DataSet BL_BuscarArticulo(string valor)
        {
            return dsArticulos = da_articulo.DA_BuscarArticulos(valor);
        }
    }
}
