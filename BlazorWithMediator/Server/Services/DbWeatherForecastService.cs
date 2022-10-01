using BlazorWithMediator.Server.Entities;
using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Features;
using Microsoft.EntityFrameworkCore;

namespace BlazorWithMediator.Server.Services;

public class DbWeatherForecastService : IWeatherForecastService
{
    private WeatherDbContext Db { get; }

    public DbWeatherForecastService(WeatherDbContext db)
    {
        Db = db;
    }

    public async Task<CreateForecast.Response> Create(CreateForecast.Request request, CancellationToken ct)
    {
        var forecast = new WeatherForecast
        {
            Date = request.Date,
            TemperatureC = request.TemperatureC,
            Summary = request.Summary
        };

        Db.WeatherForecasts.Add(forecast);
        await Db.SaveChangesAsync(ct);

        return new CreateForecast.Response(forecast.Id, forecast.Date, forecast.TemperatureC, forecast.Summary);
    }

    public async Task<GetAllForecasts.Response> GetAll(CancellationToken ct)
    {
        var forecasts = await Db.WeatherForecasts
            .Select(x => new GetAllForecasts.WeatherForecastDto(x.Id, x.Date, x.TemperatureC, x.Summary))
            .AsNoTracking()
            .ToArrayAsync(ct);

        return new GetAllForecasts.Response(forecasts);
    }

    public async Task<GetForecastById.Response?> GetById(int id, CancellationToken ct)
    {
        return await Db.WeatherForecasts
            .Where(x => x.Id == id)
            .Select(x => new GetForecastById.Response(x.Id, x.Date, x.TemperatureC, x.Summary))
            .AsNoTracking()
            .FirstOrDefaultAsync(ct);
    }

    public async Task<UpdateForecast.Response> Update(UpdateForecast.Request request, CancellationToken ct)
    {
        var forecast = await Db.WeatherForecasts.FirstAsync(x => x.Id == request.Id, ct);

        forecast.Date = request.Date;
        forecast.TemperatureC = request.TemperatureC;
        forecast.Summary = request.Summary;

        await Db.SaveChangesAsync(ct);

        return new UpdateForecast.Response(forecast.Id, forecast.Date, forecast.TemperatureC, forecast.Summary);
    }
}
