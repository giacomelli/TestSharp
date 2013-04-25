using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSharp.Tests.Helpers
{
	public static class WebSiteStubHelper
	{
		#region Constants
		public const string VirtualPath = "http://localhost:12345";
		public const string ProjectFolderName = "TestSharp.WebSiteStubTest";
		#endregion

		#region Methods
		public static void Start()
		{
			Stop();
			WebHostHelper.Start(ProjectFolderName, 12345);
		}

		public static void StartAndWaitForResponse()
		{
			Stop();
			WebHostHelper.StartAndWaitForResponse(ProjectFolderName, 12345);
		}

		public static void Stop()
		{
			WebHostHelper.KillAll();
		}
		#endregion
	}
}
