using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using TestSharp.Tests.Helpers;
using NUnit.Framework;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class ConfigAssertTest
	{
		[Test]
		public void IsAppSetting_ValueDiffAppSettings_AssertFailedException()
		{
			ExceptionAssert.IsThrowingAny(() => {
				ConfigAssert.IsAppSetting("value0", WebSiteStubHelper.ProjectFolderName, "key1");			
			}, typeof(AssertFailedException));
		}

		[Test]
		public void IsAppSetting_ValueEqualsAppSettings_Ok()
		{
			ConfigAssert.IsAppSetting("value1", WebSiteStubHelper.ProjectFolderName, "key1");
		}
	}
}
