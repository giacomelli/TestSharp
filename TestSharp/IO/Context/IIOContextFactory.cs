namespace TestSharp.IO.Context
{
	/// <summary>
	/// Defines the interface o IO context's factory.
	/// </summary>
	internal interface IIOContextFactory
	{
		#region Methods
		/// <summary>
		/// Creates a directory discoverer.
		/// </summary>
		/// <returns>The directory discoverer.</returns>
		IDirectoryDiscoverer CreateDirectoryDiscoverer();
		#endregion
	}
}
