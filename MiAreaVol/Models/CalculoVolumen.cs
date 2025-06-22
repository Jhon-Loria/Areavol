using System.ComponentModel.DataAnnotations;

namespace MiAreaVol.Models;

public class CalculoVolumen
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string TipoFigura { get; set; } = string.Empty;
    
    public double Resultado { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    // Propiedades para diferentes figuras 3D
    public double? Base { get; set; }
    public double? Altura { get; set; }
    public double? Radio { get; set; }
    public double? Lado { get; set; }
    public double? Largo { get; set; }
    public double? Ancho { get; set; }
    public double? Profundidad { get; set; }
} 