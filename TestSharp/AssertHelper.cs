using System;
using System.Globalization;

namespace TestSharp
{
	/// <summary>
	/// Asserts utilities.
	/// </summary>
	internal static class AssertHelper
	{
		/// <summary>
		/// Throws the assert.
		/// </summary>
		/// <param name="assertClass">The assert class.</param>
		/// <param name="assertMethod">The assert method.</param>
		/// <param name="expected">The expected.</param>
		/// <param name="actual">The actual.</param>
		public static void ThrowAssert(string assertClass, string assertMethod, object expected, object actual)
		{
			var msg = String.Format(
				CultureInfo.InvariantCulture, 
				"{0}.{1} failed. Expected:<{2}>. Actual:<{3}>.",
				assertClass,
				assertMethod,
				expected,
				actual);

			throw new AssertFailedException(msg);
		}

		/// <summary>
		/// Assert if x and y are equal
		/// </summary>
		/// <param name="assertClass">The assert class.</param>
		/// <param name="assertMethod">The assert method.</param>
		/// <param name="expected">The expected value to compare.</param>
		/// <param name="actual">The actual value to compare.</param>
		public static void AreEqual(string assertClass, string assertMethod, string expected, string actual)
		{
			if (expected == null && actual == null)
			{
				return;
			}

			if (expected == null || actual == null || !expected.Equals(actual, StringComparison.OrdinalIgnoreCase))
			{
				ThrowAssert(assertClass, assertMethod, expected, actual);
			}
		}
	}
}
