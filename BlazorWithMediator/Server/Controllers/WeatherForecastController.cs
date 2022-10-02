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
    public async Task<PagedResult<WeatherForecastDto>> GetAll(CancellationToken ct)
    {
        var request = new GetAllForecasts.Request();
        return await _mediator.Send(request, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<Result<WeatherForecastDto>> GetById(int id, CancellationToken ct)
    {
        var request = new GetForecastById.Request(id);
        return await _mediator.Send(request, ct);
    }       

    [HttpPost]
    public async Task<Result<WeatherForecastDto>> Create(CreateForecast.Request request, CancellationToken ct)
    {
        return await _mediator.Send(request, ct);
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateForecast.Request request, CancellationToken ct)
    {
        if (id != request.Id)
            throw new BadHttpRequestException($"Request id does not match url id.");

        await _mediator.Send(request, ct);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        var request = new DeleteForecast.Request(id);
        await _mediator.Send(request, ct);
    }
}
