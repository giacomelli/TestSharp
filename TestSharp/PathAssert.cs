using System.IO;

namespace TestSharp
{
	/// <summary>
	/// Asserts para caminhos no sistema de arquivos.
	/// </summary>
	public static class PathAssert
	{
		/// <summary>
		/// Verifica se o caminho é absoluto.
		/// </summary>
		/// <param name="actualPath">O caminho a ser verificado.</param>
		public static void IsPathRooted(string actualPath)
		{
			if (!Path.IsPathRooted(actualPath))
			{
				AssertExceptionHelper.ThrowAssert("PathAssert", "IsPathRooted", true, false);
			}
		}
	}
}
