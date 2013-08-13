using NUnit.Framework;
using System;
using System.Threading;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class TimeAssertTest
	{
		[Test()]
		public void LessThan_LessThanExpect_Ok ()
		{
			TimeAssert.LessThan (1000, () => {
				Thread.Sleep(100);
			});
		}

		[Test()]
		public void LessThan_GreaterThanExpect_Exception ()
		{
			ExceptionAssert.IsThrowing (new AssertFailedException (""), () => {
				TimeAssert.LessThan (50, () => {
					Thread.Sleep(100);
				});
			});
		}
	}
}

