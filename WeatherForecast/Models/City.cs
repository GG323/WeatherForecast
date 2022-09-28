using Newtonsoft.Json;

namespace WeatherForecast.Models;

public class City
{
    public City(string name, string latitude, string longitude)
    {
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
    }

    [JsonProperty("name")]
    public string Name { get; }

    [JsonProperty("latitude")]
    public string Latitude { get; }
    
    [JsonProperty("longitude")]
    public string Longitude { get; }

}
