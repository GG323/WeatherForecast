using System.Collections.Generic;
using WeatherForecast.Models;

namespace WeatherForecastTests;

public static class ModelFactory
{
    public static List<City> GetNumberOfCities(int numberOfCities)
    {
        var cities = new List<City>();
        for (var i = 1; i < numberOfCities + 1; i++)
        {
            var city = new City($"name{i}", $"latitude{i}", $"longitude{i}");
            cities.Add(city);
        }

        return cities;
    }

    public static WeatherForecast.Models.WeatherForecast GetForecastWithLocation(string conditionText1, string conditionText2)
    {

        return new WeatherForecast.Models.WeatherForecast
        {
            Forecast = new Forecast
            {
                ForecastDays = new List<ForecastDay>
                {
                    new()
                    {
                        Day = new Day
                        {
                            Condition = new Condition
                            {
                                Text = conditionText1
                            }
                        }
                    },
                    new()
                    {
                        Day = new Day
                        {
                            Condition = new Condition
                            {
                                Text = conditionText2
                            }
                        }
                    }
                }
            }
        };
    }
}
