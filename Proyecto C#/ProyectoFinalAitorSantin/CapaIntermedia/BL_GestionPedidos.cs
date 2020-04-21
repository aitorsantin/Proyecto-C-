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
    public class BL_GestionPedidos
    {
        DataSet ds_gestionpedidos = new DataSet();
        DA_GestionPedidos gestionpedidos = new DA_GestionPedidos();

        //Mandamos a la capa cliente un dataset con las cabeceras de los pedidos pendientes
        public DataSet BL_PedidosPendientes()
        {
            ds_gestionpedidos = gestionpedidos.DA_PedidosPendientes();
            return ds_gestionpedidos;
        }

        public string BL_GenerarAlbaran(string codpedido)
        {
            string mensaje = "";
            int resultado = gestionpedidos.DA_GenerarAlbaran(codpedido);

            if (resultado == -1)
            {
                mensaje = "No existe el codigo de pedido en la base de datos";

            }
            if (resultado == 0)
            {
                mensaje = "Albaran generado";

            }
            return mensaje;
        }

        //función  que evalua el resultado recibido por la caba de datos y envia un mensaje
        public string BL_GenerarFactura(string codpedido)
        {
            string mensaje = "";
            int resultado = gestionpedidos.DA_GenerarFactura(codpedido);

            if (resultado == 1)
            {
                mensaje = "El pedido no esta pagado la factura se quedara en estado pendiente hasta recibir el pago";
            }
            if (resultado == 2)
            {
                mensaje = "Factura compleatada";
            }
            else
            {
                mensaje = "Ha ocurrido un error al intentar generar la factura";
            }
            return mensaje;
        }

        //función  que evalua el resultado recibido por la caba de datos y envia un mensaje
        public string BL_ComprobarPedido(string codpedido)
        {
            string mensaje = "";
            int resultado = gestionpedidos.DA_EvaluarPedido(codpedido);

            if (resultado == 1)
            {
                mensaje = "El pedido a pasado a estado Activo";
            }
            else if (resultado == 2)
            {
                mensaje = "No habia unidades suficientes y el pedido a tenido que modificarse antes de pasar a estar Activo";
            }
            else if (resultado == 0)
            {
                mensaje = "El pedido ha pasado a estado Activo";
            }
            return mensaje;
        }

        //función  que envia el codigo de pedido a la capa datos para actualizar su estado a enviado y devuelve un mensaje

        public string BL_EnviarPedido(string codpedido)
        {
            string mensaje = gestionpedidos.DA_EnviarPedido(codpedido);
            return mensaje;
        }

        //Insertamos en la tabla Gestion un  nuevo registro
        public void BL_RegistrarEstadoGestion(string codpedido, int gestion)
        {
            gestionpedidos.DA_RegistrarEstadoGestion(codpedido, gestion);
        }

        //Actualizamos el registro de la tabla Gestion
        public void BL_ActualizarEstadoGestion(string codpedido, int gestion)
        {
            gestionpedidos.DA_ActualizarEstadoGestion(codpedido, gestion);
        }

        //Consultamos es estado de la gestion
        public int BL_ConsultarEstadoGestion(string codpedido)
        {
            int gestion = gestionpedidos.DA_ConsultarEstadoGestion(codpedido);
            return gestion;
        }

    }
}
