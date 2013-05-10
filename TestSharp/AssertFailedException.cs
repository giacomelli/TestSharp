using System;

namespace TestSharp
{
	/// <summary>
	/// Excepetion used when a assertion fialed.
	/// </summary>
	public class AssertFailedException : Exception
	{
		#region Constructors
		/// <summary>
		/// Initializes a new <see cref="AssertFailedException"/> instance.
		/// </summary>
		/// <param name="message">The message.</param>
		public AssertFailedException(string message) : base(message)
		{
		}
		#endregion
	}
}
