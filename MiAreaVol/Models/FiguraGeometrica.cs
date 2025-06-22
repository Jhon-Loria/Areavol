using System.ComponentModel.DataAnnotations;

namespace MiAreaVol.Models;

public class FiguraGeometrica
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    public string Tipo { get; set; } = string.Empty;
    
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    // Propiedades comunes para cálculos
    public double? Base { get; set; }
    public double? Altura { get; set; }
    public double? Radio { get; set; }
    public double? Lado { get; set; }
    public double? Largo { get; set; }
    public double? Ancho { get; set; }
    public double? Profundidad { get; set; }
} 