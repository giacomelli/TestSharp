using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class RegexAssertTest
	{
		[Test]
		public void IsMatch_NullRegex_ArgumentNullException()
		{
			ExceptionAssert.IsThrowing(new ArgumentNullException("expectedRegexPattern"), () =>
			{
				RegexAssert.IsMatch(null, "TESTE");
			});
		}

		[Test]
		public void IsMatch_InvalidRegex_ArgumentException()
		{
			ExceptionAssert.IsThrowing(new ArgumentException("A expressão regular informada é inválida.", "expectedRegexPattern"), () =>
			{
				RegexAssert.IsMatch(@"\", "TESTE");
			});
		}

		[Test]
		public void IsMatch_NullActualContent_ThrowsAssertException()
		{
			ExceptionAssert.IsThrowing(typeof(AssertFailedException), () =>
			{
				RegexAssert.IsMatch(@"\d", null);
			});
		}

		[Test]
		public void IsMatch_RegexDoesNotMatchContent_ThrowsAssertException()
		{
			ExceptionAssert.IsThrowing(typeof(AssertFailedException), () =>
			{
				RegexAssert.IsMatch(@"\d", "a");
			});
		}

		[Test]
		public void IsMatch_RegexMatchesContent_DoesNotThrowsAssertException()
		{
			RegexAssert.IsMatch(@"\d", "1");			
		}
	}
}
