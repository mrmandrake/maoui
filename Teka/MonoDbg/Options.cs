using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teka
{
	public class ProxyOptions
	{
		public Uri DevToolsUrl { get; set; } = new Uri("http://localhost:9222");
	}

	public class TestHarnessOptions : ProxyOptions
	{
		public string ChromePath { get; set; }
		public string AppPath { get; set; }
		public string PagePath { get; set; }
		// public string NodeApp { get; set; }
	}
}
