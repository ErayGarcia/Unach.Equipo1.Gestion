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
using System.Windows.Shapes;
using Unach.Equipo1.Logica;
using Unach.Equipo1.Datos;
using Unach.Equipo1.Vistas.Index;

namespace Unach.Equipo1.Vistas.Sesion
{
    /// <summary>
    /// Lógica de interacción para login.xaml
    /// </summary>
    public partial class login : Window
    {
        private inicio inicio;
        public login()
        {
            InitializeComponent();
            inicio = new inicio();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el correo y la contraseña de los campos de texto
            string correo = correoTextBox.Text;
            string contraseña = passwordBox.Password;

            // Verificar si los campos están vacíos
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, ingrese un correo y una contraseña.");
            }
            else
            {
                // Verificar credenciales solamente si los campos no están vacíos
                inicio inicio = new inicio(); // Suponiendo que ya tienes la clase inicio
                string nombreUsuario = inicio.VerificarCredenciales(correo, contraseña);

                if (!string.IsNullOrEmpty(nombreUsuario))
                {
                    // Las credenciales son válidas, mostrar la ventana principal
                    Principal principalControl = new Principal();
                    principalControl.usuarioTextBlock.Text = "Bienvenido, " + nombreUsuario; // Concatenar mensaje con el nombre de usuario
                    this.Content = principalControl;
                }
                else
                {
                    // Credenciales inválidas, mostrar mensaje de error
                    MessageBox.Show("Correo o contraseña incorrectos.");
                }
            }

        }

    }
}
