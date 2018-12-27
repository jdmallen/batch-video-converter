using System;
using System.IO;

namespace BatchVideoConverter
{
	public class FileWatcher
	{
		private static DateTime _lastChanged = DateTime.MinValue;

		public FileWatcher(FileSystemWatcher watcher)
		{
			// Associate event handlers with the events
			watcher.Created += FileSystemWatcher_Created;
			watcher.Changed += FileSystemWatcher_Changed;
			watcher.Deleted += FileSystemWatcher_Deleted;
			watcher.Renamed += FileSystemWatcher_Renamed;

			// tell the watcher where to look
			watcher.Path = @"C:\Temp\watchme\";

			// You must add this line - this allows events to fire.
			watcher.EnableRaisingEvents = true;

			Console.WriteLine("Listening...");
			Console.WriteLine("(Press any key to exit.)");

			Console.ReadLine();
		}

		private static void FileSystemWatcher_Renamed(
			object sender,
			RenamedEventArgs e)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(
				$"A new file has been renamed from {e.OldName} to {e.Name}");
		}

		private static void FileSystemWatcher_Deleted(
			object sender,
			FileSystemEventArgs e)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"A new file has been deleted - {e.Name}");
		}

		private static void FileSystemWatcher_Changed(
			object sender,
			FileSystemEventArgs e)
		{
			if ((DateTime.UtcNow - _lastChanged).Milliseconds < 40)
			{
				Console.ForegroundColor = ConsoleColor.DarkGreen;
				Console.WriteLine($"Another change event for file {e.Name}");
				return;
			}

			_lastChanged = DateTime.UtcNow;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"A new file has been changed - {e.Name}");
		}

		private static void FileSystemWatcher_Created(
			object sender,
			FileSystemEventArgs e)
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine($"A new file has been created - {e.Name}");
		}
	}
}
