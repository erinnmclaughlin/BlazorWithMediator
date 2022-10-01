using BlazorWithMediator.Client;
using BlazorWithMediator.Client.Clients;
using BlazorWithMediator.Shared;
using BlazorWithMediator.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastClient>();
builder.Services.AddMediatR(typeof(Program), typeof(SharedAssembly));


await builder.Build().RunAsync();
