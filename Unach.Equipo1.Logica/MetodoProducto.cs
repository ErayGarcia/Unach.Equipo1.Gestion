using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Unach.Equipo1.Datos;

namespace Unach.Equipo1.Logica
{
    public class MetodoProducto
    {
        public void LlenarComboBoxProveedor(ComboBox comboBoxProveedor)
        {
            // Cadena de conexión a tu base de datos
            string connectionString = conexion.ObtenerCadenaConexion();

            // Consulta SQL para obtener las categorías con sus IDs y nombres
            string sql = "SELECT IdProveedor, NombreProveedor FROM Proveedor";

            // Lista para almacenar las categorías (IdCategoria como clave y Categoria como valor)
            List<KeyValuePair<int, string>> categorias = new List<KeyValuePair<int, string>>();

            // Establecer conexión a la base de datos y ejecutar la consulta
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Leer los resultados y agregarlos a la lista
                while (reader.Read())
                {
                    int idCategoria = (int)reader["IdProveedor"];
                    string nombreCategoria = reader["NombreProveedor"].ToString();
                    categorias.Add(new KeyValuePair<int, string>(idCategoria, nombreCategoria));
                }
            }

            // Limpiar ComboBox antes de cargar datos
            comboBoxProveedor.Items.Clear();

            // Asignar el origen de datos al ComboBox
            comboBoxProveedor.ItemsSource = categorias;

            // Establecer la propiedad DisplayMember para mostrar el valor (nombre de la categoría) en el ComboBox
            comboBoxProveedor.DisplayMemberPath = "Value";

        }
        public void AgregarProducto(string nombre, string descripcion, decimal precioUnitario, int cantidadInventario, int proveedorID)
        {
            // Cadena de conexión a la base de datos
            string connectionString = conexion.ObtenerCadenaConexion();

            // Query para insertar un nuevo producto
            string query = "INSERT INTO Producto (Nombre, Descripcion, PrecioUnitario, CantidadInventario, ProveedorID) " +
                           "VALUES (@Nombre, @Descripcion, @PrecioUnitario, @CantidadInventario, @ProveedorID)";

            // Establecer la conexión con la base de datos y ejecutar la consulta
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Descripcion", descripcion);
                command.Parameters.AddWithValue("@PrecioUnitario", precioUnitario);
                command.Parameters.AddWithValue("@CantidadInventario", cantidadInventario);
                command.Parameters.AddWithValue("@ProveedorID", proveedorID);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"Se agregó {rowsAffected} producto(s) a la base de datos.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al agregar el producto: " + ex.Message);
                }
            }
        }
        public void EliminarProducto(int IDProducto)
        {
            string connectionString = conexion.ObtenerCadenaConexion();
            // Crear la consulta SQL para eliminar el proveedor
            string query = "DELETE FROM Producto WHERE IDProducto = @IDProducto";

            // Crear y abrir la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Crear y ejecutar el comando SQL para eliminar el proveedor
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Pasar el parámetro @IdProveedor al comando SQL
                    command.Parameters.AddWithValue("@IDProducto", IDProducto);

                    // Ejecutar el comando SQL
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
