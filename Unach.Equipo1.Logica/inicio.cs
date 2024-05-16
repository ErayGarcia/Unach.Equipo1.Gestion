using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Unach.Equipo1.Datos;

namespace Unach.Equipo1.Logica
{
    public class inicio
    {
        public string VerificarCredenciales(string correo, string contraseña)
        {
            string connectionString = conexion.ObtenerCadenaConexion();
            string query = "SELECT NombreUsuario FROM Usuario WHERE CorreoElectronico = @Correo AND Contraseña = @Contraseña";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Correo", correo);
                command.Parameters.AddWithValue("@Contraseña", contraseña);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string nombreUsuario = reader["NombreUsuario"].ToString();
                    reader.Close();
                    return nombreUsuario;
                }
                else
                {
                    reader.Close();
                    return null; // Credenciales incorrectas
                }
            }
        }

    }
}
