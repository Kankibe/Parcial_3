using System;

namespace Parte1_Figuras
{
    public abstract class Figura
    {
        public abstract double CalcularArea();
        public abstract double CalcularPerimetro();
    }

    public class Rectangulo : Figura
    {
        private double baseR;
        private double altura;

        public Rectangulo(double baseR, double altura)
        {
            this.baseR = baseR;
            this.altura = altura;
        }

        public override double CalcularArea()
        {
            return baseR * altura;
        }

        public override double CalcularPerimetro()
        {
            return 2 * (baseR + altura);
        }
    }

    public class Cuadrado : Figura
    {
        private double lado;

        public Cuadrado(double lado)
        {
            this.lado = lado;
        }

        public override double CalcularArea()
        {
            return lado * lado;
        }

        public override double CalcularPerimetro()
        {
            return 4 * lado;
        }
    }

    public class Triangulo : Figura
    {
        private double baseT;
        private double altura;
        private double lado2;
        private double lado3;

        public Triangulo(double baseT, double altura, double lado2, double lado3)
        {
            this.baseT = baseT;
            this.altura = altura;
            this.lado2 = lado2;
            this.lado3 = lado3;
        }

        public override double CalcularArea()
        {
            return (baseT * altura) / 2;
        }

        public override double CalcularPerimetro()
        {
            return baseT + lado2 + lado3;
        }
    }
}
