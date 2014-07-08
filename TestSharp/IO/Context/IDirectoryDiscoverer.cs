namespace TestSharp.IO.Context
{
	/// <summary>
	/// Defines an interface for a directory discoverer.
	/// </summary>
	internal interface IDirectoryDiscoverer
	{
		/// <summary>
		/// Try to discover the path of the folder with the specified name.
		/// </summary>
		/// <returns>The path.</returns>
		/// <param name="folderName">The folder name.</param>
		string DiscoverPath(string folderName);
	}
}
