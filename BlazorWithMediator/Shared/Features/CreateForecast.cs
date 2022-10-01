using BlazorWithMediator.Shared.Contracts;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class CreateForecast
{
    public record Request(DateTime Date, int TemperatureC, string? Summary) : IRequest<Response>;
    public record Response(int Id, DateTime Date, int TemperatureC, string? Summary) : IWeatherForecast;

    public record ForecastCreated(Response Forecast) : INotification;

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
            var response = await ForecastService.Create(request, ct);
            await Publisher.Publish(new ForecastCreated(response), ct);
            return response;
        }
    }
}
