using MiAreaVol.Data;
using MiAreaVol.Models;
using Microsoft.EntityFrameworkCore;

namespace MiAreaVol.Services;

public class DataSeederService : IDataSeederService
{
    private readonly MiAreaVolContext _context;
    private readonly ILogger<DataSeederService> _logger;
    private readonly Random _random;

    public DataSeederService(MiAreaVolContext context, ILogger<DataSeederService> logger)
    {
        _context = context;
        _logger = logger;
        _random = new Random();
    }

    public async Task SeedDataAsync()
    {
        try
        {
            _logger.LogInformation("Iniciando seeding de datos...");

            // Verificar si ya hay datos
            var areasCount = await _context.CalculosArea.CountAsync();
            var volumenesCount = await _context.CalculosVolumen.CountAsync();

            if (areasCount == 0)
            {
                await SeedAreasAsync();
                _logger.LogInformation("Se agregaron 100 registros de áreas");
            }

            if (volumenesCount == 0)
            {
                await SeedVolumenesAsync();
                _logger.LogInformation("Se agregaron 100 registros de volúmenes");
            }

            _logger.LogInformation("Seeding completado exitosamente");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error durante el seeding de datos");
            throw;
        }
    }

    public async Task<int> GetAreasCountAsync()
    {
        return await _context.CalculosArea.CountAsync();
    }

    public async Task<int> GetVolumenesCountAsync()
    {
        return await _context.CalculosVolumen.CountAsync();
    }

    private async Task SeedAreasAsync()
    {
        var areas = new List<CalculoArea>();
        var tiposFigura = new[] { "cuadrado", "rectangulo", "triangulo", "circulo", "trapecio" };
        var nombres = new[]
        {
            "Cuadrado del parque", "Rectángulo de la casa", "Triángulo del jardín", "Círculo de la fuente",
            "Trapecio del techo", "Cuadrado de la mesa", "Rectángulo de la ventana", "Triángulo del árbol",
            "Círculo del reloj", "Trapecio de la escalera", "Cuadrado del azulejo", "Rectángulo del libro",
            "Triángulo de la señal", "Círculo del botón", "Trapecio del puente"
        };

        for (int i = 1; i <= 100; i++)
        {
            var tipoFigura = tiposFigura[_random.Next(tiposFigura.Length)];
            var nombre = $"{nombres[_random.Next(nombres.Length)]} #{i}";
            var calculoArea = new CalculoArea
            {
                Nombre = nombre,
                TipoFigura = tipoFigura,
                FechaCreacion = DateTime.Now.AddDays(-_random.Next(365))
            };

            // Generar parámetros según el tipo de figura
            switch (tipoFigura)
            {
                case "cuadrado":
                    var lado = _random.NextDouble() * 20 + 1;
                    calculoArea.Lado = lado;
                    calculoArea.Resultado = lado * lado;
                    break;

                case "rectangulo":
                    var largo = _random.NextDouble() * 15 + 2;
                    var ancho = _random.NextDouble() * 10 + 1;
                    calculoArea.Largo = largo;
                    calculoArea.Ancho = ancho;
                    calculoArea.Resultado = largo * ancho;
                    break;

                case "triangulo":
                    var base_ = _random.NextDouble() * 12 + 2;
                    var altura = _random.NextDouble() * 8 + 1;
                    calculoArea.Base = base_;
                    calculoArea.Altura = altura;
                    calculoArea.Resultado = (base_ * altura) / 2;
                    break;

                case "circulo":
                    var radio = _random.NextDouble() * 8 + 1;
                    calculoArea.Radio = radio;
                    calculoArea.Resultado = Math.PI * radio * radio;
                    break;

                case "trapecio":
                    var baseMayor = _random.NextDouble() * 15 + 3;
                    var baseMenor = _random.NextDouble() * 10 + 1;
                    var alturaTrap = _random.NextDouble() * 6 + 1;
                    calculoArea.Base = baseMayor;
                    calculoArea.Lado = baseMenor;
                    calculoArea.Altura = alturaTrap;
                    calculoArea.Resultado = ((baseMayor + baseMenor) * alturaTrap) / 2;
                    break;
            }

            areas.Add(calculoArea);
        }

        await _context.CalculosArea.AddRangeAsync(areas);
        await _context.SaveChangesAsync();
    }

    private async Task SeedVolumenesAsync()
    {
        var volumenes = new List<CalculoVolumen>();
        var tiposFigura = new[] { "cubo", "prisma", "cilindro", "esfera", "cono", "paralelepipedo", "piramide" };
        var nombres = new[]
        {
            "Cubo de hielo", "Prisma de cristal", "Cilindro de agua", "Esfera de metal",
            "Cono de helado", "Paralelepípedo de madera", "Pirámide de arena", "Cubo de azúcar",
            "Prisma de plástico", "Cilindro de gas", "Esfera de vidrio", "Cono de papel",
            "Paralelepípedo de piedra", "Pirámide de cristal", "Cubo de sal"
        };

        for (int i = 1; i <= 100; i++)
        {
            var tipoFigura = tiposFigura[_random.Next(tiposFigura.Length)];
            var nombre = $"{nombres[_random.Next(nombres.Length)]} #{i}";
            var calculoVolumen = new CalculoVolumen
            {
                Nombre = nombre,
                TipoFigura = tipoFigura,
                FechaCreacion = DateTime.Now.AddDays(-_random.Next(365))
            };

            // Generar parámetros según el tipo de figura
            switch (tipoFigura)
            {
                case "cubo":
                    var lado = _random.NextDouble() * 10 + 1;
                    calculoVolumen.Lado = lado;
                    calculoVolumen.Resultado = Math.Pow(lado, 3);
                    break;

                case "prisma":
                    var base_ = _random.NextDouble() * 20 + 5;
                    var altura = _random.NextDouble() * 15 + 2;
                    calculoVolumen.Base = base_;
                    calculoVolumen.Altura = altura;
                    calculoVolumen.Resultado = base_ * altura;
                    break;

                case "cilindro":
                    var radio = _random.NextDouble() * 6 + 1;
                    var alturaCil = _random.NextDouble() * 12 + 2;
                    calculoVolumen.Radio = radio;
                    calculoVolumen.Altura = alturaCil;
                    calculoVolumen.Resultado = Math.PI * Math.Pow(radio, 2) * alturaCil;
                    break;

                case "esfera":
                    var radioEsf = _random.NextDouble() * 5 + 1;
                    calculoVolumen.Radio = radioEsf;
                    calculoVolumen.Resultado = (4.0 / 3.0) * Math.PI * Math.Pow(radioEsf, 3);
                    break;

                case "cono":
                    var radioCono = _random.NextDouble() * 4 + 1;
                    var alturaCono = _random.NextDouble() * 10 + 2;
                    calculoVolumen.Radio = radioCono;
                    calculoVolumen.Altura = alturaCono;
                    calculoVolumen.Resultado = (1.0 / 3.0) * Math.PI * Math.Pow(radioCono, 2) * alturaCono;
                    break;

                case "paralelepipedo":
                    var largo = _random.NextDouble() * 12 + 2;
                    var ancho = _random.NextDouble() * 8 + 1;
                    var alturaPar = _random.NextDouble() * 6 + 1;
                    calculoVolumen.Largo = largo;
                    calculoVolumen.Ancho = ancho;
                    calculoVolumen.Altura = alturaPar;
                    calculoVolumen.Resultado = largo * ancho * alturaPar;
                    break;

                case "piramide":
                    var basePir = _random.NextDouble() * 25 + 5;
                    var alturaPir = _random.NextDouble() * 12 + 2;
                    calculoVolumen.Base = basePir;
                    calculoVolumen.Altura = alturaPir;
                    calculoVolumen.Resultado = (1.0 / 3.0) * basePir * alturaPir;
                    break;
            }

            volumenes.Add(calculoVolumen);
        }

        await _context.CalculosVolumen.AddRangeAsync(volumenes);
        await _context.SaveChangesAsync();
    }
} 