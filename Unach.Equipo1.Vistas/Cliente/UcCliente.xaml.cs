using System;
using System.Collections.Generic;
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
using Unach.Equipo1.Vistas;

namespace Unach.Equipo1.Vistas.Cliente
{
    /// <summary>
    /// Lógica de interacción para UcCliente.xaml
    /// </summary>
    public partial class UcCliente : UserControl
    {
        private ClienteMetodo ClienteMetodo;
        private string tipoCliente;
        private int idCliente;

        public UcCliente()
        {
            InitializeComponent();
            ClienteMetodo = new ClienteMetodo();

            // Llena el combobox de tipo de cliente al cargar el formulario
            ClienteMetodo.LlenarComboBoxTipoCliente(comboBoxTipoCliente);
        }

        public UcCliente(int idCliente, string Nombre, string Direccion, string NumeroTelefono, string Correoelectronico, string ApellidoPaterno, string ApellidoMaterno) : this()
        {
            this.idCliente = idCliente;
            txtNombre.Text = Nombre;
            txtApellidoPaterno.Text = ApellidoPaterno;
            txtApellidoMaterno.Text = ApellidoMaterno;
            txtDireccion.Text = Direccion;
            txtNumeroTelefono.Text = NumeroTelefono;
            txtCorreoElectronico.Text = Correoelectronico;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtNombre.Text; // Suponiendo que txtNombre es el TextBox donde el usuario ingresa el nombre del cliente
            string direccion = txtDireccion.Text; // Suponiendo que txtDireccion es el TextBox donde el usuario ingresa la dirección del cliente
            string numeroTelefono = txtNumeroTelefono.Text; // Suponiendo que txtTelefono es el TextBox donde el usuario ingresa el número de teléfono del cliente
            string correoElectronico = txtCorreoElectronico.Text; // Suponiendo que txtCorreo es el TextBox donde el usuario ingresa el correo electrónico del cliente
            string apellidoPaterno = txtApellidoPaterno.Text; // Suponiendo que txtApellidoPaterno es el TextBox donde el usuario ingresa el apellido paterno del cliente
            string apellidoMaterno = txtApellidoMaterno.Text; // Suponiendo que txtApellidoMaterno es el TextBox donde el usuario ingresa el apellido materno del cliente

            // Obtener el ID del tipo de cliente seleccionado en el ComboBox
            int tipoClienteID = 0;

            // Verificar si hay un valor seleccionado en el ComboBox
            if (comboBoxTipoCliente.SelectedItem != null)
            {
                // Obtener el elemento seleccionado del ComboBox
                KeyValuePair<int, string> tipoSeleccionado = (KeyValuePair<int, string>)comboBoxTipoCliente.SelectedItem;

                // Obtener el IDTipo del objeto seleccionado
                tipoClienteID = tipoSeleccionado.Key;
            }
            else
            {
                // Manejar el caso donde no hay un valor seleccionado
                MessageBox.Show("Por favor, seleccione un tipo de cliente.");
                return;
            }

            // Instanciar un objeto de la clase ClienteMetodo
            ClienteMetodo clienteMetodo = new ClienteMetodo();

            // Llamar al método AgregarCliente con los datos del cliente y el ID del tipo de cliente
            clienteMetodo.AgregarCliente(nombre, direccion, numeroTelefono, correoElectronico, apellidoPaterno, apellidoMaterno, tipoClienteID);
        }

        private void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtDireccion.Clear();
            txtApellidoMaterno.Clear();
            txtApellidoPaterno.Clear();
            txtCorreoElectronico.Clear();
            txtNumeroTelefono.Clear();
            txtNombre.Clear();
        }

        private void btnVisualizar_Click(object sender, RoutedEventArgs e)
        {
            UcClienteInfo info = new UcClienteInfo();
            info.Show();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            string nuevoNombre = txtNombre.Text;
            string nuevaDireccion = txtDireccion.Text;
            string nuevoNumeroTelefono = txtNumeroTelefono.Text;
            string nuevoCorreoElectronico = txtCorreoElectronico.Text;
            string nuevoApellidoPaterno = txtApellidoPaterno.Text;
            string nuevoApellidoMaterno = txtApellidoMaterno.Text;

            string connectionString = conexion.ObtenerCadenaConexion();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE Cliente SET Nombre = @Nombre, Direccion = @Direccion, NumeroTelefono = @NumeroTelefono, CorreoElectronico = @CorreoElectronico, ApellidoPaterno = @ApellidoPaterno , ApellidoMaterno = @ApellidoMaterno WHERE IDCliente = @IDCliente";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nuevoNombre);
                    command.Parameters.AddWithValue("@Direccion", nuevaDireccion);
                    command.Parameters.AddWithValue("@NumeroTelefono", nuevoNumeroTelefono);
                    command.Parameters.AddWithValue("@CorreoElectronico", nuevoCorreoElectronico);
                    command.Parameters.AddWithValue("@ApellidoPaterno", nuevoApellidoPaterno);
                    command.Parameters.AddWithValue("@ApellidoMaterno", nuevoApellidoMaterno);
                    command.Parameters.AddWithValue("@IDCliente", idCliente);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cliente modificado correctamente.", "Modificar CLiente", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar el proveedor.", "Modificar Cliente", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar el proveedor: " + ex.Message, "Modificar Proveedor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
