using System;
using System.IO;

namespace BatchVideoConverter
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var watcher = new FileWatcher(new FileSystemWatcher());
		}
	}
}
