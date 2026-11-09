using MySqlConnector;

namespace Parte3_Login
{
    public class Conexion
    {
        private static string cadenaConexion =
            "Server=localhost;Port=3306;Database=LoginBCrypt;Uid=root;Pwd= ------- ;";

        public static MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }
}
