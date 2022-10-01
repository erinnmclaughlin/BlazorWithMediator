using BlazorWithMediator.Client.Modals;
using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Features;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace BlazorWithMediator.Client.Pages.WeatherForecasts;

public partial class Index : IDisposable
{
    private CreateForecastModal? CreateModal;
    private List<WeatherForecastDto>? Forecasts { get; set; }

    [Inject] private IMediator Mediator { get; set; } = null!;

    public void Dispose()
    {
        CreateForecast.OnCreate -= HandleCreate;
        UpdateForecast.OnUpdate -= HandleUpdate;
        DeleteForecast.OnDelete -= HandleDelete;
    }

    protected override async Task OnInitializedAsync()
    {
        var response = await Mediator.Send(new GetAllForecasts.Request());
        Forecasts = response.Data?.ToList();

        CreateForecast.OnCreate += HandleCreate;
        UpdateForecast.OnUpdate += HandleUpdate;
        DeleteForecast.OnDelete += HandleDelete;
    }

    private void HandleCreate(object? _, WeatherForecastDto forecast)
    {
        Forecasts!.Add(forecast);
        StateHasChanged();
    }

    private void HandleUpdate(object? _, WeatherForecastDto forecast)
    {
        var forecastToReplace = Forecasts!.First(x => x.Id == forecast.Id);
        var index = Forecasts!.IndexOf(forecastToReplace);
        Forecasts[index] = forecast;
        StateHasChanged();
    }

    private void HandleDelete(object? _, int id)
    {
        Forecasts!.RemoveAll(x => x.Id == id);
        StateHasChanged();
    }
}
