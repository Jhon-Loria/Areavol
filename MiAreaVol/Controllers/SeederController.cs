using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Services;

namespace MiAreaVol.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeederController : ControllerBase
{
    private readonly IDataSeederService _seederService;
    private readonly ILogger<SeederController> _logger;

    public SeederController(IDataSeederService seederService, ILogger<SeederController> logger)
    {
        _seederService = seederService;
        _logger = logger;
    }

    /// <summary>
    /// Ejecuta el seeder para poblar la base de datos con datos de prueba
    /// </summary>
    /// <returns>Resultado del seeding</returns>
    [HttpPost("seed")]
    public async Task<ActionResult<object>> SeedData()
    {
        try
        {
            _logger.LogInformation("Ejecutando seeder de datos...");
            
            await _seederService.SeedDataAsync();
            
            var areasCount = await _seederService.GetAreasCountAsync();
            var volumenesCount = await _seederService.GetVolumenesCountAsync();
            
            return Ok(new
            {
                message = "Datos sembrados exitosamente",
                areas = areasCount,
                volumenes = volumenesCount,
                total = areasCount + volumenesCount,
                timestamp = DateTime.Now
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al ejecutar el seeder");
            return StatusCode(500, new { error = "Error al sembrar datos", details = ex.Message });
        }
    }

    /// <summary>
    /// Obtiene el estado actual de la base de datos
    /// </summary>
    /// <returns>Conteo de registros en cada tabla</returns>
    [HttpGet("status")]
    public async Task<ActionResult<object>> GetDatabaseStatus()
    {
        try
        {
            var areasCount = await _seederService.GetAreasCountAsync();
            var volumenesCount = await _seederService.GetVolumenesCountAsync();
            
            return Ok(new
            {
                areas = areasCount,
                volumenes = volumenesCount,
                total = areasCount + volumenesCount,
                timestamp = DateTime.Now,
                status = areasCount > 0 && volumenesCount > 0 ? "Poblada" : "Vacía"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estado de la base de datos");
            return StatusCode(500, new { error = "Error al obtener estado", details = ex.Message });
        }
    }

    /// <summary>
    /// Limpia todos los datos de la base de datos
    /// </summary>
    /// <returns>Resultado de la limpieza</returns>
    [HttpDelete("clear")]
    public async Task<ActionResult<object>> ClearData()
    {
        try
        {
            _logger.LogInformation("Limpiando datos de la base de datos...");
            
            // Este método requeriría implementación adicional en el servicio
            // Por ahora solo retornamos un mensaje
            return Ok(new
            {
                message = "Función de limpieza no implementada por seguridad",
                suggestion = "Usa DELETE en los endpoints específicos para eliminar registros individuales"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al limpiar datos");
            return StatusCode(500, new { error = "Error al limpiar datos", details = ex.Message });
        }
    }
} 