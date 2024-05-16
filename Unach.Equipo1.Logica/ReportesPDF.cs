using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unach.Equipo1.Datos;
using Unach.Equipo1.Logica;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using System.IO;

namespace Unach.Equipo1.Logica
{
    public class ReportesPDF
    {
        public void GenerarReporteClientePDF(string idCliente)
        {
            try
            {
                string connectionString = conexion.ObtenerCadenaConexion();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT IdCliente, Nombre, ApellidoPaterno, ApellidoMaterno, Direccion, NumeroTelefono, CorreoElectronico, TipoCliente, UsuarioId 
                                    FROM Cliente 
                                    WHERE IdCliente = @IdCliente";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdCliente", idCliente);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Document doc = new Document();
                        string path = $"{reader["Nombre"].ToString()}_{reader["ApellidoPaterno"].ToString()}_Reporte.pdf";
                        PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
                        doc.Open();

                        Paragraph paragraph = new Paragraph();
                        paragraph.Add($"ID Cliente: {reader["IdCliente"].ToString()}\n");
                        paragraph.Add($"Nombre: {reader["Nombre"].ToString()}\n");
                        paragraph.Add($"Apellido Paterno: {reader["ApellidoPaterno"].ToString()}\n");
                        paragraph.Add($"Apellido Materno: {reader["ApellidoMaterno"].ToString()}\n");
                        paragraph.Add($"Dirección: {reader["Direccion"].ToString()}\n");
                        paragraph.Add($"Número de Teléfono: {reader["NumeroTelefono"].ToString()}\n");
                        paragraph.Add($"Correo Electrónico: {reader["CorreoElectronico"].ToString()}\n");
                        paragraph.Add($"Tipo de Cliente: {reader["TipoCliente"].ToString()}\n");
                        paragraph.Add($"Usuario ID: {reader["UsuarioId"].ToString()}\n");

                        doc.Add(paragraph);
                        doc.Close();
                        Console.WriteLine($"PDF generado exitosamente para {reader["Nombre"].ToString()} {reader["ApellidoPaterno"].ToString()} en {path}");
                    }
                    else
                    {
                        Console.WriteLine($"No se encontró cliente con el ID: {idCliente}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al generar PDF: " + ex.Message);
            }
        }
    }
}
