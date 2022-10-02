using BlazorWithMediator.Shared.Common;
using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Features;
using System.Net.Http.Json;

namespace BlazorWithMediator.Client.Clients;

public class WeatherForecastClient : IRepository<WeatherForecastDto>
{
    private const string BaseUrl = "forecasts";

    private readonly HttpClient _httpClient;

    public WeatherForecastClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<WeatherForecastDto>> GetAll(CancellationToken ct)
    {
        var result = await _httpClient.GetFromJsonAsync<PagedResult<WeatherForecastDto>>(BaseUrl, ct);
        return result!.Data.ToList();
    }

    public async Task<WeatherForecastDto?> GetById(int id, CancellationToken ct)
    {
        var result = await _httpClient.GetFromJsonAsync<Result<WeatherForecastDto>>($"{BaseUrl}/{id}", ct);
        return result?.Data;
    }

    public async Task<int> Create(WeatherForecastDto forecast, CancellationToken ct)
    {
        var request = new CreateForecast.Request(forecast.Date, forecast.TemperatureC, forecast.Summary);
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, request, ct);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Result<WeatherForecastDto>>(cancellationToken: ct);
        return result!.Data!.Id;
    }

    public async Task Update(WeatherForecastDto forecast, CancellationToken ct)
    {
        var request = new UpdateForecast.Request(forecast.Id, forecast.Date, forecast.TemperatureC, forecast.Summary);
        var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{request.Id}", request, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task Delete(int id, CancellationToken ct)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}", ct);
        response.EnsureSuccessStatusCode();
    }
}
