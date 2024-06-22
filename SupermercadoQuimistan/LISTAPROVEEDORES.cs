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
    public partial class LISTAPROVEEDORES : Form
    {
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();
        public LISTAPROVEEDORES()
        {
            InitializeComponent();
        }
        public void INICIO()
        {
            txtCodigo.Text = ""; txtNombre.Text = "";
            dataGridView1.Rows.Clear();
            btnCancelar.Enabled = false; btnBuscar.Enabled = false; btnSalir.Enabled = true;

            try
            {
                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();
                COMANDO.CommandText = "SELECT * FROM PROVEEDOR";
                OleDbDataReader LEE = COMANDO.ExecuteReader();

                while (LEE.Read())
                {
                    int N = dataGridView1.Rows.Add();
                    dataGridView1.Rows[N].Cells[0].Value = LEE["PROVEEDORID"].ToString();
                    dataGridView1.Rows[N].Cells[1].Value = LEE["NOMBRE"].ToString();
                    dataGridView1.Rows[N].Cells[2].Value = LEE["DIRECCION"].ToString();
                    dataGridView1.Rows[N].Cells[3].Value = LEE["RTN"].ToString();
                    dataGridView1.Rows[N].Cells[4].Value = LEE["TELEFONO"].ToString();
                   
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex);
            }

            CONEXION.Cerrar();
        }
        private void LISTAPROVEEDORES_Load(object sender, EventArgs e)
        {
            INICIO();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            btnBuscar.Enabled = false;
            btnCancelar.Enabled = true;
            try
            {
                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();

                if (txtNombre.Text == "")
                {
                    COMANDO.CommandText = "SELECT * FROM PROVEEDOR WHERE PROVEEDORID =" + txtCodigo.Text;
                }
                if (txtCodigo.Text == "")
                {
                    COMANDO.CommandText = "SELECT * FROM PROVEEDOR WHERE NOMBRE ='" + txtNombre.Text + "'";
                }


                OleDbDataReader LEE = COMANDO.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (LEE.Read())
                {
                    int N = dataGridView1.Rows.Add();
                    dataGridView1.Rows[N].Cells[0].Value = LEE["PROVEEDORID"].ToString();
                    dataGridView1.Rows[N].Cells[1].Value = LEE["NOMBRE"].ToString();
                    dataGridView1.Rows[N].Cells[2].Value = LEE["DIRECCION"].ToString();
                    dataGridView1.Rows[N].Cells[3].Value = LEE["RTN"].ToString();
                    dataGridView1.Rows[N].Cells[4].Value = LEE["TELEFONO"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex);
            }

            CONEXION.Cerrar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            INICIO();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            CONEXION.Cerrar();
            this.Close();
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            btnBuscar.Enabled = true;
            if (txtNombre.Text == "" && txtCodigo.Text == "")
            {
                btnBuscar.Enabled = false;
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

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            btnBuscar.Enabled = true;
            if (txtNombre.Text == "" && txtCodigo.Text == "")
            {
                btnBuscar.Enabled = false;
            }
        }

    }
}
