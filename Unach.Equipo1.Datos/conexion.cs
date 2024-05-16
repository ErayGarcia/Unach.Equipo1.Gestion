using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unach.Equipo1.Datos
{
    public class conexion
    {
        public static string ObtenerCadenaConexion()
        {
            return "Data Source=Pc-Garpi\\SQLEXPRESS;Initial Catalog=Defin;Integrated Security=True";
        }
    }
}
