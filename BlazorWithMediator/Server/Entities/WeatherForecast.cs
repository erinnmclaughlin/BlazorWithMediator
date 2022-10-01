using BlazorWithMediator.Shared;

namespace BlazorWithMediator.Server.Entities;

public class WeatherForecast
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}

public static class WeatherForecastMappings
{
    public static WeatherForecastDto ToDto(this WeatherForecast forecast)
    {
        return new WeatherForecastDto(forecast.Id, forecast.Date, forecast.TemperatureC, forecast.Summary);
    }
}