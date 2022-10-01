using BlazorWithMediator.Shared.Contracts;
using BlazorWithMediator.Shared.Features;
using System.Net.Http.Json;

namespace BlazorWithMediator.Client.Services;

public class HttpWeatherForecastService : IWeatherForecastService
{
	private const string BaseUrl = "forecasts";
    private HttpClient Http { get; }

	public HttpWeatherForecastService(HttpClient http)
	{
		Http = http;
	}

	public async Task<CreateForecast.Response> Create(CreateForecast.Request request, CancellationToken ct)
	{
        var response = await Http.PostAsJsonAsync(BaseUrl, request, ct);
        response.EnsureSuccessStatusCode();

		var createdForecast = await response.Content.ReadFromJsonAsync<CreateForecast.Response>(cancellationToken: ct);
		return createdForecast!;
    }

	public async Task<UpdateForecast.Response> Update(UpdateForecast.Request request, CancellationToken ct)
	{
        var response = await Http.PutAsJsonAsync($"{BaseUrl}/{request.Id}", request, ct);
        response.EnsureSuccessStatusCode();

		var updatedForecast = await response.Content.ReadFromJsonAsync<UpdateForecast.Response>(cancellationToken: ct);
		return updatedForecast!;
    }

	public async Task<GetAllForecasts.Response> GetAll(CancellationToken ct)
	{
		var forecasts = await Http.GetFromJsonAsync<GetAllForecasts.Response>(BaseUrl, ct);
		return forecasts!;
	}

	public async Task<GetForecastById.Response> GetById(int id, CancellationToken ct)
	{
		var forecast = await Http.GetFromJsonAsync<GetForecastById.Response>($"{BaseUrl}/{id}", ct);
		return forecast!;
	}
}
