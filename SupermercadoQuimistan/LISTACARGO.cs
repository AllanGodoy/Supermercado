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
    public partial class LISTACARGO : Form
    {
        SupermercadoQuimistan.Conexion CONEXION = new SupermercadoQuimistan.Conexion();
        public LISTACARGO()
        {
            InitializeComponent();
        }
        public void INICIO()
        {
            txtCodigo.Text = ""; txtCargo.Text = "";
            dataGridView1.Rows.Clear();
            btnCancelar.Enabled = false; btnBuscar.Enabled = false; btnSalir.Enabled = true;

            try
            {
                OleDbCommand COMANDO = new OleDbCommand();
                COMANDO.Connection = CONEXION.Abrir();
                COMANDO.CommandText = "SELECT * FROM CARGO LEFT JOIN AREA ON CARGO.AREAID = AREA.AREAID";
                OleDbDataReader LEE = COMANDO.ExecuteReader();

                while (LEE.Read())
                {
                    int N = dataGridView1.Rows.Add();
                    dataGridView1.Rows[N].Cells[0].Value = LEE["CARGOID"].ToString();
                    dataGridView1.Rows[N].Cells[1].Value = LEE["NOMBRECARGO"].ToString();
                    dataGridView1.Rows[N].Cells[2].Value = LEE["NOMBRE"].ToString();                 

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:" + ex);
            }

            CONEXION.Cerrar();
        }
        private void LISTACARGO_Load(object sender, EventArgs e)
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

                if (txtCargo.Text == "")
                {
                    COMANDO.CommandText = "SELECT * FROM CARGO LEFT JOIN AREA ON CARGO.AREAID = AREA.AREAID WHERE CARGOID =" + txtCodigo.Text;
                }
                if (txtCodigo.Text == "")
                {
                    COMANDO.CommandText = "SELECT * FROM CARGO LEFT JOIN AREA ON CARGO.AREAID = AREA.AREAID WHERE NOMBRECARGO ='" + txtCargo.Text + "'";
                }


                OleDbDataReader LEE = COMANDO.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (LEE.Read())
                {
                    int N = dataGridView1.Rows.Add();
                    dataGridView1.Rows[N].Cells[0].Value = LEE["CARGOID"].ToString();
                    dataGridView1.Rows[N].Cells[1].Value = LEE["NOMBRECARGO"].ToString();
                    dataGridView1.Rows[N].Cells[2].Value = LEE["NOMBRE"].ToString();

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
            txtCargo.Text = "";
            btnBuscar.Enabled = true;
            if (txtCargo.Text == "" && txtCodigo.Text == "")
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

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            btnBuscar.Enabled = true;
            if (txtCargo.Text == "" && txtCodigo.Text == "")
            {
                btnBuscar.Enabled = false;
            }
        }
    }
}
