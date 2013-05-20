#if WIN
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class WmiHelperTest
	{	
		[Test]
		public void GetPropertyValueTest()
		{
			Assert.AreEqual("Win32_DesktopMonitor", WmiHelper.GetPropertyValue<string>(@"\root\cimv2", "Win32_DesktopMonitor", "CreationClassName"));
		}
	}
}
#endif