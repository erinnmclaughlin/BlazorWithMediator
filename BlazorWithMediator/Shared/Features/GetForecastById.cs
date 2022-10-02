using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class GetForecastById
{
    public record Request(int Id) : IRequest<Result<WeatherForecastDto>>;

    public class Handler : IRequestHandler<Request, Result<WeatherForecastDto>>
    {
        private readonly IRepository<WeatherForecastDto> _repository;

        public Handler(IRepository<WeatherForecastDto> repository)
        {
            _repository = repository;
        }

        public async Task<Result<WeatherForecastDto>> Handle(Request request, CancellationToken ct)
        {
            var forecast = await _repository.GetById(request.Id, ct);

            if (forecast == null)
                return Result.Fail(forecast, $"Forecast with id {request.Id} was not found.");

            return Result.Success(forecast);
        }
    }
}
