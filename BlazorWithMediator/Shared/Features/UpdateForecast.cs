using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Services;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class UpdateForecast
{
    public record Request(int Id, DateTime Date, int TemperatureC, string? Summary) : IRequest<Result<WeatherForecastDto>>;
    public record ForecastUpdated(WeatherForecastDto Forecast) : INotification;

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
            var forecast = new WeatherForecastDto(request.Id, request.Date, request.TemperatureC, request.Summary);

            await ForecastService.Update(forecast, ct);
            await Publisher.Publish(new ForecastUpdated(forecast), ct);

            return Result.Success(forecast);
        }
    }
}