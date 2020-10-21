using Oliver.DTO.Yts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Oliver.YtsService.API {
	public static class Call {
		private static readonly HttpClient Web = new HttpClient();


		// List Movies
		public static async Task<Response<ListMoviesData>> ListMovies(ListMoviesRequest? request) {
			// TODO: move to config file
			var endpoint = "https://yts.mx/api/v2/list_movies.json";

			// If not provided, use default request
			if (request == null) {
				request = new ListMoviesRequest();
			}

			//try {
			var url = $"{endpoint}{request.QueryString()}";
			var responseString = await Web.GetStringAsync(url);
			//Console.WriteLine(responseString);
			
			var options = new JsonSerializerOptions {
				PropertyNameCaseInsensitive = true,
			};

			var response = JsonSerializer.Deserialize<Response<ListMoviesData>>(responseString, options);
			return response;
			//} catch (HttpRequestException ex) {
			//	// TODO: maybe deal with this
			//	throw ex;
			//}
		}
	}
}
