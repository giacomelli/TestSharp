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
				startInfo.UseShellExecute = false;

				if (waitForExit)
				{
					startInfo.StandardOutputEncoding = Encoding.GetEncoding("ibm850");
					startInfo.RedirectStandardOutput = true;
				}

				// Se inicia com uma variável de sistema.
				if (exePath.StartsWith("%", StringComparison.OrdinalIgnoreCase))
				{
					startInfo.FileName = Environment.ExpandEnvironmentVariables(exePath);
				}
				else
				{
#if WIN
					startInfo.FileName = Path.GetFullPath(exePath);
#else
					startInfo.FileName = exePath;
#endif
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
			// Ugly, but needed to avoid finalizing instances to be counted.
#if WIN
			Thread.Sleep(5000); 
#else	
			Thread.Sleep(2000); 
#endif
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
		/// Kill the process with the specified ID.
		/// </summary>
		/// <param name="id">Identifier.</param>
		public static void Kill (int id)
		{
			var ps = Process.GetProcessById (id);

			if (ps != null) {
				ps.Kill ();
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
