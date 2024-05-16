using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Unach.Equipo1.Datos;
using Unach.Equipo1.Logica;


namespace Unach.Equipo1.Logica
{
    public class Transferencias
    {
        public void GuardarTransferencia(DateTime fecha, string tipo, decimal monto, string descripcion, int categoriaId, int productoId)
        {
            string connectionString = conexion.ObtenerCadenaConexion();
            // Establecer la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear el comando SQL para insertar datos en la tabla "transferencia"
                    string query = "INSERT INTO Transaccion (Fecha, Tipo, Monto, Descripcion, CategoriaId, ProductoId) VALUES (@Fecha, @Tipo, @Monto, @Descripcion, @CategoriaId, @ProductoId)";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Agregar parámetros al comando
                    command.Parameters.AddWithValue("@Fecha", fecha);
                    command.Parameters.AddWithValue("@Tipo", tipo);
                    command.Parameters.AddWithValue("@Monto", monto);
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@CategoriaId", categoriaId);
                    command.Parameters.AddWithValue("@ProductoId", productoId);

                    // Ejecutar el comando
                    command.ExecuteNonQuery();

                    Console.WriteLine("Datos de transferencia guardados correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al guardar los datos de transferencia: " + ex.Message);
                }
            }
        }
        public void LlenarComboBoxCategoria(ComboBox comboBoxCategoria)
        {
            // Cadena de conexión a tu base de datos
            string connectionString = conexion.ObtenerCadenaConexion();

            // Consulta SQL para obtener las categorías con sus IDs y nombres
            string sql = "SELECT IdCategoria, Categoria FROM Categoria";

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
                    int idCategoria = (int)reader["IdCategoria"];
                    string nombreCategoria = reader["Categoria"].ToString();
                    categorias.Add(new KeyValuePair<int, string>(idCategoria, nombreCategoria));
                }
            }

            // Limpiar ComboBox antes de cargar datos
            comboBoxCategoria.Items.Clear();

            // Asignar el origen de datos al ComboBox
            comboBoxCategoria.ItemsSource = categorias;

            // Establecer la propiedad DisplayMember para mostrar el valor (nombre de la categoría) en el ComboBox
            comboBoxCategoria.DisplayMemberPath = "Value";

        }
        public void LlenarComboBoxProducto(ComboBox comboBoxProducto)
        {
            // Cadena de conexión a tu base de datos
            string connectionString = conexion.ObtenerCadenaConexion();

            // Consulta SQL para obtener los productos con sus IDs y nombres
            string sql = "SELECT IdProducto, Nombre FROM Producto";

            // Lista para almacenar los productos (IdProducto como clave y Nombre como valor)
            List<KeyValuePair<int, string>> productos = new List<KeyValuePair<int, string>>();

            // Establecer conexión a la base de datos y ejecutar la consulta
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Leer los resultados y agregarlos a la lista
                while (reader.Read())
                {
                    int idProducto = (int)reader["IdProducto"];
                    string nombreProducto = reader["Nombre"].ToString();
                    productos.Add(new KeyValuePair<int, string>(idProducto, nombreProducto));
                }
            }

            // Limpiar ComboBox antes de cargar datos
            comboBoxProducto.Items.Clear();

            // Asignar el origen de datos al ComboBox
            comboBoxProducto.ItemsSource = productos;

            // Establecer la propiedad DisplayMemberPath para mostrar el valor (nombre del producto) en el ComboBox
            comboBoxProducto.DisplayMemberPath = "Value";
            // Establecer la propiedad SelectedValuePath para usar el IdProducto como valor seleccionado
            comboBoxProducto.SelectedValuePath = "Key";
        }
        public void EliminarTrans(int IDTransaccion)
        {
            string connectionString = conexion.ObtenerCadenaConexion();
            // Crear la consulta SQL para eliminar el proveedor
            string query = "DELETE FROM Transaccion WHERE IDTransaccion = @IDTransaccion";

            // Crear y abrir la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Crear y ejecutar el comando SQL para eliminar el proveedor
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Pasar el parámetro @IdProveedor al comando SQL
                    command.Parameters.AddWithValue("@IDTransaccion", IDTransaccion);

                    // Ejecutar el comando SQL
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
