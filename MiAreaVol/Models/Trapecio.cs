namespace MiAreaVol.Models
{
    public class Trapecio
    {
        public int Id { get; set; }
        public double BaseMayor { get; set; }
        public double BaseMenor { get; set; }
        public double Altura { get; set; }
        public double Area => ((BaseMayor + BaseMenor) * Altura) / 2;
    }
} 