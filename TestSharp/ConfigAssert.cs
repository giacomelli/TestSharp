using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSharp
{
	/// <summary>
	/// Web.config and app.config asserts.
	/// </summary>
	public static class ConfigAssert
	{
		#region Methods
		/// <summary>
		/// Determines if the AppSettings value is the expected one.
		/// </summary>
		/// <returns><c>true</c> if is the expected value; otherwise, <c>false</c>.</returns>
		/// <param name="expectedValue">The expected value.</param>
		/// <param name="projectFolderName">The project folder name.</param>
		/// <param name="key">The aappSettings Key.</param>
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
