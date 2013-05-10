using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class EnumerableAssertTest
	{
		[Test]
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
