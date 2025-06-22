using MiAreaVol.Models;

namespace MiAreaVol.Services;

public interface IVolumenService
{
    Task<CalculoResponse> CalcularVolumenAsync(CalculoVolumenRequest request);
    Task<PagedResult<CalculoResponse>> ObtenerPaginadoAsync(int pageNumber, int pageSize);
    Task<CalculoResponse?> ObtenerPorIdAsync(int id);
    Task<CalculoResponse> ActualizarAsync(int id, CalculoVolumenRequest request);
    Task<bool> EliminarAsync(int id);
    Task<IEnumerable<CalculoResponse>> BuscarPorTipoAsync(string tipoFigura);
    Task<IEnumerable<CalculoResponse>> BuscarPorNombreAsync(string nombre);
} 