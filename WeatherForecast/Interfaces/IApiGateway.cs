using WeatherForecast.Models;

namespace WeatherForecast.Interfaces;

public interface IApiGateway
{
    public Task<List<City>> GetCitiesAsync();
    public Task<Models.WeatherForecast> GetWhetherForecastAsync(string latitude, string longitude);
}
