using System.IO;
using System.Net;
using System.Text;

namespace TestSharp
{
	/// <summary>
	/// Utilitária para questões relativas a rede.
	/// </summary>
	public static class NetHelper
	{
		#region Methods
		/// <summary>
		/// Obtém o conteúdo de uma página web.
		/// </summary>
		/// <param name="url">A URL da página.</param>
		/// <returns>O conteúdo da página.</returns>
		public static string GetContent(string url)
		{
			return GetContent(url, null);
		}

		/// <summary>
		/// Obtém o conteúdo de uma página web.
		/// </summary>
		/// <param name="url">A URL da página.</param>
		/// <param name="timeout">O timeout, em milisegundos, para a requisição. O padrão é 100.000.</param>
		/// <returns>O conteúdo da página.</returns>
		public static string GetContent(string url, int timeout)
		{
			return GetContent(url, null, timeout);
		}

		/// <summary>
		/// Obtém o conteúdo de uma página web.
		/// </summary>
		/// <param name="url">A URL da página.</param>
		/// <param name="encoding">O enconding utilizado para ler a página.</param>
		/// <param name="timeout">O timeout, em milisegundos, para a requisição. O padrão é 100.000.</param>
		/// <returns>O conteúdo da página.</returns>
		public static string GetContent(string url, Encoding encoding, int timeout = 100000)
		{
			WebResponse response = null;
			StreamReader reader = null;

			try
			{				
				WebRequest request = WebRequest.Create(url);				
				request.Timeout = timeout;				
				response = request.GetResponse();

				if (encoding == null)
				{
					reader = new StreamReader(response.GetResponseStream());
				}
				else
				{
					reader = new StreamReader(response.GetResponseStream(), encoding);
				}

				return reader.ReadToEnd();
			}
			finally
			{
				if (response != null)
				{
					response.Close();

					if (reader != null)
					{
						reader.Close();
					}
				}
			}
		}

		/// <summary>
		/// Verifica se a URL foi redirecionado no servidor.
		/// </summary>
		/// <param name="url">A URL.</param>
		/// <param name="timeout">O timeout, em milisegundos, para a requisição. O padrão é 100.000.</param>
		/// <returns>True se foi redirecionado, false no contrário.</returns>
		public static bool IsRedirected(string url, int timeout = 100000)
		{
			HttpWebResponse response = null;

			try
			{
				var request = (HttpWebRequest)WebRequest.Create(url);
				request.Timeout = timeout;
				request.AllowAutoRedirect = false;
				response = (HttpWebResponse)request.GetResponse();

				if (response.StatusCode == HttpStatusCode.MovedPermanently
				|| response.StatusCode == HttpStatusCode.Moved)
				{
					return true;
				}

				if (response.StatusCode == HttpStatusCode.OK
				|| response.StatusCode == HttpStatusCode.Found)
				{
					return response.Headers["location"] != null;
				}

				return false;
			}
			finally
			{
				if (response != null)
				{
					response.Close();
				}
			}
		}

		/// <summary>
		/// Requisita uma URL sem considerar seu conteúdo.
		/// <remarks>Útil para casos onde não é necessário ler o conteúdo da página e onde redirecionamentos acabam em status code 403, por exemplo.</remarks>
		/// </summary>
		/// <param name="url">A URL da página.</param>
		/// <param name="throwError">Se deve lançar erro caso ocorra uma exceção, por exemplo 403.</param>
		public static void Request(string url, bool throwError = true)
		{
			try
			{
				GetContent(url);
			}
			catch
			{
				if (throwError)
				{
					throw;
				}
			}
		}

		/// <summary>
		/// Verifica se uma URL está sendo "respondida" pelo servidor.
		/// </summary>
		/// <param name="url">A URL a ser verificada.</param>
		/// <returns>True se está respondend, false no contrário.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static bool IsResponding(string url)
		{
			try
			{
				GetContent(url);
				return true;
			}
			catch
			{
				return false;
			}
		}
		#endregion
	}
}
