using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Features;
using MediatR;

namespace BlazorWithMediator.Client.Pages.WeatherForecasts;

public class StateContainer :
    INotificationHandler<CreateForecast.ForecastCreated>,
    INotificationHandler<DeleteForecast.ForecastDeleted>,
    INotificationHandler<UpdateForecast.ForecastUpdated>
{
    public static event EventHandler<WeatherForecastDto>? OnCreate;
    public static event EventHandler<WeatherForecastDto>? OnUpdate;
    public static event EventHandler<int>? OnDelete;

    public Task Handle(CreateForecast.ForecastCreated notification, CancellationToken cancellationToken)
    {
        OnCreate?.Invoke(this, notification.Forecast);
        return Task.CompletedTask;
    }

    public Task Handle(DeleteForecast.ForecastDeleted notification, CancellationToken cancellationToken)
    {
        OnDelete?.Invoke(this, notification.Id);
        return Task.CompletedTask;
    }

    public Task Handle(UpdateForecast.ForecastUpdated notification, CancellationToken cancellationToken)
    {
        OnUpdate?.Invoke(this, notification.Forecast);
        return Task.CompletedTask;
    }
}
