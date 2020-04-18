using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Oliver.API {
	public class ResponseMeta {

		[JsonPropertyName("server_time")]
		public long ServerTime { get; set; }

		[JsonPropertyName("server_timezone")]
		public string? ServerTimezone { get; set; }

		[JsonPropertyName("api_version")]
		public int ApiVersion { get; set; }

		[JsonPropertyName("execution_time")]
		public string? ExecutionTime { get; set; }
	}
}
