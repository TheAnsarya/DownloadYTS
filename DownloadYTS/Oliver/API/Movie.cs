using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Oliver.API {
	class Movie {
		[JsonPropertyName("id")]
		int Id { get; set; }

		[JsonPropertyName("url")]
		string Url { get; set; }

		[JsonPropertyName("imdb_code")]
		string ImdbCode { get; set; }

		[JsonPropertyName("title")]
		string Title { get; set; }

		[JsonPropertyName("title_english")]
		string TitleEnglish { get; set; }

		[JsonPropertyName("title_long")]
		string TitleLong { get; set; }

		[JsonPropertyName("slug")]
		string Slug { get; set; }

		[JsonPropertyName("year")]
		int Year { get; set; }

		[JsonPropertyName("rating")]
		decimal Rating { get; set; }

		[JsonPropertyName("runtime")]
		int Runtime { get; set; }

		[JsonPropertyName("genres")]
		List<string> Genres { get; set; }

		[JsonPropertyName("summary")]
		string Summary { get; set; }

		[JsonPropertyName("description_full")]
		string DescriptionFull { get; set; }

		[JsonPropertyName("synopsis")]
		string Synopsis { get; set; }

		[JsonPropertyName("yt_trailer_code")]
		string YtTrailerCode { get; set; }

		[JsonPropertyName("language")]
		string Language { get; set; }
		
		[JsonPropertyName("mpa_rating")]
		string MpaRating { get; set; }

		[JsonPropertyName("background_image")]
		string BackgroundImage { get; set; }

		[JsonPropertyName("background_image_original")]
		string BackgroundImageOriginal { get; set; }

		[JsonPropertyName("small_cover_image")]
		string SmallCoverImage { get; set; }

		[JsonPropertyName("medium_cover_image")]
		string MediumCoverImage { get; set; }

		[JsonPropertyName("large_cover_image")]
		string LargeCoverImage { get; set; }

		[JsonPropertyName("state")]
		string State { get; set; }

		[JsonPropertyName("torrents")]
		List<TorrentInfo> Torrents { get; set; }

		[JsonPropertyName("date_uploaded")]
		string DateUploaded { get; set; }

		[JsonPropertyName("date_uploaded_unix")]
		long DateUploadedUnix { get; set; }
	}
}
