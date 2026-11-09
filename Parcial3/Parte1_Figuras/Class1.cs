using Parte1_Figuras;
using System;

class Program
{
    static void Main(string[] args)
    {
        Figura[] figuras = new Figura[3];

        figuras[0] = new Rectangulo(5, 3);
        figuras[1] = new Cuadrado(4);
        figuras[2] = new Triangulo(6, 4, 5, 5);

        Console.WriteLine(" FIGURAS GEOMÉTRICAS \n");


        for (int i = 0; i < figuras.Length; i++)
        {
            Console.WriteLine("Clase:     " + figuras[i].GetType().Name);
            Console.WriteLine("Área:      " + figuras[i].CalcularArea());
            Console.WriteLine("Perímetro: " + figuras[i].CalcularPerimetro());
            Console.WriteLine("-----------------------------");
        }

        Console.ReadKey();
    }
}