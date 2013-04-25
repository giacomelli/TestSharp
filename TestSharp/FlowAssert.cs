using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSharp
{
	/// <summary>
	/// Asserts para fluxos de execução.
	/// </summary>
	public static class FlowAssert
	{
		/// <summary>
		/// Valida se pelo menos um dos fluxos de execução executa sem exceção.
		/// </summary>
		/// <param name="flows">As actions para os fluxos de execução a serem executados.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static void IsAtLeastOneOk(params Action[] flows)
		{
			if (flows == null)
			{
				throw new ArgumentNullException("flows");
			}

			bool ok = false;
		
			foreach (var a in flows)
			{
				try
				{
					a();
					ok = true;
					break;
				}
				catch
				{
					ok = false;
				}
			}

			if (!ok)
			{
				AssertExceptionHelper.ThrowAssert("FlowAssert", "IsAtLeastOneOk", true, false);
			}
		}

		/// <summary>
		/// Tenta executar o fluxo de execução até que não gere exceção ou até alcançar o número máximo de tentativas.
		/// </summary>
		/// <param name="maxAttempts">Número máximo de tentativas.</param>
		/// <param name="flow">O fluxo a ser executado.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static void IsAtLeastOneAttemptOk(int maxAttempts, Action flow)
		{
			bool ok = false;

			for(int i = 0; i < maxAttempts; i++)
			{
				try
				{
					flow();
					ok = true;
					break;
				}
				catch
				{
					ok = false;
				}
			}

			if (!ok)
			{
				AssertExceptionHelper.ThrowAssert("FlowAssert", "TryUntilOk", true, false);
			}
		}
	}
}
