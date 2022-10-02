using BlazorWithMediator.Server.Entities;
using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BlazorWithMediator.Server.Services;

public class WeatherForecastRepository : IRepository<WeatherForecastDto>
{
    private readonly WeatherDbContext _context;

    public WeatherForecastRepository(WeatherDbContext context)
    { 
        _context = context;
    }

    public async Task<List<WeatherForecastDto>> GetAll(CancellationToken ct)
    {
        return await _context.WeatherForecasts
            .Select(x => new WeatherForecastDto(x.Id, x.Date, x.TemperatureC, x.Summary))
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<WeatherForecastDto?> GetById(int id, CancellationToken ct)
    {
        return await _context.WeatherForecasts
            .Where(x => x.Id == id)
            .Select(x => new WeatherForecastDto(x.Id, x.Date, x.TemperatureC, x.Summary))
            .AsNoTracking()
            .FirstOrDefaultAsync(ct);
    }

    public async Task<int> Create(WeatherForecastDto request, CancellationToken ct)
    {
        var entity = new WeatherForecast
        {
            Date = request.Date,
            TemperatureC = request.TemperatureC,
            Summary = request.Summary
        };

        _context.WeatherForecasts.Add(entity);
        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task Update(WeatherForecastDto request, CancellationToken ct)
    {
        var forecast = await _context.WeatherForecasts.FirstAsync(x => x.Id == request.Id, ct);

        forecast.Date = request.Date;
        forecast.TemperatureC = request.TemperatureC;
        forecast.Summary = request.Summary;

        await _context.SaveChangesAsync(ct);
    }

    public async Task Delete(int id, CancellationToken ct)
    {
        var forecast = await _context.WeatherForecasts.FirstAsync(x => x.Id == id, ct);

        _context.Remove(forecast);
        await _context.SaveChangesAsync(ct);
    }
}
