using System;
using System.Text;

namespace Parte3_Login
{
    // Esta clase SOLO tiene el menú y lo que se ve en consola
    class Program
    {
        static LoginServicio servicio = new LoginServicio();

        static void Main(string[] args)
        {
            int opcion = 0;

            while (opcion != 3)
            {
                Console.WriteLine("\n=== LOGIN CON BCRYPT ===");
                Console.WriteLine("1. Registrar usuario");
                Console.WriteLine("2. Iniciar sesión");
                Console.WriteLine("3. Salir");
                Console.Write("Opción: ");
                int.TryParse(Console.ReadLine(), out opcion);

                if (opcion == 1) PantallaRegistro();
                else if (opcion == 2) PantallaLogin();
            }
        }

        static void PantallaRegistro()
        {
            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine();
            Console.Write("Usuario: ");
            string usuario = Console.ReadLine();
            Console.Write("Contraseña: ");
            string clave = LeerClave();

            bool registrado = servicio.Registrar(nombre, usuario, clave);

            if (registrado)
                Console.WriteLine("Usuario registrado. El hash ya quedó guardado en la BD.");
            else
                Console.WriteLine("No se pudo registrar (¿ese usuario ya existe?).");
        }

        static void PantallaLogin()
        {
            Console.Write("Usuario: ");
            string usuario = Console.ReadLine();
            Console.Write("Contraseña: ");
            string clave = LeerClave();

            string nombre = servicio.IniciarSesion(usuario, clave);

            if (nombre != null)
                Console.WriteLine("\nAcceso exitoso. Bienvenido, " + nombre + " (" + usuario + ")");
            else
                Console.WriteLine("\nUsuario o contraseña incorrectos.");
        }

        // Lee la contraseña mostrando asteriscos
        static string LeerClave()
        {
            StringBuilder clave = new StringBuilder();
            ConsoleKeyInfo tecla;

            while ((tecla = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (tecla.Key == ConsoleKey.Backspace && clave.Length > 0)
                {
                    clave.Remove(clave.Length - 1, 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(tecla.KeyChar))
                {
                    clave.Append(tecla.KeyChar);
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return clave.ToString();
        }
    }
}