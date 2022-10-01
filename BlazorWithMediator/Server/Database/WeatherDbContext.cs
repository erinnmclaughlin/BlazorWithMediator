using Microsoft.EntityFrameworkCore;

namespace BlazorWithMediator.Server.Database;

public class WeatherDbContext : DbContext
{
	public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

	public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
	{
		Database.EnsureCreated();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<WeatherForecast>()
			.HasData
			(
				new WeatherForecast { Id = 1, Date = new DateTime(2022, 10, 1), TemperatureC = 16, Summary = "Rainy" },
				new WeatherForecast { Id = 2, Date = new DateTime(2022, 10, 2), TemperatureC = 14, Summary = "Rainy" },
				new WeatherForecast { Id = 3, Date = new DateTime(2022, 10, 3), TemperatureC = 16, Summary = "Partly Cloudy" },
				new WeatherForecast { Id = 4, Date = new DateTime(2022, 10, 4), TemperatureC = 18, Summary = "Partly Cloudy" },
				new WeatherForecast { Id = 5, Date = new DateTime(2022, 10, 5), TemperatureC = 22, Summary = "Sunny" },
				new WeatherForecast { Id = 6, Date = new DateTime(2022, 10, 6), TemperatureC = 24, Summary = "Sunny" },
				new WeatherForecast { Id = 7, Date = new DateTime(2022, 10, 7), TemperatureC = 21, Summary = "Sunny" },
				new WeatherForecast { Id = 8, Date = new DateTime(2022, 10, 8), TemperatureC = 16, Summary = "Sunny" },
				new WeatherForecast { Id = 9, Date = new DateTime(2022, 10, 9), TemperatureC = 17, Summary = "Sunny" },
				new WeatherForecast { Id = 10, Date = new DateTime(2022, 10, 10), TemperatureC = 17, Summary = "Sunny" }
			);
	}
}
