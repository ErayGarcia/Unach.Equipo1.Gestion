using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unach.Equipo1.Datos;
using Unach.Equipo1.Logica;

namespace Unach.Equipo1.Vistas.Producto
{
    /// <summary>
    /// Lógica de interacción para UCProducto.xaml
    /// </summary>
    public partial class UCProducto : UserControl
    {
        private int idProducto;
        MetodoProducto metodo = new MetodoProducto();
        public UCProducto()
        {
            InitializeComponent();
            metodo.LlenarComboBoxProveedor(comboBoxProveedor);
        }
        public UCProducto(int idProducto, string Nombre, string Descripcion, string Preciounitario, string CantidadInventario) : this()
        {
            this.idProducto = idProducto;
            txtNombre.Text = Nombre;
            txtDescripcion.Text = Descripcion;
            txtPrecioUnitario.Text = Preciounitario;
            txtCantidadInventario.Text = CantidadInventario;
        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si todos los campos están completos
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtPrecioUnitario.Text) ||
                string.IsNullOrWhiteSpace(txtCantidadInventario.Text) ||
                comboBoxProveedor.SelectedItem == null)
            {
                MessageBox.Show("Por favor complete todos los campos antes de guardar.", "Error");
                return;
            }

            // Convertir los valores ingresados por el usuario
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            decimal precioUnitario;
            int cantidadInventario;

            if (!decimal.TryParse(txtPrecioUnitario.Text, out precioUnitario) ||
                !int.TryParse(txtCantidadInventario.Text, out cantidadInventario))
            {
                MessageBox.Show("Ingrese valores válidos para el precio unitario y la cantidad en inventario.", "Error");
                return;
            }

            if (comboBoxProveedor.SelectedItem != null)
            {
                KeyValuePair<int, string> proveedorSeleccionado = (KeyValuePair<int, string>)comboBoxProveedor.SelectedItem;
                int proveedorID = proveedorSeleccionado.Key;

                // Crear una instancia de la clase MetodoProducto y llamar al método AgregarProducto con los valores obtenidos
                MetodoProducto producto = new MetodoProducto();
                producto.AgregarProducto(nombre, descripcion, precioUnitario, cantidadInventario, proveedorID);

                // Mostrar mensaje de éxito
                MessageBox.Show("El producto ha sido guardado correctamente.", "Éxito");
            }
            else
            {
                MessageBox.Show("Seleccione un proveedor antes de guardar.", "Error");
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtPrecioUnitario.Text = "";
            txtCantidadInventario.Text = "";
            txtDescripcion.Text = "";
            txtNombre.Text="";
        }

        private void btnVisualizar_Click(object sender, RoutedEventArgs e)
        {
            InfoProducto inf = new InfoProducto();
            Pro.Children.Clear();
            Pro.Children.Add(inf);
        }

        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            string nuevoNombre = txtNombre.Text;
            string nuevaDescripcion = txtDescripcion.Text;
            string nuevoPreciounitario = txtPrecioUnitario.Text;
            string nuevoCantidadInventario = txtCantidadInventario.Text;

            string connectionString = conexion.ObtenerCadenaConexion();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE Producto SET Nombre = @Nombre, Descripcion = @Descripcion, PrecioUnitario = @PrecioUnitario, CantidadInventario = @CantidadInventario WHERE IDProducto = @IDProducto";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nuevoNombre);
                    command.Parameters.AddWithValue("@Descripcion", nuevaDescripcion);
                    command.Parameters.AddWithValue("@PrecioUnitario", nuevoPreciounitario);
                    command.Parameters.AddWithValue("@CantidadInventario", nuevoCantidadInventario);
                    command.Parameters.AddWithValue("@IDProducto", idProducto);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Producto modificado correctamente.", "Modificar Proveedor", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar el producto.", "Modificar Proveedor", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar el producto: " + ex.Message, "Modificar Proveedor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
