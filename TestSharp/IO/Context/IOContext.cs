using TestSharp.IO.Context.TeamCity;
using TestSharp.IO.Context.VisualStudio;

namespace TestSharp.IO.Context
{
	/// <summary>
	/// Contexto de IO utilizando internamente pelos utilitários durante a execução dos testes.
	/// </summary>
	internal static class IOContext
	{
		#region Fields
		private static IDirectoryDiscoverer s_visualStudioDirectoryDiscover = new VisualStudioIOContextFactory().CreateDirectoryDiscoverer();
		private static IDirectoryDiscoverer s_teamCityDirectoryDiscover = new TeamCityIOContextFactory().CreateDirectoryDiscoverer();
		#endregion
		
		#region Public Methods
		/// <summary>
		/// Tenta descobrir qual o caminho da pasta com o nome informado.
		/// </summary>
		/// <param name="folderName">O nome da pasta.</param>
		/// <returns>O caminho da pasta informada. Se não for localizada será retornado nulo.</returns>
		public static string DiscoverPath(string folderName)
		{
			var fullPath = s_visualStudioDirectoryDiscover.DiscoverPath(folderName);

			if (fullPath == null)
			{
				fullPath = s_teamCityDirectoryDiscover.DiscoverPath(folderName);
			}
			
			return fullPath;
		}
		#endregion
	}
}
