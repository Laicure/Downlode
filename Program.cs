using System;
using System.IO;
using System.Linq;
using System.Net;

namespace Downlode
{
	class Program
	{
		static void Main(string[] args)
		{
			args = new string[] { "" };
			string setPath = AppDomain.CurrentDomain.BaseDirectory + @"downlode_set.txt";
			if (!File.Exists(setPath))
			{
				File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\downlode_error.txt", "downlode_set.txt not found!");
				return;
			}
			Console.SetWindowSize(4, 1);
			string[] cred = Environment.CommandLine.Replace(Environment.GetCommandLineArgs()[0], "").Replace("\"\"" , "").Trim().Split(new string[] { "--"}, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
			string[] setLines = File.ReadAllLines(setPath).Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
			if (setLines.Count() == 2)
			{
				try
				{
					using (WebClient clie = new WebClient())
					{
						if(cred.Count() == 2)
						{
							clie.Credentials = new NetworkCredential(cred[0], cred[1]);
						}
						else
						{
							clie.UseDefaultCredentials = true;
						}							
						clie.DownloadFile(setLines[0], setLines[1]);
					}
				}
				catch (Exception ex)
				{
					File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\error_" + DateTime.UtcNow.ToString("ffff") + ".txt", ex.Source + ": " + ex.Message);
				}
			}
		}
	}
}
