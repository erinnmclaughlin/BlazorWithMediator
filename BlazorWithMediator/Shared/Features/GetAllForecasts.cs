using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class GetAllForecasts
{
    public record Request : IRequest<PagedResult<WeatherForecastDto>>;

    public class Handler : IRequestHandler<Request, PagedResult<WeatherForecastDto>>
    {
        public readonly IRepository<WeatherForecastDto> _repository;

        public Handler(IRepository<WeatherForecastDto> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<WeatherForecastDto>> Handle(Request _, CancellationToken ct)
        {
            var forecasts = await _repository.GetAll(ct);
            return new PagedResult<WeatherForecastDto>(forecasts.ToArray(), 1, forecasts.Count, forecasts.Count, forecasts.Count, Array.Empty<string>());
        }
    }
}
