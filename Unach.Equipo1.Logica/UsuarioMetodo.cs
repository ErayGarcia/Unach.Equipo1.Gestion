using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unach.Equipo1.Datos;

namespace Unach.Equipo1.Logica
{
    public class UsuarioMetodo
    {

        public void GuardarUsuario(string nombre, string correo, string contraseña)
        {
            string connectionString = conexion.ObtenerCadenaConexion();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Usuario (NombreUsuario, CorreoElectronico, Contraseña) VALUES (@NombreUsuario, @CorreoElectronico, @Contraseña)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreUsuario", nombre);
                command.Parameters.AddWithValue("@CorreoElectronico", correo);
                command.Parameters.AddWithValue("@Contraseña", contraseña);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public static bool UsuarioExiste(string nombreUsuario, string correoElectronico)
        {
            // Aquí implementarás la lógica para verificar si el usuario ya existe en la base de datos
            // Puedes utilizar una consulta SQL para buscar en la tabla de usuarios
            // Retorna true si el usuario ya existe, false si no existe

            // Cadena de conexión a tu base de datos SQL Server
            string connectionString = conexion.ObtenerCadenaConexion();

            // Consulta SQL para verificar si el usuario ya existe
            string query = "SELECT COUNT(*) FROM Usuario WHERE NombreUsuario = @nombreUsuario OR CorreoElectronico = @correoElectronico";

            // Utilizamos un bloque using para asegurarnos de que los recursos se liberen correctamente
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrimos la conexión
                connection.Open();

                // Creamos un SqlCommand con la consulta SQL y los parámetros necesarios
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Añadimos los parámetros para el nombre de usuario y el correo electrónico
                    command.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    command.Parameters.AddWithValue("@correoElectronico", correoElectronico);

                    // Ejecutamos la consulta y obtenemos el número de filas con ese nombre de usuario o correo electrónico
                    int count = (int)command.ExecuteScalar();

                    // Si count es mayor que 0, significa que el usuario ya existe
                    return count > 0;
                }
            }
        }
    }
}
