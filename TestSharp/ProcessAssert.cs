using System.Security.Permissions;

namespace TestSharp
{
	/// <summary>
	/// Asserts para processos.
	/// </summary>
	[EnvironmentPermission(SecurityAction.LinkDemand)]
	public static class ProcessAssert
	{
		/// <summary>
		/// Verifica se o número de instâncias do processo é o esperado.
		/// </summary>
		/// <param name="expectedProcessesCount">O número esperado de instâncias do processo.</param>
		/// <param name="processName">O nome do processo.</param>
		public static void IsProcessInstancesCount(int expectedProcessesCount, string processName)
		{
			var actualProcessesCount = ProcessHelper.CountInstances(processName);

			if (actualProcessesCount != expectedProcessesCount)
			{
				AssertHelper.ThrowAssert("ProcessAssert", "IsProcessInstancesCount", expectedProcessesCount, actualProcessesCount);
			}
		}
	}
}
