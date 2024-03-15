using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UiTests.Lib.Config;

public class ConfigLoader {
    public static Dictionary<string, string> loadConfig() {
        string fileName = "WeatherForecast.json";
        string jsonString = File.ReadAllText(fileName);
        var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        // WeatherForecast weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(jsonString)!;

        throw new Exception("not implemented");
    }
    
    
    
    
}