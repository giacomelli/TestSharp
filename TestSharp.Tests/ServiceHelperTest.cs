using NUnit.Framework;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class ServiceHelperTest
	{
		#region Constants
		private const string ServiceName = "Audiosrv";
		#endregion

		#region Tests
		[Test]
		public void ForceStartTest()
		{
			ServiceHelper.ForceStart(ServiceName);
			ServiceAssert.IsRunning(ServiceName);

			ServiceHelper.ForceStart(ServiceName);
			ServiceAssert.IsRunning(ServiceName);
			
			ServiceHelper.Stop(ServiceName);
		}

		[Test]
		public void IsRunningTest()
		{			
			ServiceHelper.ForceStart(ServiceName);
			Assert.IsTrue(ServiceHelper.IsRunning(ServiceName));

			ServiceHelper.Stop(ServiceName);
			Assert.IsFalse(ServiceHelper.IsRunning(ServiceName));
		}

		[Test]
		public void IsStoppedTest()
		{
			ServiceHelper.ForceStart(ServiceName);
			Assert.IsFalse(ServiceHelper.IsStopped(ServiceName));

			ServiceHelper.Stop(ServiceName);
			Assert.IsTrue(ServiceHelper.IsStopped(ServiceName));
		}

		[Test]
		public void StopTest()
		{			
			ServiceHelper.ForceStart(ServiceName);
			ServiceAssert.IsRunning(ServiceName);
			ServiceHelper.Stop(ServiceName);
			ServiceAssert.IsStopped(ServiceName);
		}

		[Test]
		public void InstallTest()
		{
			ServiceHelper.Install("teste");
		}

		[Test]
		public void UninstallTest()
		{
			ServiceHelper.Uninstall("teste");
		}

		[Test]
		public void ReinstallTest()
		{
			ServiceHelper.Reinstall("teste");
		}
		#endregion
	}
}
