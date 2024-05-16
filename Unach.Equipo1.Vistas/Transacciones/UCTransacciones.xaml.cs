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

namespace Unach.Equipo1.Vistas.Transacciones
{
    /// <summary>
    /// Lógica de interacción para UCTransacciones.xaml
    /// </summary>
    public partial class UCTransacciones : UserControl
    {
        Transferencias transferencias = new Transferencias();

        public UCTransacciones()
        {
            InitializeComponent();
            transferencias.LlenarComboBoxCategoria(comboBoxCategoria);
            transferencias.LlenarComboBoxProducto(comboBoxProducto);
        }
        public UCTransacciones(int idTransaccion, string Fecha, string Tipo, string Monto, string Descripcion) : this()
        {
            this.idTransaccion = idTransaccion;
            datePickerFecha.SelectedDate = DateTime.Parse(Fecha);
            txtTipo.Text = Tipo;
            txtMonto.Text = Monto;
            txtDescripcion.Text = Descripcion;
        }
        private void btnVisualizar_Click(object sender, RoutedEventArgs e)
        {
            InfoTransaccion watch = new InfoTransaccion();
            Fino.Children.Clear();
            Fino.Children.Add(watch);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            txtDescripcion.Text = "";
            txtMonto.Text = "";
            txtTipo.Text = "";
            datePickerFecha.SelectedDate = null;

        }

        private int idTransaccion;

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            DateTime? fecha = datePickerFecha.SelectedDate;
            string tipo = txtTipo.Text;
            decimal monto;
            string descripcion = txtDescripcion.Text;
            int categoriaId;
            int productoId;

            if (fecha.HasValue && !string.IsNullOrEmpty(tipo) && decimal.TryParse(txtMonto.Text, out monto) && int.TryParse(((KeyValuePair<int, string>)comboBoxCategoria.SelectedItem).Key.ToString(), out categoriaId) && int.TryParse(((KeyValuePair<int, string>)comboBoxProducto.SelectedItem).Key.ToString(), out productoId))
            {
                // Instanciar la clase GestorTransferencias y llamar al método GuardarTransferencia
                Transferencias gestor = new Transferencias();
                gestor.GuardarTransferencia(fecha.Value, tipo, monto, descripcion, categoriaId, productoId);

                // Mostrar un mensaje de éxito o realizar otras acciones necesarias
                MessageBox.Show("Transferencia guardada correctamente.");
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos correctamente.");
            }

        }

        private void comboBoxCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtDescripcion_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void comboBoxProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = datePickerFecha.SelectedDate;
            string nuevafecha = selectedDate != null ? selectedDate.Value.ToString("yyyy-MM-dd") : null;
            string nuevatipo = txtTipo.Text;
            string nuevoMonto = txtMonto.Text;
            string nuevoDescripcion = txtDescripcion.Text;

            string connectionString = conexion.ObtenerCadenaConexion();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "UPDATE Transaccion SET Fecha = @Fecha, Tipo = @Tipo, Monto = @Monto, Descripcion = @Descripcion WHERE IDTransaccion = @IDTransaccion";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Fecha", nuevafecha);
                    command.Parameters.AddWithValue("@Tipo", nuevatipo);
                    command.Parameters.AddWithValue("@Monto", nuevoMonto);
                    command.Parameters.AddWithValue("@Descripcion", nuevoDescripcion);
                    command.Parameters.AddWithValue("@IDTransaccion", idTransaccion);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Proveedor modificado correctamente.", "Modificar Proveedor", MessageBoxButton.OK, MessageBoxImage.Information);

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
    }
}
