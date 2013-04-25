using TestSharp.IO.Context;

namespace TestSharp
{
	/// <summary>
	/// Utilitários para projetos do Visual Studio.
	/// </summary>
	public static class VSProjectHelper
	{
		/// <summary>
		/// Obtém o caminho para a pasta do projeto.
		/// </summary>
		/// <param name="projectFolderName">O nome da pasta do projeto.</param>
		/// <returns>O caminho para a pasta do projeto.</returns>
		public static string GetProjectFolderPath(string projectFolderName)
		{
			return IOContext.DiscoverPath(projectFolderName);
		}
	}
}
