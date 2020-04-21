using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaDatos;
using CapaEntidad;

namespace CapaIntermedia
{
    public class BL_Almacen
    {
        DA_Almacen almacen = new DA_Almacen();

        public DataSet BL_CargarGridMovimientosAlmacen()
        {
            DataSet dsMovimientosAlmacen = new DataSet();
            dsMovimientosAlmacen = almacen.DA_CargarGridMovimientosAlmacen();
            return dsMovimientosAlmacen;
        }

        public List<EN_MoviAlmacen> BL_LlenarObjetoAlmacen()
        {
            List<EN_MoviAlmacen> listmovialmacen = new List<EN_MoviAlmacen>();
            listmovialmacen = almacen.LlenarObjetoAlmacen();
            return listmovialmacen;
        }

        public EN_Articulo BL_MostrarArticulo(EN_Articulo articulo)
        {
            EN_Articulo en_articulo = new EN_Articulo();
            en_articulo = almacen.DA_MostrarArticulo(articulo);
            return en_articulo;
        }

        public DataSet BL_CargarGridArticulos()
        {
            DataSet dsArticulos = new DataSet();
            dsArticulos = almacen.DA_CargarGridArticulos();
            return dsArticulos;
        }

        public int BL_EvaluarCantidadaSolicitar(int sumCantidad, int maximo)
        {
            int resultado = 0;
            //Si el stock maximo es igual a 0 todavia no se ha calculado el stock medio minimo y maximo
            if (maximo == 0)
            {
                resultado = 1;
                return resultado;
            }
            //Si la cantidad solicitada mas el stock exceden el stock maximo no se puede realizar la solicitud
            if (sumCantidad > maximo)
            {
                resultado = 2;
                return resultado;
            }
            //Podemos solicitar Articulos
            else
            {
                resultado = 3;
                return resultado;
            }
            
        }

        public bool BL_ComprarArticulos(String codArticulo, int cantidad)
        {
            bool resultado = almacen.DA_ComprarArticulos(codArticulo, cantidad);
            return resultado;
        }

    }
}
