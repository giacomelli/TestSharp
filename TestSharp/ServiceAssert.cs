
namespace TestSharp
{
	/// <summary>
	/// Asserts para serviços.
	/// </summary>
	public static class ServiceAssert
	{
		/// <summary>
		/// Verifica se o serviço está em execução.
		/// </summary>
		/// <param name="serviceName">O nome do serviço.</param>
		public static void IsRunning(string serviceName)
		{
			if (!ServiceHelper.IsRunning(serviceName))
			{
				AssertHelper.ThrowAssert("ServiceAssert", "IsRunning", "true", "false");
			}
		}

		/// <summary>
		/// Verifica se o serviço está parado.
		/// </summary>
		/// <param name="serviceName">O nome do serviço.</param>
		public static void IsStopped(string serviceName)
		{
			if (!ServiceHelper.IsStopped(serviceName))
			{
				AssertHelper.ThrowAssert("ServiceAssert", "IsStopped", "true", "false");
			}
		}
	}
}
