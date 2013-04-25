
namespace TestSharp
{
	/// <summary>
	/// Asserts para strings (o nome StringAssert já estava ocupado :p).
	/// </summary>
	public static class TextAssert
	{
		/// <summary>
		/// Verifica se a string é nula ou vazia.
		/// </summary>
		/// <param name="actual">A string a ser verificada.</param>
		public static void IsNullOrEmpty(string actual)
		{
			if (!string.IsNullOrEmpty(actual))
			{
				AssertExceptionHelper.ThrowAssert("TextAssert", "IsNullOrEmpty", true, false);
			}
		}

		/// <summary>
		/// Verifica se a string não é nula ou vazia.
		/// </summary>
		/// <param name="actual">A string a ser verificada.</param>
		public static void IsNotNullOrEmpty(string actual)
		{
			if (string.IsNullOrEmpty(actual))
			{
				AssertExceptionHelper.ThrowAssert("TextAssert", "IsNotNullOrEmpty", true, false);
			}
		}
	}
}
