using NUnit.Framework;
using TestSharp.Tests.Helpers;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class WebHostHelperTest
	{
		[Test]
		public void KillAllTest()
		{
			WebHostHelper.KillAll();
			ProcessAssert.IsProcessInstancesCount(0, WebHostHelper.WebHostProcessName);

			WebSiteStubHelper.Start();
			ProcessAssert.IsProcessInstancesCount(1, WebHostHelper.WebHostProcessName);
			WebHostHelper.KillAll();
			ProcessAssert.IsProcessInstancesCount(0, WebHostHelper.WebHostProcessName);
		}

		[Test]
		public void StartTest()
		{
			WebHostHelper.KillAll();
			WebSiteStubHelper.Start();
			ProcessAssert.IsProcessInstancesCount(1, WebHostHelper.WebHostProcessName);
			WebHostHelper.KillAll();
		}

		[Test]
		public void StartAndWaitForResponseTest()
		{
			WebHostHelper.KillAll();
			WebSiteStubHelper.StartAndWaitForResponse();
			ProcessAssert.IsProcessInstancesCount(1, WebHostHelper.WebHostProcessName);
			WebHostHelper.KillAll();
		}
	}
}
