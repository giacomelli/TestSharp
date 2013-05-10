using System;
using NUnit.Framework;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class FlowAssertTest
	{
		[Test]
		public void IsAtLeastOneOkTest()
		{
			FlowAssert.IsAtLeastOneOk(
				() =>
				{
					throw new Exception("First wrong");
				},
				() =>
				{
					return; // OK.
				},
				() =>
				{
					throw new Exception("Second wrong");
				});

			ExceptionAssert.IsThrowing(new AssertFailedException("FlowAssert.IsAtLeastOneOk failed. Expected:<True>. Actual:<False>."), () =>
			{
				FlowAssert.IsAtLeastOneOk(
					() =>
					{
						throw new Exception("First wrong");
					},
					() =>
					{
						throw new Exception("Second wrong");
					});
			});
		}

		[Test]
		public void IsAtLeastOneAttemptOkTest()
		{
			ExceptionAssert.IsThrowing(typeof(AssertFailedException), () =>
			{
				FlowAssert.IsAtLeastOneAttemptOk(0, () =>
				{
					throw new Exception("Deveria lançar exceção, pois nunca será executado fluxo algum.");
				});
			});

			ExceptionAssert.IsThrowing(typeof(AssertFailedException), () =>
			{
				FlowAssert.IsAtLeastOneAttemptOk(1, () =>
				{
					throw new Exception("Deveria lançar exceção, pois será executado.");
				});
			});

			int count = 0;

			FlowAssert.IsAtLeastOneAttemptOk(3, () =>
			{
				count++;

				if (count == 1)
				{
					throw new Exception("Não deveria lançar exceção, pois será executado um fluxo que não tem exceção.");
				}
			});

			Assert.AreEqual(2, count, "Deveria executar o fluxo somente duas vezes, pois a segunda tentativa já não lançará exceção.");
		}
	}
}
