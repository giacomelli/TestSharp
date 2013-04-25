
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestSharp.Tests
{
	[TestClass]
	public class ZipHelperTest
	{
		[TestMethod]
		public void ExtractAll_NonExistingZipFilePath_ArgumentException()
		{
			var dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TESTDIR");
			DirectoryHelper.CreateIfNotExists(dirPath);

			ExceptionAssert.IsThrowing(new ArgumentException(@"Arquivo 'c:\arquivo.zip.inexistente.zip' não existe.", "zipFilePath"), () => 
			{
				ZipHelper.ExtractAll(@"c:\arquivo.zip.inexistente.zip", dirPath);
			});
		}

		[TestMethod]
		public void ExtractAll_NonExistingDestinationDirectoryPath_ArgumentException()
		{
			var zipFilePath = Path.Combine(VSProjectHelper.GetProjectFolderPath("TestSharp.Tests"), @"Helpers\Resources\ZipHelperTest.zip");
			var dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TESTDIR");
						
			DirectoryHelper.DeleteIfNotExists(dirPath);

			ExceptionAssert.IsThrowing(new ArgumentException("Diretório destino '" + dirPath + "' não existe.", "destinationDirectoryPath"), () =>
			{
				ZipHelper.ExtractAll(zipFilePath, dirPath);
			});
		}

		[TestMethod]
		public void ExtractAll_ValidZipAndDirectory_AllFilesExtracteds()
		{
			var zipFilePath = Path.Combine(VSProjectHelper.GetProjectFolderPath("TestSharp.Tests"), @"Helpers\Resources\ZipHelperTest.zip");			
			var dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TESTDIR");
			DirectoryHelper.CreateIfNotExists(dirPath);
			DirectoryHelper.DeleteAllFiles(dirPath);

			ZipHelper.ExtractAll(zipFilePath, dirPath);

			DirectoryAssert.IsFilesCount(5, dirPath, "*.*", true);
			DirectoryAssert.IsFilesCount(5, dirPath, "*.log", true);
		}
	}
}
