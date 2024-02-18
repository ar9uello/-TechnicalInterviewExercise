using Application.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Api.Tests
{
    public class Tests
    {
        [TestFixture]
        public class WeatherForecastControllerTests
        {
            private WebApplicationFactory<Program> _factory;
            private HttpClient _client;

            [SetUp]
            public void Setup()
            {
                _factory = new WebApplicationFactory<Program>();
                _client = _factory.CreateClient();
            }

            [TearDown]
            public void TearDown()
            {
                _client.Dispose();
                _factory.Dispose();
            }

            [Test]
            public async Task Get_ReturnsWeatherForecasts()
            {
                // Act
                var response = await _client.GetAsync("/WeatherForecast");

                // Assert
                response.EnsureSuccessStatusCode(); 
                var forecasts = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
                Assert.That(forecasts?.Length, Is.EqualTo(5)); 
            }
        }
    }
}