using System.ServiceProcess;
using System.Security.Permissions;

namespace TestSharp
{
	/// <summary>
	/// Utilitária para questões referentes a Windows Services.
	/// </summary>
    public static class ServiceHelper
    {
        #region Methods
		/// <summary>
		/// Força o início da execução de um serviço. Caso o serviço já esteja em execução, esse será encerrado e iniciado novamente.
		/// </summary>
		/// <param name="serviceName">O nome do serviço.</param>
        public static void ForceStart(string serviceName)
        {
			using (var service = new ServiceController(serviceName))
			{
				if (service.Status != ServiceControllerStatus.Stopped)
				{
					service.Stop();
				}
				
				service.WaitForStatus(ServiceControllerStatus.Stopped);
				service.Start();
				service.WaitForStatus(ServiceControllerStatus.Running);
			}
        }

		/// <summary>
		/// Para a execução do serviço informado.
		/// </summary>
		/// <param name="serviceName">O nome do serviço.</param>
        public static void Stop(string serviceName)
        {
			using (var service = new ServiceController(serviceName))
			{
				if (service.Status != ServiceControllerStatus.Stopped)
				{
					service.Stop();
				}

				service.WaitForStatus(ServiceControllerStatus.Stopped);
			}
        }

		/// <summary>
		/// Verifica se o serviço informado está em execução.
		/// </summary>
		/// <param name="serviceName">O nome do serviço.</param>
		/// <returns>True se o serviço está em execução, false no contrário.</returns>
		public static bool IsRunning(string serviceName)
		{
			using (var service = new ServiceController(serviceName))
			{
				return service.Status == ServiceControllerStatus.Running;
			}
		}

		/// <summary>
		/// Verifica se o serviço informado está parado.
		/// </summary>
		/// <param name="serviceName">O nome do serviço.</param>
		/// <returns>True se o serviço está parado, false no contrário.</returns>
		public static bool IsStopped(string serviceName)
		{
			using (var service = new ServiceController(serviceName))
			{
				return service.Status == ServiceControllerStatus.Stopped;
			}
		}

		/// <summary>
		/// Realiza a instalação do serviço.
		/// </summary>
		/// <param name="serviceFilePath">O caminho do arquivo executável do serviço a ser instalado.</param>
		[EnvironmentPermission(SecurityAction.LinkDemand)]
		public static void Install(string serviceFilePath)
		{
			ProcessHelper.Run(@"C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe", "\"" + serviceFilePath + "\"", true);
		}

		/// <summary>
		/// Realiza a desinstalação do serviço.
		/// </summary>
		/// <param name="serviceFilePath">O caminho do arquivo executável do serviço a ser desinstalado.</param>
		[EnvironmentPermission(SecurityAction.LinkDemand)]
		public static void Uninstall(string serviceFilePath)
		{
			ProcessHelper.Run(@"C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe", "/u \"" + serviceFilePath + "\"", true);
		}

		/// <summary>
		/// Realiza a reinstalação do serviço.
		/// </summary>
		/// <param name="serviceFilePath">O caminho do arquivo executável do serviço a ser reinstalado.</param>
		[EnvironmentPermission(SecurityAction.LinkDemand)]
		public static void Reinstall(string serviceFilePath)
		{
			Uninstall(serviceFilePath);
			Install(serviceFilePath);
		}
        #endregion
	}
}
