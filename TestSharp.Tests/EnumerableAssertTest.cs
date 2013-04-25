using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSharp.Tests
{
	[TestClass]
	public class EnumerableAssertTest
	{
		[TestMethod]
		public void AllItemsAreNotNullTest()
		{
			var notNulls = new string[] { "", "1", "null", "3" };
			EnumerableAssert.AllItemsAreNotNull(notNulls);

			var withNulls = new string[] { "", "1", null, "3" };
			ExceptionAssert.IsThrowing(typeof(AssertFailedException), () =>
			{
				EnumerableAssert.AllItemsAreNotNull(withNulls);
			});
		}
	}
}
