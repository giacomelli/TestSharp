using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace TestSharp
{
	/// <summary>
	/// Utilitária para questões relativas a processos.
	/// </summary>
	[EnvironmentPermission(SecurityAction.LinkDemand)]
	public static class ProcessHelper
	{
		#region Methods
		/// <summary>
		/// Executa um processo.
		/// </summary>
		/// <param name="exePath">O caminho do executável do processo.</param>
		/// <param name="arguments">Argumentos para o processo.</param>
		/// <param name="waitForExit">Se deve aguardar pelo fim da execução do processo.</param>
		/// <returns>O conteúdo da saída do processo.</returns>		
		public static string Run(string exePath, string arguments = "", bool waitForExit = true)
		{
			string output = String.Empty;
			using (var p = new Process())
			{
				var startInfo = p.StartInfo;
				startInfo.CreateNoWindow = true;
				startInfo.WindowStyle = ProcessWindowStyle.Hidden;

				if (waitForExit)
				{
					startInfo.StandardOutputEncoding = Encoding.GetEncoding("ibm850");
					startInfo.UseShellExecute = false;
					startInfo.RedirectStandardOutput = true;
				}

				// Se inicia com uma variável de sistema.
				if (exePath.StartsWith("%", StringComparison.OrdinalIgnoreCase))
				{
					startInfo.FileName = Environment.ExpandEnvironmentVariables(exePath);
				}
				else
				{
					startInfo.FileName = Path.GetFullPath(exePath);
				}
				
				startInfo.Arguments = arguments;

				p.Start();

				if (waitForExit)
				{
					output = p.StandardOutput.ReadToEnd();
					p.WaitForExit();
				}

				return output;
			}
		}

		/// <summary>
		/// Conta o número de instâncias do processo.
		/// </summary>
		/// <param name="processName">Nome do processo.</param>
		/// <returns>Número de instâncias do processo.</returns>        
		public static int CountInstances(string processName)
		{            
			Thread.Sleep(5000); // Feio, mas necessário para evitar que instâncias que estão em finalização sejam contados.
			return Process.GetProcessesByName(processName).Length;            
		}

		/// <summary>
		/// Aguarda pela finalização do processo informado.
		/// </summary>
		/// <param name="processName">Nome do processo.</param>		
		public static void WaitForExit(string processName)
		{
			var ps = Process.GetProcessesByName(processName);

			if (ps.Length > 0)
			{
				ps[0].WaitForExit();
			}
		}

		/// <summary>
		/// Encerra a primeira instância do processo informado.
		/// </summary>
		/// <param name="processName">O nome do processo.</param>	    
		public static void KillFirst(string processName)
		{
			var ps = Process.GetProcessesByName(processName);

			if (ps.Length > 0)
			{
				ps[0].Kill();
			}
		}

		/// <summary>
		/// Encerra todas as instâncias do processo informado.
		/// </summary>
		/// <param name="processName">O nome do processo.</param>		
		public static void KillAll(string processName)
		{
			var ps = Process.GetProcessesByName(processName);

			foreach (var p in ps)
			{
				try
				{
					p.Kill();
				}
				catch (Win32Exception)
				{
					// http://msdn.microsoft.com/en-us/library/system.diagnostics.process.kill.aspx
					Debug.Write(String.Format(CultureInfo.InvariantCulture, "ProcessHelper.KillAll could not kill the process '{0}' because it is already terminating.", processName));
				}
				catch (InvalidOperationException)
				{
					// http://msdn.microsoft.com/en-us/library/system.diagnostics.process.kill.aspx
					Debug.Write(String.Format(CultureInfo.InvariantCulture, "ProcessHelper.KillAll could not kill the process '{0}' because it is already terminated.", processName));
				}

			}
		}
		#endregion
	}
}
