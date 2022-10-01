using BlazorWithMediator.Shared.Exceptions;
using BlazorWithMediator.Shared.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWithMediator.Server.Controllers;

[ApiController, Route("forecasts")]
public class WeatherForecastController : ControllerBase
{
    private IMediator Mediator { get; }

    public WeatherForecastController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet]
    public async Task<GetAllForecasts.Response> GetAll(CancellationToken ct)
    {
        var request = new GetAllForecasts.Request();
        return await Mediator.Send(request, ct);
    }

    [HttpGet("{id:int}")]
    public async Task<GetForecastById.Response> GetById(int id, CancellationToken ct)
    {
        var request = new GetForecastById.Request(id);
        return await Mediator.Send(request, ct);        
    }       

    [HttpPost]
    public async Task<CreateForecast.Response> Create(CreateForecast.Request request, CancellationToken ct)
    {
        return await Mediator.Send(request, ct);
    }

    [HttpPut("{id:int}")]
    public async Task<UpdateForecast.Response> Update(int id, UpdateForecast.Request request, CancellationToken ct)
    {
        if (id != request.Id)
            throw new BadRequestException($"Request id does not match url id.");

        return await Mediator.Send(request, ct);

    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id, CancellationToken ct)
    {
        var request = new DeleteForecast.Request(id);
        await Mediator.Send(request, ct);
    }
}
