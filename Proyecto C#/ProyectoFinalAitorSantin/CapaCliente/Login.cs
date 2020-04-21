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
    public partial class Login : Form
    {
        //Creamos las variables para almacenar los datos introducidos en las cajas de texto
        public static String user, pass;
        //La variable departamento almacenara el departamento donde trabaja el usuario que quiere conectarse
        public static string departamento;
        //La variable idempleo almacenara el id del empleo del empleado
        public static int idempleo;
        //La variable id almacenara el id del empleado
        public static int id;
        BL_Login login = new BL_Login();
        SqlDataReader dr;
        Principal p = new Principal();

        private void btnentrar_Click(object sender, EventArgs e)
        {
            user = txtuser.Text;
            pass = txtpass.Text;
            login.BL_Access(user, pass.Trim());
            ComprobarDatos();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public Login()
        {
            InitializeComponent();
        }

        //comparamos los datos introducidos con los datos almacenados en el datareader
        private void ComprobarDatos()
        {
            dr = login.BL_Access(user, pass);
            if (dr.Read())
            {
                MessageBox.Show("Usuario y contraseña correctos");
                departamento = dr["Departamento"].ToString();
                idempleo = int.Parse(dr["Empleo"].ToString());
                id = int.Parse(dr["IdEmpleado"].ToString());
                p.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No se reconoce usuario o contraseña");
                txtpass.Text = "";
            }
            dr.Close();
        }
    }
}
