
namespace TestSharp.IO.Context
{
	/// <summary>
	/// Define uma factory para contexto de IO durante a execução dos testes.
	/// </summary>
	internal interface IIOContextFactory
	{
		#region Methods
		/// <summary>
		/// Cria um descobridor de diretórios.
		/// </summary>
		/// <returns>O descobridor de diretórios.</returns>
		IDirectoryDiscoverer CreateDirectoryDiscoverer();
		#endregion
	}
}
