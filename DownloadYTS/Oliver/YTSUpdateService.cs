using Oliver.API;
using Oliver.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oliver {
	class YTSUpdateService {
		private OliverContext Context { get; set; }

		public YTSUpdateService(OliverContext context) {
			Context = context;
		}

		public async Task<bool> RefreshAllAsync() {
			//Context.
			var response = await API.Call.ListMovies(null);

			if (response.IsOK) {
				//response.Data.Movies



			} else {
				//error
			}

		}

		public void UpdateMovie(Movie movie) {
			var current = Context.Find<Movie>(movie.Id);

			if (current == null) {
				current = Context.Add(movie).Entity;
			} else {
			}



		}
	}
}
