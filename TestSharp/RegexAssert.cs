using System.Text.RegularExpressions;
using System;
namespace TestSharp
{
	/// <summary>
	/// Asserções para expressões regulares.
	/// </summary>
	public static class RegexAssert
	{
		/// <summary>
		/// Valida se expressão regular informada combina com a entrada.
		/// </summary>
		/// <param name="expectedRegexPattern">A expressão regular.</param>
		/// <param name="actualInput">O conteúdo onde será executada a expressão regular.</param>
		public static void IsMatch(string expectedRegexPattern, string actualInput)
		{
			if (expectedRegexPattern == null)
			{
				throw new ArgumentNullException("expectedRegexPattern");
			}

			try
			{
				if (actualInput == null || !Regex.IsMatch(actualInput, expectedRegexPattern))
				{
					AssertHelper.ThrowAssert("RegexAssert", "IsMatch", expectedRegexPattern, actualInput);
				}
			}
			catch (ArgumentException ex)
			{
				throw new ArgumentException("A expressão regular informada é inválida.", "expectedRegexPattern", ex);
			}
		}
	}
}
