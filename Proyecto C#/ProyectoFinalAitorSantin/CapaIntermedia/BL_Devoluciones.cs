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
    public class BL_Devoluciones
    {
        DA_Devoluciones devoluciones = new DA_Devoluciones();
        DataSet dsPedidosDevueltos = new DataSet();

        public DataSet BL_CargarComboPedidosDevueltos()
        {
            dsPedidosDevueltos = devoluciones.DA_CargarComboPedidosDevueltos();
            return dsPedidosDevueltos;
        }

        public DataSet BL_CargarLineasDevoluciones(string codDevolucion)
        {
            return dsPedidosDevueltos = devoluciones.DA_CargarLineasDevoluciones(codDevolucion);
        }

        public String BL_MostrarInformacion(string codDevolucion, int nLinea)
        {
            String mensaje = devoluciones.DA_MostrarInformacion(codDevolucion, nLinea);
            return mensaje;
        }

        public bool BL_DesecharArticulo(string codDevolucion, int nLinea)
        {
            bool resultado = devoluciones.DA_DesecharArticulo(codDevolucion, nLinea);
            return resultado;
        }

        public bool BL_DevolverArticuloAlmacen(string codArticulo, int cantidad)
        {
            bool resultado = devoluciones.DA_DevolverArticuloAlmacen(codArticulo, cantidad);
            return resultado;
        }

        public void BL_ActualizarEstadoDevuelto(string codDevolucion, int nLinea)
        {
            devoluciones.DA_ActualizarEstadoDevuelto(codDevolucion, nLinea);
        }

        public bool BL_DevolucionRevisada(string codDevolucion)
        {
            bool resultado = devoluciones.DA_DevolucionRevisada(codDevolucion);
            return resultado;
        }
    }
}
