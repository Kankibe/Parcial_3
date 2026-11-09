using System;
using MySqlConnector;

namespace Parte3_Login
{
    public class LoginServicio
    {

        public bool Registrar(string nombreCompleto, string usuario, string clave)
        {

            string hash = BCrypt.Net.BCrypt.HashPassword(clave);

            try
            {
                using (MySqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();

                    string sql = "INSERT INTO Usuarios (NombreUsuario, NombreCompleto, PasswordHash) " +
                                 "VALUES (@u, @n, @h)";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@u", usuario);
                    cmd.Parameters.AddWithValue("@n", nombreCompleto);
                    cmd.Parameters.AddWithValue("@h", hash);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        // INICIAR SESIÓN: verifica con BCrypt.Verify //
        public string IniciarSesion(string usuario, string clave)
        {
            string hashGuardado = null;
            string nombreCompleto = null;

            using (MySqlConnection con = Conexion.ObtenerConexion())
            {
                con.Open();

              //  usuario en la BD
                string sql = "SELECT PasswordHash, NombreCompleto FROM Usuarios WHERE NombreUsuario = @u";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@u", usuario);

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        hashGuardado = dr.GetString(0);
                        nombreCompleto = dr.GetString(1);
                    }
                }

                // verivficar la contraseña original contra el hash
                bool exitoso = false;
                if (hashGuardado != null)
                {
                    exitoso = BCrypt.Net.BCrypt.Verify(clave, hashGuardado);
                }

                // guardar el intento (exitoso o fallido)
                GuardarIntento(con, usuario, exitoso);

                if (exitoso)
                    return nombreCompleto;
                else
                    return null;
            }
        }

        //GUARDAR INTENTO: para el análisis posterior 
        private void GuardarIntento(MySqlConnection con, string usuario, bool exitoso)
        {
            string sql = "INSERT INTO IntentosLogin (NombreUsuario, Exitoso, Fecha) " +
                         "VALUES (@u, @e, NOW())";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@u", usuario);
            cmd.Parameters.AddWithValue("@e", exitoso);
            cmd.ExecuteNonQuery();
        }
    }
}