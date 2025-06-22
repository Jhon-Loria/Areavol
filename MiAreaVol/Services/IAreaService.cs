using MiAreaVol.Models;

namespace MiAreaVol.Services;

public interface IAreaService
{
    Task<CalculoResponse> CalcularAreaAsync(CalculoAreaRequest request);
    Task<PagedResult<CalculoResponse>> ObtenerPaginadoAsync(int pageNumber, int pageSize);
    Task<CalculoResponse?> ObtenerPorIdAsync(int id);
    Task<CalculoResponse> ActualizarAsync(int id, CalculoAreaRequest request);
    Task<bool> EliminarAsync(int id);
    Task<IEnumerable<CalculoResponse>> BuscarPorTipoAsync(string tipoFigura);
    Task<IEnumerable<CalculoResponse>> BuscarPorNombreAsync(string nombre);
} 