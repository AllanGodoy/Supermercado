using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SupermercadoQuimistan
{
       

    public partial class LOGIN : Form
    {
       
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();
        public LOGIN()
        {
            InitializeComponent();         
         
        }
              
        private void button1_Click(object sender, EventArgs e)
        {
          
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "SELECT USUARIO, CONTRASENA FROM USUARIOS WHERE USUARIO ='" + txtUsuario.Text + "' AND CONTRASENA='" + txtContrasena.Text + "'";
            OleDbDataReader LEE = COMANDO.ExecuteReader();

            if (LEE.Read()) {
               
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña Incorrecto");
            }
            CONEXION.Cerrar();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            txtContrasena.PasswordChar = '*';
            LOGIN login = new LOGIN();

            login.StartPosition = FormStartPosition.CenterParent;
        }

        private void txtContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnIngresar.Focus();
            }
        }


    }
}
