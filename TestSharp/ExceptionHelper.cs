using System;
using System.Diagnostics.CodeAnalysis;

namespace TestSharp
{
	/// <summary>
	/// Utilitários para exceções.
	/// </summary>
	public static class ExceptionHelper
	{
		/// <summary>
		/// Obtém a exceção lançada pela action ou nulo caso não ocorra exceção na execução da action.
		/// </summary>
		/// <param name="action">A ação a ser executada.</param>
		/// <returns>A execeção.</returns>
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static Exception GetExceptionThrown(Action action)
		{
			Exception exceptionThrown = null;

			try
			{
				action();
			}
			catch (Exception ex)
			{
				exceptionThrown = ex;
			}

			return exceptionThrown;
		}
	}
}
