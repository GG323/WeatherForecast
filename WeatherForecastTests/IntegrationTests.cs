using System.Linq;
using Castle.Core.Internal;
using NUnit.Framework;
using WeatherForecast;
using WeatherForecast.Interfaces;

namespace WeatherForecastTests;

internal class IntegrationTests
{
    private IWhetherService whetherService;
    private IApiGateway apiGateway;

    [SetUp]
    public void Setup()
    {
        apiGateway = new ApiGateway();
        whetherService = new WhetherService(apiGateway);
    }

    [Test]
    public void GetCitiesAsync_ReturnsValidResponse()
    {
        var cities = apiGateway.GetCitiesAsync().Result;
        var city = cities.FirstOrDefault();

        Assert.IsFalse(city?.Name.IsNullOrEmpty());

        Assert.IsFalse(city?.Latitude.IsNullOrEmpty());
        Assert.IsTrue(double.TryParse(city?.Latitude, out double d));

        Assert.IsFalse(city?.Longitude.IsNullOrEmpty());
        Assert.IsTrue(double.TryParse(city?.Longitude, out double o));
    }

    [Test]
    public void GetWhetherForecastAsync_ReturnsValidResponse()
    {
        const string latitude = "41.162";
        const string longitude = "-8.623";

        var whetherForecast = apiGateway.GetWhetherForecastAsync(latitude, longitude).Result;
        var whetherConditionTexts = whetherForecast.ForecastDays
            .Select(f => f.ConditionText).ToList();

        Assert.AreEqual(2, whetherConditionTexts.Count);

        Assert.IsFalse(string.IsNullOrEmpty(whetherConditionTexts[0]));
        Assert.IsFalse(string.IsNullOrEmpty(whetherConditionTexts[1]));
    }

    [Test]
    public void GetWhetherForecastsForAllCities_ReturnsValidResponse()
    {
        var whetherForecasts = whetherService.GetWhetherForecastsForAllCities();

        var whetherForecast = whetherForecasts.FirstOrDefault();
        var cityNameAndForecastParts = whetherForecast?.Split('|');
        var cityNamePart = cityNameAndForecastParts?[0];
        var forecastParts = cityNameAndForecastParts?[1].Split('-');

        Assert.IsFalse(string.IsNullOrEmpty(whetherForecast));

        Assert.IsTrue(cityNamePart?.StartsWith("Processed city "));
        Assert.IsTrue(cityNamePart?.Length > 16);

        Assert.AreEqual(2, forecastParts?.Length);
        Assert.IsTrue(forecastParts?[0].Length > 2);
        Assert.IsTrue(forecastParts?[1].Length > 2);
    }
}
