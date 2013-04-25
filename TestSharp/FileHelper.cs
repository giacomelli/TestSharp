using System;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Text;
using System.Collections.Generic;

namespace TestSharp
{
	/// <summary>
	/// Utilitária para questões relativas a arquivos.
	/// </summary>
    public static class FileHelper
    {   
        #region Methods
		/// <summary>
		/// Obtém a data da última modificação do arquivo.
		/// </summary>
		/// <param name="fileName">O caminho do arquivo.</param>
		/// <returns>A data da última modificação.</returns>
        public static DateTime GetLastModification(string fileName)
        {			
			var fileInfo = new FileInfo(fileName);
			fileInfo.Refresh();

			return fileInfo.LastWriteTime;
        }

		/// <summary>
		/// Cria os arquivos informados no diretório.
		/// </summary>
		/// <param name="directoryPath">O caminho do diretório onde serão criados os arquivos.</param>
		/// <param name="filesNames">O nome dos arquivos a serem criados.</param>
		public static string[] CreateFiles(string directoryPath, params string[] filesNames)
		{
			return CreateFilesWithContent(directoryPath, delegate { return String.Empty; }, filesNames);
		}

		/// <summary>
		/// Cria os arquivos informados no diretório com o conteudo retornado por getContent.
		/// </summary>
		/// <param name="directoryPath">O caminho do diretório onde serão criados os arquivos.</param>
		/// <param name="getContent">A função que recebe o arquivo que será criado e retorna o conteúdo para o mesmo.</param>
		/// <param name="filesNames">O nome dos arquivos a serem criados.</param>
		public static string[] CreateFilesWithContent(string directoryPath, Func<string, string> getContent, params string[] filesNames)
		{
			if (getContent == null)
			{
				throw new ArgumentNullException("getContent");
			}

			if(filesNames == null)
			{
				throw new ArgumentNullException("filesNames");
			}

			var filesPaths = new List<string>();

			foreach (var fileName in filesNames)
			{
				var filePath = Path.Combine(directoryPath, fileName);
				filesPaths.Add(filePath);

				// Nos argumentos filesNames podem ser passados subdiretórios relativos a directoryPath.
				var realFileDirectoryPath = Path.GetDirectoryName(filePath);

				if (!Directory.Exists(realFileDirectoryPath))
				{
					Directory.CreateDirectory(realFileDirectoryPath);
				}

				// Escreve os arquivos nos caminhos especificados.
				if (!File.Exists(filePath))
				{
					File.WriteAllText(filePath, getContent(fileName));
				}
			}

			return filesPaths.ToArray();
		}

		/// <summary>
		/// Exclui os arquivos informados do diretório.
		/// </summary>
		/// <param name="directoryPath">O caminho do diretório onde serão excluídos os arquivos.</param>
		/// <param name="filesNames">O nome dos arquivos a serem excluídos.</param>
		public static void DeleteFilesFromDirectory(string directoryPath, params string[] filesNames)
		{
            if (!Directory.Exists(directoryPath))
            {
				var msg = String.Format(CultureInfo.InvariantCulture, "Directory '{0}' does not exists.", directoryPath);
                throw new ArgumentException(msg, "directoryPath");
            }

			if (filesNames != null)
			{
				foreach (var fileName in filesNames)
				{
					var filePath = Path.Combine(directoryPath, fileName);

					if (File.Exists(filePath))
					{
						File.Delete(filePath);
					}
				}
			}
		}

		/// <summary>
		/// Exclui os arquivos informados.
		/// </summary>
		/// <param name="filesPaths">Os caminhos dos arquivos.</param>
		public static void DeleteFiles(params string[] filesPaths)
		{
			if (filesPaths != null)
			{
				foreach (var filePath in filesPaths)
				{
					if (File.Exists(filePath))
					{
						File.Delete(filePath);
					}
				}
			}
		}

		/// <summary>
		/// Lê todo o conteúdo do arquivo utilizando apenas acesso de leitura e um FileShare.ReadWrite, 
		/// isso garante que será possível ler o arquivo mesmo que outro processo tenha um lock de escrita no mesmo.
		/// </summary>
		/// <param name="fileName">O caminho do arquivo.</param>
		/// <param name="encoding">O encoding utilizado para fazer a leitura do arquivo.</param>
		/// <returns>O conteúdo do arquivo.</returns>
		public static string ReadAllTextWithoutLock(string fileName, Encoding encoding)
		{
			string allText = String.Empty;

			using (FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				var reader = new StreamReader(stream, encoding);
				
				if (!reader.EndOfStream)
				{
					allText = reader.ReadToEnd();
				}				
			}

			return allText;
		}

		/// <summary>
		/// Lê todo o conteúdo do arquivo utilizando apenas acesso de leitura e um FileShare.ReadWrite, 
		/// isso garante que será possível ler o arquivo mesmo que outro processo tenha um lock de escrita no mesmo.
		/// </summary>
		/// <param name="fileName">O caminho do arquivo.</param>
		/// <returns>O conteúdo do arquivo.</returns>
		public static string ReadAllTextWithoutLock(string fileName)
		{
			return ReadAllTextWithoutLock(fileName, Encoding.UTF8);
		}

		/// <summary>
		/// Lê a última linha do arquivo utilizando apenas acesso de leitura e um FileShare.ReadWrite, 
		/// isso garante que será possível ler o arquivo mesmo que outro processo tenha um lock de escrita no mesmo.
		/// </summary>
		/// <param name="fileName">O caminho do arquivo.</param>
		/// <returns>O conteúdo da última linha do arquivo.</returns>
		public static string ReadLastLineWithoutLock(string fileName)
		{
			// TODO: otimizar para ler direto a última linha e não utilizar o ReadAllTextWithoutLock.
			string lastLine = String.Empty;
			var fileContent = ReadAllTextWithoutLock(fileName).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			var linesLength = fileContent.Length;

			if (linesLength > 0)
			{
				lastLine = fileContent[linesLength - 1];
			}

			return lastLine;
		}

		/// <summary>
		/// Lê a todas as linhas do arquivo utilizando apenas acesso de leitura e um FileShare.ReadWrite, 
		/// isso garante que será possível ler o arquivo mesmo que outro processo tenha um lock de escrita no mesmo.
		/// </summary>
		/// <param name="fileName">O caminho do arquivo.</param>
		/// <returns>As linhas do arquivo.</returns>
		public static string[] ReadAllLines(string fileName)
		{
			return ReadAllTextWithoutLock(fileName).Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		}

		/// <summary>
		/// Conta o número de linhas de um arquivo.
		/// </summary>
		/// <param name="fileName">O caminho do arquivo.</param>
		/// <returns>O número de linhas do arquivo.</returns>
		public static int CountLines(string fileName)
		{
			if (File.Exists(fileName))
			{
				return ReadAllLines(fileName).Length;
			}

			return 0;
		}

		/// <summary>
		/// Atualiza a data de atualização do arquivo.
		/// </summary>
		/// <param name="fileName">O caminho do arquivo.</param>
		public static void Touch(string fileName)
		{
			Touch(fileName, DateTime.UtcNow);
		}

		/// <summary>
		/// Atualiza a data de atualização do arquivo.
		/// </summary>
		/// <param name="fileName">O caminho do arquivo.</param>
		/// <param name="date">A data de atualização do arquivo.</param>
		public static void Touch(string fileName, DateTime date)
		{
			File.SetLastWriteTimeUtc(fileName, date.ToUniversalTime());
		}

		/// <summary>
		/// Atualiza a data de atualização de todos os arquivos.
		/// </summary>
		/// <param name="fileNames">Os caminhos dos arquivos.</param>
		/// <param name="date">A data de atualização dos arquivos.</param>
		public static void Touch(string[] fileNames, DateTime date)
		{
			foreach (var f in fileNames)
			{
				Touch(f, date);
			}
		}

		/// <summary>
		/// Aguarda até que o arquivo tenha em seu conteúdo a substring informada.
		/// </summary>
		/// <param name="fileName">O caminho do arquivo.</param>
		/// <param name="expectedContentSubstring">A sustring a ser aguardada a existir no arquivo</param>
		/// <param name="encoding">O encoding utilizado na leitura do arquivo.</param>
		/// <param name="secondsTimeout">Os segundos que devem ser aguardados até que retorne.</param>
		public static void WaitForFileContentContains(string fileName, string expectedContentSubstring, Encoding encoding, int secondsTimeout = 60)
		{
			while (secondsTimeout > 0 && (!File.Exists(fileName) || !FileHelper.ReadAllTextWithoutLock(fileName, encoding).Contains(expectedContentSubstring)))
			{
				Thread.Sleep(1000);
				secondsTimeout--;
			}
		}

		/// <summary>
		/// Aguarda até que o arquivo tenha em seu conteúdo a substring informada.
		/// </summary>
		/// <param name="fileName">O caminho do arquivo.</param>
		/// <param name="expectedContentSubstring">A sustring a ser aguardada a existir no arquivo</param>
		/// <param name="secondsTimeout">Os segundos que devem ser aguardados até que retorne.</param>
		public static void WaitForFileContentContains(string fileName, string expectedContentSubstring, int secondsTimeout = 60)
		{
			WaitForFileContentContains(fileName, expectedContentSubstring, Encoding.UTF8, secondsTimeout);
		}

		/// <summary>
		/// Verifica se o arquivo informado contém a substring informada.
		/// </summary>
		/// <param name="fileName">O caminho do arquivo.</param>
		/// <param name="expectedSubstringContent">A substring a ser verificada no conteúdo do arquivo.</param>
		/// <returns>True se a substring existe no conteúdo do arquivo, false no contrário.</returns>
		public static bool ContainsContent(string fileName, string expectedSubstringContent)
		{
			return FileHelper.ReadAllTextWithoutLock(fileName).Contains(expectedSubstringContent);
		}
        #endregion
	}
}
