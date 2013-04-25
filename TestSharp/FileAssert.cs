using System.IO;
using System;

namespace TestSharp
{
	/// <summary>
	/// Asserts para arquivos.
	/// </summary>
	public static class FileAssert
	{
		/// <summary>
		/// Verifica se os arquivos informados existem.
		/// </summary>
		/// <param name="filesPaths">Os caminhos dos arquivos.</param>
		public static void Exists(params string[] filesPaths)
		{
			foreach (var filePath in filesPaths)
			{
				if (!File.Exists(filePath))
				{
					AssertExceptionHelper.ThrowAssert("FileAssert", "Exists", "true", "false");
				}
			}
		}

		/// <summary>
		/// Verifica se o arquivo informado não existe.
		/// </summary>
		/// <param name="filePath">O caminho do arquivo.</param>
		public static void NonExists(string filePath)
		{
			if (File.Exists(filePath))
			{
				AssertExceptionHelper.ThrowAssert("FileAssert", "NonExists", "true", "false");
			}
		}

		/// <summary>
		/// Verifica se o conteúdo do arquivo é igual ao esperado.
		/// </summary>
		/// <param name="expectedContent">O conteúdo esperado do arquivo.</param>
		/// <param name="filePath">O caminho do arquivo.</param>
		public static void IsContent(string expectedContent, string filePath)
		{
			var actualContent = FileHelper.ReadAllTextWithoutLock(filePath);

			if (!actualContent.Equals(expectedContent))
			{
				AssertExceptionHelper.ThrowAssert("FileAssert", "IsContent", expectedContent, actualContent);
			}
		}

		/// <summary>
		/// Verifica se o conteúdo do arquivo possui a substring espeada.
		/// </summary>
		/// <param name="expectedSubstringContent">A substring esperada.</param>
		/// <param name="filePath">O caminho do arquivo.</param>
		public static void ContainsContent(string expectedSubstringContent, string filePath)
		{
			if (!FileHelper.ContainsContent(filePath, expectedSubstringContent))
			{
				AssertExceptionHelper.ThrowAssert("FileAssert", "ContainsContent", true, false);
			}
		}

		/// <summary>
		/// Verifica se o conteúdo de dois arquivos são iguais.
		/// </summary>
		/// <param name="expectedFilePath">O caminho do arquivo com o conteúdo esperado.</param>
		/// <param name="actualFilePath">O caminho do arquivo com o conteúdo atual.</param>
		public static void AreContentEqual(string expectedFilePath, string actualFilePath)
		{
			var expectedContent = FileHelper.ReadAllTextWithoutLock(expectedFilePath);
			var actualContent = FileHelper.ReadAllTextWithoutLock(actualFilePath);

			if (!actualContent.Equals(expectedContent))
			{
				AssertExceptionHelper.ThrowAssert("FileAssert", "AreContentEqual", expectedContent, actualContent);
			}
		}

		/// <summary>
		/// Verifica se o conteúdo da última linha do arquivo é igual ao esperado.
		/// </summary>
		/// <param name="expectedLastLineContent">O conteúdo esperado para a última linha do arquivo.</param>
		/// <param name="filePath">O caminho do arquivo.</param>
		public static void IsLastLineContent(string expectedLastLineContent, string filePath)
		{
			var actualLastLineContent = FileHelper.ReadLastLineWithoutLock(filePath);

			if (!actualLastLineContent.Equals(expectedLastLineContent))
			{
				AssertExceptionHelper.ThrowAssert("FileAssert", "IsLastLineContent", expectedLastLineContent, actualLastLineContent);
			}
		}

		/// <summary>
		/// Verifica se o número de linhas do arquivo é igual ao esperado.
		/// </summary>
		/// <param name="expectedCountLines">O número de linhas esperado do arquivo.</param>
		/// <param name="filePath">O caminho do arquivo.</param>
		public static void IsCountLines(int expectedCountLines, string filePath)
		{
			var actualCountLines = FileHelper.CountLines(filePath);

			if (!actualCountLines.Equals(expectedCountLines))
			{
				AssertExceptionHelper.ThrowAssert("FileAssert", "IsCountLines", expectedCountLines, actualCountLines);
			}
		}

		/// <summary>
		/// Verifica se um arquivo está vazio.
		/// </summary>
		/// <param name="filePath">O caminho do arquivo.</param>
		public static void IsEmpty(string filePath)
		{
			var actualContent = FileHelper.ReadAllTextWithoutLock(filePath);

			if (!String.IsNullOrEmpty(actualContent))
			{
				AssertExceptionHelper.ThrowAssert("FileAssert", "IsEmpty", "true", "false");
			}
		}

		/// <summary>
		/// Verifica se um arquivo não está vazio.
		/// </summary>
		/// <param name="filePath">O caminho do arquivo.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "NonEmpty")]
		public static void IsNonEmpty(string filePath)
		{
			var actualContent = FileHelper.ReadAllTextWithoutLock(filePath);

			if (String.IsNullOrEmpty(actualContent))
			{
				AssertExceptionHelper.ThrowAssert("FileAssert", "IsNonEmpty", "true", "false");
			}
		}

		/// <summary>
		/// Verifica se a data da última modificação do arquivo é a esperada.
		/// </summary>
		/// <param name="expectedLastModification">A data esperada da última modificação do arquivo.</param>
		/// <param name="filePath">O caminho do arquivo.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
		public static void IsLastModification(DateTime expectedLastModification, string filePath)
		{
			var actualLastModification = FileHelper.GetLastModification(filePath);

			try
			{
				DateTimeAssert.AreEqualIgnoringMilliseconds(expectedLastModification, actualLastModification);
			}
			catch
			{
				AssertExceptionHelper.ThrowAssert("FileAssert", "IsLastModification", expectedLastModification, actualLastModification);
			}
		}
	}
}
