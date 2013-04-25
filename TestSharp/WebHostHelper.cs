using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using TestSharp.IO.Context;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace TestSharp
{
	/// <summary>
	/// Utilitária para auxiliar na inicialização e finalização do servidor de desenvolvimento do VS.
	/// </summary>
    public static class WebHostHelper
    {
		#region Properties
		/// <summary>
		/// Obtém o nome do processo do servidor de desenvolvimento do VS.
		/// </summary>
		public static string WebHostProcessName { get; private set; }
		
		/// <summary>
		/// Obtém o caminho do executável do servidor de desenvolvimento do VS.
		/// </summary>
		public static string WebDevWebServerPath { get; private set; }

		#endregion

		#region Constructor
		[SuppressMessage ("Microsoft.Globalization", "CA1302:DoNotHardcodeLocaleSpecificStrings", MessageId=@"\Program Files\"), 
		SuppressMessage ("Microsoft.Globalization", "CA1302:DoNotHardcodeLocaleSpecificStrings", MessageId=@"\Program Files (x86)\"), 
		SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		static WebHostHelper()
        {
			var frameworkVersion = RuntimeEnvironment.GetSystemVersion();

			switch (frameworkVersion)
			{
				case "v2.0.50727":
					WebDevWebServerPath = @"C:\Program Files\Common Files\microsoft shared\DevServer\9.0\WebDev.WebServer.EXE";
					break;

				default:
					WebDevWebServerPath = @"C:\Program Files\Common Files\microsoft shared\DevServer\10.0\WebDev.WebServer40.EXE";
					break;
			}
		
			if (!File.Exists(WebDevWebServerPath))
			{
				WebDevWebServerPath = WebDevWebServerPath.Replace(@"\Program Files\", @"\Program Files (x86)\");

				if (!File.Exists(WebDevWebServerPath))
				{
					WebDevWebServerPath = ConfigurationManager.AppSettings["TestSharp::WebHostHelper::WebDevWebServerPath"];

					if (String.IsNullOrEmpty(WebDevWebServerPath))
					{
						throw new InvalidOperationException(@"TestSharp could not found de WebDevWebServer. Please add the key 'TestSharp::WebHostHelper::WebDevWebServerPath' to AppSettings with the path of your WebDev.WebServer (eg.: C:\Program Files\Common Files\microsoft shared\DevServer\10.0\WebDev.WebServer40.EXE).");
					}
				}
			}

			WebHostProcessName = Path.GetFileNameWithoutExtension(WebDevWebServerPath);
			KillAll();
        }
        #endregion
		
        #region Methods
		/// <summary>
		/// Encerra todos os processos do WebDev.WebServer.
		/// </summary>
		[EnvironmentPermission(SecurityAction.LinkDemand)]
		public static void KillAll()
		{
			ProcessHelper.KillAll(WebHostProcessName);
		}

		/// <summary>
		/// Inicia a hospedagem do web site referente a pasta de projeto informada através do WebDev.WebServer.
		/// </summary>
		/// <param name="projectFolderName">O nome da pasta do projeto. NOTA: apenas o nome.</param>
		/// <param name="port">Porta que deve ser utilizada no WebDev.WebServer.</param>
		[EnvironmentPermission(SecurityAction.LinkDemand)]
		public static void Start(string projectFolderName, int port)
        {			
            var path = Path.GetFullPath(VSProjectHelper.GetProjectFolderPath(projectFolderName));			
            var arguments = String.Format(CultureInfo.InvariantCulture, "/port:{0} /vpath:/ /path:\"{1}\"", port, path);
            Process.Start(WebDevWebServerPath, arguments);
        }

		/// <summary>
		/// Inicia a hospedagem do web site referente a pasta de projeto informada através do WebDev.WebServer e aguarda até que o servidor comece a responder.
		/// </summary>
		/// <param name="projectFolderName">O nome da pasta do projeto. NOTA: apenas o nome.</param>
		/// <param name="port">Porta que deve ser utilizada no WebDev.WebServer.</param>
		[EnvironmentPermission(SecurityAction.LinkDemand)]
		public static void StartAndWaitForResponse(string projectFolderName, int port)
		{
			Start(projectFolderName, port);

			// Aguarda até que o servidor esteja pronto para receber requisições.
			// Nas máquinas de desenvolvimento não é necessário, mas no de integração contínua sim ;)
			var url = "http://localhost:" + port;

			while (!NetHelper.IsResponding(url))
			{
				Thread.Sleep(1000);
			}
		}
        #endregion
    }
}
