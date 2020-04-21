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
    public class BL_Pedidos
    {
        DA_Pedidos pedidos = new DA_Pedidos();

        //En este metodo implementamos la logica de negocio de la empresa
        public DataSet BL_ComprobarCargo(int idempleo, int codigo)
        {
            //si el que accede a la aplicacion es un genrente o un jefe de equipo
            if (idempleo == 1 || idempleo == 2)
            {
                //Llenamos el combobox con todos los comerciales
                DataSet dsempleados = new DataSet();
                dsempleados = pedidos.DA_ComboComercial();
                return dsempleados;
            }
            else
            {
                //En el caso de que inicie sesion un comercial lo cargaremos con los datos del comercial
                DataSet dscomercial = new DataSet();
                dscomercial = pedidos.DA_ComboUnicoComercial(codigo);
                //Una vez lleno el dataset se le enviara a la capa cliente
                return dscomercial;
            }
        }
        //Este metodo se utilizara para pasar el codigo del empleado a la capa de datos y llenar un datareader
        public SqlDataReader BL_CargarClientes(string codigo)
        {
            SqlDataReader drclientes;
            drclientes = pedidos.DA_ComboClientes(codigo);
            //Despues ese dataset se devolvera a la capa del cliente lleno con los datos del cliente
            return drclientes;
        }

        public SqlDataReader BL_CargarCodCliente(string cif)
        {
            //Para poder obtener el cif del cliente utilizamos la propiedad substring
            int fin = cif.Length;
            string resultado;
            //lo que estamos realizando es contando los nueve caracteres desde atras saca 9 elementos
            resultado = cif.Substring((fin - 9), 9);
            SqlDataReader drcodcliente = pedidos.DA_CargarCodCliente(resultado);
            return drcodcliente;
        }

        //Este metodo obtiene el codigo de cliente y se lo manda a la capa cliente para utilizarlo como parametro
        //De esta manera obtendremos de que tipo es cada cliente

        public SqlDataReader BL_CargarTipo(string codigo)
        {
            SqlDataReader dtTipo;
            dtTipo = pedidos.DA_CargarTipoCliente(codigo);
            //En la capa cliente obtendremos un datareader con el tipo de cliente.
            return dtTipo;
        }
        //Este metodo se utiliza para cargar la cabecera de pedidos
        public DataSet BL_CargarPedidos(string codcliente, string codempleado)
        {
            return pedidos.DA_CargarCabeceraPedido(codcliente, codempleado);
        }

        public DataSet BL_CargarLineasPedido(string codpedido)
        {
            return pedidos.DA_CargarLineasPedido(codpedido);
        }

        public SqlDataReader BL_ComprobarPago(string codpedido)
        {
            return pedidos.DA_ComprobarPago(codpedido);
        }

        public string BL_ActualizarEstadoPagado(string codpedido)
        {
            string resultado;
            resultado = pedidos.DA_ActualizarEstadoPagado(codpedido);
            return resultado;
        }

        public string BL_EliminarLineaPedido(string codpedido, int numfila, string codArticulo, int cantidad)
        {
            string resultado;
            resultado = pedidos.DA_EliminarLineasPedido(codpedido, numfila, codArticulo, cantidad);
            return resultado;
        }

        public string BL_EliminarCabeceraPedido(string codPedido)
        {
            string resultado;
            resultado = pedidos.DA_EliminarCabecera(codPedido);
            return resultado;
        }

        public string BL_ComprobarEstado(string valor, string estado)
        {
            if (estado != valor)
            {
                return "-1";
            }
            else
            {
                return "1";
            }
        }

        public string BL_ActualizarPedido( EN_LineasPedido en_LineasPedido)
        {
            string resultado = pedidos.DA_ActualizarPedido(en_LineasPedido);
            return resultado;
        }

        public bool BL_CancelarPedido(string codPedido)
        {
            bool resultado = pedidos.DA_CancelarPedido(codPedido);
            return resultado;
        }
    }
}

