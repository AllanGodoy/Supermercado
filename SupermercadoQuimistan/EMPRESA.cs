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
    public partial class EMPRESA : Form
    {
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();

        int EditarCambio = 0;
        public EMPRESA()
        {
            InitializeComponent();
        }
        public void INICIO()
        {
            txtCodigo.Text = ""; txtContacto.Text = ""; txtDireccion.Text = ""; txtNombre.Text = ""; txtTelefono.Text = "";
           
            txtCodigo.Enabled = false; txtContacto.Enabled = false; txtDireccion.Enabled = false; txtNombre.Enabled = false; txtTelefono.Enabled = false;
           

            btnBuscar.Enabled = false; btnCancelar.Enabled = false; btnGuardar.Enabled = false; btnNuevo.Enabled = true; btnSalir.Enabled = true;
            btnEditar.Enabled = false; btnEliminar.Enabled = false;
            btnNuevo.Focus();
        }

        private void EMPRESA_Load(object sender, EventArgs e)
        {
            INICIO();
        }
        public void ACTIVAR()
        {
            if (txtNombre.Text == "" || txtTelefono.Text == "" || txtContacto.Text == "")
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
            txtCodigo.Enabled = true;

            btnBuscar.Enabled = false; btnCancelar.Enabled = true; btnGuardar.Enabled = false; btnNuevo.Enabled = false; btnSalir.Enabled = true;
            txtCodigo.Focus();
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

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            if (txtCodigo.Text == "")
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
                if (int.Parse(txtCodigo.Text) >= 0) { }
            }
            catch
            {
                txtCodigo.Text = "";
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtCodigo.Enabled = false;

            btnNuevo.Enabled = false;
            try
            {
                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();
                COMANDO.CommandText = "SELECT * FROM EMPRESA WHERE EMPRESAID =" + txtCodigo.Text;
                OleDbDataReader LEE = COMANDO.ExecuteReader();

                while (LEE.Read())
                {
                    txtNombre.Text = LEE["NOMBRE"].ToString();
                    txtDireccion.Text = LEE["DIRECCION"].ToString();
                    txtTelefono.Text = LEE["TELEFONO"].ToString();
                    txtContacto.Text = LEE["CONTACTO"].ToString();
                    btnEliminar.Enabled = true;
                }

                CONEXION.Cerrar();
               
                if (txtNombre.Text == "")
                {
                    txtNombre.Enabled = true; txtDireccion.Enabled = true; txtTelefono.Enabled = true; txtContacto.Enabled = true;
                  
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

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }

        private void txtContacto_TextChanged(object sender, EventArgs e)
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
                    COMANDO.CommandText = "INSERT INTO EMPRESA VALUES ( '" + txtCodigo.Text + "','" + txtNombre.Text + "','" + txtDireccion.Text + "','" + txtTelefono.Text + "','" + txtContacto.Text + "')";
                   // MessageBox.Show("" + COMANDO.CommandText);
                    COMANDO.ExecuteNonQuery();
                    MessageBox.Show("Registro almacenado correctamente");
                }
                else
                {
                    COMANDO.CommandText = "UPDATE EMPRESA SET  NOMBRE='" + txtNombre.Text + "' , DIRECCION='"+ txtDireccion.Text + "' , TELEFONO='" +txtTelefono.Text+ "' ,CONTACTO='"+txtContacto.Text+ "' WHERE  EMPRESAID =" + txtCodigo.Text;
                   // MessageBox.Show("" + COMANDO.CommandText);
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

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtNombre.Focus();
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtTelefono.Focus();
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtContacto.Focus();
            }
        }

        private void txtContacto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtDireccion.Focus();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            txtNombre.Enabled = true; txtTelefono.Enabled = true; txtContacto.Enabled = true; txtDireccion.Enabled = true;
            
            btnGuardar.Enabled = true;

            EditarCambio = 1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "DELETE FROM EMPRESA WHERE EMPRESAID=" + txtCodigo.Text + "";
            // MessageBox.Show("" + COMANDO.CommandText);
            COMANDO.ExecuteNonQuery();
            MessageBox.Show("Registro Eliminado Correctamente");

            INICIO();
            CONEXION.Cerrar();
        }
    }
}
