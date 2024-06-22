using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SupermercadoQuimistan
{
    internal class Conexion
    {
        OleDbConnection CONECTAR = new OleDbConnection();

        public OleDbConnection Abrir()
        {
            try
            {
                CONECTAR.ConnectionString = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + ".\\SupermercadoQuimistan.mdb;Persist Security Info=false;";
                CONECTAR.Open();
               // MessageBox.Show("se conecto");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se conecto correctamente a la Base de Datos, error =" + ex.ToString());

            }

            return CONECTAR;
        }
        public void Cerrar()
        {
            CONECTAR.Close();
        }
    }
}
