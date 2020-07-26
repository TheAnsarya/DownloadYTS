using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Oliver.Domain {
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "DTO class")]
	public class GenreString : Entity {
		public string? Name { get; set; }

		public Movie? Movie { get; set; }

		public GenreString() : base() { }

		public GenreString(string name, Movie movie) {
			Name = name;
			Movie = movie;
		}
	}
}
