using System;

namespace MiAreaVol.Models
{
    public class Circulo
    {
        public int Id { get; set; }
        public double Radio { get; set; }
        public double Circunferencia => 2 * Math.PI * Radio;
        public double Area => Math.PI * Math.Pow(Radio, 2);
    }
} 