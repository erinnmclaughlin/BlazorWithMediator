using BlazorWithMediator.Shared.Features;

namespace BlazorWithMediator.Shared.Contracts;

public interface IWeatherForecastService
{
    Task<GetAllForecasts.Response> GetAll(CancellationToken ct);
    Task<GetForecastById.Response?> GetById(int id, CancellationToken ct);

    Task<CreateForecast.Response> Create(CreateForecast.Request request, CancellationToken ct);
    Task<UpdateForecast.Response> Update(UpdateForecast.Request request, CancellationToken ct);
    Task Delete(DeleteForecast.Request request, CancellationToken ct);
}
