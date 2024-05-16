using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Unach.Equipo1.Datos;
using Unach.Equipo1.Logica;

namespace Unach.Equipo1.Logica
{
    public class ClienteMetodo
    {
       

        // Método para agregar un nuevo cliente a la base de datos
        public void AgregarCliente(string nombre, string direccion, string numeroTelefono, string correoElectronico, string apellidoPaterno, string apellidoMaterno, int tipoClienteID)
        {
            // Cadena de conexión a la base de datos
            string connectionString = conexion.ObtenerCadenaConexion();

            try
            {
                // Consulta SQL para insertar un nuevo cliente
                string sql = "INSERT INTO Cliente (Nombre, Direccion, NumeroTelefono, CorreoElectronico, ApellidoPaterno, ApellidoMaterno, TipoClienteID) " +
                             "VALUES (@Nombre, @Direccion, @NumeroTelefono, @CorreoElectronico, @ApellidoPaterno, @ApellidoMaterno, @TipoClienteID)";

                // Establece la conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Crea un objeto SqlCommand para ejecutar la consulta SQL
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Asigna los parámetros de la consulta con los valores proporcionados
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Direccion", direccion);
                        command.Parameters.AddWithValue("@NumeroTelefono", numeroTelefono);
                        command.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);
                        command.Parameters.AddWithValue("@ApellidoPaterno", apellidoPaterno);
                        command.Parameters.AddWithValue("@ApellidoMaterno", apellidoMaterno);
                        command.Parameters.AddWithValue("@TipoClienteID", tipoClienteID);

                        // Abre la conexión a la base de datos
                        connection.Open();

                        // Ejecuta la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cliente agregado exitosamente.");
                        }
                        else
                        {
                            MessageBox.Show("Error al agregar cliente. No se pudo insertar en la base de datos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar agregar cliente: " + ex.Message);
            }
        }

        public void LlenarComboBoxTipoCliente(ComboBox comboBoxTipoCliente)
        {
            // Cadena de conexión a tu base de datos
            string connectionString = conexion.ObtenerCadenaConexion();

            // Consulta SQL para obtener los tipos de cliente con sus IDs y nombres
            string sql = "SELECT IDTipo, TipoCliente FROM TipoCliente";

            // Lista para almacenar los tipos de cliente (IDTipo como clave y TipoCliente como valor)
            List<KeyValuePair<int, string>> tiposClientes = new List<KeyValuePair<int, string>>();

            // Establecer conexión a la base de datos y ejecutar la consulta
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Leer los resultados y agregarlos a la lista
                while (reader.Read())
                {
                    int idTipo = (int)reader["IDTipo"];
                    string nombreTipo = reader["TipoCliente"].ToString();
                    tiposClientes.Add(new KeyValuePair<int, string>(idTipo, nombreTipo));
                }
            }

            // Asignar el origen de datos al ComboBox
            comboBoxTipoCliente.ItemsSource = tiposClientes;

            // Establecer la propiedad DisplayMemberPath para mostrar el valor (nombre del tipo de cliente) en el ComboBox
            comboBoxTipoCliente.DisplayMemberPath = "Value";

            // Establecer la propiedad SelectedValuePath para utilizar la clave (IDTipo) como valor seleccionado
            comboBoxTipoCliente.SelectedValuePath = "Key";

        }
        public void EliminarCliente(int IDCliente)
        {
            string connectionString = conexion.ObtenerCadenaConexion();
            // Crear la consulta SQL para eliminar el proveedor
            string query = "DELETE FROM Cliente WHERE IDCliente = @IDCliente";

            // Crear y abrir la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Crear y ejecutar el comando SQL para eliminar el proveedor
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Pasar el parámetro @IdProveedor al comando SQL
                    command.Parameters.AddWithValue("@IDCliente", IDCliente);

                    // Ejecutar el comando SQL
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

