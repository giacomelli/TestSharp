
namespace TestSharp.IO.Context.VisualStudio
{
	/// <summary>
	/// Cria uma fábrica de contexto de IO para os testes quando executados no Visual Studio.
	/// </summary>
	internal class VisualStudioIOContextFactory : IIOContextFactory
	{
		/// <summary>
		/// Cria um descobridor de diretórios.
		/// </summary>
		/// <returns>O descobridor de diretórios.</returns>
		public IDirectoryDiscoverer CreateDirectoryDiscoverer()
		{
			return new VisualStudioDirectoryDiscoverer();
		}
	}
}
