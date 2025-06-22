using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiAreaVol.Data;

namespace MiAreaVol.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnosticController : ControllerBase
{
    private readonly MiAreaVolContext _context;
    private readonly ILogger<DiagnosticController> _logger;

    public DiagnosticController(MiAreaVolContext context, ILogger<DiagnosticController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Diagnóstico completo de la aplicación
    /// </summary>
    [HttpGet("full")]
    public async Task<ActionResult<object>> FullDiagnostic()
    {
        var diagnostic = new
        {
            timestamp = DateTime.Now,
            database = new
            {
                connection = "Verificando...",
                tables = new List<string>(),
                areasCount = -1,
                volumenesCount = -1
            },
            application = new
            {
                version = "1.0.0",
                environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"
            }
        };

        try
        {
            // Verificar conexión a la base de datos
            var canConnect = await _context.Database.CanConnectAsync();
            diagnostic = new
            {
                timestamp = DateTime.Now,
                database = new
                {
                    connection = canConnect ? "✅ Conectado" : "❌ No conectado",
                    tables = canConnect ? await GetTablesAsync() : new List<string>(),
                    areasCount = canConnect ? await _context.CalculosArea.CountAsync() : -1,
                    volumenesCount = canConnect ? await _context.CalculosVolumen.CountAsync() : -1
                },
                application = new
                {
                    version = "1.0.0",
                    environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"
                }
            };

            return Ok(diagnostic);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en diagnóstico completo");
            return StatusCode(500, new
            {
                error = "Error en diagnóstico",
                details = ex.Message,
                innerException = ex.InnerException?.Message,
                stackTrace = ex.StackTrace
            });
        }
    }

    /// <summary>
    /// Verificar solo la conexión a la base de datos
    /// </summary>
    [HttpGet("connection")]
    public async Task<ActionResult<object>> TestConnection()
    {
        try
        {
            var canConnect = await _context.Database.CanConnectAsync();
            return Ok(new
            {
                connected = canConnect,
                timestamp = DateTime.Now,
                message = canConnect ? "Conexión exitosa" : "No se puede conectar a la base de datos"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                connected = false,
                error = ex.Message,
                details = ex.InnerException?.Message
            });
        }
    }

    /// <summary>
    /// Verificar si las tablas existen
    /// </summary>
    [HttpGet("tables")]
    public async Task<ActionResult<object>> CheckTables()
    {
        try
        {
            var tables = await GetTablesAsync();
            return Ok(new
            {
                tables = tables,
                count = tables.Count,
                timestamp = DateTime.Now
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Error al verificar tablas",
                details = ex.Message
            });
        }
    }

    /// <summary>
    /// Verificar conteo de registros
    /// </summary>
    [HttpGet("counts")]
    public async Task<ActionResult<object>> GetCounts()
    {
        try
        {
            var areasCount = await _context.CalculosArea.CountAsync();
            var volumenesCount = await _context.CalculosVolumen.CountAsync();
            
            return Ok(new
            {
                areas = areasCount,
                volumenes = volumenesCount,
                total = areasCount + volumenesCount,
                timestamp = DateTime.Now
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "Error al obtener conteos",
                details = ex.Message
            });
        }
    }

    private async Task<List<string>> GetTablesAsync()
    {
        try
        {
            // Para MySQL, usar SHOW TABLES
            var tables = new List<string>();
            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "SHOW TABLES";
            
            if (command.Connection.State != System.Data.ConnectionState.Open)
                await command.Connection.OpenAsync();
            
            using var result = await command.ExecuteReaderAsync();
            while (await result.ReadAsync())
            {
                tables.Add(result.GetString(0));
            }
            
            return tables;
        }
        catch
        {
            return new List<string> { "Error al obtener tablas" };
        }
    }
} 