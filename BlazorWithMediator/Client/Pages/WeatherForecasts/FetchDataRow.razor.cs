using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Features;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace BlazorWithMediator.Client.Pages.WeatherForecasts;

public partial class FetchDataRow
{
    private FetchDataRowEditModel EditModel { get; } = new();
    private bool IsEditing { get; set; }
    private bool IsSubmitting { get; set; }

    [Inject] private IMediator Mediator { get; set; } = null!;
    [Parameter] public WeatherForecastDto Forecast { get; set; } = null!;

    protected override void OnParametersSet()
    {
        EditModel.Reset(Forecast);
    }

    private void Edit()
    {
        IsEditing = true;
    }

    private async Task Delete()
    {
        IsSubmitting = true;
        StateHasChanged();

        await Mediator.Send(new DeleteForecast.Request(Forecast.Id));

        IsSubmitting = false;
    }

    private void DiscardChanges()
    {
        IsEditing = false;
        EditModel.Reset(Forecast);
    }

    private async Task SaveChanges()
    {
        if (IsSubmitting)
            return;

        IsSubmitting = true;
        StateHasChanged();

        var request = new UpdateForecast.Request(Forecast.Id, EditModel.Date, EditModel.TemperatureC, EditModel.Summary);
        await Mediator.Send(request);

        IsSubmitting = false;
        IsEditing = false;
    }
}

public class FetchDataRowEditModel
{
    private int _tempC;
    private int _tempF;

    public DateTime Date { get; set; }
    public string? Summary { get; set; }
    public int TemperatureC
    {
        get => _tempC;
        set
        {
            _tempC = value;
            _tempF = 32 + (int)(value / 0.5556);
        }
    }
    public int TemperatureF
    {
        get => _tempF;
        set
        {
            _tempF = value;
            _tempC = (int)((value - 32) * 0.5556);
        }
    }

    public void Reset(WeatherForecastDto dto)
    {
        Date = dto.Date;
        Summary = dto.Summary;
        TemperatureC = dto.TemperatureC;
    }
}