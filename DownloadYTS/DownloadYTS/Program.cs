﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DownloadYTS {
	class Program {
		static string LogFileName = @"c:\working\yts\log\log-{0}.txt";
		static string ErrorFileName = @"c:\working\yts\error\error-{0}.txt";
		static string ListFileName = @"c:\working\yts\list\list-{0}.txt";
		static string PageFileName = @"c:\working\yts\page\page-{0}-{1}.txt";

		static StreamWriter _logFile;
		static StreamWriter LogFile {
			get {
				if (_logFile == null) {
					_logFile = File.CreateText(String.Format(LogFileName, Now()));
				}
				return _logFile;
			}
		}

		static StreamWriter _errorFile;
		static StreamWriter ErrorFile {
			get {
				if (_errorFile == null) {
					_errorFile = File.CreateText(String.Format(ErrorFileName, Now()));
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

		static string ClockStamp() {
			TimeSpan delta = Clock.Elapsed;
			string stamp = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", delta.Hours, delta.Minutes, delta.Seconds, delta.Milliseconds / 10);
			return stamp;
		}

		static string Now() {
			return DateTime.Now.ToString("yyyy-MM-dd-HHmmss-ffff");
		}

		static void Main(string[] args) {
			Log("START");

			Random rng = new Random();
			WebClient web = new WebClient();




			End();
		}

		static void End() {
			LogFile.Flush();
			LogFile.Close();

			Clock.Stop();
			Log("END");
			Console.ReadKey();
		}


		static void Log(string message) {
			message = Now() + " " + message;
			Console.WriteLine(message);
			LogFile.WriteLine(message);
		}

		static void Error(string message) {
			Log(message);
			message = Now() + " " + message;
			ErrorFile.WriteLine(message);
		}
	}
}
