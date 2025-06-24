using System;

namespace MiAreaVol.Models
{
    public class Circulo
    {
        public int Id { get; set; }
        public double Radio { get; set; }
        public string TipoFigura { get; set; } = "circulo";
        public string Nombre { get; set; } = string.Empty;
        public double Circunferencia => 2 * Math.PI * Radio;
        public double Area => Math.PI * Math.Pow(Radio, 2);
    }
} 