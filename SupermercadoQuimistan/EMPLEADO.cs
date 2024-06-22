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
    public partial class EMPLEADO : Form
    {
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();

        int EditarCambio = 0;

        public EMPLEADO()
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
        public void COMBOCARGA() {
            cmbCargo.Items.Clear();
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "SELECT CARGOID, NOMBRECARGO FROM CARGO ";
            OleDbDataReader LEE = COMANDO.ExecuteReader();

            cmbCargo.Items.Clear();
            List<SELECIONITEMS> COMBOBOXITEMS = new List<SELECIONITEMS>();
            //------llenado del Combobox Cargos
          
            COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = "", Value = "-1" }); //---para que quede en blanco el primer item del combobox
            
            while (LEE.Read())
            {
                COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = LEE["NOMBRECARGO"].ToString(), Value = LEE["CARGOID"].ToString() });
            }
            cmbCargo.DataSource = COMBOBOXITEMS;
            cmbCargo.DisplayMember = "Text";
            cmbCargo.ValueMember = "Value";

            CONEXION.Cerrar();
            cmbCargo.SelectedValue = "-1";
        }

        public void INICIO() { 
            txtCodigoEmpleado.Text = ""; txtNombre.Text = "";txtSueldo.Text=""; txtDireccion.Text = ""; txtDNI.Text = "";
            dtpFechaNac.Text = DateTime.Now.ToString(); dtpFechaIngreso.Text= DateTime.Now.ToString();
            dtpFechaNac.Text = ""; dtpFechaIngreso.Text = "";
            cmbCargo.Text = "";

            txtCodigoEmpleado.Enabled = false; txtNombre.Enabled = false; txtSueldo.Enabled = false; txtDireccion.Enabled = false; txtDNI.Enabled = false;
            dtpFechaIngreso.Enabled= false; dtpFechaNac.Enabled = false;
            cmbCargo.Enabled = false; 

            btnBuscar.Enabled = false;btnCancelar.Enabled = false;btnGuardar.Enabled = false;btnNuevo.Enabled = true;btnSalir.Enabled = true; 
            btnEditar.Enabled = false;btnEliminar.Enabled= false;
            btnNuevo.Focus();
            cmbCargo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void EMPLEADO_Load(object sender, EventArgs e)
        {
            COMBOCARGA();
            INICIO();
        }

        //----------ACTIVAR 

        public void ACTIVAR() {
            if (txtNombre.Text=="" || txtSueldo.Text=="" || cmbCargo.Text == "" ) {
            btnGuardar.Enabled = false; btnEliminar.Enabled = false;
            }
            else
            {
                btnGuardar.Enabled = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            
            txtCodigoEmpleado.Enabled = true; 
           
            btnBuscar.Enabled = false; btnCancelar.Enabled = true; btnGuardar.Enabled = false; btnNuevo.Enabled = false; btnSalir.Enabled = true;
            txtCodigoEmpleado.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            CONEXION.Cerrar();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            INICIO();
            cmbCargo.SelectedValue = "-1";
        }

        private void txtCodigoEmpleado_TextChanged(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            if (txtCodigoEmpleado.Text == "")
            {
                btnBuscar.Enabled = false;
            }
            else
            {
                btnBuscar.Enabled = true;
               
            }

            //----------- si no es numerico elimina el registro
            try {
                if (int.Parse(txtCodigoEmpleado.Text) >= 0) { }
            }
            catch {
                txtCodigoEmpleado.Text = "";
            }
        }           

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtCodigoEmpleado.Enabled = false;
            
            btnNuevo.Enabled = false;
            try {
                
                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();
                COMANDO.CommandText = "SELECT * FROM EMPLEADO WHERE EMPLEADOID ="+txtCodigoEmpleado.Text;
                OleDbDataReader LEE = COMANDO.ExecuteReader();
               
                while (LEE.Read())
                {
                    txtNombre.Text = LEE["NOMBRE"].ToString();
                    txtDireccion.Text = LEE["DIRECCIONEMPLEADO"].ToString();
                    txtSueldo.Text =LEE["SUELDO"].ToString();
                    cmbCargo.SelectedValue = LEE["CARGOID"].ToString();
                   // MessageBox.Show("" + cmbCargo.ValueMember);

                    dtpFechaIngreso.Text = LEE["FECHAINGRESO"].ToString();
                    dtpFechaNac.Text= LEE["FECHANACIMIENTO"].ToString();
                    txtDNI.Text = LEE["DNI"].ToString();
                    btnEliminar.Enabled = true;
                }

                CONEXION.Cerrar();
                // MessageBox.Show("" + cmbCargo.SelectedValue);


                if (txtNombre.Text == "")
                {
                    txtNombre.Enabled = true; txtSueldo.Enabled = true; txtDireccion.Enabled = true; txtDNI.Enabled = true;
                    dtpFechaIngreso.Enabled = true; dtpFechaNac.Enabled = true;
                    cmbCargo.Enabled = true;

                    btnNuevo.Enabled = false;

                }
                else { btnEditar.Enabled = true; }

                btnGuardar.Enabled = false;
            }
            catch (Exception ex) { 
                MessageBox.Show("ERROR: "+ ex);            
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }

        private void cmbCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }
             
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;

            try {
                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();

                if (EditarCambio==0) { 
                    COMANDO.CommandText = "INSERT INTO EMPLEADO VALUES ( '" + txtCodigoEmpleado.Text+"','"+txtNombre.Text+"','"+txtDireccion.Text+"','"+txtSueldo.Text+"','"+ cmbCargo.SelectedValue+ "','"+dtpFechaIngreso.Text+"','"+dtpFechaNac.Text+ "','" + txtDNI.Text+"')";
                    //MessageBox.Show("" + COMANDO.CommandText);
                    COMANDO.ExecuteNonQuery();
                    MessageBox.Show("Registro almacenado correctamente");
                }
                else
                {
                    COMANDO.CommandText = "UPDATE EMPLEADO SET  NOMBRE='" + txtNombre.Text + "' , DIRECCIONEMPLEADO='" + txtDireccion.Text + "' , SUELDO='" + txtSueldo.Text + "', CARGOID='" + cmbCargo.SelectedValue + "',FECHAINGRESO='" + dtpFechaIngreso.Text + "',FECHANACIMIENTO='" + dtpFechaIngreso.Text + "',DNI='"+ txtDNI.Text + "' WHERE  EMPLEADOID ="+txtCodigoEmpleado.Text;
                    //MessageBox.Show("" + COMANDO.CommandText);
                    COMANDO.ExecuteNonQuery();
                    MessageBox.Show("Registro Editado correctamente");
                    EditarCambio = 0;
                }
                
                INICIO();
                CONEXION.Cerrar();
            }
            catch(Exception ex) { 
            MessageBox.Show("ERROR :"+ex);
            }
        }

        private void txtCodigoEmpleado_KeyPress(object sender, KeyPressEventArgs e)
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
                dtpFechaNac.Focus();
            }
        }

        private void dtpFechaNac_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cmbCargo.Focus();
            }
        }

        private void cmbCargo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                dtpFechaIngreso.Focus();
            }
        }

        private void dtpFechaIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtSueldo.Focus();
            }
        }
        private void txtSueldo_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            ACTIVAR();
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtDireccion.Focus();
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = false;
            txtNombre.Enabled = true; txtSueldo.Enabled = true; txtDireccion.Enabled = true; txtDNI.Enabled = true;
            dtpFechaIngreso.Enabled = true; dtpFechaNac.Enabled = true;
            cmbCargo.Enabled = true;

            btnGuardar.Enabled=true;

            EditarCambio = 1;
        }

        private void txtSueldo_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(txtSueldo.Text) >= 0) { }
            }
            catch
            {
                txtSueldo.Text = "";
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "DELETE FROM EMPLEADO WHERE EMPLEADOID=" + txtCodigoEmpleado.Text + "";
           // MessageBox.Show("" + COMANDO.CommandText);
            COMANDO.ExecuteNonQuery();
            MessageBox.Show("Registro Eliminado Correctamente");

            INICIO();
            CONEXION.Cerrar();
        }      

    }
}
