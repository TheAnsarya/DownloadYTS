using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Oliver.API {
	class Response<T> {
		[JsonPropertyName("status")]
		string Status { get; set; }

		[JsonPropertyName("status_message")]
		string StatusMessage { get; set; }

		[JsonPropertyName("data")]
		T Data { get; set; }

		[JsonPropertyName("@meta")]
		ResponseMeta Meta { get; set; }
		
	}
	}
}
