using System;
using System.IO;
using System.Threading.Tasks;
using ExifLib;

namespace JPEGStats
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length > 1)
			{
				Console.WriteLine("Buzz off");
				return;
			}
			var workingDirectory = Environment.CurrentDirectory;
			if (args.Length == 1)
			{
				workingDirectory = Path.IsPathRooted(args[0]) ? args[0] : Path.Combine(workingDirectory, args[0]);
			}
			if (!Directory.Exists(workingDirectory))
			{
				Console.WriteLine("Buzz off");
				return;
			}

			int count = 0;
			int amount = 0;

			Parallel.ForEach(Directory.GetFiles(workingDirectory, "*.jpeg", SearchOption.AllDirectories), file =>
			{
				int f;
				if (!new ExifLib.ExifReader(file).GetTagValue(ExifTags.FNumber, out f)) return;
				amount += f;
				count++;
			});

			if (count == 0 )
			{
				Console.WriteLine("No jpeg fails found");
				return;
			}
			Console.WriteLine("Total {0} Avg {1}", count, amount / count);

		}
	}
}
