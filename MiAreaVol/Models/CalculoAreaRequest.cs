using System.ComponentModel.DataAnnotations;

namespace MiAreaVol.Models;

public class CalculoAreaRequest
{
    [Required]
    public string TipoFigura { get; set; } = string.Empty;
    
    [Required]
    public string Nombre { get; set; } = string.Empty;
    
    // Propiedades para diferentes figuras
    public double? Base { get; set; }
    public double? Altura { get; set; }
    public double? Radio { get; set; }
    public double? Lado { get; set; }
    public double? Largo { get; set; }
    public double? Ancho { get; set; }
} 