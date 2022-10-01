using BlazorWithMediator.Shared.Features;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace BlazorWithMediator.Client.Modals;

public partial class CreateForecastModal
{
    [Inject] private IMediator Mediator { get; set; } = null!;

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

        var request = new CreateForecast.Request
        (
            Model.Date,
            Model.TemperatureC,
            Model.Summary
        );

        var response = await Mediator.Send(request);

        if (response.IsSuccess)
            Close();

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
