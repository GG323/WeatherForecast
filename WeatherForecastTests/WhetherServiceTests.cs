using System.Linq;
using FakeItEasy;
using NUnit.Framework;

using WeatherForecast;
using WeatherForecast.Interfaces;
using WeatherForecast.Models;

namespace WeatherForecastTests;

public class WhetherServiceTests
{
    private IWhetherService whetherService;
    private IApiGateway apiGatewayFake;
    private readonly string conditionText1 = "conditionText1";
    private readonly string conditionText2 = "conditionText2";

    [SetUp]
    public void Setup()
    {
        apiGatewayFake = A.Fake<IApiGateway>();

        A.CallTo(() => apiGatewayFake.GetWhetherForecastAsync(A<string>._, A<string>._))
            .Returns(ModelFactory.GetForecastWithLocation(conditionText1, conditionText2));

        whetherService = new WhetherService(apiGatewayFake);
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void GetWhetherForecastForAllCities_ReturnNumberOfForecasts_EqualToNumberOfCities(int numberOfCities)
    {
        FakeGetCitiesAsync(numberOfCities);

        var whetherForecasts = whetherService.GetWhetherForecastsForAllCities();

        Assert.AreEqual(numberOfCities, whetherForecasts.Count);
    }

    [Test]
    public void GetWhetherForecastForAllCities_ReturnExpectedForecastText()
    {
        const string firstCitiesName = "name1";
        var expectedForecast = $"Processed city {firstCitiesName} | {conditionText1} - {conditionText2}";

        FakeGetCitiesAsync(1);

        var whetherForecasts = whetherService.GetWhetherForecastsForAllCities();

        Assert.AreEqual(expectedForecast, whetherForecasts.First());
    }

    [Test]
    public void GetWhetherForecastForCity_ReturnExpectedForecastText()
    {
        const string cityName = "cityName";
        var expectedForecast = $"Processed city {cityName} | {conditionText1} - {conditionText2}";

        var city = new City(cityName, "latitude", "longitude");

        var whetherForecasts = whetherService.GetWhetherForecastForCity(city);

        Assert.AreEqual(expectedForecast, whetherForecasts);
    }

    private void FakeGetCitiesAsync(int numberOfCities)
    {
        var cities = ModelFactory.GetNumberOfCities(numberOfCities);
        A.CallTo(() => apiGatewayFake.GetCitiesAsync())
            .Returns(cities);
    }
}
