
using Ionic.Utils.Zip;
using System.IO;
using System;
namespace TestSharp
{
	/// <summary>
	/// Utilitários para compactação/descompatação no formato ZIP.
	/// </summary>
	public static class ZipHelper
	{
		/// <summary>
		/// Descompacta todo o arquivo informado no diretório de destino.
		/// </summary>
		/// <param name="zipFilePath">O caminho do arquivo .zip.</param>
		/// <param name="destinationDirectoryPath">O diretório destino para os arquivos descompactados.</param>
		public static void ExtractAll(string zipFilePath, string destinationDirectoryPath)
		{
			if (!File.Exists(zipFilePath))
			{
				throw new ArgumentException("Arquivo '" + zipFilePath + "' não existe.", "zipFilePath");
			}

			if (!Directory.Exists(destinationDirectoryPath))
			{
				throw new ArgumentException("Diretório destino '" + destinationDirectoryPath + "' não existe.", "destinationDirectoryPath");
			}

			ZipFile.Read(zipFilePath).ExtractAll(destinationDirectoryPath);
		}
	}
}
