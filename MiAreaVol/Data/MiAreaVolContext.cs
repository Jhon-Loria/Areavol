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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CalculoArea>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TipoFigura).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Resultado).HasPrecision(18, 6);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            // Configuraciones específicas para MySQL
            entity.ToTable("CalculosArea");
        });

        modelBuilder.Entity<CalculoVolumen>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.TipoFigura).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Resultado).HasPrecision(18, 6);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
            
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
    }
} 