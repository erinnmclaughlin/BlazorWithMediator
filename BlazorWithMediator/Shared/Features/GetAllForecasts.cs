using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Services;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class GetAllForecasts
{
    public record Request : IRequest<PagedResult<WeatherForecastDto>>;

    public class Handler : IRequestHandler<Request, PagedResult<WeatherForecastDto>>
    {
        public IWeatherForecastService ForecastService { get; }

        public Handler(IWeatherForecastService forecastService)
        {
            ForecastService = forecastService;
        }

        public async Task<PagedResult<WeatherForecastDto>> Handle(Request _, CancellationToken ct)
        {
            var forecasts = await ForecastService.GetAll(ct);
            return new PagedResult<WeatherForecastDto>(forecasts.ToArray(), 1, forecasts.Count, forecasts.Count, forecasts.Count, Array.Empty<string>());
        }
    }
}
