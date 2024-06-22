using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SupermercadoQuimistan
{
    public partial class USUARIOS : Form
    {

        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();

        int EditarCambio = 0;
        public USUARIOS()
        {
            InitializeComponent();
           
        }

        
        public void INICIO()
        {
            txtCodigoUsuario.Text = ""; txtUsuario.Text = ""; txtContrasena.Text = ""; 
            

            txtCodigoUsuario.Enabled = false; txtUsuario.Enabled = false; txtContrasena.Enabled = false; 

            btnBuscar.Enabled = false; btnCancelar.Enabled = false; btnGuardar.Enabled = false; btnNuevo.Enabled = true; btnSalir.Enabled = true;
            btnEditar.Enabled = false; btnEliminar.Enabled = false;
            btnNuevo.Focus();
        }
        private void USUARIOS_Load(object sender, EventArgs e)
        {
            txtContrasena.PasswordChar = '*';
            INICIO();
        }
        public void ACTIVAR()
        {
            if (txtUsuario.Text == "" || txtContrasena.Text == "")
            {
                btnGuardar.Enabled = false; btnEliminar.Enabled = false;
            }
            else
            {
                btnGuardar.Enabled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtCodigoUsuario.Enabled = true;

            btnBuscar.Enabled = false; btnCancelar.Enabled = true; btnGuardar.Enabled = false; btnNuevo.Enabled = false; btnSalir.Enabled = true;
            txtCodigoUsuario.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            CONEXION.Cerrar();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            INICIO();
        }

        private void txtCodigoUsuario_TextChanged(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            if (txtCodigoUsuario.Text == "")
            {
                btnBuscar.Enabled = false;
            }
            else
            {
                btnBuscar.Enabled = true;

            }

            //----------- si no es numerico elimina el registro
            try
            {
                if (int.Parse(txtCodigoUsuario.Text) >= 0) { }
            }
            catch
            {
                txtCodigoUsuario.Text = "";
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtCodigoUsuario.Enabled = false;

            btnNuevo.Enabled = false;
            try
            {

                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();
                COMANDO.CommandText = "SELECT * FROM USUARIOS WHERE ID =" + txtCodigoUsuario.Text;
                OleDbDataReader LEE = COMANDO.ExecuteReader();

                while (LEE.Read())
                {
                    txtUsuario.Text = LEE["USUARIO"].ToString();
                    txtContrasena.Text = LEE["CONTRASENA"].ToString();
                    // MessageBox.Show("" + cmbCargo.ValueMember);
                                      
                    btnEliminar.Enabled = true;
                }

                CONEXION.Cerrar();
                // MessageBox.Show("" + cmbCargo.SelectedValue);


                if (txtUsuario.Text == "")
                {
                    txtUsuario.Enabled = true; txtContrasena.Enabled = true; 
                    btnNuevo.Enabled = false;
                }
                else { btnEditar.Enabled = true; }

                btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;

            try
            {
                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();

                if (EditarCambio == 0)
                {
                    COMANDO.CommandText = "INSERT INTO USUARIOS VALUES ( '" + txtCodigoUsuario.Text + "','" + txtCodigoUsuario.Text + "','" + txtContrasena.Text + "')";
                    //MessageBox.Show("" + COMANDO.CommandText);
                    COMANDO.ExecuteNonQuery();
                    MessageBox.Show("Registro almacenado correctamente");
                }
                else
                {
                    COMANDO.CommandText = "UPDATE USUARIOS SET  USUARIO='" + txtCodigoUsuario.Text + "' , CONTRASENA='" + txtContrasena.Text + "' WHERE  ID =" + txtCodigoUsuario.Text;
                    //MessageBox.Show("" + COMANDO.CommandText);
                    COMANDO.ExecuteNonQuery();
                    MessageBox.Show("Registro Editado correctamente");
                    EditarCambio = 0;
                }

                INICIO();
                CONEXION.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR :" + ex);
            }
        }

        private void txtCodigoUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtUsuario.Focus();
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtContrasena.Focus();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            txtCodigoUsuario.Enabled = true; txtContrasena.Enabled = true; 

            btnGuardar.Enabled = true;

            EditarCambio = 1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "DELETE FROM USUARIOS WHERE ID=" + txtCodigoUsuario.Text + "";
            // MessageBox.Show("" + COMANDO.CommandText);
            COMANDO.ExecuteNonQuery();
            MessageBox.Show("Registro Eliminado Correctamente");

            INICIO();
            CONEXION.Cerrar();
        }


    }
}
