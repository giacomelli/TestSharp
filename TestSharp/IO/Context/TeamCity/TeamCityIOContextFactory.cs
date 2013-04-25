
namespace TestSharp.IO.Context.TeamCity
{
	/// <summary>
	/// Cria uma fábrica de contexto de IO para os testes quando executados no TeamCity.
	/// </summary>
	internal class TeamCityIOContextFactory : IIOContextFactory
	{
		/// <summary>
		/// Cria um descobridor de diretórios.
		/// </summary>
		/// <returns>O descobridor de diretórios.</returns>
		public IDirectoryDiscoverer CreateDirectoryDiscoverer()
		{
			return new TeamCityDirectoryDiscoverer();
		}
	}
}
