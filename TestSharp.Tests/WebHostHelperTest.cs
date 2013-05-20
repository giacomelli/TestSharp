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
			Assert.AreEqual (0, WebHostHelper.InstancesCount);

			WebSiteStubHelper.Start();
			Assert.AreEqual (1, WebHostHelper.InstancesCount);

			WebHostHelper.KillAll();
			Assert.AreEqual (0, WebHostHelper.InstancesCount);
		}

		[Test]
		public void StartTest()
		{
			WebHostHelper.KillAll();
			WebSiteStubHelper.Start();
			Assert.AreEqual (1, WebHostHelper.InstancesCount);
			WebHostHelper.KillAll();
		}

		[Test]
		public void StartAndWaitForResponseTest()
		{
			WebHostHelper.KillAll();
			WebSiteStubHelper.StartAndWaitForResponse();
			Assert.AreEqual (1, WebHostHelper.InstancesCount);
			WebHostHelper.KillAll();
		}
	}
}
