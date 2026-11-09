using System;
using Newtonsoft.Json;   // Paso 2: importar

namespace Parte2_Json
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Paso 3: crear objeto con datos
            var persona = new
            {
                Nombre = "Cristell",
                Edad = 20,
                Carrera = "Ingeniería en Sistemas"
            };

            // Paso 4: convertir objeto a cadena JSON
            string json = JsonConvert.SerializeObject(persona);

            Console.WriteLine(json);
        }
    }
}
