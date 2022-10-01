namespace BlazorWithMediator.Shared;

public class CreateForecastRequest
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}
