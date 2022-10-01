using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Services;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class CreateForecast
{
    public static event EventHandler<WeatherForecastDto>? OnCreate;

    public record Request(DateTime Date, int TemperatureC, string? Summary) : IRequest<Result<WeatherForecastDto>>;

    public class Handler : IRequestHandler<Request, Result<WeatherForecastDto>>
    {
        private IWeatherForecastService ForecastService { get; }

        public Handler(IWeatherForecastService forecastService)
        {
            ForecastService = forecastService;
        }

        public async Task<Result<WeatherForecastDto>> Handle(Request request, CancellationToken ct)
        {
            var forecast = new WeatherForecastDto(default, request.Date, request.TemperatureC, request.Summary);
            var id = await ForecastService.Create(forecast, ct);

            forecast = forecast with { Id = id };

            OnCreate?.Invoke(this, forecast);
            return Result.Success(forecast);
        }
    }
}
