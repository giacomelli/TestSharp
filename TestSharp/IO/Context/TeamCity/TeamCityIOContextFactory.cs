
namespace TestSharp.IO.Context.TeamCity
{
	/// <summary>
	/// TeamCity IO context factory.
	/// </summary>
	internal class TeamCityIOContextFactory : IIOContextFactory
	{
		/// <summary>
		/// Creates a directory discoverer.
		/// </summary>
		/// <returns>The directory discoverer.</returns>
		public IDirectoryDiscoverer CreateDirectoryDiscoverer()
		{
			return new TeamCityDirectoryDiscoverer();
		}
	}
}
