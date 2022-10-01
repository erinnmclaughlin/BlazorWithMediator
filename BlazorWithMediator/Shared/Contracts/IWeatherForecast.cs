namespace BlazorWithMediator.Shared.Contracts;

public interface IWeatherForecast
{
    int Id { get; }
    int TemperatureC { get; }
    string? Summary { get; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
