using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;
using MiAreaVol.Services;
using Microsoft.AspNetCore.Authorization;

namespace MiAreaVol.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AreaController : ControllerBase
{
    private readonly IAreaService _areaService;
    private readonly ILogger<AreaController> _logger;

    public AreaController(IAreaService areaService, ILogger<AreaController> logger)
    {
        _areaService = areaService;
        _logger = logger;
    }

    /// <summary>
    /// Calcula el área de una figura geométrica
    /// </summary>
    /// <param name="request">Datos de la figura para calcular el área</param>
    /// <returns>Resultado del cálculo del área</returns>
    [HttpPost("calcular")]
    public async Task<ActionResult<CalculoResponse>> CalcularArea([FromBody] CalculoAreaRequest request)
    {
        try
        {
            _logger.LogInformation("Calculando área para figura: {TipoFigura} - {Nombre}", request.TipoFigura, request.Nombre);
            
            var resultado = await _areaService.CalcularAreaAsync(request);
            return Ok(resultado);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Error en el cálculo de área: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al calcular área");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene todos los cálculos de área
    /// </summary>
    /// <returns>Lista de todos los cálculos de área</returns>
    [HttpGet]
    public async Task<ActionResult<PagedResult<CalculoResponse>>> ObtenerPaginado(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            _logger.LogInformation("Obteniendo cálculos de área paginados (Página: {PageNumber}, Tamaño: {PageSize})", pageNumber, pageSize);
            
            var calculos = await _areaService.ObtenerPaginadoAsync(pageNumber, pageSize);
            return Ok(calculos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener cálculos de área paginados");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene un cálculo de área específico por su ID
    /// </summary>
    /// <param name="id">ID del cálculo</param>
    /// <returns>Cálculo de área específico</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CalculoResponse>> ObtenerPorId(int id)
    {
        try
        {
            _logger.LogInformation("Obteniendo cálculo de área con ID: {Id}", id);
            
            var calculo = await _areaService.ObtenerPorIdAsync(id);
            if (calculo == null)
            {
                return NotFound(new { error = "Cálculo no encontrado" });
            }
            
            return Ok(calculo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener cálculo de área con ID: {Id}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza un cálculo de área existente
    /// </summary>
    /// <param name="id">ID del cálculo a actualizar</param>
    /// <param name="request">Nuevos datos del cálculo</param>
    /// <returns>Cálculo actualizado</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<CalculoResponse>> Actualizar(int id, [FromBody] CalculoAreaRequest request)
    {
        try
        {
            _logger.LogInformation("Actualizando cálculo de área con ID: {Id}", id);
            
            var resultado = await _areaService.ActualizarAsync(id, request);
            return Ok(resultado);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Error al actualizar cálculo de área: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar cálculo de área con ID: {Id}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina un cálculo de área
    /// </summary>
    /// <param name="id">ID del cálculo a eliminar</param>
    /// <returns>Resultado de la eliminación</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        try
        {
            _logger.LogInformation("Eliminando cálculo de área con ID: {Id}", id);
            
            var eliminado = await _areaService.EliminarAsync(id);
            if (!eliminado)
            {
                return NotFound(new { error = "Cálculo no encontrado" });
            }
            
            return Ok(new { message = "Cálculo eliminado exitosamente" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar cálculo de área con ID: {Id}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Busca cálculos de área por tipo de figura
    /// </summary>
    /// <param name="tipoFigura">Tipo de figura a buscar</param>
    /// <returns>Lista de cálculos filtrados por tipo</returns>
    [HttpGet("buscar/tipo/{tipoFigura}")]
    public async Task<ActionResult<IEnumerable<CalculoResponse>>> BuscarPorTipo(string tipoFigura)
    {
        try
        {
            _logger.LogInformation("Buscando cálculos de área por tipo: {TipoFigura}", tipoFigura);
            
            var calculos = await _areaService.BuscarPorTipoAsync(tipoFigura);
            return Ok(calculos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al buscar cálculos por tipo: {TipoFigura}", tipoFigura);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Busca cálculos de área por nombre
    /// </summary>
    /// <param name="nombre">Nombre a buscar</param>
    /// <returns>Lista de cálculos filtrados por nombre</returns>
    [HttpGet("buscar/nombre/{nombre}")]
    public async Task<ActionResult<IEnumerable<CalculoResponse>>> BuscarPorNombre(string nombre)
    {
        try
        {
            _logger.LogInformation("Buscando cálculos de área por nombre: {Nombre}", nombre);
            
            var calculos = await _areaService.BuscarPorNombreAsync(nombre);
            return Ok(calculos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al buscar cálculos por nombre: {Nombre}", nombre);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene información sobre las figuras soportadas para cálculo de área
    /// </summary>
    /// <returns>Lista de figuras soportadas</returns>
    [HttpGet("figuras-soportadas")]
    public ActionResult<object> ObtenerFigurasSoportadas()
    {
        var figuras = new[]
        {
            new { 
                nombre = "Cuadrado", 
                tipo = "cuadrado", 
                parametros = new[] { "lado" },
                formula = "lado²"
            },
            new { 
                nombre = "Rectángulo", 
                tipo = "rectangulo", 
                parametros = new[] { "largo", "ancho" },
                formula = "largo × ancho"
            },
            new { 
                nombre = "Triángulo", 
                tipo = "triangulo", 
                parametros = new[] { "base", "altura" },
                formula = "(base × altura) / 2"
            },
            new { 
                nombre = "Círculo", 
                tipo = "circulo", 
                parametros = new[] { "radio" },
                formula = "π × radio²"
            },
            new { 
                nombre = "Trapecio", 
                tipo = "trapecio", 
                parametros = new[] { "base", "altura", "lado" },
                formula = "((base + lado) × altura) / 2"
            }
        };

        return Ok(figuras);
    }
}

public class Circulo
{
    public int Id { get; set; }
    public double Radio { get; set; }
    public double Circunferencia => 2 * Math.PI * Radio;
    public double Area => Math.PI * Math.Pow(Radio, 2);
}

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