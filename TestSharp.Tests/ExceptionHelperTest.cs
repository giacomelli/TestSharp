using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class ExceptionHelperTest
	{
		[Test]
		public void GetExceptionThrownTest()
		{
			Assert.IsNull(ExceptionHelper.GetExceptionThrown(() => { }));
			
			var exception = new InvalidOperationException("TESTE");
			Assert.AreEqual(exception, ExceptionHelper.GetExceptionThrown(() => { throw exception; }));
		}
	}
}
