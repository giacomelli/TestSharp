using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSharp.Tests.Helpers;

namespace TestSharp.Tests
{
	[TestClass]
	public class WebHostHelperTest
	{
		[TestMethod]
		public void KillAllTest()
		{
			WebHostHelper.KillAll();
			ProcessAssert.IsProcessInstancesCount(0, WebHostHelper.WebHostProcessName);

			WebSiteStubHelper.Start();
			ProcessAssert.IsProcessInstancesCount(1, WebHostHelper.WebHostProcessName);
			WebHostHelper.KillAll();
			ProcessAssert.IsProcessInstancesCount(0, WebHostHelper.WebHostProcessName);
		}

		[TestMethod]
		public void StartTest()
		{
			WebHostHelper.KillAll();
			WebSiteStubHelper.Start();
			ProcessAssert.IsProcessInstancesCount(1, WebHostHelper.WebHostProcessName);
			WebHostHelper.KillAll();
		}

		[TestMethod]
		public void StartAndWaitForResponseTest()
		{
			WebHostHelper.KillAll();
			WebSiteStubHelper.StartAndWaitForResponse();
			ProcessAssert.IsProcessInstancesCount(1, WebHostHelper.WebHostProcessName);
			WebHostHelper.KillAll();
		}
	}
}
