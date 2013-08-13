using System;
using System.Diagnostics;

namespace TestSharp
{
	/// <summary>
	/// Time assert.
	/// </summary>
	public static class TimeAssert
	{
		#region Methods
		/// <summary>
		/// Assert if action will be executed in less than expected milliseconds.
		/// </summary>
		/// <param name="milliseconds">Milliseconds.</param>
		/// <param name="action">Action.</param>
		public static void LessThan(int milliseconds, Action action)
		{
			var sw = new Stopwatch ();
			sw.Start ();
			action ();
			sw.Stop ();

			if (sw.ElapsedMilliseconds >= milliseconds) {
				AssertHelper.ThrowAssert("TimeAssert", "LessThan", milliseconds, sw.ElapsedMilliseconds);
			}
		}
		#endregion
	}
}

