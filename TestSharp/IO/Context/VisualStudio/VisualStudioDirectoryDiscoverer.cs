using System.IO;

namespace TestSharp.IO.Context.VisualStudio
{
	/// <summary>
	/// Visual Studio directory discoverer.
	/// </summary>
	internal class VisualStudioDirectoryDiscoverer : IDirectoryDiscoverer
	{
		#region Public Methods
		/// <summary>
		/// Try to discover the path of the folder with the specified name.
		/// </summary>
		/// <returns>The path.</returns>
		/// <param name="folderName">The folder name.</param>
		public string DiscoverPath(string folderName)
		{
			return DiscoverFullPath(folderName, Directory.GetCurrentDirectory());
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Discovers the full path.
		/// </summary>
		/// <returns>The full path.</returns>
		/// <param name="folderName">Folder name.</param>
		/// <param name="rootFolderPath">Root folder path.</param>
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
