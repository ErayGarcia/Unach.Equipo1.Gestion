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

namespace Unach.Equipo1.Vistas.Index
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : UserControl
    {
       

        public Principal()
        {
            InitializeComponent();
           
        }
        
        private void btnAreas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            Cliente.UcCliente _cliente = new Cliente.UcCliente();
            Informacion.Children.Clear();
            Informacion.Children.Add(_cliente);
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnProveedor_Click(object sender, RoutedEventArgs e)
        {
            Proveedor.UCProveedor prove = new Proveedor.UCProveedor();
            Informacion.Children.Clear();
            Informacion.Children.Add(prove);
        }

        private void btnTransaccion_Click(object sender, RoutedEventArgs e)
        {
            Transacciones.UCTransacciones tra = new Transacciones.UCTransacciones();
            Informacion.Children.Clear();
            Informacion.Children.Add(tra);
        }

        private void btnProducto_Click(object sender, RoutedEventArgs e)
        {
            Producto.UCProducto pro = new Producto.UCProducto();
            Informacion.Children.Clear();
            Informacion.Children.Add(pro);
        }

        private void btnReportes_Click(object sender, RoutedEventArgs e)
        {
            Reporte.UcReporte report = new Reporte.UcReporte();
            Informacion.Children.Clear();
            Informacion.Children.Add(report);
        }

        private void btnReportes_Click_1(object sender, RoutedEventArgs e)
        {
            Reporte.UcReporte report = new Reporte.UcReporte();
            Informacion.Children.Clear();
            Informacion.Children.Add(report);
        }

        private void btnEstadoCuenta_Click(object sender, RoutedEventArgs e)
        {
            EstadoCT.UCEstado cuenta = new EstadoCT.UCEstado();
            Informacion.Children.Clear();
            Informacion.Children.Add(cuenta);
        }
    }
}
