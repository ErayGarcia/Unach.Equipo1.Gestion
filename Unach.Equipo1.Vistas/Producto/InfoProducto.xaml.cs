using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para InfoProducto.xaml
    /// </summary>
    public partial class InfoProducto : UserControl
    {
        public InfoProducto()
        {
            InitializeComponent();
            CargarDatosProducto();
        }

        private void dgProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void CargarDatosProducto()
        {
            try
            {
                // Obtener la cadena de conexión desde el archivo de configuración (App.config)
                string connectionString = conexion.ObtenerCadenaConexion();

                // Crear la conexión a la base de datos SQL Server
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();

                    // Consulta SQL para obtener los datos de la tabla de clientes
                    string query = "SELECT * FROM Producto";

                    // Crear un adaptador de datos para ejecutar la consulta y llenar un DataSet
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();

                    // Llenar el DataSet con los datos de la tabla de clientes
                    adapter.Fill(dataSet, "Producto");

                    // Asignar el DataTable como origen de datos para el DataGrid
                    dgProducto.ItemsSource = dataSet.Tables["Producto"].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de clientes: " + ex.Message);
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada en el DataGridView
            if (dgProducto.SelectedItems.Count > 0)
            {
                // Mostrar un mensaje de confirmación
                MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar el elemento seleccionado?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Verificar si el usuario confirmó la eliminación
                if (result == MessageBoxResult.Yes)
                {
                    // Obtener el IDProveedor de la fila seleccionada
                    int idProductoAEliminar = Convert.ToInt32((dgProducto.SelectedItem as DataRowView)["IDProducto"]);

                    // Llamar al método EliminarProveedor para eliminar el proveedor
                    MetodoProducto gestor = new MetodoProducto();
                    gestor.EliminarProducto(idProductoAEliminar);

                    // Refrescar el DataGrid después de eliminar el proveedor
                    CargarDatosProducto(); // Suponiendo que CargarDatos es un método que actualiza el DataGrid
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un proveedor para eliminar.", "Eliminar Proveedor",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (dgProducto.SelectedItems.Count > 0)
            {
                DataRowView row = dgProducto.SelectedItem as DataRowView;
                int idProducto = Convert.ToInt32(row["IDProducto"]);
                string Nombre = row["Nombre"].ToString();
                string descripcion = row["Descripcion"].ToString();
                string PrecioUnitario = row["PrecioUnitario"].ToString();
                string cantidadInventario = row["CantidadInventario"].ToString();

                // Crear una instancia de UCProveedor.xaml y pasar los datos
                UCProducto ucProducto = new UCProducto(idProducto, Nombre, descripcion, PrecioUnitario, cantidadInventario);

                // Limpiar el contenido actual de MainGrid y agregar UCProveedor
                
                dios.Children.Clear();
                dios.Children.Add(ucProducto);


                
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para modificar.", "Modificar Proveedor",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
