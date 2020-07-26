using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Oliver.Web.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class MovieListController : ControllerBase {
		private readonly ILogger<MovieListController> _logger;

		public MovieListController(ILogger<MovieListController> logger) {
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get() {
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}
