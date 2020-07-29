using Microsoft.Extensions.Logging;
using NLog;
using Oliver.Data;
using Oliver.DTO.Yts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Oliver.YtsService {
	public class Actions {
		private readonly ILogger<Actions> _logger;

		public Actions(ILogger<Actions> logger) {
			_logger = logger;
		}

		public async Task<string> InitialGrab(OliverContext context) {
			var response = await API.Call.ListMovies(null);

			if (response.IsOK) {
				_logger.LogInformation($"Got response: {response}");
				var movies =
					response.Data.Movies
						.Select(x => new Domain.Movie(x))
						;

				await context.AddAsync(movies);

				await context.SaveChangesAsync();

			} else {
				_logger.LogError($"Response bad: {response}");
			}

			return "who knows";

		}
	}
}
