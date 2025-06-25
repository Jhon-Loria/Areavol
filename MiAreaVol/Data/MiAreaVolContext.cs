using Microsoft.EntityFrameworkCore;
using MiAreaVol.Models;

namespace MiAreaVol.Data;

public class MiAreaVolContext : DbContext
{
    public MiAreaVolContext(DbContextOptions<MiAreaVolContext> options) : base(options)
    {
    }

    public DbSet<CalculoArea> CalculosArea { get; set; }
    public DbSet<CalculoVolumen> CalculosVolumen { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Circulo> Circulos { get; set; }
    public DbSet<Cuadrado> Cuadrados { get; set; }
    public DbSet<Rectangulo> Rectangulos { get; set; }
    public DbSet<Triangulo> Triangulos { get; set; }
    public DbSet<Trapecio> Trapecios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CalculoArea>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TipoFigura).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Resultado).HasPrecision(18, 6);
            
            // Configuraciones específicas para MySQL
            entity.ToTable("CalculosArea");
        });

        modelBuilder.Entity<CalculoVolumen>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TipoFigura).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Resultado).HasPrecision(18, 6);
            
            // Configuraciones específicas para MySQL
            entity.ToTable("CalculosVolumen");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.Password).IsRequired();
            entity.ToTable("Users");
        });

        modelBuilder.Entity<Circulo>().ToTable("Circulos");
        modelBuilder.Entity<Cuadrado>().ToTable("Cuadrados");
        modelBuilder.Entity<Rectangulo>().ToTable("Rectangulos");
        modelBuilder.Entity<Triangulo>().ToTable("Triangulos");
        modelBuilder.Entity<Trapecio>().ToTable("Trapecios");
    }
} 