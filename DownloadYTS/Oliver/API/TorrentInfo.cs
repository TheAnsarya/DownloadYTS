using System.Text.Json.Serialization;

namespace Oliver.API {
	class TorrentInfo {
		[JsonPropertyName("url")]
		string Url { get; set; }

		[JsonPropertyName("hash")]
		string Hash { get; set; }

		[JsonPropertyName("quality")]
		string Quality { get; set; }

		[JsonPropertyName("type")]
		string Type { get; set; }

		[JsonPropertyName("seeds")]
		int Seeds { get; set; }

		[JsonPropertyName("peers")]
		int Peers { get; set; }

		[JsonPropertyName("size")]
		string Size { get; set; }

		[JsonPropertyName("size_bytes")]
		long SizeBytes { get; set; }

		[JsonPropertyName("date_uploaded")]
		string DateUploaded { get; set; }

		[JsonPropertyName("date_uploaded_unix")]
		long DateUploadedUnix { get; set; }
	}
}