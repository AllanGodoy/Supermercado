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
    public partial class CLIENTE : Form
    {
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();

        int EditarCambio = 0;
        public CLIENTE()
        {
            InitializeComponent();
        }
        public void INICIO()
        {
            txtCodigoCliente.Text = ""; txtDireccion.Text = ""; txtNombre.Text = ""; txtDNI.Text = ""; txtTelefono.Text = "";

            txtCodigoCliente.Enabled = false; txtDireccion.Enabled = false; txtNombre.Enabled = false; txtDNI.Enabled = false; txtTelefono.Enabled = false;
           
            btnBuscar.Enabled = false; btnCancelar.Enabled = false; btnGuardar.Enabled = false; btnNuevo.Enabled = true; btnSalir.Enabled = true; 
            btnEditar.Enabled = false; btnEliminar.Enabled = false;
            btnNuevo.Focus();

        }

        private void CLIENTE_Load(object sender, EventArgs e)
        {
            INICIO();
        }

        public void ACTIVAR()
        {
            if (txtNombre.Text == "" || txtDNI.Text == "" || txtTelefono.Text == "")
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
           
            txtCodigoCliente.Enabled = true;

            btnBuscar.Enabled = false; btnCancelar.Enabled = true; btnGuardar.Enabled = false; btnNuevo.Enabled = false; btnSalir.Enabled = true;
            txtCodigoCliente.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            CONEXION.Cerrar();
            this.Close();
        }

        private void txtCodigoCliente_TextChanged(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            if (txtCodigoCliente.Text == "")
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
                if (int.Parse(txtCodigoCliente.Text) >= 0) { }
            }
            catch
            {
                txtCodigoCliente.Text = "";
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtCodigoCliente.Enabled = false;

            btnNuevo.Enabled = false;
            try
            {

                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();
                COMANDO.CommandText = "SELECT * FROM CLIENTE WHERE CLIENTEID =" + txtCodigoCliente.Text;
                OleDbDataReader LEE = COMANDO.ExecuteReader();

                while (LEE.Read())
                {
                    txtNombre.Text = LEE["NOMBRECLIENTE"].ToString();
                    txtDNI.Text = LEE["DNI"].ToString();
                    txtDireccion.Text = LEE["DIRECCION"].ToString();
                    txtTelefono.Text = LEE["TELEFONO"].ToString();

                    // MessageBox.Show("" + cmbCargo.ValueMember);
                    btnEliminar.Enabled = true;
                }

                CONEXION.Cerrar();
                // MessageBox.Show("" + cmbCargo.SelectedValue);


                if (txtNombre.Text == "")
                {
                    txtNombre.Enabled = true; txtDNI.Enabled = true; txtDireccion.Enabled = true; txtTelefono.Enabled = true;
                    
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

        private void txtRTN_TextChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
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
                    COMANDO.CommandText = "INSERT INTO CLIENTE VALUES ( '" + txtCodigoCliente.Text + "','" + txtNombre.Text + "','" + txtDNI.Text + "','" + txtDireccion.Text + "','" + txtTelefono.Text + "')";
                    //MessageBox.Show("" + COMANDO.CommandText);

                    COMANDO.ExecuteNonQuery();
                    MessageBox.Show("Registro almacenado correctamente");
                }
                else
                {
                    COMANDO.CommandText = "UPDATE CLIENTE SET  NOMBRECLIENTE='" + txtNombre.Text + "' , DNI='" + txtDNI.Text + "' , DIRECCION='" + txtDireccion.Text + "', TELEFONO='" + txtTelefono.Text+ "' WHERE  CLIENTEID =" + txtCodigoCliente.Text;
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

        private void txtCodigoCliente_KeyPress(object sender, KeyPressEventArgs e)
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
                txtDNI.Focus();
            }
        }

        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
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
                txtDireccion.Focus();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            INICIO();

        }

        private void btnEditr_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            txtNombre.Enabled = true; txtDNI.Enabled = true; txtTelefono.Enabled = true; txtDireccion.Enabled = true;
           
            btnGuardar.Enabled = true;

            EditarCambio = 1;
        }
              

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "DELETE FROM CLIENTE WHERE CLIENTEID=" + txtCodigoCliente.Text + "";
            MessageBox.Show("" + COMANDO.CommandText);
            COMANDO.ExecuteNonQuery();
            MessageBox.Show("Registro Eliminado Correctamente");

            INICIO();
            CONEXION.Cerrar();
        }
    }
}
