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
    public partial class CARGO : Form
    {
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();

        int EditarCambio = 0;

        public CARGO()
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
            cmbArea.Items.Clear();
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "SELECT AREAID, NOMBRE FROM AREA ";
            OleDbDataReader LEE = COMANDO.ExecuteReader();

            cmbArea.Items.Clear();
            List<SELECIONITEMS> COMBOBOXITEMS = new List<SELECIONITEMS>();
            //------llenado del Combobox Cargos

            COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = "", Value = "-1" }); //---para que quede en blanco el primer item del combobox

            while (LEE.Read())
            {
                COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = LEE["NOMBRE"].ToString(), Value = LEE["AREAID"].ToString() });
            }
            cmbArea.DataSource = COMBOBOXITEMS;
            cmbArea.DisplayMember = "Text";
            cmbArea.ValueMember = "Value";

            CONEXION.Cerrar();
            cmbArea.SelectedValue = "-1";
        }
        public void INICIO()
        {
            
            txtCodigoCargo.Text = ""; txtNombreCargo.Text = ""; 
            cmbArea.Text = "";

            txtCodigoCargo.Enabled = false; txtNombreCargo.Enabled =
            cmbArea.Enabled = false;

            btnBuscar.Enabled = false; btnCancelar.Enabled = false; btnGuardar.Enabled = false; btnNuevo.Enabled = true; btnSalir.Enabled = true;
            btnEditar.Enabled = false; btnEliminar.Enabled = false;
            btnNuevo.Focus();
            cmbArea.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CARGO_Load(object sender, EventArgs e)
        {
            COMBOCARGA();
            INICIO();
        }

        //----------ACTIVAR 

        public void ACTIVAR()
        {
            if (txtNombreCargo.Text == "" || cmbArea.Text == "")
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
            txtCodigoCargo.Enabled = true;

            btnBuscar.Enabled = false; btnCancelar.Enabled = true; btnGuardar.Enabled = false; btnNuevo.Enabled = false; btnSalir.Enabled = true;
            txtCodigoCargo.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            CONEXION.Cerrar();
            this.Close();
        }

        private void txtCodigoCargo_TextChanged(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            if (txtCodigoCargo.Text == "")
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
                if (int.Parse(txtCodigoCargo.Text) >= 0) { }
            }
            catch
            {
                txtCodigoCargo.Text = "";
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtCodigoCargo.Enabled = false;

            btnNuevo.Enabled = false;
            try
            {

                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();
                COMANDO.CommandText = "SELECT * FROM CARGO WHERE CARGOID =" + txtCodigoCargo.Text;
                OleDbDataReader LEE = COMANDO.ExecuteReader();

                while (LEE.Read())
                {
                    txtNombreCargo.Text = LEE["NOMBRECARGO"].ToString();
                    //MessageBox.Show("" + cmbCargo.ValueMember);
                    cmbArea.SelectedValue = LEE["AREAID"].ToString();
                    btnEliminar.Enabled = true;
                }

                CONEXION.Cerrar();
                // MessageBox.Show("" + cmbCargo.SelectedValue);


                if (txtNombreCargo.Text == "")
                {
                    txtNombreCargo.Enabled = true;
                    cmbArea.Enabled = true;

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

        private void txtNombreCargo_TextChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }

        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
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
                    COMANDO.CommandText = "INSERT INTO CARGO VALUES ( '" + txtCodigoCargo.Text + "','" + txtNombreCargo.Text + "','" + cmbArea.SelectedValue + "')";
                    MessageBox.Show("" + COMANDO.CommandText);
                    COMANDO.ExecuteNonQuery();
                    MessageBox.Show("Registro almacenado correctamente");
                }
                else
                {
                    COMANDO.CommandText = "UPDATE CARGO SET  NOMBRECARGO='" + txtNombreCargo.Text + "' , AREAID='" + cmbArea.SelectedValue + "' WHERE  CARGOID =" + txtCodigoCargo.Text;
                    MessageBox.Show("" + COMANDO.CommandText);
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

        private void txtNombreCargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cmbArea.Focus();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            txtNombreCargo.Enabled = true;
            cmbArea.Enabled = true;

            btnGuardar.Enabled = true;

            EditarCambio = 1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "DELETE FROM CARGO WHERE CARGOID=" + txtCodigoCargo.Text + "";
            // MessageBox.Show("" + COMANDO.CommandText);
            COMANDO.ExecuteNonQuery();
            MessageBox.Show("Registro Eliminado Correctamente");

            INICIO();
            CONEXION.Cerrar();
        }
    }
}
