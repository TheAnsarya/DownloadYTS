using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ManageYTS {
	class Program {
		private static string DBFilename = @"c:\working\yts\yts.sqlite";
		private static MD5 md5 = MD5.Create();

		static void Main(string[] args) {
			Console.WriteLine("Creating DB");
			//CreateDB();

			Console.WriteLine(@"HashAFolder   h:\yts\");
			Console.WriteLine();
			HashAFolder(@"h:\yts\");

			Console.WriteLine();
			Console.WriteLine("Done!");
			Console.ReadKey();
		}

		public static void CreateDB() {
			SQLiteConnection.CreateFile(DBFilename);
			var sql = "CREATE TABLE MovieHashes (FileName varchar(2000), FullPath varchar(2000), Size int, MD5 varchar(32))";

			using var connection = new SQLiteConnection($"Data Source={DBFilename};Version=3;");
			connection.Open();
			var command = new SQLiteCommand(sql, connection);
			command.ExecuteNonQuery();

			connection.Close();
		}

		public static void HashAFolder(string folderPath, bool replace = false) {
			using var connection = new SQLiteConnection($"Data Source={DBFilename};Version=3;");
			connection.Open();
			var sql = "INSERT INTO MovieHashes (FileName, FullPath, Size, MD5) values (@filename, @fullpath, @size, @md5)";
			var command = new SQLiteCommand(sql, connection);

			command.Parameters.Add("@filename", DbType.String);
			command.Parameters.Add("@fullpath", DbType.String);
			command.Parameters.Add("@size", DbType.Int64);
			command.Parameters.Add("@md5", DbType.String);

			var dir = Directory.GetFiles(folderPath);

			foreach (var filepath in dir) {
				Console.WriteLine($"{ClockStamp()} File path: {filepath}");
				var filename = Path.GetFileName(filepath);
				Console.WriteLine($"{ClockStamp()} File name: {filename}");
				var size = new FileInfo(filepath).Length;
				Console.WriteLine($"{ClockStamp()} File size: {size}");
				var hash = FileToMD5(filepath);
				Console.WriteLine($"{ClockStamp()} File hash: {hash}");

				
				command.Parameters["@filename"].Value = filename;
				command.Parameters["@fullpath"].Value = filepath;
				command.Parameters["@size"].Value = size;
				command.Parameters["@md5"].Value = hash;

				command.ExecuteNonQuery();
				Console.WriteLine($"{ClockStamp()} -------------------------------------------------------");
			}

			connection.Close();
		}

		static string FileToMD5(string filename) {
			FileStream fs = File.Open(filename, FileMode.Open);
			byte[] crc = md5.ComputeHash(fs);
			StringBuilder sb = new StringBuilder();
			for (int k = 0; k < crc.Length; k++) {
				sb.Append(crc[k].ToString("x2"));
			}
			string hash = sb.ToString();
			fs.Close();

			return hash;
		}

		static Stopwatch _clock;
		static Stopwatch Clock {
			get {
				if (_clock == null) {
					_clock = new Stopwatch();
					_clock.Start();
				}
				return _clock;
			}
		}
		static string ClockStamp() {
			TimeSpan delta = Clock.Elapsed;
			string stamp = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", delta.Hours, delta.Minutes, delta.Seconds, delta.Milliseconds / 10);
			return stamp;
		}
	}
}
