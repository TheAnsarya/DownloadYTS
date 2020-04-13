using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Oliver.API {
	class ListData {
		[JsonPropertyName("movie_count")]
		int MovieCount { get; set; }

		[JsonPropertyName("limit")]
		int Limit { get; set; }

		[JsonPropertyName("page_number")]
		int PageNumber { get; set; }

		[JsonPropertyName("movies")]
		List<Movie> Movies { get; set; }
	}
}
