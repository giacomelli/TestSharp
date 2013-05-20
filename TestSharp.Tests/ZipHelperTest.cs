
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.IO;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class ZipHelperTest
	{
		#region Fields
		private string m_zipFilePath = Path.Combine(VSProjectHelper.GetProjectFolderPath("TestSharp.Tests"), "Helpers", "Resources", "ZipHelperTest.zip");
		#endregion

		[Test]
		public void ExtractAll_NonExistingZipFilePath_ArgumentException()
		{
			var dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TESTDIR");
			DirectoryHelper.CreateIfNotExists(dirPath);

			ExceptionAssert.IsThrowing(new ArgumentException(@"Arquivo 'c:\arquivo.zip.inexistente.zip' não existe.", "zipFilePath"), () => 
			{
				ZipHelper.ExtractAll(@"c:\arquivo.zip.inexistente.zip", dirPath);
			});
		}

		[Test]
		public void ExtractAll_NonExistingDestinationDirectoryPath_ArgumentException()
		{
			var dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TESTDIR");
						
			DirectoryHelper.DeleteIfNotExists(dirPath);

			ExceptionAssert.IsThrowing(new ArgumentException("Diretório destino '" + dirPath + "' não existe.", "destinationDirectoryPath"), () =>
			{
				ZipHelper.ExtractAll(m_zipFilePath, dirPath);
			});
		}

		[Test]
		public void ExtractAll_ValidZipAndDirectory_AllFilesExtracteds()
		{
			var dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TESTDIR");
			DirectoryHelper.CreateIfNotExists(dirPath);
			DirectoryHelper.DeleteAllFiles(dirPath);

			ZipHelper.ExtractAll(m_zipFilePath, dirPath);

			DirectoryAssert.IsFilesCount(5, dirPath, "*.*", true);
			DirectoryAssert.IsFilesCount(5, dirPath, "*.log", true);
		}
	}
}
