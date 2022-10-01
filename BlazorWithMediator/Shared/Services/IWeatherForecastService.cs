using BlazorWithMediator.Shared.Contracts;

namespace BlazorWithMediator.Shared.Services;

public interface IWeatherForecastService
{
    Task<List<WeatherForecastDto>> GetAll(CancellationToken ct);
    Task<WeatherForecastDto> GetById(int id, CancellationToken ct);
    Task<int> Create(WeatherForecastDto request, CancellationToken ct);
    Task Update(WeatherForecastDto request, CancellationToken ct);
    Task Delete(int id, CancellationToken ct);
}
