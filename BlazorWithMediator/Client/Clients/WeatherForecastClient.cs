using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Features;
using BlazorWithMediator.Shared.Services;
using System.Net.Http.Json;

namespace BlazorWithMediator.Client.Clients;

public class WeatherForecastClient : IWeatherForecastService
{
    private const string BaseUrl = "forecasts";
    private HttpClient Http { get; }

    public WeatherForecastClient(HttpClient http)
    {
        Http = http;
    }

    public async Task<List<WeatherForecastDto>> GetAll(CancellationToken ct)
    {
        var result = await Http.GetFromJsonAsync<PagedResult<WeatherForecastDto>>(BaseUrl, ct);
        return result!.Data.ToList();
    }

    public async Task<WeatherForecastDto> GetById(int id, CancellationToken ct)
    {
        var result = await Http.GetFromJsonAsync<Result<WeatherForecastDto>>($"{BaseUrl}/{id}", ct);
        return result!.Data!;
    }

    public async Task<int> Create(WeatherForecastDto forecast, CancellationToken ct)
    {
        var request = new CreateForecast.Request(forecast.Date, forecast.TemperatureC, forecast.Summary);
        var response = await Http.PostAsJsonAsync(BaseUrl, request, ct);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Result<WeatherForecastDto>>(cancellationToken: ct);
        return result!.Data!.Id;
    }

    public async Task Update(WeatherForecastDto forecast, CancellationToken ct)
    {
        var request = new UpdateForecast.Request(forecast.Id, forecast.Date, forecast.TemperatureC, forecast.Summary);
        var response = await Http.PutAsJsonAsync($"{BaseUrl}/{request.Id}", request, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task Delete(int id, CancellationToken ct)
    {
        var response = await Http.DeleteAsync($"{BaseUrl}/{id}", ct);
        response.EnsureSuccessStatusCode();
    }
}
