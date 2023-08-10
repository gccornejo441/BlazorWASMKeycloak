using BlazorWASMKeycloak.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BlazorWASMKeycloak.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize (Roles ="User")]            //👈 new addition
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        HttpClient Http;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            // Get the token from the authorization header
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            var token = authorizationHeader.Substring("Bearer ".Length);


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}