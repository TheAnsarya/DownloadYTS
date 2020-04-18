using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Oliver.API {
	public class Response<T> where T : class {
		[JsonPropertyName("status")]
		public string? Status { get; set; }

		[JsonPropertyName("status_message")]
		public string? StatusMessage { get; set; }

		[JsonPropertyName("data")]
		public T? Data { get; set; }

		[JsonPropertyName("@meta")]
		public ResponseMeta? Meta { get; set; }
	}
}
