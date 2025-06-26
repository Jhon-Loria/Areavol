using MiAreaVol.Data;
using MiAreaVol.Models;
using Microsoft.EntityFrameworkCore;

namespace MiAreaVol.Services;

public class AreaService : IAreaService
{
    private readonly MiAreaVolContext _context;
    private readonly ILogger<AreaService> _logger;

    public AreaService(MiAreaVolContext context, ILogger<AreaService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CalculoResponse> CalcularAreaAsync(CalculoAreaRequest request)
    {
        string tipoFiguraNormalizado = NormalizarFigura(request.TipoFigura);
        ValidarParametrosArea(tipoFiguraNormalizado, request);
        double resultado = CalcularArea(tipoFiguraNormalizado, request);

        var calculoArea = new CalculoArea
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
            FechaCreacion = DateTime.Now
        };

        _context.CalculosArea.Add(calculoArea);
        await _context.SaveChangesAsync();

        return ConvertirAResponse(calculoArea);
    }

    public async Task<PagedResult<CalculoResponse>> ObtenerPaginadoAsync(int pageNumber, int pageSize)
    {
        var query = _context.CalculosArea.OrderByDescending(c => c.FechaCreacion);
        
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
        var calculo = await _context.CalculosArea.FindAsync(id);
        return calculo != null ? ConvertirAResponse(calculo) : null;
    }

    public async Task<CalculoResponse> ActualizarAsync(int id, CalculoAreaRequest request)
    {
        var calculo = await _context.CalculosArea.FindAsync(id);
        if (calculo == null)
            throw new ArgumentException($"Cálculo de área con ID {id} no encontrado");

        double resultado = CalcularArea(request.TipoFigura.ToLower(), request);

        calculo.Nombre = request.Nombre;
        calculo.TipoFigura = request.TipoFigura;
        calculo.Resultado = resultado;
        calculo.Base = request.Base;
        calculo.Altura = request.Altura;
        calculo.Radio = request.Radio;
        calculo.Lado = request.Lado;
        calculo.Largo = request.Largo;
        calculo.Ancho = request.Ancho;

        await _context.SaveChangesAsync();

        return ConvertirAResponse(calculo);
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var calculo = await _context.CalculosArea.FindAsync(id);
        if (calculo == null)
            return false;

        _context.CalculosArea.Remove(calculo);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CalculoResponse>> BuscarPorTipoAsync(string tipoFigura)
    {
        var calculos = await _context.CalculosArea
            .Where(c => c.TipoFigura.ToLower().Contains(tipoFigura.ToLower()))
            .OrderByDescending(c => c.FechaCreacion)
            .ToListAsync();

        return calculos.Select(ConvertirAResponse);
    }

    public async Task<IEnumerable<CalculoResponse>> BuscarPorNombreAsync(string nombre)
    {
        var calculos = await _context.CalculosArea
            .Where(c => c.Nombre.ToLower().Contains(nombre.ToLower()))
            .OrderByDescending(c => c.FechaCreacion)
            .ToListAsync();

        return calculos.Select(ConvertirAResponse);
    }

    private double CalcularArea(string tipoFigura, CalculoAreaRequest request)
    {
        return tipoFigura switch
        {
            "cuadrado" => CalcularAreaCuadrado(request.Lado ?? 0),
            "rectangulo" => CalcularAreaRectangulo(request.Largo ?? 0, request.Ancho ?? 0),
            "triangulo" => CalcularAreaTriangulo(request.Base ?? 0, request.Altura ?? 0),
            "circulo" => CalcularAreaCirculo(request.Radio ?? 0),
            "trapecio" => CalcularAreaTrapecio(request.Base ?? 0, request.Altura ?? 0, request.Lado ?? 0),
            _ => throw new ArgumentException($"Tipo de figura no soportado: {tipoFigura}")
        };
    }

    private CalculoResponse ConvertirAResponse(CalculoArea calculo)
    {
        var parametros = new Dictionary<string, double>();
        if (calculo.Base.HasValue) parametros["Base"] = calculo.Base.Value;
        if (calculo.Altura.HasValue) parametros["Altura"] = calculo.Altura.Value;
        if (calculo.Radio.HasValue) parametros["Radio"] = calculo.Radio.Value;
        if (calculo.Lado.HasValue) parametros["Lado"] = calculo.Lado.Value;
        if (calculo.Largo.HasValue) parametros["Largo"] = calculo.Largo.Value;
        if (calculo.Ancho.HasValue) parametros["Ancho"] = calculo.Ancho.Value;

        return new CalculoResponse
        {
            Id = calculo.Id,
            Nombre = calculo.Nombre,
            TipoFigura = calculo.TipoFigura,
            Resultado = calculo.Resultado,
            Unidad = "unidades cuadradas",
            FechaCalculo = calculo.FechaCreacion,
            Parametros = parametros
        };
    }

    private double CalcularAreaCuadrado(double lado) => lado * lado;
    private double CalcularAreaRectangulo(double largo, double ancho) => largo * ancho;
    private double CalcularAreaTriangulo(double base_, double altura) => (base_ * altura) / 2;
    private double CalcularAreaCirculo(double radio) => Math.PI * radio * radio;
    private double CalcularAreaTrapecio(double baseMayor, double altura, double baseMenor) => ((baseMayor + baseMenor) * altura) / 2;

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

    private void ValidarParametrosArea(string tipoFigura, CalculoAreaRequest request)
    {
        switch (tipoFigura)
        {
            case "cuadrado":
                if (request.Lado == null)
                    throw new ArgumentException("El parámetro 'lado' es obligatorio para un cuadrado.");
                break;
            case "rectangulo":
                if (request.Largo == null || request.Ancho == null)
                    throw new ArgumentException("Los parámetros 'largo' y 'ancho' son obligatorios para un rectángulo.");
                break;
            case "triangulo":
                if (request.Base == null || request.Altura == null)
                    throw new ArgumentException("Los parámetros 'base' y 'altura' son obligatorios para un triángulo.");
                break;
            case "circulo":
                if (request.Radio == null)
                    throw new ArgumentException("El parámetro 'radio' es obligatorio para un círculo.");
                break;
            case "trapecio":
                if (request.Base == null || request.Lado == null || request.Altura == null)
                    throw new ArgumentException("Los parámetros 'base', 'lado' y 'altura' son obligatorios para un trapecio.");
                break;
            default:
                throw new ArgumentException($"Tipo de figura no soportado: {tipoFigura}");
        }
    }
} 