﻿namespace BlazorWithMediator.Shared;

public record WeatherForecastDto(int Id, DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
