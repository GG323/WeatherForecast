using WeatherForecast.Models;

namespace WeatherForecast.Interfaces;

public interface IWhetherService
{
    public List<string> GetWhetherForecastsForAllCities();
    public string GetWhetherForecastForCity(City city);
}
