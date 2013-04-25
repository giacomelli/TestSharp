using System.Linq;

namespace TestSharp
{
	/// <summary>
	/// Asserts para WMI.
	/// </summary>
	public static class WmiAssert
	{
		/// <summary>
		/// Verifica se o valor de propriedade WMI é o espeardo.
		/// </summary>
		/// <typeparam name="TValue">O tipo do valor da propriedade.</typeparam>
		/// <param name="expectedValue">O valor esperado da propriedade.</param>
		/// <param name="scope">O escopo onde está publicada a entidade WMI. Por exemplo: "\root\cimv2"</param>
		/// <param name="entityName">O nome da entidade WMI.</param>
		/// <param name="propertyName">O nome da propriedade a ser lida.</param>
		public static void IsPropertyValue<TValue>(TValue expectedValue, string scope, string entityName, string propertyName)
		{
			var actualValue = WmiHelper.GetPropertyValue<TValue>(scope, entityName, propertyName);

			if (!actualValue.Equals(expectedValue))
			{
				AssertExceptionHelper.ThrowAssert("WmiAssert", "IsPropertyValue", expectedValue, actualValue);
			}
		}

		/// <summary>
		/// Verifica se pelo menos uma das instâncias da propriedade WMI possui o valor espeardo.
		/// </summary>
		/// <typeparam name="TValue">O tipo do valor da propriedade.</typeparam>
		/// <param name="expectedValue">O valor esperado da propriedade.</param>
		/// <param name="scope">O escopo onde está publicada a entidade WMI. Por exemplo: "\root\cimv2"</param>
		/// <param name="entityName">O nome da entidade WMI.</param>
		/// <param name="propertyName">O nome da propriedade a ser lida.</param>
		public static void AtLeastOnePropertyValue<TValue>(TValue expectedValue, string scope, string entityName, string propertyName)
		{
			var actualValues = WmiHelper.GetPropertyValues<TValue>(scope, entityName, propertyName);

			if (actualValues.Count(v => v.Equals(expectedValue)) == 0)
			{
				AssertExceptionHelper.ThrowAssert("WmiAssert", "AtLeastOnePropertyValue", expectedValue, actualValues[0]);
			}
		}
	}
}
