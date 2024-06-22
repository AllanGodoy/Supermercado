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

namespace SupermercadoQuimistan
{
    public partial class PRODUCTO : Form
    {
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();

        int EditarCambio = 0;

        public PRODUCTO()
        {
            InitializeComponent();
        }
        public void INICIO()
        {
            txtCodigoProducto.Text = ""; txtExistencia.Value = 0; txtNombreProducto.Text = ""; txtPrecio.Text = "";

            txtCodigoProducto.Enabled = false; txtExistencia.Enabled = false; txtNombreProducto.Enabled = false; txtPrecio.Enabled = false;
           
            btnBuscar.Enabled = false; btnCancelar.Enabled = false; btnGuardar.Enabled = false; btnNuevo.Enabled = true; btnSalir.Enabled = true;
            btnEditar.Enabled = false; btnEliminar.Enabled = false;
            btnNuevo.Focus();
        }
        private void PRODUCTO_Load(object sender, EventArgs e)
        {
            INICIO();
        }
        //----------ACTIVAR 
        public void ACTIVAR()
        {
            if (txtNombreProducto.Text == "" || txtPrecio.Text=="" )
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
            txtCodigoProducto.Enabled = true;

            btnBuscar.Enabled = false; btnCancelar.Enabled = true; btnGuardar.Enabled = false; btnNuevo.Enabled = false; btnSalir.Enabled = true;
            txtCodigoProducto.Focus();
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

        private void txtCodigoProducto_TextChanged(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            if (txtCodigoProducto.Text == "")
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
                if (int.Parse(txtCodigoProducto.Text) >= 0) { }
            }
            catch
            {
                txtCodigoProducto.Text = "";
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
           txtCodigoProducto.Enabled = false;

            btnNuevo.Enabled = false;
            try
            {

                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();
                COMANDO.CommandText = "SELECT * FROM PRODUCTO WHERE PRODUCTOID =" + txtCodigoProducto.Text;
                OleDbDataReader LEE = COMANDO.ExecuteReader();

                while (LEE.Read())
                {
                    txtNombreProducto.Text = LEE["NOMBREPRODUCTO"].ToString();
                    txtPrecio.Text = LEE["PRECIOPRODUCTO"].ToString();
                    txtExistencia.Text = LEE["EXISTENCIA"].ToString();
                    btnEliminar.Enabled = true;
                }

                CONEXION.Cerrar();
                // MessageBox.Show("" + cmbCargo.SelectedValue);


                if (txtNombreProducto.Text == "")
                {
                    txtNombreProducto.Enabled = true; txtPrecio.Enabled = true; txtExistencia.Enabled = true;
                    
                }
                else { btnEditar.Enabled = true; }

                btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }

        }

        private void txtNombreProducto_TextChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                if (double.Parse(txtPrecio.Text) >= 0) { }
            }
            catch
            {
                txtPrecio.Text = "";
            }
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
                    COMANDO.CommandText = "INSERT INTO PRODUCTO VALUES ( '" + txtCodigoProducto.Text + "','" + txtNombreProducto.Text + "','" + txtPrecio.Text + "','" + txtExistencia.Text + "')";
                   // MessageBox.Show("" + COMANDO.CommandText);
                    COMANDO.ExecuteNonQuery();
                    MessageBox.Show("Registro almacenado correctamente");
                }
                else
                {
                    COMANDO.CommandText = "UPDATE PRODUCTO SET  NOMBREPRODUCTO='" + txtNombreProducto.Text + "' , PRECIOPRODUCTO='" + txtPrecio.Text + "' , EXISTENCIA='" + txtExistencia.Text + "' WHERE  PRODUCTOID =" + txtCodigoProducto.Text;
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

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtNombreProducto.Focus();
            }
        }

        private void txtNombreProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtPrecio.Focus();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            txtPrecio.Enabled = true; txtNombreProducto.Enabled = true; txtExistencia.Enabled = true;
            
            btnGuardar.Enabled = true;

            EditarCambio = 1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "DELETE FROM PRODUCTO WHERE PRODUCTOID=" + txtCodigoProducto.Text + "";
           // MessageBox.Show("" + COMANDO.CommandText);
            COMANDO.ExecuteNonQuery();
            MessageBox.Show("Registro Eliminado Correctamente");

            INICIO();
            CONEXION.Cerrar();
        }
    }
}
