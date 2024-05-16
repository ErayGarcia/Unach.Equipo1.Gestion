using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Unach.Equipo1.Vistas.Proveedor
{
    /// <summary>
    /// Lógica de interacción para UCProveedorInfo.xaml
    /// </summary>
    public partial class UCProveedorInfo : UserControl
    {
        private int idProveedorSeleccionadoGlobal;
        private int idProveedorSeleccionado;


        public UCProveedorInfo()
        {
            InitializeComponent();
            CargarDatosProveedor();
            
        }


        private void CargarDatosProveedor()
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
                    string query = "SELECT * FROM Proveedor";

                    // Crear un adaptador de datos para ejecutar la consulta y llenar un DataSet
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();

                    // Llenar el DataSet con los datos de la tabla de clientes
                    adapter.Fill(dataSet, "Proveedor");

                    // Asignar el DataTable como origen de datos para el DataGrid
                    dgProveedor.ItemsSource = dataSet.Tables["Proveedor"].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de clientes: " + ex.Message);
            }
        }


        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (dgProveedor.SelectedItems.Count > 0)
            {
                DataRowView row = dgProveedor.SelectedItem as DataRowView;
                int idProveedor = Convert.ToInt32(row["IDProveedor"]);
                string nombreProveedor = row["NombreProveedor"].ToString();
                string direccionProveedor = row["DireccionProveedor"].ToString();
                string numeroTelefonoProveedor = row["NumeroTelefonoProveedor"].ToString();
                string correoElectronicoProveedor = row["CorreoElectronicoProveedor"].ToString();

                // Crear una instancia de UCProveedor.xaml y pasar los datos
                UCProveedor ucProveedor = new UCProveedor(idProveedor, nombreProveedor, direccionProveedor, numeroTelefonoProveedor, correoElectronicoProveedor);

                // Limpiar el contenido actual de MainGrid y agregar UCProveedor
                MainGrid.Children.Clear();
                MainGrid.Children.Add(ucProveedor);
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un proveedor para modificar.", "Modificar Proveedor",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada en el DataGridView
            if (dgProveedor.SelectedItems.Count > 0)
            {
                // Mostrar un mensaje de confirmación
                MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar el elemento seleccionado?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Verificar si el usuario confirmó la eliminación
                if (result == MessageBoxResult.Yes)
                {
                    // Obtener el IDProveedor de la fila seleccionada
                    int idProveedorAEliminar = Convert.ToInt32((dgProveedor.SelectedItem as DataRowView)["IDProveedor"]);

                    // Llamar al método EliminarProveedor para eliminar el proveedor
                    AGProveedor gestor = new AGProveedor();
                    gestor.EliminarProveedor(idProveedorAEliminar);

                    // Refrescar el DataGrid después de eliminar el proveedor
                    CargarDatosProveedor(); // Suponiendo que CargarDatos es un método que actualiza el DataGrid
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un proveedor para eliminar.", "Eliminar Proveedor",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void dgProveedor_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Verificar si se hizo clic en el botón derecho del mouse
            if (e.RightButton == MouseButtonState.Pressed)
            {
                // Verificar si hay al menos una fila seleccionada en el DataGridView
                if (dgProveedor.SelectedItems.Count > 0)
                {
                    // Obtener el IDProveedor de la fila seleccionada
                    int idProveedorAEliminar = Convert.ToInt32((dgProveedor.SelectedItem as DataRowView)["IDProveedor"]);

                    // Llamar al método EliminarProveedor para eliminar el proveedor
                    AGProveedor gestor = new AGProveedor();
                    gestor.EliminarProveedor(idProveedorAEliminar);

                    // Refrescar el DataGrid u otra acción necesaria después de eliminar el proveedor
                    // Por ejemplo:
                    // CargarDatos(); // Método para recargar los datos en el DataGrid
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un proveedor para eliminar.", "Eliminar Proveedor",
                                    MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void dgProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
    }
}
