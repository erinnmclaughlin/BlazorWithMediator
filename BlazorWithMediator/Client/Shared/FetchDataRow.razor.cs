using BlazorWithMediator.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorWithMediator.Client.Shared;

public partial class FetchDataRow
{
    [Parameter] public WeatherForecastDto Forecast { get; set; } = null!;
    [Parameter] public EventCallback<UpdateForecastRequest> OnRequestUpdate { get; set; }
    [Parameter] public EventCallback<int> OnRequestDelete { get; set; }

    private FetchDataRowEditModel EditModel { get; } = new();
    private bool IsEditing { get; set; }
    private bool IsSubmitting { get; set; }

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

        await OnRequestDelete.InvokeAsync(Forecast.Id);

        IsSubmitting = false;
    }

    private void DiscardChanges()
    {
        IsEditing = false;
        EditModel.Reset(Forecast);
    }

    private async Task SaveChanges()
    {
        IsSubmitting = true;
        StateHasChanged();

        await Task.Delay(500);
        await OnRequestUpdate.InvokeAsync(new UpdateForecastRequest
        {
            Id = Forecast.Id,
            Date = EditModel.Date,
            Summary = EditModel.Summary,
            TemperatureC = EditModel.TemperatureC
        });

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