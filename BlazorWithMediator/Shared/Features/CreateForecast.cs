using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class CreateForecast
{
    public static event EventHandler<WeatherForecastDto>? OnCreate;

    public record Request(DateTime Date, int TemperatureC, string? Summary) : IRequest<Result<WeatherForecastDto>>;

    public class Handler : IRequestHandler<Request, Result<WeatherForecastDto>>
    {
        private readonly IRepository<WeatherForecastDto> _repository;

        public Handler(IRepository<WeatherForecastDto> repository)
        {
            _repository = repository;
        }

        public async Task<Result<WeatherForecastDto>> Handle(Request request, CancellationToken ct)
        {
            var forecast = new WeatherForecastDto(default, request.Date, request.TemperatureC, request.Summary);
            var id = await _repository.Create(forecast, ct);

            forecast = forecast with { Id = id };

            OnCreate?.Invoke(this, forecast);
            return Result.Success(forecast);
        }
    }
}
