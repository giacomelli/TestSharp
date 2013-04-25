
namespace TestSharp.IO.Context
{
	/// <summary>
	/// Um descobridor de diretórios durante a execução dos testes.
	/// </summary>
	internal interface IDirectoryDiscoverer
	{
		/// <summary>
		/// Tenta descobrir o camanhio do diretório com o nome informado.
		/// </summary>
		/// <param name="folderName">O nome do diretório.</param>
		/// <returns>O caminho do diretório.</returns>
		string DiscoverPath(string folderName);
	}
}
