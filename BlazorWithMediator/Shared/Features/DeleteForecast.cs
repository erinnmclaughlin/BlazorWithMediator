using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Services;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class DeleteForecast
{
    public record Request(int Id) : IRequest<Result>;
    public record ForecastDeleted(int Id) : INotification;

    public class Handler : IRequestHandler<Request, Result>
    {
        private IWeatherForecastService ForecastService { get; }
        private IPublisher Publisher { get; }

        public Handler(IWeatherForecastService forecastService, IPublisher publisher)
        {
            ForecastService = forecastService;
            Publisher = publisher;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            await ForecastService.Delete(request.Id, cancellationToken);
            await Publisher.Publish(new ForecastDeleted(request.Id), cancellationToken);
            return Result.Success();
        }
    }
}
