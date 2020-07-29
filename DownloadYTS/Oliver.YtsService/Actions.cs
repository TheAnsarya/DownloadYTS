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

		private readonly OliverContext _context;

		public Actions(OliverContext context, ILogger<Actions> logger) {
			_logger = logger;
			_context = context;
		}

		public async Task<string> InitialGrab() {
			var response = await API.Call.ListMovies(null);

			if (response.IsOK) {
				_logger.LogInformation($"Got response: {response}");
				var movies =
					response.Data.Movies
						.Select(x => new Domain.Movie(x))
						;

				await _context.AddAsync(movies);

				await _context.SaveChangesAsync();

			} else {
				_logger.LogError($"Response bad: {response}");
			}

			return "who knows";

		}
	}
}
