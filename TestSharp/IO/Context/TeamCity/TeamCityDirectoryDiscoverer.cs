using System;
using System.IO;
using System.Linq;

namespace TestSharp.IO.Context.TeamCity
{
	/// <summary>
	/// TeamCity directory discoverer.
	/// </summary>
	internal class TeamCityDirectoryDiscoverer : IDirectoryDiscoverer
	{
		#region Public Methods
		/// <summary>
		/// Try to discover the path of the folder with the specified name.
		/// </summary>
		/// <returns>The path.</returns>
		/// <param name="folderName">The folder name.</param>
		public string DiscoverPath(string folderName)
		{
			var dir = Directory.GetCurrentDirectory().ToUpperInvariant();
			string path = null;
			
			if (dir.Contains(@"\BUILDAGENT\"))
			{
				var dirParts = dir.Split(new string[] { @"BUILDAGENT\" }, StringSplitOptions.RemoveEmptyEntries);

				if (dirParts.Length > 1)
				{
					var rootPath = Path.Combine(dirParts[0], @"BUILDAGENT\WORK\");
					path = DiscoverPath(folderName, rootPath);
				}
			}

			return path;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Discovers the path.
		/// </summary>
		/// <returns>The path.</returns>
		/// <param name="folderName">Folder name.</param>
		/// <param name="rootPath">Root path.</param>
		private string DiscoverPath(string folderName, string rootPath)
		{
			var fullPath = Path.Combine(rootPath, folderName);

			if (!Directory.Exists(fullPath))
			{
				fullPath = null;
				var subDirs = DirectoryHelper.GetDirectoriesInfos(rootPath).OrderByDescending(d => d.LastWriteTime).Select(d => d.FullName.ToUpperInvariant());

				foreach (var subDir in subDirs)
				{
					if (subDir.Contains(".old"))
					{
						continue;
					}

					fullPath = DiscoverPath(folderName, subDir);

					if(!String.IsNullOrEmpty(fullPath))
					{
						break;
					}
				}

			}

			return fullPath;
		}
		#endregion
	}
}
