namespace MiAreaVol.Models
{
    public class Rectangulo
    {
        public int Id { get; set; }
        public double Largo { get; set; }
        public double Ancho { get; set; }
        public double Area => Largo * Ancho;
    }
} 