using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Services;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class UpdateForecast
{
    public static event EventHandler<WeatherForecastDto>? OnUpdate;
    public record Request(int Id, DateTime Date, int TemperatureC, string? Summary) : IRequest<Result<WeatherForecastDto>>;

    public class Handler : IRequestHandler<Request, Result<WeatherForecastDto>>
    {
        private IWeatherForecastService ForecastService { get; }

        public Handler(IWeatherForecastService forecastService)
        {
            ForecastService = forecastService;
        }

        public async Task<Result<WeatherForecastDto>> Handle(Request request, CancellationToken ct)
        {
            var forecast = new WeatherForecastDto(request.Id, request.Date, request.TemperatureC, request.Summary);
            await ForecastService.Update(forecast, ct);
            OnUpdate?.Invoke(this, forecast);
            return Result.Success(forecast);
        }
    }
}