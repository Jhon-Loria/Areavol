using MiAreaVol.Models;

namespace MiAreaVol.Services;

public interface IDataSeederService
{
    Task SeedDataAsync();
    Task<int> GetAreasCountAsync();
    Task<int> GetVolumenesCountAsync();
} 