using System;
using System.Linq;

namespace TestSharp
{
	/// <summary>
	/// Assert para Exceptions.
	/// </summary>
	public static class ExceptionAssert
	{
		#region Methods
		/// <summary>
		/// Verifica se o bloco de código executando dentro da Action informada está lançando a exceção informada.
		/// </summary>
		/// <remarks>
		/// Além do lançamento da exceção, também será validado o tipo e a mensagem da exceção.
		/// </remarks>		
		/// <param name="expectedException">A exceção esperada.</param>
		/// <param name="action">Action com o bloco de código que deve lançar a exceção.</param>
		public static void IsThrowing(Exception expectedException, Action action)
		{
			if (expectedException == null)
			{
				throw new ArgumentNullException("expectedException");
			}

			IsThrowing(expectedException.GetType(), expectedException.Message, action);
		}

		/// <summary>
		/// Verifica se o bloco de código executando dentro da Action informada está lançando a exceção informada sem considerar a mensagem da exceção.
		/// </summary>
		/// <remarks>
		/// Além do lançamento da exceção, também será validado o tipo e a mensagem da exceção.
		/// </remarks>		
		/// <param name="expectedExceptionType">O tipo da exceção esperada.</param>
		/// <param name="action">Action com o bloco de código que deve lançar a exceção.</param>
		public static void IsThrowing(Type expectedExceptionType, Action action)
		{
			IsThrowing(expectedExceptionType, null, action);
		}

		/// <summary>
		/// Verifica se o bloco de código executando dentro da Action informada está lançada qualquer um dos tipos das exceções informadas.
		/// </summary>
		/// <param name="action">Action com o bloco de código que deve lançar exceção.</param>
		/// <param name="exceptionsTypes">Os tipos de exceções esperadas.</param>
		public static void IsThrowingAny(Action action, params Type[] exceptionsTypes)
		{
			var exceptionThrown = ExceptionHelper.GetExceptionThrown(action);

			if (exceptionThrown == null || exceptionsTypes.Count(e => e.FullName.Equals(exceptionThrown.GetType().FullName)) == 0)
			{
				AssertHelper.ThrowAssert("ExceptionAssert", "IsThrowingAny", exceptionsTypes, exceptionThrown);
			}
		}

		/// <summary>
		/// Verifica se o bloco de código executando dentro da Action informada está lançando a exceção informada.
		/// </summary>
		/// <remarks>
		/// Além do lançamento da exceção, também será validado o tipo e a mensagem da exceção.
		/// </remarks>		
		/// <param name="expectedExceptionType">O tipo da exceção esperada.</param>
		/// <param name="expectedMessage">A mensagem esperada na exceção.</param>
		/// <param name="action">Action com o bloco de código que deve lançar a exceção.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static void IsThrowing(Type expectedExceptionType, string expectedMessage, Action action)
		{
			if (expectedExceptionType == null)
			{
				throw new ArgumentNullException("expectedExceptionType");
			}

			if (action == null)
			{
				throw new ArgumentNullException("action");
			}

			var exceptionThrown = ExceptionHelper.GetExceptionThrown(action);

			if (exceptionThrown == null)
			{
				AssertHelper.ThrowAssert("ExceptionAssert", "IsThrowing", "true", "false");
			}
			else
			{
				AssertHelper.AreEqual("ExceptionAssert", "IsThrowing", expectedExceptionType.FullName, exceptionThrown.GetType().FullName);

				if (!String.IsNullOrEmpty(expectedMessage))
				{
					AssertHelper.AreEqual("ExceptionAssert", "IsThrowing", expectedMessage, exceptionThrown.Message);
				}
			}
		}
		#endregion
	}
}
