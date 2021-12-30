using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS_GameApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("DemoMethod/{fname}/{lname}")]
        public IActionResult MyDemoMethod(string fname, string lname)
        {
            return NotFound($"Congrats you, {fname} {lname}, got to the new method!");
        }

        [HttpPut]
        [Route("postaplayer/{id}")]
        public IActionResult PostAPlayer(Player p, int id)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine($"The id of the player is {id}");
                return Ok(p);
            }
            else
                return Conflict(p);

        }
    }
}
