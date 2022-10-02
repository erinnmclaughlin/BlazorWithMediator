using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class UpdateForecast
{
    public static event EventHandler<WeatherForecastDto>? OnUpdate;

    public record Request(int Id, DateTime Date, int TemperatureC, string? Summary) : IRequest<Result<WeatherForecastDto>>;

    public class Handler : IRequestHandler<Request, Result<WeatherForecastDto>>
    {
        private readonly IRepository<WeatherForecastDto> _repository;

        public Handler(IRepository<WeatherForecastDto> repository)
        {
            _repository = repository;
        }

        public async Task<Result<WeatherForecastDto>> Handle(Request request, CancellationToken ct)
        {
            var forecast = new WeatherForecastDto(request.Id, request.Date, request.TemperatureC, request.Summary);
            await _repository.Update(forecast, ct);
            OnUpdate?.Invoke(this, forecast);
            return Result.Success(forecast);
        }
    }
}