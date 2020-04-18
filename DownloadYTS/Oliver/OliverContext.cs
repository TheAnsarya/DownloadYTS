using Microsoft.EntityFrameworkCore;
using Oliver.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oliver {
	class OliverContext : DbContext {
		public OliverContext() {
			this.Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseSqlite(connectionString: "filename=./Oliver.db");
		}

		public DbSet<Product> Products { get; set; }

		public DbSet<Movie> Movies { get; set; }

		public DbSet<TorrentInfo> TorrentInfos { get; set; }
	}

	public class Product {
		public int Id { get; set; }

		public string Name { get; set; }
	}
}
