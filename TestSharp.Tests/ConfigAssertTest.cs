using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using TestSharp.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSharp.Tests
{
	[TestClass]
	public class ConfigAssertTest
	{
		[TestMethod]
		public void IsAppSetting_ValueDiffAppSettings_AssertFailedException()
		{
			ExceptionAssert.IsThrowingAny(() => {
				ConfigAssert.IsAppSetting("value0", WebSiteStubHelper.ProjectFolderName, "key1");			
			}, typeof(AssertFailedException));
		}

		[TestMethod]
		public void IsAppSetting_ValueEqualsAppSettings_Ok()
		{
			ConfigAssert.IsAppSetting("value1", WebSiteStubHelper.ProjectFolderName, "key1");
		}
	}
}
