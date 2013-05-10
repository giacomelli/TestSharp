using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSharp
{
	/// <summary>
	/// Asserts para web.config e app.config
	/// </summary>
	public static class ConfigAssert
	{
		#region Methods
		/// <summary>
		/// Verifica se o valor da chave no AppSettings é igual ao informado.
		/// </summary>
		/// <param name="expectedValue">O valor esperado da chave.</param>
		/// <param name="projectFolderName">O nome do projeto web.</param>
		/// <param name="key">A chave no AppSettings</param>
		public static void IsAppSetting(string expectedValue, string projectFolderName, string key)
		{
			var actual = ConfigHelper.ReadAppSetting(projectFolderName, key);

			if (actual != expectedValue)
			{
				AssertHelper.ThrowAssert("ConfigAssert", "IsAppSetting", expectedValue, actual);
			}
		}
		#endregion
	}
}
