namespace MiAreaVol.Models;

public class CalculoResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string TipoFigura { get; set; } = string.Empty;
    public double Resultado { get; set; }
    public string Unidad { get; set; } = string.Empty;
    public DateTime FechaCalculo { get; set; }
    public Dictionary<string, double> Parametros { get; set; } = new();
} 