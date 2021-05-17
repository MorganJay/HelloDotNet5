using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HelloDotNet5
{
    public class WeatherClient
    {
        private readonly HttpClient httpClient;

        private readonly ServiceSettings settings;

        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            this.httpClient = httpClient;
            settings = options.Value;
        }

        public record Weather(string Description);

        public record Main(decimal Temp);
        public record Forecast(Weather[] Weather, Main Main, long Dt);

        public async Task<Forecast> GetCurrentWeatherAsync(string city)
        {
            var forecast = await httpClient.GetFromJsonAsync<Forecast>($"https://{settings.OpenWeatherHost}/data/2.5/weather?q={city}&appid={settings.ApiKey}&units=metric");
            return forecast;
        }
    }
}