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
    public class BL_NuevoPedido
    {
        DA_NuevoPedido nuevopedido = new DA_NuevoPedido();

        //Esta funcion enviara a la capa cliente la fecha actual
        public String BL_FechaActual()
        {
            DateTime fecha = DateTime.Now;
            string fechaactual;

            fechaactual = fecha.ToString("dd/MM/yy");
            return fechaactual;
        }

        //Esta funcion envia un datareader con el codigo del nuevo pedido
        public string BL_NumeroPedido()
        {

            string nuevo = nuevopedido.DA_NumeroPedido();
            return nuevo;
        }

        //En este metodo implementamos la logica de negocio de la empresa
        public DataSet BL_ComprobarCargo(int idempleo, int codigo)
        {
            //si el que accede a la aplicacion es un genrente o un jefe de equipo
            if (idempleo == 1 || idempleo == 2)
            {
                //Llenamos el combobox con todos los comerciales
                DataSet dsempleados = new DataSet();
                dsempleados = nuevopedido.DA_ComboComercial();
                return dsempleados;
            }
            else
            {
                //En el caso de que inicie sesion un comercial lo cargaremos con los datos del comercial
                DataSet dscomercial = new DataSet();
                dscomercial = nuevopedido.DA_ComboUnicoComercial(codigo);
                //Una vez lleno el dataset se le enviara a la capa cliente
                return dscomercial;
            }
        }

        //Este metodo se utilizara para pasar el codigo del empleado a la capa de datos y llenar un datareader
        public SqlDataReader BL_CargaCliente(string codigo)
        {
            SqlDataReader drclientes;
            drclientes = nuevopedido.DA_ComboClientes(codigo);
            //Despues ese dataset se devolvera a la capa del cliente lleno con los datos del cliente
            return drclientes;
        }
        public static string stock;

        public String BL_ComprobarUnidades(string codarticulo, string cantidad)
        {
            string mensaje = "";
            SqlDataReader dr_Unidades = nuevopedido.DA_ComprobarUnidades(codarticulo);
            if (dr_Unidades.Read())
            {
                stock = dr_Unidades["Stock"].ToString();
                if (int.Parse(cantidad) > int.Parse(stock))
                {
                    mensaje = "-1";

                    BL_StockMaximo();
                    return mensaje;

                }
                if (int.Parse(cantidad) <= 0)
                {
                    mensaje = "-2";
                    return mensaje;
                }
                else
                {
                    mensaje = "0";
                    return mensaje;
                }

            }
            dr_Unidades.Close();
            return mensaje;
        }

        public String BL_StockMaximo()
        {
            return stock;
        }

        public SqlDataReader BL_CargarCodCliente(string cif)
        {
            //Para poder obtener el cif del cliente utilizamos la propiedad substring
            int fin = cif.Length;
            string resultado;
            //lo que estamos realizando es contando los nueve caracteres desde atras saca 9 elementos
            resultado = cif.Substring((fin - 9), 9);
            SqlDataReader drcodcliente = nuevopedido.DA_CargarCodCliente(resultado);
            return drcodcliente;
        }

        public Boolean BL_GuardarCabeceraPedido(EN_Pedidos en_Pedidos /*string codpedido, string codcomercial, string codcliente, string fecha, string estado, string pagado*/)
        {
            Boolean resultado = nuevopedido.DA_GuardarCabecera_Pedido( en_Pedidos /*codpedido, codcomercial, codcliente, fecha, estado, pagado*/);
            return resultado;
        }

        public Boolean BL_GuardarLineasPedido( List<EN_LineasPedido> list_LineasPedido/*string codpedido, int nlinea, string codarticulo, int cantidad, decimal precio*/)
        {
            Boolean resultado = nuevopedido.DA_GuardarLineasPedido(list_LineasPedido );
            return resultado;
        }

        public void BL_ActualizarContador(string codcliente)
        {
            nuevopedido.DA_ActualizarContador(codcliente);
        }
        public void BL_ActualizarEstado(string codcliente)
        {
            nuevopedido.DA_ActualizarEstado(codcliente);
        }

        public int BL_EliminarPedido(string codpedido)
        {
            int resultado;
            return resultado = nuevopedido.DA_EliminarPedido(codpedido);
        }
    }
}
