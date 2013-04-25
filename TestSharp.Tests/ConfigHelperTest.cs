using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSharp.Tests.Helpers;
using System.Configuration;
using System;

namespace TestSharp.Tests
{
	[TestClass]
	public class ConfigHelperTest
	{
		#region Tests
		[TestMethod]
		public void ReadWebConfigTest()
		{
			var config = ConfigHelper.ReadConfig(WebSiteStubHelper.ProjectFolderName);

			Assert.AreEqual(2, config.AppSettings.Settings.Count);
			Assert.AreEqual("value1", config.AppSettings.Settings["key1"].Value);
			Assert.AreEqual("value2", config.AppSettings.Settings["key2"].Value);
		}

		[TestMethod]
		public void ReadAppSettingTest()
		{
			Assert.AreEqual("value1", ConfigHelper.ReadAppSetting(WebSiteStubHelper.ProjectFolderName, "key1"));
			Assert.AreEqual("value2", ConfigHelper.ReadAppSetting(WebSiteStubHelper.ProjectFolderName, "key2"));
		}

		[TestMethod]
		public void WriteAppSetting_KeyDoesNotExists_Exception()
		{
			ExceptionAssert.IsThrowing(new ArgumentException("A chave 'KeyDoesNotExists' não existe no AppSettings.", "key"), () =>
			{
				ConfigHelper.WriteAppSetting(WebSiteStubHelper.ProjectFolderName, "KeyDoesNotExists", "value");
			});
		}

		[TestMethod]
		public void WriteAppSetting_KeysExists_UpdatedToFile()
		{
			// Key 1.
			ConfigHelper.WriteAppSetting(WebSiteStubHelper.ProjectFolderName, "key1", "value999");
			Assert.AreEqual("value999", ConfigHelper.ReadAppSetting(WebSiteStubHelper.ProjectFolderName, "key1"));

			ConfigHelper.WriteAppSetting(WebSiteStubHelper.ProjectFolderName, "key1", "value1");
			Assert.AreEqual("value1", ConfigHelper.ReadAppSetting(WebSiteStubHelper.ProjectFolderName, "key1"));

			// Key 2.
			ConfigHelper.WriteAppSetting(WebSiteStubHelper.ProjectFolderName, "key2", "value000");
			Assert.AreEqual("value000", ConfigHelper.ReadAppSetting(WebSiteStubHelper.ProjectFolderName, "key2"));

			ConfigHelper.WriteAppSetting(WebSiteStubHelper.ProjectFolderName, "key2", "value2");
			Assert.AreEqual("value2", ConfigHelper.ReadAppSetting(WebSiteStubHelper.ProjectFolderName, "key2"));

		}
		#endregion
	}
}
