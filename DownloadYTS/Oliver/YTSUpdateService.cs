using Oliver.API;
using Oliver.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oliver {
	class YTSUpdateService {

		//public void UpdateMovie(Movie movie) {
		//	var current = Context.Find<Movie>(movie.Id);

		//	if (current == null) {
		//		current = Context.Add(movie).Entity;
		//	} else {
		//		if (current.
		//	}




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
