using Microsoft.Extensions.DependencyInjection;

using WeatherForecast;
using WeatherForecast.Interfaces;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IWhetherService, WhetherService>()
    .AddSingleton<IApiGateway, ApiGateway>()
    .BuildServiceProvider();

var whetherService = serviceProvider.GetService<IWhetherService>();
var weatherForecasts = new List<string>();

try
{
    weatherForecasts = whetherService.GetWhetherForecastsForAllCities();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine(string.Join(Environment.NewLine, weatherForecasts));
