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
    public partial class AREA : Form
    {
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();

        int EditarCambio = 0;

        public AREA()
        {
            InitializeComponent();
        }

        //-- clase para ir almacenando los items del combobox---
        class SELECIONITEMS
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }
        //--llenando el combobox con los items de la base de datos--
        public void COMBOCARGA()
        {
            cmbSucursal.Items.Clear();
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "SELECT EMPRESAID, NOMBRE FROM EMPRESA ";
            OleDbDataReader LEE = COMANDO.ExecuteReader();

            cmbSucursal.Items.Clear();
            List<SELECIONITEMS> COMBOBOXITEMS = new List<SELECIONITEMS>();
            //------llenado del Combobox Cargos

            COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = "", Value = "-1" }); //---para que quede en blanco el primer item del combobox

            while (LEE.Read())
            {
                COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = LEE["NOMBRE"].ToString(), Value = LEE["EMPRESAID"].ToString() });
            }
            cmbSucursal.DataSource = COMBOBOXITEMS;
            cmbSucursal.DisplayMember = "Text";
            cmbSucursal.ValueMember = "Value";

            CONEXION.Cerrar();
            cmbSucursal.SelectedValue = "-1";
        }

        public void INICIO()
        {
            txtNombre.Text = ""; txtNombre.Text = "";
            cmbSucursal.Text = "";

            txtCodigo.Enabled = false; txtNombre.Enabled = false;
            cmbSucursal.Enabled = false;

            btnBuscar.Enabled = false; btnCancelar.Enabled = false; btnGuardar.Enabled = false; btnNuevo.Enabled = true; btnSalir.Enabled = true;
            btnEditar.Enabled = false; btnEliminar.Enabled = false;
            btnNuevo.Focus();
            cmbSucursal.DropDownStyle = ComboBoxStyle.DropDownList;
           
        }

        //private void button6_Click(object sender, EventArgs e)
        //{

        //}

        private void AREA_Load(object sender, EventArgs e)
        {
            COMBOCARGA();
            INICIO();
        }

        //----------ACTIVAR 

        public void ACTIVAR()
        {
            if (txtNombre.Text == "" || cmbSucursal.Text=="")
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
            cmbSucursal.SelectedValue = "-1";
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
                COMANDO.CommandText = "SELECT * FROM AREA WHERE AREAID =" + txtCodigo.Text;
                OleDbDataReader LEE = COMANDO.ExecuteReader();

                while (LEE.Read())
                {
                   txtNombre.Text = LEE["NOMBRE"].ToString();
                   cmbSucursal.SelectedValue = LEE["EMPRESAID"].ToString();

                    // MessageBox.Show("" + cmbCargo.ValueMember);
                                       
                    btnEliminar.Enabled = true;
                }

                CONEXION.Cerrar();
                // MessageBox.Show("" + cmbCargo.SelectedValue);


                if (txtNombre.Text == "")
                {
                    txtNombre.Enabled = true; cmbSucursal.Enabled = true;
                    
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

        private void cmbSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
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
                    COMANDO.CommandText = "INSERT INTO AREA VALUES ( '" + txtCodigo.Text + "','" + txtNombre.Text + "','" + cmbSucursal.SelectedValue + "')";
                    //MessageBox.Show("" + COMANDO.CommandText);
                    COMANDO.ExecuteNonQuery();
                    MessageBox.Show("Registro almacenado correctamente");
                }
                else
                {
                    COMANDO.CommandText = "UPDATE AREA SET  NOMBRE='" + txtNombre.Text + "' , EMPRESAID='" + cmbSucursal.SelectedValue  + "' WHERE  AREAID =" + txtCodigo.Text; ;
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
                cmbSucursal.Focus();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            txtNombre.Enabled = true; cmbSucursal.Enabled = true;

            btnGuardar.Enabled = true;

            EditarCambio = 1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "DELETE FROM AREA WHERE AREAID=" + txtCodigo.Text + "";
            // MessageBox.Show("" + COMANDO.CommandText);
            COMANDO.ExecuteNonQuery();
            MessageBox.Show("Registro Eliminado Correctamente");

            INICIO();
            CONEXION.Cerrar();
        }
    }
}
