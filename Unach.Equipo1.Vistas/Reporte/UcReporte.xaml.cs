using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

namespace Unach.Equipo1.Vistas.Reporte
{
    /// <summary>
    /// Lógica de interacción para UcReporte.xaml
    /// </summary>
    public partial class UcReporte : UserControl
    {
        public UcReporte()
        {
            InitializeComponent();
            CargarDatosClientes();
           
        }

        private void dgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgClientes_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
        private void CargarDatosClientes()
        {
            try
            {
                string connectionString = conexion.ObtenerCadenaConexion();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT IdCliente, Nombre, Direccion, NumeroTelefono, CorreoElectronico, TipoClienteID, UsuarioID, ApellidoPaterno, ApellidoMaterno FROM Cliente";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    dgClientes.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de clientes: " + ex.Message);
            }
        }

        private void btnDescarga_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedItem != null)
            {
                DataRowView cliente = dgClientes.SelectedItem as DataRowView;
                string idCliente = cliente["IdCliente"].ToString();
                GenerarReporteClientePDF(idCliente);
            }
            else
            {
                MessageBox.Show("Selecciona un cliente antes de descargar el PDF.");
            }
        }

        private void GenerarReporteClientePDF(string idCliente)
        {
            try
            {
                // Lógica para generar el PDF
                Document doc = new Document();

                // Usar SaveFileDialog para permitir que el usuario seleccione la ubicación y el nombre del archivo
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
                saveFileDialog.Title = "Guardar Reporte de Cliente";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
                    doc.Open();

                    // Agregar imagen en la parte superior izquierda
                    // Si tienes una imagen de tu empresa o algo relacionado, puedes agregarla aquí

                    // Título centrado
                    iTextSharp.text.Paragraph titulo = new iTextSharp.text.Paragraph("Reporte de Cliente", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD));
                    titulo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(titulo);

                    // Información adicional sobre el reporte
                    iTextSharp.text.Paragraph info = new iTextSharp.text.Paragraph("Este reporte contiene información detallada sobre el cliente seleccionado.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10));
                    info.Alignment = Element.ALIGN_CENTER;
                    doc.Add(info);

                    // Agregar contenido al PDF
                    doc.Add(new iTextSharp.text.Paragraph("\nInformación del cliente:", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD)));

                    // Obtener la información del cliente seleccionado
                    DataRowView cliente = dgClientes.SelectedItem as DataRowView;
                    string nombre = cliente["Nombre"].ToString();
                    string direccion = cliente["Direccion"].ToString();
                    string numeroTelefono = cliente["NumeroTelefono"].ToString();
                    string correoElectronico = cliente["CorreoElectronico"].ToString();
                    string apellidoPaterno = cliente["ApellidoPaterno"].ToString();
                    string apellidoMaterno = cliente["ApellidoMaterno"].ToString();

                    // Agregar información del cliente
                    doc.Add(new iTextSharp.text.Paragraph("\nNombre: " + nombre, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));
                    doc.Add(new iTextSharp.text.Paragraph("Dirección: " + direccion, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));
                    doc.Add(new iTextSharp.text.Paragraph("Número de Teléfono: " + numeroTelefono, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));
                    doc.Add(new iTextSharp.text.Paragraph("Correo Electrónico: " + correoElectronico, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));
                    doc.Add(new iTextSharp.text.Paragraph("Apellido Paterno: " + apellidoPaterno, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));
                    doc.Add(new iTextSharp.text.Paragraph("Apellido Materno: " + apellidoMaterno, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));

                    // Agregar línea separadora
                    doc.Add(new iTextSharp.text.Paragraph("\n", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10)));

                    // Aquí puedes agregar más detalles sobre el cliente si lo deseas

                    doc.Close();

                    MessageBox.Show("PDF generado correctamente y guardado en: " + filePath);
                }
                else
                {
                    MessageBox.Show("Se canceló la generación del PDF.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message);
            }



        }
    }
}
