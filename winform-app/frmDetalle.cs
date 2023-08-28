using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;

namespace winform_app
{
    public partial class frmDetalle : Form
    {
        private Articulo articulo = null;
        public frmDetalle()
        {
            InitializeComponent();
        }
        public frmDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Detalle de " + articulo.Nombre;
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                txtName.Text = articulo.Nombre;
                txtCode.Text = articulo.Codigo;
                txtBrand.Text = articulo.NombreMarca.Descripcion;
                txtCategory.Text = articulo.TipoCat.Descripcion;
                txtPrice.Text = articulo.Precio.ToString("C");
                txtDescription.Text = articulo.Descripcion;

                txtName.Enabled = false;
                txtCode.Enabled = false;
                txtBrand.Enabled = false;
                txtCategory.Enabled = false;
                txtPrice.Enabled = false;
                txtDescription.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
