using Microsoft.AspNetCore.Mvc;
using MiAreaVol.Models;
using MiAreaVol.Services;
using Microsoft.AspNetCore.Authorization;

namespace MiAreaVol.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VolumenController : ControllerBase
{
    private readonly IVolumenService _volumenService;
    private readonly ILogger<VolumenController> _logger;

    public VolumenController(IVolumenService volumenService, ILogger<VolumenController> logger)
    {
        _volumenService = volumenService;
        _logger = logger;
    }

    /// <summary>
    /// Calcula el volumen de una figura geométrica 3D
    /// </summary>
    /// <param name="request">Datos de la figura para calcular el volumen</param>
    /// <returns>Resultado del cálculo del volumen</returns>
    [HttpPost("calcular")]
    public async Task<ActionResult<CalculoResponse>> CalcularVolumen([FromBody] CalculoVolumenRequest request)
    {
        try
        {
            _logger.LogInformation("Calculando volumen para figura: {TipoFigura} - {Nombre}", request.TipoFigura, request.Nombre);
            
            var resultado = await _volumenService.CalcularVolumenAsync(request);
            return Ok(resultado);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Error en el cálculo de volumen: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al calcular volumen");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene todos los cálculos de volumen
    /// </summary>
    /// <returns>Lista de todos los cálculos de volumen</returns>
    [HttpGet]
    public async Task<ActionResult<PagedResult<CalculoResponse>>> ObtenerPaginado(int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            _logger.LogInformation("Obteniendo todos los cálculos de volumen paginados (Página: {PageNumber}, Tamaño: {PageSize})", pageNumber, pageSize);
            
            var calculos = await _volumenService.ObtenerPaginadoAsync(pageNumber, pageSize);
            return Ok(calculos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener cálculos de volumen paginados");
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene un cálculo de volumen específico por su ID
    /// </summary>
    /// <param name="id">ID del cálculo</param>
    /// <returns>Cálculo de volumen específico</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CalculoResponse>> ObtenerPorId(int id)
    {
        try
        {
            _logger.LogInformation("Obteniendo cálculo de volumen con ID: {Id}", id);
            
            var calculo = await _volumenService.ObtenerPorIdAsync(id);
            if (calculo == null)
            {
                return NotFound(new { error = "Cálculo no encontrado" });
            }
            
            return Ok(calculo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener cálculo de volumen con ID: {Id}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Actualiza un cálculo de volumen existente
    /// </summary>
    /// <param name="id">ID del cálculo a actualizar</param>
    /// <param name="request">Nuevos datos del cálculo</param>
    /// <returns>Cálculo actualizado</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<CalculoResponse>> Actualizar(int id, [FromBody] CalculoVolumenRequest request)
    {
        try
        {
            _logger.LogInformation("Actualizando cálculo de volumen con ID: {Id}", id);
            
            var resultado = await _volumenService.ActualizarAsync(id, request);
            return Ok(resultado);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning("Error al actualizar cálculo de volumen: {Message}", ex.Message);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar cálculo de volumen con ID: {Id}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina un cálculo de volumen
    /// </summary>
    /// <param name="id">ID del cálculo a eliminar</param>
    /// <returns>Resultado de la eliminación</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(int id)
    {
        try
        {
            _logger.LogInformation("Eliminando cálculo de volumen con ID: {Id}", id);
            
            var eliminado = await _volumenService.EliminarAsync(id);
            if (!eliminado)
            {
                return NotFound(new { error = "Cálculo no encontrado" });
            }
            
            return Ok(new { message = "Cálculo eliminado exitosamente" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar cálculo de volumen con ID: {Id}", id);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Busca cálculos de volumen por tipo de figura
    /// </summary>
    /// <param name="tipoFigura">Tipo de figura a buscar</param>
    /// <returns>Lista de cálculos filtrados por tipo</returns>
    [HttpGet("buscar/tipo/{tipoFigura}")]
    public async Task<ActionResult<IEnumerable<CalculoResponse>>> BuscarPorTipo(string tipoFigura)
    {
        try
        {
            _logger.LogInformation("Buscando cálculos de volumen por tipo: {TipoFigura}", tipoFigura);
            
            var calculos = await _volumenService.BuscarPorTipoAsync(tipoFigura);
            return Ok(calculos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al buscar cálculos por tipo: {TipoFigura}", tipoFigura);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Busca cálculos de volumen por nombre
    /// </summary>
    /// <param name="nombre">Nombre a buscar</param>
    /// <returns>Lista de cálculos filtrados por nombre</returns>
    [HttpGet("buscar/nombre/{nombre}")]
    public async Task<ActionResult<IEnumerable<CalculoResponse>>> BuscarPorNombre(string nombre)
    {
        try
        {
            _logger.LogInformation("Buscando cálculos de volumen por nombre: {Nombre}", nombre);
            
            var calculos = await _volumenService.BuscarPorNombreAsync(nombre);
            return Ok(calculos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al buscar cálculos por nombre: {Nombre}", nombre);
            return StatusCode(500, new { error = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene información sobre las figuras soportadas para cálculo de volumen
    /// </summary>
    /// <returns>Lista de figuras soportadas</returns>
    [HttpGet("figuras-soportadas")]
    public ActionResult<object> ObtenerFigurasSoportadas()
    {
        var figuras = new[]
        {
            new { 
                nombre = "Cubo", 
                tipo = "cubo", 
                parametros = new[] { "lado" },
                formula = "lado³"
            },
            new { 
                nombre = "Prisma", 
                tipo = "prisma", 
                parametros = new[] { "base", "altura" },
                formula = "base × altura"
            },
            new { 
                nombre = "Cilindro", 
                tipo = "cilindro", 
                parametros = new[] { "radio", "altura" },
                formula = "π × radio² × altura"
            },
            new { 
                nombre = "Esfera", 
                tipo = "esfera", 
                parametros = new[] { "radio" },
                formula = "(4/3) × π × radio³"
            },
            new { 
                nombre = "Cono", 
                tipo = "cono", 
                parametros = new[] { "radio", "altura" },
                formula = "(1/3) × π × radio² × altura"
            },
            new { 
                nombre = "Paralelepípedo", 
                tipo = "paralelepipedo", 
                parametros = new[] { "largo", "ancho", "altura" },
                formula = "largo × ancho × altura"
            },
            new { 
                nombre = "Pirámide", 
                tipo = "piramide", 
                parametros = new[] { "base", "altura" },
                formula = "(1/3) × base × altura"
            }
        };

        return Ok(figuras);
    }
} 