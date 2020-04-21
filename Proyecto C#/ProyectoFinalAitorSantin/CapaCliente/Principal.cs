using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using CapaIntermedia;

namespace CapaCliente
{
    public partial class Principal : Form
    {
        SqlDataReader drLogin;
        BL_Login blLogin = new BL_Login();
        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            string departamento = Login.departamento;
            string user = Login.user;
            string pass = Login.pass;

            drLogin = blLogin.BL_DatosPersonales(user, pass);
            if (drLogin.Read())
            {
                lblcodigo.Text = drLogin["IdEmpleado"].ToString();
                lblnombre.Text = drLogin["Name"].ToString();
                lblapellidos.Text = drLogin["Apellidos"].ToString();
                lblpuesto.Text = drLogin["job"].ToString();
            }
            drLogin.Close();

            if (departamento == "GER")
            {
                btnalmacen.Enabled = true;
                btndevoluciones.Enabled = true;
                btnfacturas.Enabled = true;
                btnalbaranes.Enabled = true;
                btngestion.Enabled = true;
                btnlibro.Enabled = true;

                btnpedidos.Enabled = true;
                btnclientes.Enabled = true;
                btnnuevo.Enabled = true;
                btnventas.Enabled = true;

            }
            if (departamento == "COM")
            {
                btnalmacen.Enabled = false;
                btndevoluciones.Enabled = false;
                btnfacturas.Enabled = false;
                btnalbaranes.Enabled = false;
                btngestion.Enabled = false;
                btnlibro.Enabled = false;

                btnpedidos.Enabled = true;
                btnclientes.Enabled = true;
                btnnuevo.Enabled = true;
                btnventas.Enabled = true;
            }
            if (departamento == "ALM")
            {
                btnalmacen.Enabled = true;
                btndevoluciones.Enabled = true;
                btnfacturas.Enabled = true;
                btnalbaranes.Enabled = true;
                btngestion.Enabled = true;
                btnlibro.Enabled = true;

                btnpedidos.Enabled = false;
                btnclientes.Enabled = false;
                btnnuevo.Enabled = false;
                btnventas.Enabled = false;
            }
        }

        private void btnpedidos_Click(object sender, EventArgs e)
        {
            Pedidos p = new Pedidos();
            p.Show();
            this.Close();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Login f = new Login();
            f.Show();
            this.Close();
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            NuevoPedido f = new NuevoPedido();
            f.Show();
            this.Close();
        }

        private void btngestion_Click(object sender, EventArgs e)
        {
            GestionPedidos f = new GestionPedidos();
            f.Show();
            this.Close();
        }

        private void btnalmacen_Click(object sender, EventArgs e)
        {
            Almacen almacen = new Almacen();
            almacen.Show();
            this.Close();
        }

        private void btndevoluciones_Click(object sender, EventArgs e)
        {
            Devoluciones devoluciones = new Devoluciones();
            devoluciones.Show();
            this.Close();
        }
    }
}
