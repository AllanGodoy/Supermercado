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
    public partial class LISTASOCURSALES : Form
    {
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();

        public LISTASOCURSALES()
        {
            InitializeComponent();
        }

        public void INICIO()
        {
            txtCodigo.Text = ""; txtSucursal.Text = "";
            // txtCodigo.Enabled = true; txtNombre.Enabled = true;
            // dataGridView1 = new DataGridView();
            dataGridView1.Rows.Clear();
            btnCancelar.Enabled = false; btnBuscar.Enabled = false; btnSalir.Enabled = true;

            try
            {
                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();
                COMANDO.CommandText = "SELECT * FROM EMPRESA";
                OleDbDataReader LEE = COMANDO.ExecuteReader();

                while (LEE.Read())
                {
                    int N = dataGridView1.Rows.Add();
                    dataGridView1.Rows[N].Cells[0].Value = LEE["EMPRESAID"].ToString();
                    dataGridView1.Rows[N].Cells[1].Value = LEE["NOMBRE"].ToString();
                    dataGridView1.Rows[N].Cells[2].Value = LEE["DIRECCION"].ToString();
                    dataGridView1.Rows[N].Cells[3].Value = LEE["TELEFONO"].ToString();
                    dataGridView1.Rows[N].Cells[4].Value = LEE["CONTACTO"].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex);
            }

            CONEXION.Cerrar();
        }
        private void LISTASOCURSALES_Load(object sender, EventArgs e)
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

                if (txtSucursal.Text == "")
                {
                    COMANDO.CommandText = "SELECT * FROM EMPRESA WHERE EMPRESAID =" + txtCodigo.Text;
                }
                if (txtCodigo.Text == "")
                {
                    COMANDO.CommandText = "SELECT * FROM EMPRESA WHERE NOMBRE ='" + txtSucursal.Text + "'";
                }


                OleDbDataReader LEE = COMANDO.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (LEE.Read())
                {
                    int N = dataGridView1.Rows.Add();
                    dataGridView1.Rows[N].Cells[0].Value = LEE["EMPRESAID"].ToString();
                    dataGridView1.Rows[N].Cells[1].Value = LEE["NOMBRE"].ToString();
                    dataGridView1.Rows[N].Cells[2].Value = LEE["DIRECCION"].ToString();
                    dataGridView1.Rows[N].Cells[3].Value = LEE["TELEFONO"].ToString();
                    dataGridView1.Rows[N].Cells[4].Value = LEE["CONTACTO"].ToString();
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
            txtSucursal.Text = "";
            btnBuscar.Enabled = true;
            if (txtSucursal.Text == "" && txtCodigo.Text == "")
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

        private void txtSucursal_TextChanged(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            btnBuscar.Enabled = true;
            if (txtSucursal.Text == "" && txtCodigo.Text == "")
            {
                btnBuscar.Enabled = false;
            }
        }
    }
}
