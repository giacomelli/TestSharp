using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestSharp.Tests
{
	[TestClass]
	public class DirectoryHelperTest
	{
		#region Fields
		private static string s_directoryPath;
		#endregion

		#region Initialize / Cleanup
		[ClassInitialize]
		public static void Initialize(TestContext context)
		{
			s_directoryPath = Directory.CreateDirectory("DirectoryHelperTest").FullName;
		}

		[ClassCleanup]
		public static void Cleanup()
		{
			Directory.Delete(s_directoryPath, true);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			DirectoryHelper.DeleteAllFiles(s_directoryPath, "*.*", true);
		}
		#endregion

		#region Tests
		[TestMethod]
		public void CountAllFilesTest()
		{
			FileHelper.CreateFiles(
				s_directoryPath,
				"text1.txt",
				"text2.txt",
				"doc1.doc",
				"doc2.doc",
				"doc3.pdf",
				"doc4.pdf");

			DirectoryAssert.IsFilesCount(6, s_directoryPath);
			DirectoryAssert.IsFilesCount(2, s_directoryPath, "*.txt");
			DirectoryAssert.IsFilesCount(2, s_directoryPath, "*.doc");
			DirectoryAssert.IsFilesCount(2, s_directoryPath, "*.pdf");
			DirectoryAssert.IsFilesCount(0, s_directoryPath, "*.xls");

			DirectoryHelper.DeleteAllFiles(s_directoryPath);
			DirectoryAssert.IsFilesCount(0, s_directoryPath);
			DirectoryAssert.IsFilesCount(0, s_directoryPath, "*.txt");
			DirectoryAssert.IsFilesCount(0, s_directoryPath, "*.doc");
			DirectoryAssert.IsFilesCount(0, s_directoryPath, "*.pdf");
			DirectoryAssert.IsFilesCount(0, s_directoryPath, "*.xls");

			FileHelper.CreateFiles(
				s_directoryPath,
				"text1.txt",
				"text2.txt",
				"doc1.doc",
				"doc2.pdf");

			DirectoryAssert.IsFilesCount(4, s_directoryPath);
			DirectoryAssert.IsFilesCount(2, s_directoryPath, "*.txt");
			DirectoryAssert.IsFilesCount(1, s_directoryPath, "*.doc");
			DirectoryAssert.IsFilesCount(1, s_directoryPath, "*.pdf");
			DirectoryAssert.IsFilesCount(0, s_directoryPath, "*.xls");

			DirectoryHelper.DeleteAllFiles(s_directoryPath);
			FileHelper.CreateFiles(
				s_directoryPath,
				@"a\text1.txt",
				@"b\text2.txt",
				@"c\doc1.doc",
				@"doc2.pdf");

			DirectoryAssert.IsFilesCount(1, s_directoryPath, "*.*");
			DirectoryAssert.IsFilesCount(4, s_directoryPath, "*.*", true);
			DirectoryAssert.IsFilesCount(2, s_directoryPath, "*.txt", true);
			DirectoryAssert.IsFilesCount(1, s_directoryPath, "*.doc", true);
			DirectoryAssert.IsFilesCount(1, s_directoryPath, "*.pdf", true);
			DirectoryAssert.IsFilesCount(0, s_directoryPath, "*.xls", true);

		}

		[TestMethod]
		public void CreateIfNotExistsTest()
		{
			Directory.Delete(s_directoryPath, true);
			Assert.IsTrue(DirectoryHelper.CreateIfNotExists(s_directoryPath));
			DirectoryAssert.Exists(s_directoryPath);
			Assert.IsFalse(DirectoryHelper.CreateIfNotExists(s_directoryPath));
			DirectoryAssert.Exists(s_directoryPath);
		}

		[TestMethod]
		public void DeleteAllFilesTest()
		{
			DirectoryHelper.DeleteAllFiles("diretorio inexistente");

			FileHelper.CreateFiles(
				s_directoryPath, 
				"text1.txt", 
				"text2.txt", 
				"doc1.doc", 
				"doc2.doc");

			DirectoryAssert.IsFilesCount(4, s_directoryPath);
			DirectoryAssert.IsFilesCount(2, s_directoryPath, "*.txt");
			DirectoryAssert.IsFilesCount(2, s_directoryPath, "*.doc");

			DirectoryHelper.DeleteAllFiles(s_directoryPath);
			DirectoryAssert.IsFilesCount(0, s_directoryPath);

			FileHelper.CreateFiles(
				s_directoryPath,
				"text1.txt",
				"text2.txt",
				"doc1.doc",
				"doc2.doc");

			DirectoryAssert.IsFilesCount(4, s_directoryPath);
			DirectoryAssert.IsFilesCount(2, s_directoryPath, "*.txt");
			DirectoryAssert.IsFilesCount(2, s_directoryPath, "*.doc");

			DirectoryHelper.DeleteAllFiles(s_directoryPath, "*.txt");
			DirectoryAssert.IsFilesCount(2, s_directoryPath);
			DirectoryAssert.IsFilesCount(0, s_directoryPath, "*.txt");
			DirectoryAssert.IsFilesCount(2, s_directoryPath, "*.doc");

			DirectoryHelper.DeleteAllFiles(s_directoryPath, "*.doc");
			DirectoryAssert.IsFilesCount(0, s_directoryPath);
			DirectoryAssert.IsFilesCount(0, s_directoryPath, "*.txt");
			DirectoryAssert.IsFilesCount(0, s_directoryPath, "*.doc");
		}

		[TestMethod]
		public void ReadAllFilesTest()
		{
			FileHelper.CreateFiles(
				s_directoryPath,
				"text1.txt",				
				"text2.txt",
				"doc1.doc");

			DirectoryAssert.IsAllFilesContent("", s_directoryPath);

			File.WriteAllText(Path.Combine(s_directoryPath, "doc1.doc"), "1");
			File.WriteAllText(Path.Combine(s_directoryPath, "text1.txt"), "22");
			File.WriteAllText(Path.Combine(s_directoryPath, "text2.txt"), "333");

			DirectoryAssert.IsAllFilesContent("1\r\n22\r\n333", s_directoryPath);

			DirectoryHelper.DeleteAllFiles(s_directoryPath, "*.txt");
			DirectoryAssert.IsAllFilesContent("1", s_directoryPath);
		}

		[TestMethod]
		public void GetDirectoriesInfosTest()
		{
			Directory.Delete(s_directoryPath, true);
			Directory.CreateDirectory(s_directoryPath);

			var subdir1 = Path.Combine(s_directoryPath, "subdir1");
			var subdir2 = Path.Combine(s_directoryPath, "subdir2");

			Directory.CreateDirectory(subdir1);
			Directory.CreateDirectory(subdir2);

			var infos = DirectoryHelper.GetDirectoriesInfos(s_directoryPath);

			Assert.AreEqual(2, infos.Length);
			Assert.AreEqual(subdir1, infos[0].FullName);
			Assert.AreEqual(subdir2, infos[1].FullName);
		}

		[TestMethod]
		public void CopyDirectoryTest()
		{
			FileHelper.CreateFiles(
				s_directoryPath,
				"text1.txt",
				"subdir1\\text2.txt",
				"subdir1\\doc1.doc",
				"subdir2\\doc2.doc",
				"subdir3\\doc3.pdf",
				"subdir4\\doc4.pdf");

			var destDir = Directory.CreateDirectory("DirectoryHelperTest2").FullName;

			DirectoryHelper.DeleteAllFiles(destDir, "*.*", true);

			DirectoryHelper.CopyDirectory(s_directoryPath, destDir);

			DirectoryAssert.IsFilesCount(6, destDir, "*.*", true);
			FileAssert.Exists(Path.Combine(destDir, "text1.txt"));
			FileAssert.Exists(Path.Combine(destDir, "subdir1\\text2.txt"));
			FileAssert.Exists(Path.Combine(destDir, "subdir1\\doc1.doc"));
			FileAssert.Exists(Path.Combine(destDir, "subdir2\\doc2.doc"));
			FileAssert.Exists(Path.Combine(destDir, "subdir3\\doc3.pdf"));
			FileAssert.Exists(Path.Combine(destDir, "subdir4\\doc4.pdf"));

			Directory.Delete(destDir, true);
		}
		#endregion
	}
}
