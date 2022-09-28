using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

using WeatherForecast.Interfaces;
using WeatherForecast.Models;

namespace WeatherForecast;

public class ApiGateway : IApiGateway
{
    //This all should be in .config file, but I just must draw a line somewhere
    private const string MusementApiUri = "https://api.musement.com/api/v3/cities";
    private const string WeatherApiUri = "http://api.weatherapi.com/v1/forecast.json";
    private const string MyWeatherApiKey = "e6183a7c363b48f3ae2100133222109";
    private const int WeatherForecastDays = 2;

    public async Task<List<City>> GetCitiesAsync()
    {
        var content = await new HttpClient().GetStringAsync(MusementApiUri);

        return JsonConvert.DeserializeObject<List<City>>(content);
    }

    public async Task<Models.WeatherForecast> GetWhetherForecastAsync(string latitude, string longitude)
    {
        var param = new Dictionary<string, string>
        {
            { "key", MyWeatherApiKey },
            { "days", WeatherForecastDays.ToString() },
            { "q", $"{latitude},{longitude}" },
            { "aqi", "no" },
            { "alerts", "no" }
        };
        var uri = QueryHelpers.AddQueryString(WeatherApiUri, param);
        var content = await new HttpClient().GetStringAsync(uri);

        return JsonConvert.DeserializeObject<Models.WeatherForecast>(content);
    }
}
