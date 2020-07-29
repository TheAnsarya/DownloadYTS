using System;
using System.Collections.Generic;
using System.Text;

namespace Oliver.Domain {
	public class TorrentFile : StampedEntity {
		public TorrentInfo Info { get; set; }

		public string Filename { get; set; }

		public string FilePath { get; set; }

		public byte[] Content { get; set; }
	}
}
