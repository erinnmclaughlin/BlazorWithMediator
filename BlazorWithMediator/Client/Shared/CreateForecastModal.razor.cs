using BlazorWithMediator.Shared;
using Microsoft.AspNetCore.Components;

namespace BlazorWithMediator.Client.Shared;

public partial class CreateForecastModal
{
    [Parameter] public EventCallback<CreateForecastRequest> OnSubmit { get; set; }

    private bool IsOpen { get; set; }
    private bool IsSubmitting { get; set; }
    private CreateForecastModalEditModel Model { get; set; } = new();

    public void Open()
    {
        Model = new();
        IsOpen = true;
        StateHasChanged();
    }

    public void Close()
    {
        IsOpen = false;
        StateHasChanged();
    }

    private async Task Submit()
    {
        if (IsSubmitting)
            return;

        IsSubmitting = true;
        StateHasChanged();

        await OnSubmit.InvokeAsync(new CreateForecastRequest
        {
            Date = Model.Date,
            TemperatureC = Model.TemperatureC,
            Summary = Model.Summary
        });

        IsSubmitting = false;
        StateHasChanged();
    }
}

public class CreateForecastModalEditModel
{
    private int _tempC;
    private int _tempF;

    public DateTime Date { get; set; } = DateTime.Today;
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
}
