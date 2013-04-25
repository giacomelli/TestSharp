using System;
using System.IO;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace TestSharp
{
	/// <summary>
	/// Utilitário para diretórios
	/// </summary>
	public static class DirectoryHelper
	{
		#region Methods
		/// <summary>
		/// Conta o número de arquivos no diretório.
		/// </summary>
		/// <param name="directoryPath">O caminho do diretório.</param>
		/// <param name="filePattern">Filtro para os arquivos a serem contados.</param>
		/// <param name="countSubdirectoriesFiles">Se deve contar os arquivos dos subdiretórios.</param>
		/// <returns>O número de arquivos no diretório.</returns>
		public static int CountAllFiles(string directoryPath, string filePattern = "*.*", bool countSubdirectoriesFiles = false)
		{
			return Directory.GetFiles(directoryPath, filePattern, countSubdirectoriesFiles ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).Length;
		}

		/// <summary>
		/// Cria o diretório se esse ainda não existir.
		/// </summary>
		/// <param name="directoryPath">O caminho do diretório.</param>
		/// <returns>True se o diretório foi criado, false no contrário.</returns>
		public static bool CreateIfNotExists(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Exclui um diretório se esse existir.
		/// </summary>
		/// <param name="directoryPath">O caminho do diretório.</param>
		/// <returns>True se o diretório foi excluído, false no contrário.</returns>
		public static bool DeleteIfNotExists(string directoryPath)
		{
			if (Directory.Exists(directoryPath))
			{
				Directory.Delete(directoryPath, true);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Remove todos os arquivos de um diretório.
		/// </summary>
		/// <param name="directoryPath">Caminho do diretório.</param>
		/// <param name="filePattern">Filtro para os arquivos a serem removidos.</param>
		/// <param name="removeFilesFromSubdirectories">Se deve remover os arquivos dos subdiretórios.</param>
		public static void DeleteAllFiles(string directoryPath, string filePattern = "*.*", bool removeFilesFromSubdirectories = false)
		{
			if (Directory.Exists(directoryPath))
			{
				var files = Directory.GetFiles(directoryPath, filePattern, removeFilesFromSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

				foreach (var f in files)
				{
					File.Delete(f);
				}
			}
		}

		/// <summary>
		/// Lê todos os arquivos de um diretório
		/// </summary>
		/// <param name="directoryPath">Caminho do diretório.</param>
		/// <param name="filePattern">Filtro para os arquivos a serem lidos.</param>
		/// <returns>O conteúdo de todos os arquivos concatenados</returns>
		public static string ReadAllFiles(string directoryPath, string filePattern = "*.*")
		{
			var files = Directory.GetFiles(directoryPath, filePattern);
			var result = new StringBuilder();

			foreach (var f in files)
			{
				var fileContent = FileHelper.ReadAllTextWithoutLock(f);

				if (!String.IsNullOrEmpty(fileContent))
				{
					if (result.Length == 0)
					{
						result.Append(fileContent);
					}
					else
					{
						result.AppendFormat("{0}{1}", Environment.NewLine, fileContent);
					}
				}
			}

			return result.ToString();
		}

		/// <summary>
		/// Retornar os DirectoryInfo dos diretórios
		/// </summary>
		/// <param name="path">O caminho para os diretórios.</param>
		/// <returns>Os DirectoryInfo dos diretórios.</returns>
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Infos")]
		public static DirectoryInfo[] GetDirectoriesInfos(string path)
		{
			var dirs = Directory.GetDirectories(path);
			var infos = new DirectoryInfo[dirs.Length];

			for(int i = 0; i < dirs.Length; i++)
			{
				infos[i] = new DirectoryInfo(dirs[i]);
			}

			return infos;
		}

		/// <summary>
		/// Copia um diretório inteiro.
		/// Baseado nessa solução: http://www.codeproject.com/Articles/3210/Function-to-copy-a-directory-to-another-place-noth
		/// </summary>
		/// <param name="sourceDir">O diretório origem.</param>
		/// <param name="destinationDir">O diretório destiono.</param>
		public static void CopyDirectory(string sourceDir, string destinationDir)
		{
			if (destinationDir == null)
			{
				throw new ArgumentNullException("destinationDir");
			}

			String[] files;

			if (destinationDir[destinationDir.Length - 1] != Path.DirectorySeparatorChar)
			{
				destinationDir += Path.DirectorySeparatorChar;
			}

			if (!Directory.Exists(destinationDir))
			{
				Directory.CreateDirectory(destinationDir);
			}

            files = Directory.GetFileSystemEntries(sourceDir);

            foreach(string Element in files)
			{
                // Sub directories
				if (Directory.Exists(Element))
				{
					CopyDirectory(Element, destinationDir + Path.GetFileName(Element));
				}
				// Files in directory
				else
				{
					File.Copy(Element, destinationDir + Path.GetFileName(Element), true);
				}
             }
        }
		#endregion
	}
}
