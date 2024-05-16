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
using System.Windows.Shapes;
using Unach.Equipo1.Datos;
using Unach.Equipo1.Logica;

namespace Unach.Equipo1.Vistas.Cliente
{
    /// <summary>
    /// Lógica de interacción para UcClienteInfo.xaml
    /// </summary>
    public partial class UcClienteInfo : Window
    {
        public UcClienteInfo()
        {
            InitializeComponent();
            CargarDatosClientes();
        }

        private void dgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void CargarDatosClientes()
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
                    string query = "SELECT * FROM Cliente";

                    // Crear un adaptador de datos para ejecutar la consulta y llenar un DataSet
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();

                    // Llenar el DataSet con los datos de la tabla de clientes
                    adapter.Fill(dataSet, "Cliente");

                    // Asignar el DataTable como origen de datos para el DataGrid
                    dgClientes.ItemsSource = dataSet.Tables["Cliente"].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de clientes: " + ex.Message);
            }
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedItems.Count > 0)
            {
                DataRowView row = dgClientes.SelectedItem as DataRowView;
                int idCliente = Convert.ToInt32(row["IDCliente"]);
                string Nombre = row["Nombre"].ToString();
                string Direccion = row["Direccion"].ToString();
                string NumeroTelefono = row["NumeroTelefono"].ToString();
                string correoelectronico = row["Correoelectronico"].ToString();
                string ApellidoPaterno = row["ApellidoPaterno"].ToString();
                string ApellidoMaterno = row["ApellidoMaterno"].ToString();

                // Crear una instancia de UCProveedor.xaml y pasar los datos
                UcCliente ucProducto = new UcCliente(idCliente, Nombre, Direccion,NumeroTelefono,correoelectronico,ApellidoPaterno,ApellidoMaterno);

                // Limpiar el contenido actual de MainGrid y agregar UCProveedor
                Omega.Children.Clear();
                Omega.Children.Add(ucProducto);
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para modificar.", "Modificar Proveedor",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada en el DataGridView
            if (dgClientes.SelectedItems.Count > 0)
            {
                // Mostrar un mensaje de confirmación
                MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar el elemento seleccionado?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Verificar si el usuario confirmó la eliminación
                if (result == MessageBoxResult.Yes)
                {
                    // Obtener el IDProveedor de la fila seleccionada
                    int idClienteAEliminar = Convert.ToInt32((dgClientes.SelectedItem as DataRowView)["IDCliente"]);

                    // Llamar al método EliminarProveedor para eliminar el proveedor
                    ClienteMetodo ese = new ClienteMetodo();
                    ese.EliminarCliente(idClienteAEliminar);

                    // Refrescar el DataGrid después de eliminar el proveedor
                    CargarDatosClientes(); // Suponiendo que CargarDatos es un método que actualiza el DataGrid
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un proveedor para eliminar.", "Eliminar Proveedor",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
