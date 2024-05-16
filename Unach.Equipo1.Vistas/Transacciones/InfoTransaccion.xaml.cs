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

namespace Unach.Equipo1.Vistas.Transacciones
{
    /// <summary>
    /// Lógica de interacción para InfoTransaccion.xaml
    /// </summary>
    public partial class InfoTransaccion : UserControl
    {
        public InfoTransaccion()
        {
            InitializeComponent();
            CargarDatosTra();
        }

        private void dgProveedor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void CargarDatosTra()
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
                    string query = "SELECT * FROM Transaccion";

                    // Crear un adaptador de datos para ejecutar la consulta y llenar un DataSet
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataSet dataSet = new DataSet();

                    // Llenar el DataSet con los datos de la tabla de clientes
                    adapter.Fill(dataSet, "Transaccion");

                    // Asignar el DataTable como origen de datos para el DataGrid
                    dgTransacion.ItemsSource = dataSet.Tables["Transaccion"].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de clientes: " + ex.Message);
            }
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (dgTransacion.SelectedItems.Count > 0)
            {
                DataRowView row = dgTransacion.SelectedItem as DataRowView;
                int idTransaccion = Convert.ToInt32(row["IDTransaccion"]);
                string Fecha = row["Fecha"].ToString();
                string Tipo = row["Tipo"].ToString();
                string Monto = row["Monto"].ToString();
                string Descripcion= row["Descripcion"].ToString();

                // Crear una instancia de UCProveedor.xaml y pasar los datos
                UCTransacciones uctransaccion = new UCTransacciones(idTransaccion, Fecha, Tipo, Monto, Descripcion);

                // Limpiar el contenido actual de MainGrid y agregar UCProveedor

                Vizual.Children.Clear();
                Vizual.Children.Add(uctransaccion);



            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para modificar.", "Modificar Transaccion",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si hay al menos una fila seleccionada en el DataGridView
            if (dgTransacion.SelectedItems.Count > 0)
            {
                // Mostrar un mensaje de confirmación
                MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea eliminar el elemento seleccionado?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Verificar si el usuario confirmó la eliminación
                if (result == MessageBoxResult.Yes)
                {
                    // Obtener el IDProveedor de la fila seleccionada
                    int idTransaccionAEliminar = Convert.ToInt32((dgTransacion.SelectedItem as DataRowView)["IDTransaccion"]);

                    // Llamar al método EliminarProveedor para eliminar el proveedor
                    Transferencias gestor = new Transferencias();
                    gestor.EliminarTrans(idTransaccionAEliminar);

                    // Refrescar el DataGrid después de eliminar el proveedor
                    CargarDatosTra(); // Suponiendo que CargarDatos es un método que actualiza el DataGrid
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un proveedor para eliminar.", "Eliminar Proveedor",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e != null && e.Changes != null && e.Changes.Any(c => c.AddedLength > 0 && txtFilter.Text.EndsWith(" ")))
            {
                EjecutarConsulta(txtFilter.Text.Trim());
            }
            else
            {
                EjecutarConsulta(txtFilter.Text);
            }
        }
        private void EjecutarConsulta(string filtro)
        {
            string connectionString = conexion.ObtenerCadenaConexion();
            string consulta = "SELECT Fecha FROM Transaccion WHERE Fecha LIKE @filtro";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                reader.Close();
            }
        }
    }
}
