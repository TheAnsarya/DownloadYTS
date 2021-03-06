﻿using Microsoft.EntityFrameworkCore;
using Oliver.Domain;

namespace Oliver.Data {
	public class OliverContext : DbContext {
		public OliverContext(DbContextOptions<OliverContext> options) : base(options) { }

		public DbSet<Movie> Movies { get; set; }

		public DbSet<TorrentInfo> TorrentInfos { get; set; }

		public DbSet<GenreString> GenreStrings { get; set; }

		public DbSet<TorrentFile> TorrentFiles { get; set; }
	}
}