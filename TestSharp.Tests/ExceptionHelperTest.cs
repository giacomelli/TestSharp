using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSharp.Tests
{
	[TestClass]
	public class ExceptionHelperTest
	{
		[TestMethod]
		public void GetExceptionThrownTest()
		{
			Assert.IsNull(ExceptionHelper.GetExceptionThrown(() => { }));
			
			var exception = new InvalidOperationException("TESTE");
			Assert.AreEqual(exception, ExceptionHelper.GetExceptionThrown(() => { throw exception; }));
		}
	}
}
