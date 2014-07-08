
namespace TestSharp.IO.Context.VisualStudio
{
	/// <summary>
	/// Visual Studio IO context factory.
	/// </summary>
	internal class VisualStudioIOContextFactory : IIOContextFactory
	{
		/// <summary>
		/// Creates a directory discoverer.
		/// </summary>
		/// <returns>The directory discoverer.</returns>
		public IDirectoryDiscoverer CreateDirectoryDiscoverer()
		{
			return new VisualStudioDirectoryDiscoverer();
		}
	}
}
