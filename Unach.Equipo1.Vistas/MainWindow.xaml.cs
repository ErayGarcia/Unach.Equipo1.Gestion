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
using Unach.Equipo1.Vistas.Sesion;

namespace Unach.Equipo1.Vistas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UsuarioMetodo usuarioMetodo;
        public MainWindow()
        {
            InitializeComponent();
            usuarioMetodo = new UsuarioMetodo();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombre = nombreTextBox.Text;
                string correo = correoTextBox.Text;
                string contraseña = passwordBox.Password;

                // Verificar si algún campo está vacío
                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña))
                {
                    MessageBox.Show("Por favor, complete todos los campos.");
                    return; // Salir del método si hay campos vacíos
                }

                // Verificar si el usuario ya existe en la base de datos
                if (UsuarioMetodo.UsuarioExiste(nombre, correo))
                {
                    MessageBox.Show("Ya existe un usuario con este nombre de usuario o correo electrónico.");
                    return; // Salir del método si el usuario ya existe
                }

                // Si el usuario no existe y no hay campos vacíos, se guarda en la base de datos
                usuarioMetodo.GuardarUsuario(nombre, correo, contraseña);

                MessageBox.Show("Usuario registrado correctamente.");

                // Redirigir a la página de Login.xaml
                Login loginPage = new Login();
                this.Close(); // Cierra la ventana actual si es necesario
                loginPage.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar usuario: " + ex.Message);
            }

        }

        private void SesionButton_Click(object sender, RoutedEventArgs e)
        {
            login loginPage = new Login();
            this.Close(); // Cierra la ventana actual si es necesario
            loginPage.Show();
        }
    }
}
