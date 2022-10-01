using BlazorWithMediator.Shared.Contracts;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class DeleteForecast
{
    public record Request(int Id) : IRequest;

    public class Handler : IRequestHandler<Request>
    {
        private IWeatherForecastService ForecastService { get; }

        public Handler(IWeatherForecastService forecastService)
        {
            ForecastService = forecastService;
        }

        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            await ForecastService.Delete(request, cancellationToken);
            return Unit.Value;
        }
    }
}
