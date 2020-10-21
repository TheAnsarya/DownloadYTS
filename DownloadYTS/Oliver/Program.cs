using System;
using System.Threading.Tasks;
using Net.Torrent;

namespace Oliver {
	class Program {

		static string ListUrl = "https://yts.mx/api/v2/list_movies.json?limit=50&order_by=asc&page={0}";

		static async Task Main(string[] args) {


			var response = await API.Call.ListMovies(null);
			var i = 0;



			//Console.WriteLine("Hello World!");

			//var ser = new TorrentSerializer();

			//ser.Deserialize()

			/* Issues to address:
				Full download of all movie information from API
					SQLite

				Download of all non-downloaded torrents (all qualities, not just 1080)
				Occassional refresh/check of torrents?
				Updating movie information on refetch (track seeds and peers in separate table?)
				What filename format for torrent files?
				Conversion and matching of already downloaded torrent files
				Matching and hashing of already downloaded movies
				The ~folders files
					Verify against the torrents
					Can we condense them? Should we? 5745 is under 400mb
					At least hash and catalog everything
				Plex, is it all folders/files instead of a db?
					Check that PLEX api on github
				Tracking which ones have been downloaded
				Torrent file objects (datamodel)
				And torrent file parsing
				Find torrent client library so we can download/upload



			*/
		}
	}
}
