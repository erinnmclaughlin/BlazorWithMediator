using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWithMediator.Server.Controllers;

[ApiController, Route("forecasts")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var request = new GetAllForecasts.Request();
        var response = await _mediator.Send(request, ct);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var request = new GetForecastById.Request(id);
        var response = await _mediator.Send(request, ct);

        return response.Data == null ? NotFound() : Ok(response);
    }       

    [HttpPost]
    public async Task<IActionResult> Create(CreateForecast.Request request, CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateForecast.Request request, CancellationToken ct)
    {
        if (id != request.Id)
            return BadRequest($"Request id does not match url id.");

        await _mediator.Send(request, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var request = new DeleteForecast.Request(id);
        await _mediator.Send(request, ct);
        return NoContent();
    }
}
