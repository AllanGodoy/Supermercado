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
    public partial class PROVEEDOR : Form
    {
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();

        int EditarCambio = 0;
        public PROVEEDOR()
        {
            InitializeComponent();
        }

        public void INICIO()
        {
            txtCodigoProveedor.Text = ""; txtNombre.Text = ""; txtDireccion.Text = ""; txtRTN.Text = ""; txtTelefono.Text = "";
            txtCodigoProveedor.Enabled = false; txtNombre.Enabled = false; txtDireccion.Enabled=false;txtRTN.Enabled=false;txtTelefono.Enabled = false;

            btnBuscar.Enabled = false; btnCancelar.Enabled = false; btnGuardar.Enabled = false; btnNuevo.Enabled = true; btnSalir.Enabled = true;
            btnEditar.Enabled = false; btnEliminar.Enabled = false;
            btnNuevo.Focus();
        }
        private void PROVEEDOR_Load(object sender, EventArgs e)
        {
            INICIO();
        }

        //----------ACTIVAR 

        public void ACTIVAR()
        {
            if (txtNombre.Text == "" || txtRTN.Text == "" || txtTelefono.Text == "")
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
            txtCodigoProveedor.Enabled = true;

            btnBuscar.Enabled = false; btnCancelar.Enabled = true; btnGuardar.Enabled = false; btnNuevo.Enabled = false; btnSalir.Enabled = true;
            txtCodigoProveedor.Focus();
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

        private void txtCodigoProveedor_TextChanged(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            if (txtCodigoProveedor.Text == "")
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
                if (int.Parse(txtCodigoProveedor.Text) >= 0) { }
            }
            catch
            {
                txtCodigoProveedor.Text = "";
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtCodigoProveedor.Enabled = false;

            btnNuevo.Enabled = false;
            try
            {

                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();
                COMANDO.CommandText = "SELECT * FROM PROVEEDOR WHERE PROVEEDORID =" + txtCodigoProveedor.Text;
                OleDbDataReader LEE = COMANDO.ExecuteReader();

                while (LEE.Read())
                {
                    txtNombre.Text = LEE["NOMBRE"].ToString();
                    txtDireccion.Text = LEE["DIRECCION"].ToString();
                    txtRTN.Text = LEE["RTN"].ToString();
                    txtTelefono.Text = LEE["TELEFONO"].ToString();
                    btnEliminar.Enabled = true;
                }

                CONEXION.Cerrar();
              

                if (txtNombre.Text == "")
                {
                    txtNombre.Enabled = true; txtDireccion.Enabled = true; txtRTN.Enabled = true; txtTelefono.Enabled = true;
                   
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
                    COMANDO.CommandText = "INSERT INTO PROVEEDOR VALUES ( '" + txtCodigoProveedor.Text + "','" + txtNombre.Text + "','" + txtDireccion.Text + "','" + txtRTN.Text + "','" + txtTelefono.Text + "')";
                    //MessageBox.Show("" + COMANDO.CommandText);
                    COMANDO.ExecuteNonQuery();
                    MessageBox.Show("Registro almacenado correctamente");
                }
                else
                {
                    COMANDO.CommandText = "UPDATE PROVEEDOR SET  NOMBRE='" + txtNombre.Text + "' , DIRECCION='" + txtDireccion.Text + "' , RTN='" + txtRTN.Text + "', TELEFONO='" + txtTelefono.Text+ "' WHERE  PROVEEDORID =" + txtCodigoProveedor.Text;
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

        private void txtCodigoProveedor_KeyPress(object sender, KeyPressEventArgs e)
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
                txtRTN.Focus();
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            txtNombre.Enabled = true;  txtDireccion.Enabled = true; txtRTN.Enabled = true; txtTelefono.Enabled = true;
           
            btnGuardar.Enabled = true;

            EditarCambio = 1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "DELETE FROM PROVEEDOR WHERE PROVEEDORID=" + txtCodigoProveedor.Text + "";
            // MessageBox.Show("" + COMANDO.CommandText);
            COMANDO.ExecuteNonQuery();
            MessageBox.Show("Registro Eliminado Correctamente");

            INICIO();
            CONEXION.Cerrar();
        }
    }
}
