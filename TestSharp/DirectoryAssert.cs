using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestSharp
{
	/// <summary>
	/// Asserts para diretórios.
	/// </summary>
	public static class DirectoryAssert
	{
		/// <summary>
		/// Verfica se o diretório informado existe.
		/// </summary>
		/// <param name="directoryPath"></param>
		public static void Exists(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				AssertExceptionHelper.ThrowAssert("DirectoryAssert", "Exists", true, false);
			}
		}

		/// <summary>
		/// Verifica se o número de arquivos existentes no diretório é o esperado.
		/// </summary>
		/// <param name="expectedFilesCount">O número de arquivos esperado.</param>
		/// <param name="directoryPath">O diretório a ser contabilizado.</param>
		/// <param name="filePattern">O padrão para o nome dos arquivos a serem contados.</param>
		/// <param name="countSubdirectoriesFiles">Se deve contabilizar os arquivos dos subdiretórios.</param>
		public static void IsFilesCount(int expectedFilesCount, string directoryPath, string filePattern = "*.*", bool countSubdirectoriesFiles = false)
		{
			var actualFilesCount = DirectoryHelper.CountAllFiles(directoryPath, filePattern, countSubdirectoriesFiles);

			if (actualFilesCount != expectedFilesCount)
			{
				AssertExceptionHelper.ThrowAssert("DirectoryAssert", "IsFilesCount", expectedFilesCount, actualFilesCount);
			}
		}

		/// <summary>
		/// Verifica se o conteúdo de todos os arquivos é o esperado.
		/// </summary>
		/// <param name="expectedAllFilesContent">O conteúdo esperado de todos os arquivo. O conteúdo de cada arquivo é separado por uma nova linha (\r\n).</param>
		/// <param name="directoryPath">O diretório a ser lido.</param>
		/// <param name="filePattern">O padrão para o nome dos arquivos a serem lidos.</param>
		public static void IsAllFilesContent(string expectedAllFilesContent, string directoryPath, string filePattern = "*.*")
		{
			var actualAllFilesContent = DirectoryHelper.ReadAllFiles(directoryPath, filePattern);

			if (!actualAllFilesContent.Equals(expectedAllFilesContent))
			{
				AssertExceptionHelper.ThrowAssert("DirectoryAssert", "IsAllFilesContent", expectedAllFilesContent, actualAllFilesContent);
			}
		}

		/// <summary>
		/// Verifica se os diretórios são iguais (nomes e conteúdo dos arquivos e subdiretórios recursivamente).
		/// </summary>
		/// <param name="expectedDirectoryPath">O caminho do diretório esperado.</param>
		/// <param name="actualDirectoryPath">O caminho do diretório a ser comparado.</param>
		public static void AreEqual(string expectedDirectoryPath, string actualDirectoryPath)
		{
			// Compara os nomes dos arquivos.
			var expectedFiles = Directory.GetFiles(expectedDirectoryPath);
			var actualFiles = Directory.GetFiles(actualDirectoryPath);

			// Se a quantidade de arquivos for diferente, então encerra e lança a exceção.
			if (expectedFiles.Length != actualFiles.Length)
			{
				AssertExceptionHelper.ThrowAssert("DirectoryAssert", "AreEqual", expectedFiles.Length + " files", actualFiles.Length + " files");
			}

			for (int i = 0; i < expectedFiles.Length; i++)
			{
				var expectedFilePath = expectedFiles[i];
				var actualFilePath = actualFiles[i];
				var expectedFileName = Path.GetFileName(expectedFilePath);
				var actualFileName = Path.GetFileName(actualFilePath);

				// Se o nome de qualquer arquivo for diferente entre os diretório.
				if (!expectedFileName.Equals(actualFileName))
				{
					AssertExceptionHelper.ThrowAssert("DirectoryAssert", "AreEqual", expectedFileName, actualFileName);
				}

				FileAssert.AreContentEqual(expectedFilePath, actualFilePath);
			}

			// Compara os nomes dos subdiretorios e chava novamente AreEqual (para realizar a comparacao dos subdiretorios).
			var expectedSubdirs = Directory.GetDirectories(expectedDirectoryPath);
			var actualSubdirs = Directory.GetDirectories(actualDirectoryPath);

			// A quantidade de subdiretórios deve ser a mesma.
			if (expectedSubdirs.Length != actualSubdirs.Length)
			{
				AssertExceptionHelper.ThrowAssert("DirectoryAssert", "AreEqual", expectedSubdirs.Length + " directories", actualSubdirs.Length + " directories");
			}

			for (int i = 0; i < expectedSubdirs.Length; i++)
			{
				var expectedDirName = Path.GetFileName(expectedSubdirs[i]);
				var actualDirName = Path.GetFileName(actualSubdirs[i]);

				// O nome dos subdiretórios deve ser o mesmo.
				if (!expectedDirName.Equals(actualDirName))
				{
					AssertExceptionHelper.ThrowAssert("DirectoryAssert", "AreEqual", expectedDirName, actualDirName);
				}

				AreEqual(expectedSubdirs[i], actualSubdirs[i]);
			}
		}
	}
}
