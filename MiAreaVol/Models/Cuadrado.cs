namespace MiAreaVol.Models
{
    public class Cuadrado
    {
        public int Id { get; set; }
        public double Lado { get; set; }
        public double Area => Lado * Lado;
    }
} 