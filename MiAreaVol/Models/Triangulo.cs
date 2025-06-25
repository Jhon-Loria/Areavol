namespace MiAreaVol.Models
{
    public class Triangulo
    {
        public int Id { get; set; }
        public double Base { get; set; }
        public double Altura { get; set; }
        public double Area => (Base * Altura) / 2;
    }
} 