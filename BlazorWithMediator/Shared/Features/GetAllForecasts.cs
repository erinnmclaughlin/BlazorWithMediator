using BlazorWithMediator.Shared.Contracts;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class GetAllForecasts
{
    public record Request : IRequest<Response>;
    public record Response(WeatherForecastDto[] Forecasts);
    public record WeatherForecastDto(int Id, DateTime Date, int TemperatureC, string? Summary) : IWeatherForecast;

    public class Handler : IRequestHandler<Request, Response>
    {
        public IWeatherForecastService ForecastService { get; }

        public Handler(IWeatherForecastService forecastService)
        {
            ForecastService = forecastService;
        }

        public async Task<Response> Handle(Request _, CancellationToken ct)
        {
            return await ForecastService.GetAll(ct);
        }
    }
}
