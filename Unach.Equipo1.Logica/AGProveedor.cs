using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unach.Equipo1.Datos;
using Unach.Equipo1.Logica;

namespace Unach.Equipo1.Logica
{
    public class AGProveedor
    {
        public int IdProveedor { get; set; }

        public void AgregarProveedor(string nombre, string direccion, string numeroTelefono, string correoElectronico)
        {
            string connectionString = conexion.ObtenerCadenaConexion();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Proveedor (NombreProveedor, DireccionProveedor, NumeroTelefonoProveedor, CorreoElectronicoProveedor) " +
                               "VALUES (@Nombre, @Direccion, @NumeroTelefono, @CorreoElectronico)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Direccion", direccion);
                    command.Parameters.AddWithValue("@NumeroTelefono", numeroTelefono);
                    command.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al agregar proveedor: " + ex.Message);
                    }
                }
            }
        }
        public void EliminarProveedor(int IDProveedor)
        {
            string connectionString = conexion.ObtenerCadenaConexion();
            // Crear la consulta SQL para eliminar el proveedor
            string query = "DELETE FROM Proveedor WHERE IDProveedor = @IDProveedor";

            // Crear y abrir la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Crear y ejecutar el comando SQL para eliminar el proveedor
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Pasar el parámetro @IdProveedor al comando SQL
                    command.Parameters.AddWithValue("@IdProveedor", IDProveedor);

                    // Ejecutar el comando SQL
                    command.ExecuteNonQuery();
                }
            }
        }
        public bool Buscar(string nombre)
        {
            // Obtener la cadena de conexión
            string connectionString = conexion.ObtenerCadenaConexion();

            // Crear la consulta SQL para buscar proveedores por nombre
            string query = "SELECT COUNT(*) FROM Proveedor WHERE NombreProveedor LIKE @NombreProveedor";

            // Crear y abrir la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Crear y ejecutar el comando SQL para buscar proveedores por nombre
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Pasar el parámetro @NombreProveedor al comando SQL
                    command.Parameters.AddWithValue("@NombreProveedor", "%" + nombre + "%");

                    // Ejecutar el comando SQL y obtener el número de filas encontradas
                    int rowCount = (int)command.ExecuteScalar();

                    // Devolver true si se encontraron resultados, false en caso contrario
                    return rowCount > 0;
                }
            }
        }
        private void Actualizar(int idProveedor, string nuevoNombre, string nuevaDireccion, string nuevoNumeroTelefono, string nuevoCorreoElectronico)
        {
            // Aquí deberías escribir tu lógica para actualizar el registro en la base de datos
            // Por ejemplo, podrías usar una consulta SQL para actualizar los valores en la tabla de proveedores
            string connectionString = conexion.ObtenerCadenaConexion();

            string query = "UPDATE Proveedor SET NombreProveedor = @NombreProveedor, DireccionProveedor = @DireccionProveedor, NumeroTelefonoProveedor = @NumeroTelefonoProveedor, CorreoElectronicoProveedor = @CorreoElectronicoProveedor WHERE IDProveedor = @IDProveedor";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreProveedor", nuevoNombre);
                    command.Parameters.AddWithValue("@DireccionProveedor", nuevaDireccion);
                    command.Parameters.AddWithValue("@NumeroTelefonoProveedor", nuevoNumeroTelefono);
                    command.Parameters.AddWithValue("@CorreoElectronicoProveedor", nuevoCorreoElectronico);
                    command.Parameters.AddWithValue("@IDProveedor", idProveedor);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void ActualizarProveedor(int idProveedor, string nuevoNombre, string nuevaDireccion, string nuevoTelefono, string nuevoCorreo)
        {
            string connectionString = conexion.ObtenerCadenaConexion();
            // Sentencia SQL para actualizar el proveedor
            string query = "UPDATE Proveedor SET NombreProveedor = @NuevoNombre, DireccionProveedor = @NuevaDireccion, NumeroTelefonoProveedor = @NuevoTelefono, CorreoElectronicoProveedor = @NuevoCorreo WHERE IdProveedor = @IDProveedor";

            // Crear una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();

                // Crear un comando SQL con la consulta y la conexión
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Agregar parámetros a la consulta
                    command.Parameters.AddWithValue("@NuevoNombre", nuevoNombre);
                    command.Parameters.AddWithValue("@NuevaDireccion", nuevaDireccion);
                    command.Parameters.AddWithValue("@NuevoTelefono", nuevoTelefono);
                    command.Parameters.AddWithValue("@NuevoCorreo", nuevoCorreo);
                    command.Parameters.AddWithValue("@IDProveedor", idProveedor);

                    try
                    {
                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        // Verificar si se realizaron cambios en la base de datos
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Proveedor actualizado correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("No se pudo actualizar el proveedor. El IDProveedor proporcionado no existe.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al actualizar el proveedor: " + ex.Message);
                    }
                }
            }
        }
    }
}
