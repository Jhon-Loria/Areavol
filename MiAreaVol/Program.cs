using Microsoft.EntityFrameworkCore;
using MiAreaVol.Data;
using MiAreaVol.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configurar Entity Framework con MySQL Railway
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"🔗 Connection String: {connectionString}");

builder.Services.AddDbContext<MiAreaVolContext>(options =>
    options.UseMySql(connectionString, 
        ServerVersion.AutoDetect(connectionString),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()));

// Configurar JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Registrar servicios
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IVolumenService, VolumenService>();
builder.Services.AddScoped<IDataSeederService, DataSeederService>();
builder.Services.AddScoped<CilindroService>();

// Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "MiAreaVol API", 
        Version = "v1",
        Description = "API para calcular áreas y volúmenes de figuras geométricas"
    });

    // Configurar Swagger para usar JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, ingrese el token JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Ejecutar seeder automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var seederService = scope.ServiceProvider.GetRequiredService<IDataSeederService>();
    var context = scope.ServiceProvider.GetRequiredService<MiAreaVolContext>();
    
    try
    {
        Console.WriteLine("🔍 Verificando conexión a la base de datos...");
        var canConnect = await context.Database.CanConnectAsync();
        Console.WriteLine($"📡 Conexión a BD: {(canConnect ? "✅ Exitosa" : "❌ Fallida")}");
        
        if (canConnect)
        {
            Console.WriteLine("🌱 Ejecutando seeder de datos...");
            await seederService.SeedDataAsync();
            Console.WriteLine("✅ Base de datos poblada con datos de prueba");
        }
        else
        {
            Console.WriteLine("⚠️ No se puede conectar a la base de datos. Verifica la conexión.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error durante la inicialización: {ex.Message}");
        Console.WriteLine($"🔍 Detalles: {ex.InnerException?.Message}");
        Console.WriteLine($"📋 Stack Trace: {ex.StackTrace}");
    }
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiAreaVol API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

// Usar CORS
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
