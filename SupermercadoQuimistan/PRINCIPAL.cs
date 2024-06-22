using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SupermercadoQuimistan
{
    public partial class PRINCIPAL : Form
    {
        public PRINCIPAL()
        {
            InitializeComponent();
           
        }

        private void PRINCIPAL_FormClosed(object sender, FormClosedEventArgs e)
        {
            menu.Enabled = true;
        }


        private void PRINCIPAL_FormClosing(object sender, FormClosingEventArgs e)
        {
            menuEmpresa.Enabled = true;
            menuProducto.Enabled = true;
            MenuCliente.Enabled = true;
            menuProveedor.Enabled = true;
            menuVentas.Enabled = true;
            menuCompras.Enabled = true;
            MenuConfiguracion.Enabled = true;
            
        }

        private void DISABLEMENU()
        {
            menuEmpresa.Enabled = false;
            menuProducto.Enabled = false;
            MenuCliente.Enabled = false;
            menuProveedor.Enabled = false;
            menuVentas.Enabled = false;
            menuCompras.Enabled = false;
            MenuConfiguracion.Enabled = false;

        }
        //--- Muestra el LOGIN --//
        private void PRINCIPAL_Load(object sender, EventArgs e)
        {
          
            DISABLEMENU();
             // menuHistorial.Enabled = false;
             // menuProducto.Enabled = false;
             LOGIN login = new LOGIN();
            login.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            login.MdiParent = this;
            login.Show();
           
        }

    
        private void sALIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cLIENTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            CLIENTE cliente = new CLIENTE();
            cliente.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            cliente.MdiParent = this;
            cliente.Show();
        }

        private void pROVEEDORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            PROVEEDOR proveedor = new PROVEEDOR();
            proveedor.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            proveedor.MdiParent = this;
            proveedor.Show();
        }

        private void pRODUCTOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            PRODUCTO producto = new PRODUCTO();
            producto.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            producto.MdiParent = this;
            producto.Show();
        }

     

        private void cONFIGURACIONToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// ------------------Nuevo Menu---------------------
        
        private void cOMPRAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            PRODUCTO producto = new PRODUCTO();
            producto.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            producto.MdiParent = this;
            producto.Show();
        }

        private void iNGRESOCLIENTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            CLIENTE cliente = new CLIENTE();
            cliente.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            cliente.MdiParent = this;
            cliente.Show();
        }

        private void iNGRESOPROVEEDORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            PROVEEDOR proveedor = new PROVEEDOR();
            proveedor.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            proveedor.MdiParent = this;
            proveedor.Show();
        }
     
        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            COMPRAS compras = new COMPRAS();
            compras.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            compras.MdiParent = this;
            compras.Show();
        }

        private void uSUARIOSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            USUARIOS usuarios = new USUARIOS();
            usuarios.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            usuarios.MdiParent = this;
            usuarios.Show();
        }
      
        private void Inventario_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            INVENTARIO invenario = new INVENTARIO();
            invenario.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            invenario.MdiParent = this;
            invenario.Show();
        }

        private void lISTACLIENTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            LISTACLIENTES listaclientes = new LISTACLIENTES();
            listaclientes.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            listaclientes.MdiParent = this;
            listaclientes.Show();
        }

        private void lISTAPROVEEDORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            LISTAPROVEEDORES listaproveedores = new LISTAPROVEEDORES();
            listaproveedores.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            listaproveedores.MdiParent = this;
            listaproveedores.Show();
        }

        private void lISTAFACTURASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            HISTORIALFACTURAS HistorialFactura = new HISTORIALFACTURAS();
            HistorialFactura.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            HistorialFactura.MdiParent = this;
            HistorialFactura.Show();
        }

        private void lISTADECOMPRASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            LISTACOMPRAS listacompras = new LISTACOMPRAS();
            listacompras.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            listacompras.MdiParent = this;
            listacompras.Show();
        }

        private void iNGRESOAREAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            AREA area = new AREA();
            area.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            area.MdiParent = this;
            area.Show();
        }

        private void iNGRESOSUCURSALESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            EMPRESA empresa = new EMPRESA();
            empresa.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            empresa.MdiParent = this;
            empresa.Show();
        }

        private void iNGRESODECARGOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            CARGO cargo = new CARGO();
            cargo.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            cargo.MdiParent = this;
            cargo.Show();
        }

        private void iNGRESODEEMPLEADOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            EMPLEADO empleado = new EMPLEADO();
            empleado.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            empleado.MdiParent = this;
            empleado.Show();
        }

        private void lISTADODESUCURSALESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            LISTASOCURSALES sucursales = new LISTASOCURSALES();
            sucursales.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            sucursales.MdiParent = this;
            sucursales.Show();
        }

        private void lISTADODEAREAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            LISTAAREA listaarea = new LISTAAREA();
            listaarea.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            listaarea.MdiParent = this;
            listaarea.Show();
        }

        private void lISTADODEEMPLEADOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            LISTAEMPLEADOS listaempleados = new LISTAEMPLEADOS();
            listaempleados.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            listaempleados.MdiParent = this;
            listaempleados.Show();
        }

        private void lISTAUSUARIOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            LISTAUSUARIO listausuario = new LISTAUSUARIO();
            listausuario.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            listausuario.MdiParent = this;
            listausuario.Show();
        }

        private void fACTURAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            FACTURA factura = new FACTURA();
            factura.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            factura.MdiParent = this;
            factura.Show();
        }

        private void lISTADODECARGOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DISABLEMENU();
            LISTACARGO cargo = new LISTACARGO();
            cargo.FormClosing += new FormClosingEventHandler(PRINCIPAL_FormClosing);
            cargo.MdiParent = this;
            cargo.Show();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
