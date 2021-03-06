﻿using System;
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
		#region Fields
		private static Queue<Process> s_webHostProcesses = new Queue<Process> ();
		#endregion

		#region Constructor
		[SuppressMessage ("Microsoft.Globalization", "CA1302:DoNotHardcodeLocaleSpecificStrings", MessageId=@"\Program Files\"), 
		SuppressMessage ("Microsoft.Globalization", "CA1302:DoNotHardcodeLocaleSpecificStrings", MessageId=@"\Program Files (x86)\"), 
		SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
		static WebHostHelper()
		{
#if WIN
			WebDevWebServerPath = null;
			var frameworkVersion = RuntimeEnvironment.GetSystemVersion();
			var possiblePaths = new List<string>();

			switch (frameworkVersion)
			{
				case "v2.0.50727":
					possiblePaths.Add(@"C:\Program Files\Common Files\microsoft shared\DevServer\9.0\WebDev.WebServer.EXE");
					break;

				default:
					for (int version = 15; version >= 9; version--)
					{                        
						possiblePaths.Add(String.Format(CultureInfo.InvariantCulture, @"C:\Program Files\Common Files\microsoft shared\DevServer\{0}.0\WebDev.WebServer40.EXE", version));
						possiblePaths.Add(String.Format(CultureInfo.InvariantCulture, @"C:\Program Files (x86)\Common Files\microsoft shared\DevServer\{0}.0\WebDev.WebServer40.EXE", version));
					}

					break;
			}

			foreach (var path in possiblePaths)
			{
				if (File.Exists(path))
				{
					WebDevWebServerPath = path;
					break;
				}
			}
		
			if (String.IsNullOrEmpty(WebDevWebServerPath))
			{
				WebDevWebServerPath = ConfigurationManager.AppSettings["TestSharp::WebHostHelper::WebDevWebServerPath"];

				if (String.IsNullOrEmpty(WebDevWebServerPath))
				{
					throw new InvalidOperationException(@"TestSharp could not found de WebDevWebServer. Please add the key 'TestSharp::WebHostHelper::WebDevWebServerPath' to AppSettings with the path of your WebDev.WebServer (eg.: C:\Program Files\Common Files\microsoft shared\DevServer\10.0\WebDev.WebServer40.EXE).");
				}
			}

			WebHostProcessName = Path.GetFileNameWithoutExtension(WebDevWebServerPath);
#else
			WebDevWebServerPath = "xsp";
			WebHostProcessName = "xsp";
#endif
			KillAll();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Obtém o nome do processo do servidor de desenvolvimento do VS.
		/// </summary>
		public static string WebHostProcessName { get; private set; }

		/// <summary>
		/// Obtém o caminho do executável do servidor de desenvolvimento do VS.
		/// </summary>
		public static string WebDevWebServerPath { get; private set; }

		/// <summary>
		/// Gets the instances count of running web hosts.
		/// </summary>
		/// <value>The instances count.</value>
		public static int InstancesCount { 
			get {
				return s_webHostProcesses.Count;
			}
		}
		#endregion

		
		#region Methods
		/// <summary>
		/// Encerra todos os processos do WebDev.WebServer.
		/// </summary>
		[EnvironmentPermission(SecurityAction.LinkDemand)]
		public static void KillAll()
		{
			while(s_webHostProcesses.Count > 0)
			{
				ProcessHelper.Kill (s_webHostProcesses.Dequeue().Id);
			}
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
#if WIN
			var arguments = String.Format(CultureInfo.InvariantCulture, "/port:{0} /vpath:/ /path:\"{1}\"", port, path);
#else
						
			var arguments = String.Format(CultureInfo.InvariantCulture, "--port {0} --root {1} --nonstop", port, path);
#endif
			var startInfo = new ProcessStartInfo ();
			startInfo.FileName = WebDevWebServerPath;
			startInfo.Arguments = arguments;
			startInfo.UseShellExecute = false;
			s_webHostProcesses.Enqueue(Process.Start (startInfo)); 
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
