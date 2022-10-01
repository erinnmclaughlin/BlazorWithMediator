using BlazorWithMediator.Server.Entities;
using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace BlazorWithMediator.Server.Services;

public class DbWeatherForecastService : IWeatherForecastService
{
    private WeatherDbContext Db { get; }

    public DbWeatherForecastService(WeatherDbContext db)
    {
        Db = db;
    }

    public async Task<List<WeatherForecastDto>> GetAll(CancellationToken ct)
    {
        return await Db.WeatherForecasts
            .Select(x => new WeatherForecastDto(x.Id, x.Date, x.TemperatureC, x.Summary))
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<WeatherForecastDto> GetById(int id, CancellationToken ct)
    {
        return await Db.WeatherForecasts
            .Where(x => x.Id == id)
            .Select(x => new WeatherForecastDto(x.Id, x.Date, x.TemperatureC, x.Summary))
            .AsNoTracking()
            .FirstAsync(ct);
    }

    public async Task<int> Create(WeatherForecastDto request, CancellationToken ct)
    {
        var entity = new WeatherForecast
        {
            Date = request.Date,
            TemperatureC = request.TemperatureC,
            Summary = request.Summary
        };

        Db.WeatherForecasts.Add(entity);
        await Db.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task Update(WeatherForecastDto request, CancellationToken ct)
    {
        var forecast = await Db.WeatherForecasts.FirstAsync(x => x.Id == request.Id, ct);

        forecast.Date = request.Date;
        forecast.TemperatureC = request.TemperatureC;
        forecast.Summary = request.Summary;

        await Db.SaveChangesAsync(ct);
    }

    public async Task Delete(int id, CancellationToken ct)
    {
        var forecast = await Db.WeatherForecasts.FirstAsync(x => x.Id == id, ct);
        Db.Remove(forecast);
        await Db.SaveChangesAsync(ct);
    }
}
