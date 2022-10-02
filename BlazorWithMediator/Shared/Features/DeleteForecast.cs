using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using MediatR;

namespace BlazorWithMediator.Shared.Features;

public static class DeleteForecast
{
    public static event EventHandler<int>? OnDelete;

    public record Request(int Id) : IRequest<Result>;

    public class Handler : IRequestHandler<Request, Result>
    {
        private readonly IRepository<WeatherForecastDto> _repository;

        public Handler(IRepository<WeatherForecastDto> repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
        {
            await _repository.Delete(request.Id, cancellationToken);
            OnDelete?.Invoke(this, request.Id);
            return Result.Success();
        }
    }
}
