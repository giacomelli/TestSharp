using System;

namespace TestSharp
{
	/// <summary>
	/// Asserts para DateTime
	/// </summary>
	public static class DateTimeAssert
	{
		#region Public Methods
		/// <summary>
		/// Verifica se duas datas são iguais desconsiderando os milisegundos, ou seja, compara até ano, mês, dia, horas, minutos e segundos.
		/// </summary>
		/// <param name="expectedDateTime">A data esperada.</param>
		/// <param name="actualDateTime">A data atual.</param>
		public static void AreEqualIgnoringMilliseconds(DateTime expectedDateTime, DateTime actualDateTime)
		{
			var expected = CreateDateTimeIgnoringMilliseconds(expectedDateTime);
			var actual = CreateDateTimeIgnoringMilliseconds(actualDateTime);

			if (actual != expected)
			{
				AssertExceptionHelper.ThrowAssert("DateTimeAssert", "AreEqualIgnoringMilliseconds", expected, actual);
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Cria um novo DateTime sem os milisegundos.
		/// </summary>
		/// <param name="orignal">O DateTime original.</param>
		/// <returns>O novo DateTime sem milisegundos.</returns>
		private static DateTime CreateDateTimeIgnoringMilliseconds(DateTime orignal)
		{
			return new DateTime(orignal.Year, orignal.Month, orignal.Day, orignal.Hour, orignal.Minute, orignal.Second);
		}
		#endregion
	}
}
