using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Services;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class DeleteForecast
{
    public static event EventHandler<int>? OnDelete;

    public record Request(int Id) : IRequest<Result>;

    public class Handler : IRequestHandler<Request, Result>
    {
        private IWeatherForecastService ForecastService { get; }

        public Handler(IWeatherForecastService forecastService)
        {
            ForecastService = forecastService;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            await ForecastService.Delete(request.Id, cancellationToken);
            OnDelete?.Invoke(this, request.Id);
            return Result.Success();
        }
    }
}
