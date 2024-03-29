﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DownloadYTS {
	class Program {
		static string LogFileName = @"c:\working\yts\{now}\log.txt";
		static string ErrorFileName = @"c:\working\yts\{now}\error.txt";
		static string StartURL = @"https://yts.mx/browse-movies?page=1";
		static string StartFileName = @"c:\working\yts\{now}\start.txt";
		static string ListURL = @"https://yts.mx/browse-movies?page={0}";
		static string ListFileName = @"c:\working\yts\{now}\list\list-{0}.txt";
		static string PageFileName = @"c:\working\yts\{now}\page\page-{0}-{1}.txt";
		static string TorrentFileName = @"c:\working\yts\{now}\torrent\{0}-{1}-{2}.torrent";
		static string TorrentFileNameSecond = @"c:\working\yts\{now}\torrent\{0} [{1}].torrent";

		static string DiagnosticNow = @"2017-12-17_11-41-23-1358";

		static string NowString = Now();

		static int PerPage = 20;
		static int StartOnPage = 1;

		static Regex listRegex = new Regex(@"<a href=""(.+)"" class=""browse-movie-title"">(?:<span style=""color: #ACD7DE; font-size: 75%;"">\[[A-Za-z]{1,3}\]</span>)?(?: )?(.+)</a>\s+<div class=""browse-movie-year"">(.+)</div>");
		static Regex lastRegex = new Regex(@"<a href=""/browse-movies\?page=(\d+)"">Last &raquo;</a>");

		// <em class="pull-left">Available in: &nbsp;<\/em>\s+(?:<a href="(.+)" rel="nofollow" title=".+">(.+)<\/a>\s*)+
		static Regex pageRegex = new Regex(@"<em class=""pull-left"">Available in: &nbsp;<\/em>\s+(?:<a href=""(.+)"" rel=""nofollow"" title="".+"">(.+)<\/a>\s*)+");
		static Regex filenameRegex = new Regex(@"[\*\.""\/\?\\:;|=,]");

		static System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

		static StreamWriter _logFile;
		static StreamWriter LogFile {
			get {
				if (_logFile == null) {
					_logFile = File.CreateText(string.Format(LogFileName, Now()));
				}
				return _logFile;
			}
		}

		static StreamWriter _errorFile;
		static StreamWriter ErrorFile {
			get {
				if (_errorFile == null) {
					_errorFile = File.CreateText(string.Format(ErrorFileName, Now()));
				}
				return _errorFile;
			}
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

		static Random _rng;
		static Random RNG {
			get {
				if (_rng == null) {
					_rng = new Random();
				}
				return _rng;
			}
		}

		static void Main(string[] args) {
			DownloadYTS();
			//DownloadTypes();
		}

		static void DownloadYTS() {
			FixFileNames();
			Directories();

			Log("START");

			WebClient web = new WebClient();

			string startName = string.Format(StartFileName, Now());
			Log("start file - " + startName);
			web.DownloadFile(StartURL, startName);
			Wait();

			string text = File.ReadAllText(startName);
			MatchCollection startMatches = lastRegex.Matches(text);
			Log("start match count - " + startMatches.Count);

			if (startMatches.Count == 0) {
				Error("No last link");
			} else {
				int lastPage = int.Parse(startMatches[0].Groups[1].Value);
				Log("last number - " + lastPage);

				string lastName = string.Format(ListFileName, lastPage);
				Log("last file - " + lastName);
				web.DownloadFile(StartURL, lastName);
				Wait();

				text = File.ReadAllText(lastName);
				MatchCollection lastMatches = lastRegex.Matches(text);
				Log("last match count - " + lastMatches.Count);

				if (lastMatches.Count == 0) {
					Error("No last matches");
				} else {
					int total = ((lastPage - 1) * PerPage) + lastMatches.Count;
					Log("total - " + total);
					int current = total - ((StartOnPage - 1) * PerPage);
					Log("start with - " + current);

					for (int i = StartOnPage; i < lastPage; i++) {// 2; i++) {
						string listName = string.Format(ListFileName, i);
						string listUrl = string.Format(ListURL, i);
						Log("list file - " + listName);
						web.DownloadFile(listUrl, listName);
						Wait();

						text = File.ReadAllText(listName);
						MatchCollection listMatches = listRegex.Matches(text);
						Log("list match count - " + listMatches.Count);

						if (listMatches.Count == 0) {
							Error("No list matches");
						} else {
							for (int j = 0; j < listMatches.Count; j++) {
								bool go = true;
								string pageUrl = listMatches[j].Groups[1].Value;
								Log("page url - " + pageUrl);

								string name = filenameRegex.Replace(listMatches[j].Groups[2].Value, "-");
								string year = listMatches[j].Groups[3].Value;
								string title = name + " (" + year + ")";

								string pageName = string.Format(PageFileName, current, title);
								Log("page name - " + pageName);
								try {
									web.DownloadFile(pageUrl, pageName);
								} catch (Exception ex) {
									Error("bad fetch page - " + pageName + " - " + pageUrl);
									go = false;
								}

								if (go) {
									Wait();

									text = File.ReadAllText(pageName);
									MatchCollection pageMatches = pageRegex.Matches(text);
									Log("page match count - " + pageMatches.Count);

									if (pageMatches.Count == 0) {
										Error("No page matches - " + pageName + " - " + pageUrl);
									} else {
										string torrentUrl = pageMatches[0].Groups[pageMatches[0].Groups.Count - 2].Value;
										//string x1080 = null;
										//string x720 = null;
										//for (int i = 0; i < pageMatches[0].Groups[2].Captures.Count) {
										//	if ()
										//}
										//string torrentUrl = pageMatches[0].Groups[2].Captures.
										string torrentName = string.Format(TorrentFileName, i, current, title);

										Log("torrent file - " + torrentName + " - " + torrentUrl);

										try {
											web.DownloadFile(torrentUrl, torrentName);
										} catch (Exception ex) {
											Error("bad fetch torrent - " + torrentName + " - " + torrentUrl + " - " + ex.ToString());
										}

										try {
											string hash = FileToMD5(torrentName);
											string otherName = string.Format(TorrentFileNameSecond, title, hash);

											File.Move(torrentName, otherName);
										} catch (Exception ex) {
											Error("error changing md5 name - " + torrentName + " - " + torrentUrl + " - " + ex.ToString());
										}



										Wait();
									}
								}

								current--;
							}
						}
					}
				}
			}

			End();
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

		static void DownloadTypes() {

			string dir = Path.GetDirectoryName(PageFileName.Replace("{now}", NowString));
			FixFileNames();
			Directories();
			Log("START");
			Dictionary<string, int> counts = new Dictionary<string, int>();

			var pageNames = Directory.EnumerateFiles(dir);
			Log("page count - " + pageNames.Count());
			foreach (var pageName in pageNames) {
				Log("page - " + pageName);
				string content = File.ReadAllText(pageName);
				MatchCollection pageMatches = pageRegex.Matches(content);
				Log("page match count - " + pageMatches.Count);

				if (pageMatches.Count == 0) {
					Error("No page matches - " + pageName);
				} else {
					//for (pageMatches[0].Groups) {
					string title = "";
					//}
				}

			}








			End();
		}


		static void FixFileNames() {
			LogFileName = LogFileName.Replace("{now}", NowString);
			ErrorFileName = ErrorFileName.Replace("{now}", NowString);
			StartFileName = StartFileName.Replace("{now}", NowString);
			ListFileName = ListFileName.Replace("{now}", NowString);
			PageFileName = PageFileName.Replace("{now}", NowString);
			TorrentFileName = TorrentFileName.Replace("{now}", NowString);
			TorrentFileNameSecond = TorrentFileNameSecond.Replace("{now}", NowString);
		}

		static void Directories() {
			mkdir(LogFileName);
			mkdir(ErrorFileName);
			mkdir(StartFileName);
			mkdir(ListFileName);
			mkdir(PageFileName);
			mkdir(TorrentFileName);
		}

		static void mkdir(string path) {
			string dirName = Path.GetDirectoryName(path);
			if (!Directory.Exists(dirName)) {
				Directory.CreateDirectory(dirName);
			}
		}

		static string ClockStamp() {
			TimeSpan delta = Clock.Elapsed;
			string stamp = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", delta.Hours, delta.Minutes, delta.Seconds, delta.Milliseconds / 10);
			return stamp;
		}

		static string Now() {
			return DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-ffff");
		}

		static string LogStamp() {
			return Now() + " " + ClockStamp() + " --- ";
		}

		static void Log(string message) {
			message = LogStamp() + message;
			Console.WriteLine(message);
			LogFile.WriteLine(message);
			LogFile.Flush();
		}

		static void Error(string message) {
			Log(message);
			message = LogStamp() + message;
			ErrorFile.WriteLine(message);
			ErrorFile.Flush();
		}

		static void Wait() {
			//int time = 500 + RNG.Next(1000);
			//Thread.Sleep(time);
		}

		static void End() {
			Clock.Stop();
			Log("END");

			ErrorFile.Flush();
			ErrorFile.Close();

			LogFile.Flush();
			LogFile.Close();

			Console.ReadKey();
		}
	}
}

