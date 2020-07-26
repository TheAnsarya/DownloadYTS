using System.Text.Json.Serialization;

namespace Oliver.Domain {
	public class TorrentInfo : Entity {
		public string? Url { get; set; }

		public string? Hash { get; set; }

		public string? Quality { get; set; }

		public string? Type { get; set; }

		public string? Size { get; set; }

		public long SizeBytes { get; set; }

		public string? DateUploaded { get; set; }

		public long DateUploadedUnix { get; set; }

		public Movie? Movie { get; set; }

		public TorrentInfo() : base() { }

		public TorrentInfo(DTO.Yts.TorrentInfo dto, Movie movie) {
			Url = dto.Url;
			Hash = dto.Hash;
			Quality = dto.Quality;
			Type = dto.Type;
			Size = dto.Size;
			SizeBytes = dto.SizeBytes;
			DateUploaded = dto.DateUploaded;
			DateUploadedUnix = dto.DateUploadedUnix;

			Movie = movie;
		}
	}
}