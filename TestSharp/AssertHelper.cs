using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSharp
{
	/// <summary>
	/// Utilitária para exceções de assert.
	/// </summary>
	internal static class AssertExceptionHelper
	{
		/// <summary>
		/// Lança uma AssertFailedException com a mensage formatada.
		/// </summary>
		/// <param name="assertClass">O nome da classe de assert.</param>
		/// <param name="assertMethod">O nome do método de assert.</param>
		/// <param name="expected">O valor esperado.</param>
		/// <param name="actual">O valor atual.</param>
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
	}
}
