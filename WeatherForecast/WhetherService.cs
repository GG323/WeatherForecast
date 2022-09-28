using WeatherForecast.Interfaces;
using WeatherForecast.Models;

namespace WeatherForecast;

public class WhetherService : IWhetherService
{
    private readonly IApiGateway apiGateway;

    public WhetherService(IApiGateway apiGateway)
    {
        this.apiGateway = apiGateway;
    }

    public List<string> GetWhetherForecastsForAllCities()
    {
        var cities = apiGateway.GetCitiesAsync().Result;

        return cities.AsParallel()
            .Select(GetWhetherForecastForCity)
            .ToList();
    }

    public string GetWhetherForecastForCity(City city)
    {
        var weatherForecast = apiGateway.GetWhetherForecastAsync(city.Latitude, city.Longitude).Result;

        var whetherConditionTexts = weatherForecast.ForecastDays
            .Select(f => f.ConditionText);

        return $"Processed city {city.Name} | {string.Join(" - ", whetherConditionTexts)}";
    }
}
