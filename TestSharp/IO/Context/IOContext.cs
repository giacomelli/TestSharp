using TestSharp.IO.Context.TeamCity;
using TestSharp.IO.Context.VisualStudio;

namespace TestSharp.IO.Context
{
	/// <summary>
	/// Defines the IO context used internally by utilities during the tests executing.
	/// </summary>
	internal static class IOContext : IDirectoryDiscoverer
	{
		#region Fields
		private static IDirectoryDiscoverer s_visualStudioDirectoryDiscover = new VisualStudioIOContextFactory().CreateDirectoryDiscoverer();
		private static IDirectoryDiscoverer s_teamCityDirectoryDiscover = new TeamCityIOContextFactory().CreateDirectoryDiscoverer();
		#endregion
		
		#region Public Methods
		/// <summary>
		/// Try to discover the path of the folder with the specified name.
		/// </summary>
		/// <returns>The path.</returns>
		/// <param name="folderName">The folder name.</param>
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
