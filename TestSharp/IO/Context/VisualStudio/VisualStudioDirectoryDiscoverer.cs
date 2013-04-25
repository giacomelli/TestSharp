using System.IO;

namespace TestSharp.IO.Context.VisualStudio
{
	/// <summary>
	/// Descobridor de diretórios para os testes quando executados no Visual Studio.
	/// </summary>
	internal class VisualStudioDirectoryDiscoverer : IDirectoryDiscoverer
	{
		#region Public Methods
		/// <summary>
		/// Tenta descobrir o camanhio do diretório com o nome informado.
		/// </summary>
		/// <param name="folderName">O nome do diretório.</param>
		/// <returns>
		/// O caminho do diretório.
		/// </returns>
		public string DiscoverPath(string folderName)
		{
			return DiscoverFullPath(folderName, Directory.GetCurrentDirectory());
		}
		#endregion

		#region Private Methods
		private static string DiscoverFullPath(string folderName, string rootFolderPath)
		{
			var path = Path.Combine(rootFolderPath, folderName);

			if (Directory.Exists(path))
			{
				return path;
			}

			var parentFolder = Directory.GetParent(rootFolderPath);

			if (parentFolder == null)
			{
				return null;
			}
			else
			{
				return DiscoverFullPath(folderName, parentFolder.FullName);
			}
		}
		#endregion
	}
}
