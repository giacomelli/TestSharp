using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSharp.Tests
{
	[TestClass]
	public class ServiceHelperTest
	{
		#region Constants
		private const string ServiceName = "Audiosrv";
		#endregion

		#region Tests
		[TestMethod]
		public void ForceStartTest()
		{
			ServiceHelper.ForceStart(ServiceName);
			ServiceAssert.IsRunning(ServiceName);

			ServiceHelper.ForceStart(ServiceName);
			ServiceAssert.IsRunning(ServiceName);
			
			ServiceHelper.Stop(ServiceName);
		}

		[TestMethod]
		public void IsRunningTest()
		{			
			ServiceHelper.ForceStart(ServiceName);
			Assert.IsTrue(ServiceHelper.IsRunning(ServiceName));

			ServiceHelper.Stop(ServiceName);
			Assert.IsFalse(ServiceHelper.IsRunning(ServiceName));
		}

		[TestMethod]
		public void IsStoppedTest()
		{
			ServiceHelper.ForceStart(ServiceName);
			Assert.IsFalse(ServiceHelper.IsStopped(ServiceName));

			ServiceHelper.Stop(ServiceName);
			Assert.IsTrue(ServiceHelper.IsStopped(ServiceName));
		}

		[TestMethod]
		public void StopTest()
		{			
			ServiceHelper.ForceStart(ServiceName);
			ServiceAssert.IsRunning(ServiceName);
			ServiceHelper.Stop(ServiceName);
			ServiceAssert.IsStopped(ServiceName);
		}

		[TestMethod]
		public void InstallTest()
		{
			ServiceHelper.Install("teste");
		}

		[TestMethod]
		public void UninstallTest()
		{
			ServiceHelper.Uninstall("teste");
		}

		[TestMethod]
		public void ReinstallTest()
		{
			ServiceHelper.Reinstall("teste");
		}
		#endregion
	}
}
