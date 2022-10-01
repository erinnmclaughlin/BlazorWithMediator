using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Exceptions;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class GetForecastById
{
    public record Request(int Id) : IRequest<Response>;
    public record Response(int Id, DateTime Date, int TemperatureC, string? Summary) : IWeatherForecast;

    public class Handler : IRequestHandler<Request, Response>
    {
        private IWeatherForecastService ForecastService { get; }

        public Handler(IWeatherForecastService forecastService)
        {
            ForecastService = forecastService;
        }

        public async Task<Response> Handle(Request request, CancellationToken ct)
        {
            var forecast = await ForecastService.GetById(request.Id, ct);

            if (forecast == null)
                throw new NotFoundException($"Forecast with id {request.Id} was not found.");

            return forecast;
        }
    }
}
