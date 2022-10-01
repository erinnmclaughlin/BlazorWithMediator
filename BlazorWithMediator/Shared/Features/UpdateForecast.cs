using BlazorWithMediator.Shared.Contracts;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class UpdateForecast
{
    public record Request(int Id, DateTime Date, int TemperatureC, string? Summary) : IRequest<Response>;
    public record Response(int Id, DateTime Date, int TemperatureC, string? Summary) : IWeatherForecast;
    public record ForecastUpdated(Response Forecast) : INotification;

    public class Handler : IRequestHandler<Request, Response>
    {
        private IWeatherForecastService ForecastService { get; }
        private IPublisher Publisher { get; }

        public Handler(IWeatherForecastService forecastService, IPublisher publisher)
        {
            ForecastService = forecastService;
            Publisher = publisher;
        }

        public async Task<Response> Handle(Request request, CancellationToken ct)
        {
            var response = await ForecastService.Update(request, ct);
            await Publisher.Publish(new ForecastUpdated(response), ct);
            return response;
        }
    }
}