using System;
using System.Collections.Generic;
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
using Unach.Equipo1.Logica;
using Unach.Equipo1.Datos;
using System.Data.SqlClient;
using System.Data;

namespace Unach.Equipo1.Vistas.Proveedor
{
    /// <summary>
    /// Lógica de interacción para UCProveedor.xaml
    /// </summary>
    public partial class UCProveedor : UserControl
    {
        private AGProveedor Proveedor;

        private int idProveedor;
        private string nombreProveedor;
        private string direccionProveedor;
        private string numeroTelefonoProveedor;
        private string correoElectronicoProveedor;

        public UCProveedor()
        {
            InitializeComponent();
           
        }
        public UCProveedor(int idProveedor, string nombreProveedor, string direccionProveedor, string numeroTelefonoProveedor, string correoElectronicoProveedor) : this()
        {
            this.idProveedor = idProveedor;
            txtNombre.Text = nombreProveedor;
            txtDireccion.Text = direccionProveedor;
            txtNumeroTelefono.Text = numeroTelefonoProveedor;
            txtCorreoElectronico.Text = correoElectronicoProveedor;
        }

        public UCProveedor(string nombreProveedor, string direccionProveedor, string numeroTelefonoProveedor, string correoElectronicoProveedor)
        {
            this.nombreProveedor = nombreProveedor;
            this.direccionProveedor = direccionProveedor;
            this.numeroTelefonoProveedor = numeroTelefonoProveedor;
            this.correoElectronicoProveedor = correoElectronicoProveedor;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los valores de los TextBox
            string nombre = txtNombre.Text;
            string direccion = txtDireccion.Text;
            string numeroTelefono = txtNumeroTelefono.Text;
            string correoElectronico = txtCorreoElectronico.Text;

            // Verificar si algún campo está vacío
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(direccion) ||
                string.IsNullOrWhiteSpace(numeroTelefono) || string.IsNullOrWhiteSpace(correoElectronico))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            // Verificar si el número de teléfono tiene 10 dígitos
            if (numeroTelefono.Length != 10)
            {
                MessageBox.Show("El número de teléfono debe tener exactamente 10 dígitos.");
                return;
            }

            try
            {
                // Mostrar un mensaje de confirmación
                MessageBox.Show("Proveedor agregado correctamente.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar proveedor: " + ex.Message);
            }
            AGProveedor aGProveedor = new AGProveedor();

            aGProveedor.AgregarProveedor(nombre, direccion, numeroTelefono, correoElectronico);
        }

        private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtNumeroTelefono.Text = "";
            txtCorreoElectronico.Text = "";
        }

        private void Visualizar_Click(object sender, RoutedEventArgs e)
        {
            UCProveedorInfo info = new UCProveedorInfo();
            vizu.Children.Clear();
            vizu.Children.Add(info);
        }

        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            string nuevoNombreProveedor = txtNombre.Text;
            string nuevaDireccionProveedor = txtDireccion.Text;
            string nuevoNumeroTelefonoProveedor = txtNumeroTelefono.Text;
            string nuevoCorreoElectronicoProveedor = txtCorreoElectronico.Text;

            string connectionString = conexion.ObtenerCadenaConexion();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE Proveedor SET NombreProveedor = @Nombre, DireccionProveedor = @Direccion, NumeroTelefonoProveedor = @NumeroTelefono, CorreoElectronicoProveedor = @CorreoElectronico WHERE IDProveedor = @IDProveedor";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Nombre", nuevoNombreProveedor);
                    command.Parameters.AddWithValue("@Direccion", nuevaDireccionProveedor);
                    command.Parameters.AddWithValue("@NumeroTelefono", nuevoNumeroTelefonoProveedor);
                    command.Parameters.AddWithValue("@CorreoElectronico", nuevoCorreoElectronicoProveedor);
                    command.Parameters.AddWithValue("@IDProveedor", idProveedor);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Proveedor modificado correctamente.", "Modificar Proveedor", MessageBoxButton.OK, MessageBoxImage.Information);

                        CloseCurrentTab();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo modificar el proveedor.", "Modificar Proveedor", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar el proveedor: " + ex.Message, "Modificar Proveedor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private T FindParent<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(dependencyObject);
            if (parent == null)
                return null;
            T parentControl = parent as T;
            return parentControl ?? FindParent<T>(parent);
        }
        private void UCProveedor_Closed(object sender, EventArgs e)
        {
            // Obtener el TabControl que contiene esta pestaña
            var parentTabControl = FindParent<TabControl>(this);

            if (parentTabControl != null)
            {
                // Remover la pestaña actual del TabControl
                parentTabControl.Items.Remove(this);
            }
        }
        private void CloseCurrentTab()
        {
            var parentTabControl = FindParent<TabControl>(this);
            if (parentTabControl != null)
            {
                foreach (TabItem tabItem in parentTabControl.Items)
                {
                    if (tabItem.Content == this)
                    {
                        parentTabControl.Items.Remove(tabItem);
                        break;
                    }
                }
            }
        }

    }
}
