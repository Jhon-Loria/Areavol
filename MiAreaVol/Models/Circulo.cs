using System;

namespace MiAreaVol.Models
{
    public class Circulo
    {
        public int Id { get; set; }
        public double Radio { get; set; }
        public double Area => System.Math.PI * Radio * Radio;
    }
} 