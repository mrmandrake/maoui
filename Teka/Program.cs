using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Diagnostics;

namespace Teka
{

	public class Program {

        public static Process chrome;
        public static Process canary;
        public static string wwwroot;

        public static void Main(string[] args)
		{
            //if (args.Length < 2)
            //    return;

            //wwwroot = args[0];
            Task.Factory.StartNew(() => { RunStaticServe(args); });
            Task.Factory.StartNew(() => { RunMonoDebuggerHost(args); });
			Thread.Sleep(1000);
            RunChrome(args[0]);
            Thread.Sleep(1000);
            RunCanary();
			Console.ReadLine();
            chrome.Kill();
            canary.Kill();
		}

        private static void RunStaticServe(string[] args)
        {
            var host = new WebHostBuilder()
                 .UseSetting("UseIISIntegration", false.ToString())
                 .UseKestrel()
                 .UseContentRoot(Directory.GetCurrentDirectory())
                 .UseStartup<StaticStartup>()
                 .ConfigureAppConfiguration((hostingContext, config) => {config.AddCommandLine(args);})
                 .ConfigureLogging(logging => {})
                 .UseUrls("http://localhost:9301")
                 .Build();

            host.Run();
        }

        private static void RunMonoDebuggerHost(string[] args)
		{
			var host = new WebHostBuilder()
				.UseSetting("UseIISIntegration", false.ToString())
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseStartup<Startup>()
				.ConfigureAppConfiguration((hostingContext, config) => { config.AddCommandLine(args);})
				.UseUrls("http://localhost:9300")
				.Build();

			host.Run();
		}

        private static void RunChrome(string asm)
        {
            try {
                var psi = new ProcessStartInfo();
                psi.Arguments = $" --incognito --remote-debugging-port=9222 http://localhost:9301/?assembly={asm}";
                psi.UseShellExecute = false;
                psi.FileName = @"/Applications/Google Chrome.app/Contents/MacOS/Google Chrome";
                psi.RedirectStandardError = true;
                psi.RedirectStandardOutput = true;
                chrome = Process.Start(psi);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
            }
        }

        private static void RunCanary()
        {
            try
            {
                var psi = new ProcessStartInfo();
                psi.Arguments = $"--incognito http://localhost:9300";
                psi.UseShellExecute = false;
                psi.FileName = @"/Applications/Google Chrome Canary.app/Contents/MacOS/Google Chrome Canary";
                psi.RedirectStandardError = true;
                psi.RedirectStandardOutput = true;
                canary = Process.Start(psi);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
            }
        }
    }

}
