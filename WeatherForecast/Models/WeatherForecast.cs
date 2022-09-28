using Newtonsoft.Json;

namespace WeatherForecast.Models;

public class WeatherForecast
{
    [JsonProperty("forecast")]
    public Forecast Forecast { get; set; }

    public List<ForecastDay> ForecastDays =>
        Forecast.ForecastDays;
}

public class Forecast
{
    [JsonProperty("forecastday")]
    public List<ForecastDay> ForecastDays { get; set; }
}

public class ForecastDay
{
    [JsonProperty("day")]
    public Day Day { get; set; }

    public string ConditionText =>
        Day.Condition.Text;
}

public class Day
{
    [JsonProperty("condition")]
    public Condition Condition { get; set; }
}

public class Condition
{
    [JsonProperty("text")]
    public string Text { get; set; }
}
