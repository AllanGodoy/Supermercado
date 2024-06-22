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
    public partial class FACTURA : Form
    {
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();
        int EditarCambio = 0;
        public FACTURA()
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
        public void COMBOEMPLEADO()
        {
            cmbCajero.Items.Clear();
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "SELECT EMPLEADOID, NOMBRE FROM EMPLEADO INNER JOIN CARGO ON EMPLEADO.CARGOID = CARGO.CARGOID WHERE NOMBRECARGO ='CAJERO'";
            OleDbDataReader LEE = COMANDO.ExecuteReader();

            cmbCajero.Items.Clear();
            List<SELECIONITEMS> COMBOBOXITEMS = new List<SELECIONITEMS>();
            //------llenado del Combobox Cargos

            COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = "", Value = "-1" }); //---para que quede en blanco el primer item del combobox

            while (LEE.Read())
            {
                COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = LEE["NOMBRE"].ToString(), Value = LEE["EMPLEADOID"].ToString() });
            }
            cmbCajero.DataSource = COMBOBOXITEMS;
            cmbCajero.DisplayMember = "Text";
            cmbCajero.ValueMember = "Value";

            CONEXION.Cerrar();
            cmbCajero.SelectedValue = "-1";
        }
        public void COMBOCLIENTE()
        {
            cmbCliente.Items.Clear();
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "SELECT CLIENTEID, NOMBRECLIENTE FROM CLIENTE ";
            OleDbDataReader LEE = COMANDO.ExecuteReader();

            cmbCliente.Items.Clear();
            List<SELECIONITEMS> COMBOBOXITEMS = new List<SELECIONITEMS>();
            //------llenado del Combobox Cargos

            COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = "", Value = "-1" }); //---para que quede en blanco el primer item del combobox

            while (LEE.Read())
            {
                COMBOBOXITEMS.Add(new SELECIONITEMS() { Text = LEE["NOMBRECLIENTE"].ToString(), Value = LEE["CLIENTEID"].ToString() });
            }
            cmbCliente.DataSource = COMBOBOXITEMS;
            cmbCliente.DisplayMember = "Text";
            cmbCliente.ValueMember = "Value";

            CONEXION.Cerrar();
            cmbCliente.SelectedValue = "-1";
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
            txtSubTotal.Enabled= false;
            txtImpuesto.Enabled= false;
            txtTotalPagar.Enabled=false;

            dataGridView1.Rows.Clear();
            txtCodigo.Text = ""; cmbCajero.Text = ""; cmbCliente.Text = ""; cmbProducto.Text = ""; dtpFecha.Text = "";
            txtCantidad.Text = ""; 

            txtCodigo.Enabled = false; cmbProducto.Enabled = false; cmbCliente.Enabled = false; cmbCajero.Enabled = false;
            txtCantidad.Enabled = false; dtpFecha.Enabled = false; dataGridView1.Enabled = false;

            btnBuscar.Enabled = false; btnCancelar.Enabled = false; btnGuardar.Enabled = false; btnNuevo.Enabled = true; btnSalir.Enabled = true;
            btnEditar.Enabled = false; btnEliminar.Enabled = false; btnFinalizar.Enabled = false; btnAgregar.Enabled = false;
            btnNuevo.Focus();
            cmbCajero.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void FACTURA_Load(object sender, EventArgs e)
        {
            INICIO();
            COMBOEMPLEADO();
            COMBOPRODUCTO();
            COMBOCLIENTE();
        }
        public void ACTIVAR()
        {
            if (cmbCajero.Text == "" || cmbCliente.Text == "")
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
                COMANDO.CommandText = "SELECT * FROM FACTURA WHERE FACTURAID =" + txtCodigo.Text;
                OleDbDataReader LEE = COMANDO.ExecuteReader();

                while (LEE.Read())
                {
                    cmbCliente.SelectedValue = LEE["CLIENTEID"].ToString();
                    cmbCajero.SelectedValue = LEE["EMPLEADOID"].ToString();
                    dtpFecha.Text = LEE["FECHA"].ToString();

                    // MessageBox.Show("" + cmbCargo.ValueMember);
                    btnEliminar.Enabled = true;
                }



                //-------------------------------

                if (cmbCajero.Text == "")
                {
                    cmbCliente.Enabled = true; cmbCajero.Enabled = true; dtpFecha.Enabled = true;
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
               COMANDO2.CommandText = "SELECT * FROM DETALLEFACTURA LEFT JOIN PRODUCTO ON DETALLEFACTURA.PRODUCTOID = PRODUCTO.PRODUCTOID WHERE DETALLEFACTURA.FACTURAID =" + txtCodigo.Text + "";

               // COMANDO2.CommandText = "SELECT * FROM DETALLEFACTURA LEFT JOIN PRODUCTO ON DETALLEFACTURA.PRODUCTOID = PRODUCTO.PRODUCTOID ";
                OleDbDataReader LEE2 = COMANDO2.ExecuteReader();

                double SubTotal =0;
                double Impuesto = 0;
                double TotalPagar = 0;
                while (LEE2.Read())
                {
                    int N2 = dataGridView1.Rows.Add();

                    dataGridView1.Rows[N2].Cells[0].Value = LEE2["DETALLEFACTURAID"].ToString();
                    dataGridView1.Rows[N2].Cells[1].Value = LEE2["NOMBREPRODUCTO"].ToString();
                    dataGridView1.Rows[N2].Cells[2].Value = LEE2["CANTIDAD"].ToString();
                    dataGridView1.Rows[N2].Cells[3].Value = LEE2["PRECIOPRODUCTO"].ToString();
                    dataGridView1.Rows[N2].Cells[4].Value = double.Parse(LEE2["CANTIDAD"].ToString()) * double.Parse(LEE2["PRECIOPRODUCTO"].ToString());
                    SubTotal = SubTotal + double.Parse(LEE2["CANTIDAD"].ToString()) * double.Parse(LEE2["PRECIOPRODUCTO"].ToString());
                }

                txtSubTotal.Text = SubTotal + "";
                Impuesto = SubTotal * 0.15;
                TotalPagar = SubTotal + Impuesto;
                txtImpuesto.Text = Impuesto + "";
                txtTotalPagar.Text = TotalPagar + "";


                CONEXION.Cerrar();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbCajero_SelectedIndexChanged(object sender, EventArgs e)
        {
            ACTIVAR();
        }

        private void cmbCliente_SelectedIndexChanged(object sender, EventArgs e)
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
                    COMANDO.CommandText = "INSERT INTO FACTURA VALUES ( '" + txtCodigo.Text + "','" + cmbCajero.SelectedValue + "','" + cmbCliente.SelectedValue + "','" + dtpFecha.Text + "')";

                    COMANDO.ExecuteNonQuery();
                    //MessageBox.Show("Registro almacenado correctamente");

                    cmbProducto.Enabled = true;
                   
                    txtCantidad.Enabled = true;
                    btnAgregar.Enabled = false;
                    btnFinalizar.Enabled = true;

                }
                else
                {
                    COMANDO.CommandText = "UPDATE FACTURA SET  EMPLEADOID='" + cmbCajero.SelectedValue + "' , CLIENTEID='" + cmbCliente.SelectedValue + "' WHERE  FACTURAID =" + txtCodigo.Text;
                   // MessageBox.Show("" + COMANDO.CommandText);
                    COMANDO.ExecuteNonQuery();
                    MessageBox.Show("Registro Editado correctamente");
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
                cmbCajero.Focus();
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (cmbProducto.Text == "" || txtCantidad.Text == "")
            {
                btnAgregar.Enabled = false;
            }
            else
            {
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

        //private void txtPrecio_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (double.Parse(txtPrecio.Text) >= 0) { }
        //    }
        //    catch
        //    {
        //        txtPrecio.Text = "";
        //    }
        //}

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnEditar.Enabled = false; dataGridView1.Enabled = true;

            cmbCajero.Enabled = true; cmbCliente.Enabled = true; dtpFecha.Enabled = true;
             cmbProducto.Enabled = true;
            txtCantidad.Enabled = true; btnFinalizar.Enabled = true; btnAgregar.Enabled = false;

            btnGuardar.Enabled = true;

            EditarCambio = 1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //--------Elimina registro 
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "DELETE FROM FACTURA WHERE FACTURAID=" + txtCodigo.Text;
            COMANDO.ExecuteNonQuery();
            CONEXION.Cerrar();

            //--Elimina Detalle de registro
            OleDbCommand COMANDO2 = new OleDbCommand();
            COMANDO2.Connection = CONEXION.Abrir();
            COMANDO2.CommandText = "DELETE FROM DETALLEFACTURA WHERE FACTURAID=" + txtCodigo.Text + "";
            // MessageBox.Show("" + COMANDO.CommandText);
            COMANDO2.ExecuteNonQuery();
            MessageBox.Show("Registro Eliminado Correctamente");
            INICIO();
            CONEXION.Cerrar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            INICIO();
            cmbCajero.SelectedValue = "-1";
            cmbCliente.SelectedValue = "-1";
            cmbProducto.SelectedValue = "-1";
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            INICIO();
            cmbCajero.SelectedValue = "-1";
            cmbCliente.SelectedValue = "-1";
            cmbProducto.SelectedValue = "-1";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CONEXION.Cerrar();
            dataGridView1.Rows.Clear();
            try
            {
                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();

                COMANDO.CommandText = "INSERT INTO DETALLEFACTURA (FACTURAID,PRODUCTOID,CANTIDAD) VALUES ( '" + txtCodigo.Text + "','" + cmbProducto.SelectedValue + "','" + txtCantidad.Text + "')";
               
                COMANDO.ExecuteNonQuery();
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CONEXION.Cerrar();
            //------------llena datagrid--
            
            DATAGRID();
            
            AGREGAINVENTARIO();
         
            cmbProducto.SelectedValue = -1; txtCantidad.Text = ""; 
        }
        public void DATAGRID()
        {

            dataGridView1.Rows.Clear();
            try
            {
                OleDbCommand COMANDO2 = new OleDbCommand();
                COMANDO2.Connection = CONEXION.Abrir();
                COMANDO2.CommandText = "SELECT * FROM DETALLEFACTURA LEFT JOIN PRODUCTO ON DETALLEFACTURA.PRODUCTOID = PRODUCTO.PRODUCTOID WHERE DETALLEFACTURA.FACTURAID =" + txtCodigo.Text + "";
                OleDbDataReader LEE2 = COMANDO2.ExecuteReader();
                double SubTotal = 0;
                double Impuesto = 0;
                double TotalPagar = 0;
                
                while (LEE2.Read())
                {
                    int N2 = dataGridView1.Rows.Add();


                    dataGridView1.Rows[N2].Cells[0].Value = LEE2["DETALLEFACTURAID"].ToString();
                    dataGridView1.Rows[N2].Cells[1].Value = LEE2["NOMBREPRODUCTO"].ToString();
                    dataGridView1.Rows[N2].Cells[2].Value = LEE2["CANTIDAD"].ToString();
                    dataGridView1.Rows[N2].Cells[3].Value = LEE2["PRECIOPRODUCTO"].ToString();
                    dataGridView1.Rows[N2].Cells[4].Value = double.Parse(LEE2["CANTIDAD"].ToString()) * double.Parse(LEE2["PRECIOPRODUCTO"].ToString());
                    SubTotal = SubTotal + double.Parse(LEE2["CANTIDAD"].ToString()) * double.Parse(LEE2["PRECIOPRODUCTO"].ToString());
                }

                txtSubTotal.Text = SubTotal + "";
                Impuesto = SubTotal * 0.15;
                TotalPagar = SubTotal + Impuesto;
                txtImpuesto.Text = Impuesto + "";
                txtTotalPagar.Text = TotalPagar + "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CONEXION.Cerrar();
        }
        public void AGREGAINVENTARIO()
        {
            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "UPDATE PRODUCTO SET EXISTENCIA= EXISTENCIA -" + txtCantidad.Text + " WHERE  PRODUCTOID =" + cmbProducto.SelectedValue;
            //MessageBox.Show("" + COMANDO.CommandText);
            COMANDO.ExecuteNonQuery();
            CONEXION.Cerrar();
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var numero = dataGridView1.CurrentRow.Cells[0].Value;

            OleDbCommand COMANDO = new OleDbCommand();
            COMANDO.Connection = CONEXION.Abrir();
            COMANDO.CommandText = "DELETE FROM DETALLEFACTURA WHERE FACTURAID=" + numero + "";
            // MessageBox.Show("" + COMANDO.CommandText);
            COMANDO.ExecuteNonQuery();
            MessageBox.Show("Registro Eliminado Correctamente");


            CONEXION.Cerrar();
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.Text == "" || txtCantidad.Text == "")
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
