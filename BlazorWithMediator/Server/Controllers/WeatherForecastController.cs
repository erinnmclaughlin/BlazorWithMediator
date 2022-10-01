using BlazorWithMediator.Server.Database;
using BlazorWithMediator.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorWithMediator.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherDbContext _context;

    public WeatherForecastController(WeatherDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<WeatherForecastDto>>> GetAll(CancellationToken ct)
    {
        var forecasts = await _context.WeatherForecasts.Select(x => x.ToDto()).ToListAsync(ct);
        return Ok(forecasts);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var forecast = await _context.WeatherForecasts.Where(x => x.Id == id).Select(x => x.ToDto()).FirstOrDefaultAsync(ct);
        return forecast == null ? NotFound() : Ok(forecast);
    }       

    [HttpPost]
    public async Task<ActionResult<WeatherForecastDto>> Create(CreateForecastRequest request, CancellationToken ct)
    {
        var forecast = new WeatherForecast
        {
            Date = request.Date,
            TemperatureC = request.TemperatureC,
            Summary = request.Summary
        };

        _context.WeatherForecasts.Add(forecast);
        await _context.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetById), new { id = forecast.Id }, forecast.ToDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateForecastRequest request, CancellationToken ct)
    {
        if (id != request.Id)
            return BadRequest();

        var forecast = await _context.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == id, ct);

        if (forecast == null)
            return NotFound();

        forecast.Date = request.Date;
        forecast.TemperatureC = request.TemperatureC;
        forecast.Summary = request.Summary;

        _context.WeatherForecasts.Update(forecast);
        await _context.SaveChangesAsync(ct);

        return Ok(forecast.ToDto());

    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var forecast = await _context.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == id, ct);

        if (forecast == null)
            return NotFound();

        _context.WeatherForecasts.Remove(forecast);
        await _context.SaveChangesAsync(ct);

        return NoContent();
    }
}
