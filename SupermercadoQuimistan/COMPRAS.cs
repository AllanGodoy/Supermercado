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
    public partial class COMPRAS : Form
    {
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();
        int EditarCambio = 0;
        public COMPRAS()
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
        public void COMBOSUCURSAL()
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
        public void COMBOPROVEEDOR()
        {
            cmbProveedor.Items.Clear();
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "SELECT PROVEEDORID, NOMBRE FROM PROVEEDOR ";
            OleDbDataReader LEE = COMANDO.ExecuteReader();

            cmbProveedor.Items.Clear();
            List<SELECIONITEMS> COMBOBOXITEMS = new List<SELECIONITEMS>();
            //------llenado del Combobox Cargos

            COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = "", Value = "-1" }); //---para que quede en blanco el primer item del combobox

            while (LEE.Read())
            {
                COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = LEE["NOMBRE"].ToString(), Value = LEE["PROVEEDORID"].ToString() });
            }
            cmbProveedor.DataSource = COMBOBOXITEMS;
            cmbProveedor.DisplayMember = "Text";
            cmbProveedor.ValueMember = "Value";

            CONEXION.Cerrar();
            cmbProveedor.SelectedValue = "-1";
        }
        public void COMBOPRODUCTO()
        {
            cmbProducto.Items.Clear();
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "SELECT PRODUCTOID, NOMBREPRODUCTO FROM PRODUCTO";
            OleDbDataReader LEE = COMANDO.ExecuteReader();

            cmbProducto.Items.Clear();
            List<SELECIONITEMS> COMBOBOXITEMS = new List<SELECIONITEMS>();
            //------llenado del Combobox Cargos

            COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = "", Value = "-1" }); //---para que quede en blanco el primer item del combobox

            while (LEE.Read())
            {
                COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = LEE["NOMBREPRODUCTO"].ToString(), Value = LEE["PRODUCTOID"].ToString() });
            }
            cmbProducto.DataSource = COMBOBOXITEMS;
            cmbProducto.DisplayMember = "Text";
            cmbProducto.ValueMember = "Value";

            CONEXION.Cerrar();
            cmbProducto.SelectedValue = "-1";
        }

        public void INICIO()
        {
            dataGridView1.Rows.Clear();
            txtCodigo.Text = ""; cmbSucursal.Text = ""; cmbProveedor.Text = ""; cmbProducto.Text = ""; dtpFecha.Text = "";
            txtCantidad.Text = ""; txtPrecio.Text = "";

            txtCodigo.Enabled = false; cmbProducto.Enabled = false; cmbProveedor.Enabled = false; cmbSucursal.Enabled = false;
            txtCantidad.Enabled = false; txtPrecio.Enabled = false; dtpFecha.Enabled = false; dataGridView1.Enabled=false;

            btnBuscar.Enabled = false; btnCancelar.Enabled = false; btnGuardar.Enabled = false; btnNuevo.Enabled = true; btnSalir.Enabled = true;
            btnEditar.Enabled = false; btnEliminar.Enabled = false;btnFinalizar.Enabled =  false; btnAgregar.Enabled = false;
            btnNuevo.Focus();

            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSucursal.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void COMPRAS_Load(object sender, EventArgs e)
        {
            INICIO();
            COMBOSUCURSAL();
            COMBOPRODUCTO();
            COMBOPROVEEDOR();
        }
        public void ACTIVAR()
        {
            if (cmbSucursal.Text == "" || cmbProveedor.Text == "")
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

            btnBuscar.Enabled = true; btnCancelar.Enabled = true; btnGuardar.Enabled = false; btnNuevo.Enabled = false; btnSalir.Enabled = true;
            txtCodigo.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            CONEXION.Cerrar();
            this.Close();
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
            btnBuscar.Enabled = false;
            try
            {

                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();
                COMANDO.CommandText = "SELECT * FROM COMPRA WHERE COMPRAID =" + txtCodigo.Text;
                OleDbDataReader LEE = COMANDO.ExecuteReader();

                while (LEE.Read())
                {
                    cmbProveedor.SelectedValue = LEE["PROVEEDORID"].ToString();
                    cmbSucursal.SelectedValue = LEE["EMPRESAID"].ToString();
                    dtpFecha.Text = LEE["FECHA"].ToString();

                    // MessageBox.Show("" + cmbCargo.ValueMember);
                    btnEliminar.Enabled = true;
                }

              
                
                //-------------------------------

                if (cmbSucursal.Text == "")
                {
                    cmbProveedor.Enabled = true; cmbSucursal.Enabled = true; dtpFecha.Enabled = true;
                    btnNuevo.Enabled = false;
                }
                else { btnEditar.Enabled = true; }

                btnGuardar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex);
            }
            CONEXION.Cerrar();

         
            //-----------------llena datagrid--------
            dataGridView1.Rows.Clear();
            try
            {
                OleDbCommand COMANDO2 = new OleDbCommand();
                COMANDO2.Connection = CONEXION.Abrir();
                COMANDO2.CommandText = "SELECT * FROM DETALLECOMPRA LEFT JOIN PRODUCTO ON DETALLECOMPRA.PRODUCTOID = PRODUCTO.PRODUCTOID WHERE DETALLECOMPRA.COMPRAID ='"+txtCodigo.Text+"'";
                OleDbDataReader LEE2 = COMANDO2.ExecuteReader();
              

                while (LEE2.Read())
                {
                    int N2 = dataGridView1.Rows.Add();
                    
                    dataGridView1.Rows[N2].Cells[0].Value = LEE2["ID"].ToString();
                    dataGridView1.Rows[N2].Cells[1].Value = LEE2["NOMBREPRODUCTO"].ToString();
                    dataGridView1.Rows[N2].Cells[2].Value = LEE2["CANTIDAD"].ToString();
                    dataGridView1.Rows[N2].Cells[3].Value = LEE2["PRECIO"].ToString();
                    dataGridView1.Rows[N2].Cells[4].Value = double.Parse(LEE2["CANTIDAD"].ToString()) * double.Parse(LEE2["PRECIO"].ToString());
                }

                CONEXION.Cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cmbSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }
        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e)
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
                    COMANDO.CommandText = "INSERT INTO COMPRA VALUES ( '" + txtCodigo.Text + "','" + cmbProveedor.SelectedValue + "','" + cmbSucursal.SelectedValue + "','" + dtpFecha.Text +"')";
                   
                    COMANDO.ExecuteNonQuery();
                    //MessageBox.Show("Registro almacenado correctamente");
                    
                    cmbProducto.Enabled = true;
                    txtPrecio.Enabled = true;
                    txtCantidad.Enabled = true; 
                    btnAgregar.Enabled=false;
                    btnFinalizar.Enabled= true;
                  
                }
                else
                {
                    COMANDO.CommandText = "UPDATE COMPRA SET  PROVEEDORID='" + cmbProveedor.SelectedValue + "' , EMPRESAID='" + cmbSucursal.SelectedValue +"' WHERE  COMPRAID =" + txtCodigo.Text;
                    COMANDO.ExecuteNonQuery();                    
                    EditarCambio = 0;
                    INICIO();
                }

                //INICIO();
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
                cmbSucursal.Focus();
            }
           
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (cmbProducto.Text == "" || txtCantidad.Text == "" || txtPrecio.Text == "")
            {
                btnAgregar.Enabled = false;
            }
            else {
                btnAgregar.Enabled = true;
            }


            try
            {
                if (int.Parse(txtCantidad.Text) >= 0) { }
            }
            catch
            {
                txtCantidad.Text = "";
            }
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            if (cmbProducto.Text == "" || txtCantidad.Text == "" || txtPrecio.Text == "")
            {
                btnAgregar.Enabled = false;
            }
            else
            {
                btnAgregar.Enabled = true;
            }

            try
            {
                if (double.Parse(txtPrecio.Text) >= 0) { }
            }
            catch
            {
                txtPrecio.Text = "";
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = false; dataGridView1.Enabled = true;

            cmbSucursal.Enabled = true; cmbProveedor.Enabled = true; dtpFecha.Enabled = true;
            txtPrecio.Enabled = true; txtPrecio.Enabled = true; cmbProducto.Enabled = true;
            txtCantidad.Enabled = true; btnFinalizar.Enabled = true; btnAgregar.Enabled = true;

            btnGuardar.Enabled = true;
            btnAgregar.Enabled = false;
            EditarCambio = 1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //--------Elimina registro 
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "DELETE FROM COMPRA WHERE COMPRAID=" + txtCodigo.Text ;
            COMANDO.ExecuteNonQuery();
            CONEXION.Cerrar();
  
            //--Elimina Detalle de registro
            OleDbCommand COMANDO2 = new OleDbCommand();
            COMANDO2.Connection = CONEXION.Abrir();
            COMANDO2.CommandText = "DELETE FROM DETALLECOMPRA WHERE COMPRAID='" + txtCodigo.Text +"'";
            // MessageBox.Show("" + COMANDO.CommandText);
            COMANDO2.ExecuteNonQuery();
            MessageBox.Show("Registro Eliminado Correctamente");
            INICIO();
            CONEXION.Cerrar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            INICIO();
            cmbProveedor.SelectedValue = "-1";
            cmbSucursal.SelectedValue= "-1";
            cmbProducto.SelectedValue= "-1";
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            INICIO();
            cmbProveedor.SelectedValue = "-1";
            cmbSucursal.SelectedValue = "-1";
            cmbProducto.SelectedValue = "-1";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CONEXION.Cerrar();
            dataGridView1.Rows.Clear();
            try {
                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();

                COMANDO.CommandText = "INSERT INTO DETALLECOMPRA (COMPRAID,PRODUCTOID,CANTIDAD,PRECIO) VALUES ( '" + txtCodigo.Text + "','" + cmbProducto.SelectedValue + "','" + txtCantidad.Text + "','" + txtPrecio.Text + "')";
               // MessageBox.Show("" + COMANDO.CommandText);
                COMANDO.ExecuteNonQuery();
              //  MessageBox.Show("Registro almacenado correctamente");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CONEXION.Cerrar();
            //------------llena datagrid--
            DATAGRID();
            AGREGAINVENTARIO();
            CONEXION.Cerrar();
            cmbProducto.SelectedValue = -1; txtCantidad.Text = ""; txtPrecio.Text = "";
        }

        public void DATAGRID() {

            dataGridView1.Rows.Clear();
            try
            {
                OleDbCommand COMANDO2 = new OleDbCommand();
                COMANDO2.Connection = CONEXION.Abrir();
                COMANDO2.CommandText = "SELECT * FROM DETALLECOMPRA LEFT JOIN PRODUCTO ON DETALLECOMPRA.PRODUCTOID = PRODUCTO.PRODUCTOID WHERE DETALLECOMPRA.COMPRAID ='" + txtCodigo.Text + "'";
                OleDbDataReader LEE2 = COMANDO2.ExecuteReader();


                while (LEE2.Read())
                {
                    int N2 = dataGridView1.Rows.Add();

                    dataGridView1.Rows[N2].Cells[0].Value = LEE2["ID"].ToString();
                    dataGridView1.Rows[N2].Cells[1].Value = LEE2["NOMBREPRODUCTO"].ToString();
                    dataGridView1.Rows[N2].Cells[2].Value = LEE2["CANTIDAD"].ToString();
                    dataGridView1.Rows[N2].Cells[3].Value = LEE2["PRECIO"].ToString();
                    dataGridView1.Rows[N2].Cells[4].Value = double.Parse(LEE2["CANTIDAD"].ToString()) * double.Parse(LEE2["PRECIO"].ToString());
                }

                CONEXION.Cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void AGREGAINVENTARIO() {
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "UPDATE PRODUCTO SET EXISTENCIA= EXISTENCIA +" + txtCantidad.Text+ " WHERE  PRODUCTOID =" + cmbProducto.SelectedValue;
            //MessageBox.Show("" + COMANDO.CommandText);
            COMANDO.ExecuteNonQuery();
            CONEXION.Cerrar();
        }
        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
           
            var numero = dataGridView1.CurrentRow.Cells[0].Value;

            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "DELETE FROM DETALLECOMPRA WHERE ID=" + numero + "";
            // MessageBox.Show("" + COMANDO.CommandText);
            COMANDO.ExecuteNonQuery();
            MessageBox.Show("Registro Eliminado Correctamente");

           
            CONEXION.Cerrar();
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.Text == "" || txtCantidad.Text == "" || txtPrecio.Text == "")
            {
                btnAgregar.Enabled = false;
            }
            else
            {
                btnAgregar.Enabled = true;
            }
        }
    }
}
