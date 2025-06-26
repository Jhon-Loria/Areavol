using MiAreaVol.Data;
using MiAreaVol.Models;
using Microsoft.EntityFrameworkCore;

namespace MiAreaVol.Services;

public class VolumenService : IVolumenService
{
    private readonly MiAreaVolContext _context;
    private readonly ILogger<VolumenService> _logger;

    public VolumenService(MiAreaVolContext context, ILogger<VolumenService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CalculoResponse> CalcularVolumenAsync(CalculoVolumenRequest request)
    {
        string tipoFiguraNormalizado = NormalizarFigura(request.TipoFigura);
        ValidarParametrosVolumen(tipoFiguraNormalizado, request);
        double resultado = CalcularVolumen(tipoFiguraNormalizado, request);

        var calculoVolumen = new CalculoVolumen
        {
            Nombre = request.Nombre,
            TipoFigura = request.TipoFigura,
            Resultado = resultado,
            Base = request.Base,
            Altura = request.Altura,
            Radio = request.Radio,
            Lado = request.Lado,
            Largo = request.Largo,
            Ancho = request.Ancho,
            Profundidad = request.Profundidad,
            FechaCreacion = DateTime.Now
        };

        _context.CalculosVolumen.Add(calculoVolumen);
        await _context.SaveChangesAsync();

        return ConvertirAResponse(calculoVolumen);
    }

    public async Task<PagedResult<CalculoResponse>> ObtenerPaginadoAsync(int pageNumber, int pageSize)
    {
        var query = _context.CalculosVolumen.OrderByDescending(c => c.FechaCreacion);
        
        var totalCount = await query.CountAsync();
        
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var responseItems = items.Select(ConvertirAResponse).ToList();

        return new PagedResult<CalculoResponse>(responseItems, totalCount, pageNumber, pageSize);
    }

    public async Task<CalculoResponse?> ObtenerPorIdAsync(int id)
    {
        var calculo = await _context.CalculosVolumen.FindAsync(id);
        return calculo != null ? ConvertirAResponse(calculo) : null;
    }

    public async Task<CalculoResponse> ActualizarAsync(int id, CalculoVolumenRequest request)
    {
        var calculo = await _context.CalculosVolumen.FindAsync(id);
        if (calculo == null)
            throw new ArgumentException($"Cálculo de volumen con ID {id} no encontrado");

        double resultado = CalcularVolumen(request.TipoFigura.ToLower(), request);

        calculo.Nombre = request.Nombre;
        calculo.TipoFigura = request.TipoFigura;
        calculo.Resultado = resultado;
        calculo.Base = request.Base;
        calculo.Altura = request.Altura;
        calculo.Radio = request.Radio;
        calculo.Lado = request.Lado;
        calculo.Largo = request.Largo;
        calculo.Ancho = request.Ancho;
        calculo.Profundidad = request.Profundidad;

        await _context.SaveChangesAsync();

        return ConvertirAResponse(calculo);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var calculo = await _context.CalculosVolumen.FindAsync(id);
        if (calculo == null)
            return false;

        _context.CalculosVolumen.Remove(calculo);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CalculoResponse>> BuscarPorTipoAsync(string tipoFigura)
    {
        var calculos = await _context.CalculosVolumen
            .Where(c => c.TipoFigura.ToLower().Contains(tipoFigura.ToLower()))
            .OrderByDescending(c => c.FechaCreacion)
            .ToListAsync();

        return calculos.Select(ConvertirAResponse);
    }

    public async Task<IEnumerable<CalculoResponse>> BuscarPorNombreAsync(string nombre)
    {
        var calculos = await _context.CalculosVolumen
            .Where(c => c.Nombre.ToLower().Contains(nombre.ToLower()))
            .OrderByDescending(c => c.FechaCreacion)
            .ToListAsync();

        return calculos.Select(ConvertirAResponse);
    }

    private double CalcularVolumen(string tipoFigura, CalculoVolumenRequest request)
    {
        return tipoFigura switch
        {
            "cubo" => CalcularVolumenCubo(request.Lado ?? 0),
            "prisma" => CalcularVolumenPrisma(request.Base ?? 0, request.Altura ?? 0),
            "cilindro" => CalcularVolumenCilindro(request.Radio ?? 0, request.Altura ?? 0),
            "esfera" => CalcularVolumenEsfera(request.Radio ?? 0),
            "cono" => CalcularVolumenCono(request.Radio ?? 0, request.Altura ?? 0),
            "paralelepipedo" => CalcularVolumenParalelepipedo(request.Largo ?? 0, request.Ancho ?? 0, request.Altura ?? 0),
            "piramide" => CalcularVolumenPiramide(request.Base ?? 0, request.Altura ?? 0),
            _ => throw new ArgumentException($"Tipo de figura no soportado: {tipoFigura}")
        };
    }

    private CalculoResponse ConvertirAResponse(CalculoVolumen calculo)
    {
        var parametros = new Dictionary<string, double>();
        if (calculo.Base.HasValue) parametros["Base"] = calculo.Base.Value;
        if (calculo.Altura.HasValue) parametros["Altura"] = calculo.Altura.Value;
        if (calculo.Radio.HasValue) parametros["Radio"] = calculo.Radio.Value;
        if (calculo.Lado.HasValue) parametros["Lado"] = calculo.Lado.Value;
        if (calculo.Largo.HasValue) parametros["Largo"] = calculo.Largo.Value;
        if (calculo.Ancho.HasValue) parametros["Ancho"] = calculo.Ancho.Value;
        if (calculo.Profundidad.HasValue) parametros["Profundidad"] = calculo.Profundidad.Value;

        return new CalculoResponse
        {
            Id = calculo.Id,
            Nombre = calculo.Nombre,
            TipoFigura = calculo.TipoFigura,
            Resultado = calculo.Resultado,
            Unidad = "unidades cúbicas",
            FechaCalculo = calculo.FechaCreacion,
            Parametros = parametros
        };
    }

    private double CalcularVolumenCubo(double lado) => Math.Pow(lado, 3);
    private double CalcularVolumenPrisma(double base_, double altura) => base_ * altura;
    private double CalcularVolumenCilindro(double radio, double altura) => Math.PI * Math.Pow(radio, 2) * altura;
    private double CalcularVolumenEsfera(double radio) => (4.0 / 3.0) * Math.PI * Math.Pow(radio, 3);
    private double CalcularVolumenCono(double radio, double altura) => (1.0 / 3.0) * Math.PI * Math.Pow(radio, 2) * altura;
    private double CalcularVolumenParalelepipedo(double largo, double ancho, double altura) => largo * ancho * altura;
    private double CalcularVolumenPiramide(double base_, double altura) => (1.0 / 3.0) * base_ * altura;

    private string NormalizarFigura(string tipoFigura)
    {
        if (string.IsNullOrWhiteSpace(tipoFigura)) return "";
        var normalized = tipoFigura.ToLowerInvariant().Normalize(System.Text.NormalizationForm.FormD);
        var sb = new System.Text.StringBuilder();
        foreach (var c in normalized)
        {
            if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }
        return sb.ToString().Normalize(System.Text.NormalizationForm.FormC);
    }

    private void ValidarParametrosVolumen(string tipoFigura, CalculoVolumenRequest request)
    {
        switch (tipoFigura)
        {
            case "cubo":
                if (request.Lado == null)
                    throw new ArgumentException("El parámetro 'lado' es obligatorio para un cubo.");
                break;
            case "prisma":
                if (request.Base == null || request.Altura == null)
                    throw new ArgumentException("Los parámetros 'base' y 'altura' son obligatorios para un prisma.");
                break;
            case "cilindro":
                if (request.Radio == null || request.Altura == null)
                    throw new ArgumentException("Los parámetros 'radio' y 'altura' son obligatorios para un cilindro.");
                break;
            case "esfera":
                if (request.Radio == null)
                    throw new ArgumentException("El parámetro 'radio' es obligatorio para una esfera.");
                break;
            case "cono":
                if (request.Radio == null || request.Altura == null)
                    throw new ArgumentException("Los parámetros 'radio' y 'altura' son obligatorios para un cono.");
                break;
            case "paralelepipedo":
                if (request.Largo == null || request.Ancho == null || request.Altura == null)
                    throw new ArgumentException("Los parámetros 'largo', 'ancho' y 'altura' son obligatorios para un paralelepípedo.");
                break;
            case "piramide":
                if (request.Base == null || request.Altura == null)
                    throw new ArgumentException("Los parámetros 'base' y 'altura' son obligatorios para una pirámide.");
                break;
            default:
                throw new ArgumentException($"Tipo de figura no soportado: {tipoFigura}");
        }
    }
} 