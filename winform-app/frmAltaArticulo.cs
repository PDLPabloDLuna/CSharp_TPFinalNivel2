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
using negocio;


namespace winform_app
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;

        public frmAltaArticulo()
        {
            InitializeComponent();
        }
        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Artículo";
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                borrarMensajeError();
                if (validarCampos())
                {
                    MessageBox.Show("Por favor, ingrese los datos requeridos..");
                    return;
                }

                if (articulo == null)
                {
                    articulo = new Articulo();
                }
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                articulo.UrlImagen = txtUrlImagen.Text;

                articulo.NombreMarca = (Marca)cboMarca.SelectedItem;
                articulo.TipoCat = (Categoria)cboCategoria.SelectedItem;

                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }


                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    cboMarca.SelectedValue = articulo.NombreMarca.Id;
                    cboCategoria.SelectedValue = articulo.TipoCat.Id;
                    txtPrecio.Text = articulo.Precio.ToString("F");
                    txtUrlImagen.Text = articulo.UrlImagen;
                    cargarImagen(articulo.UrlImagen);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxUrlImagen.Load(imagen);
                pbxUrlImagen.SizeMode = PictureBoxSizeMode.CenterImage;
                pbxUrlImagen.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                pbxUrlImagen.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQVIiNXO1UGEUca6N_ZRXpaxUXzAUqs_1KTCpXauiZOpAO6jrQ0XwivrGx3F9UJBNCzn8E&usqp=CAU");
            }
        }
        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }


        //validaciones errorProvider1

        private bool validarCampos()
        {
            bool ok = false;
            if (txtCodigo.Text == "")
            {
                errorProvider1.SetError(txtCodigo, "Ingrese el código del artículo");
                ok = true;
            }
            if (txtNombre.Text == "")
            {
                errorProvider1.SetError(txtNombre, "Ingrese un Nombre");
                ok = true;
            }
            if (txtDescripcion.Text == "")
            {
                errorProvider1.SetError(txtDescripcion, "Ingrese la descripción del artículo");
                ok = true;
            }
            if (txtPrecio.Text == "" || !soloPrecios(txtPrecio.Text) || decimal.Parse(txtPrecio.Text) <= 0)
            {
                errorProvider1.SetError(txtPrecio, "Ingrese un valor de precio (puede incluir una coma)");
                ok = true;
            }
            return ok;
        }
        private void borrarMensajeError()
        {
            errorProvider1.SetError(txtCodigo, "");
            errorProvider1.SetError(txtNombre, "");
            errorProvider1.SetError(txtDescripcion, "");
            errorProvider1.SetError(txtPrecio, "");
        }

        private void txtPrecio_Validating(object sender, CancelEventArgs e)
        {
            decimal valor;
            if (!decimal.TryParse(txtPrecio.Text, out valor) || decimal.Parse(txtPrecio.Text) <= 0)
                errorProvider1.SetError(txtPrecio, "Ingrese un valor de precio (puede incluir una coma)");
            else
                errorProvider1.SetError(txtPrecio, "");
        }
        private bool soloPrecios(string cadena)
        {
            int n = 0, comma = 0;
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)) && n == 0)
                    return false;
                if (!char.IsNumber(caracter) && (caracter != ','))
                    return false;
                if (caracter == ',')
                    comma += 1;
                n += 1;
            }
            if (comma > 1)
                return false;
            return true;
        }

    }
}
