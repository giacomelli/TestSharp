using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSharp.Tests
{
	[TestClass]
	public class WmiHelperTest
	{	
		[TestMethod]
		public void GetPropertyValueTest()
		{
			Assert.AreEqual("Win32_DesktopMonitor", WmiHelper.GetPropertyValue<string>(@"\root\cimv2", "Win32_DesktopMonitor", "CreationClassName"));
		}
	}
}
