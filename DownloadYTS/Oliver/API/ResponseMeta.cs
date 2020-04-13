using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Oliver.API {
	class ResponseMeta {

		[JsonPropertyName("server_time")]
		long ServerTime { get; set; }

		[JsonPropertyName("server_timezone")]
		string ServerTimezone { get; set; }

		[JsonPropertyName("api_version")]
		int ApiVersion { get; set; }

		[JsonPropertyName("execution_time")]
		string ExecutionTime { get; set; }
	}
}
