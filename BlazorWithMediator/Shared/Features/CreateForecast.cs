using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Services;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class CreateForecast
{
    public record Request(DateTime Date, int TemperatureC, string? Summary) : IRequest<Result<WeatherForecastDto>>;
    public record ForecastCreated(WeatherForecastDto Forecast) : INotification;

    public class Handler : IRequestHandler<Request, Result<WeatherForecastDto>>
    {
        private IWeatherForecastService ForecastService { get; }
        private IPublisher Publisher { get; }

        public Handler(IWeatherForecastService forecastService, IPublisher publisher)
        {
            ForecastService = forecastService;
            Publisher = publisher;
        }

        public async Task<Result<WeatherForecastDto>> Handle(Request request, CancellationToken ct)
        {
            var forecast = new WeatherForecastDto(default, request.Date, request.TemperatureC, request.Summary);
            var id = await ForecastService.Create(forecast, ct);

            forecast = forecast with { Id = id };

            await Publisher.Publish(new ForecastCreated(forecast), ct);
            return Result.Success(forecast);
        }
    }
}
