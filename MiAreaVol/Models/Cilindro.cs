using System;

namespace MiAreaVol.Models
{
    public class Cilindro
    {
        public int Id { get; set; }
        public double Radio { get; set; }
        public double Altura { get; set; }
        public double Area => 2 * System.Math.PI * Radio * (Radio + Altura);
        public double Volumen => System.Math.PI * Radio * Radio * Altura;
    }
} 