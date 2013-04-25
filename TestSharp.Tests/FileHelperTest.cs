using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TestSharp.Tests
{
	[TestClass]
	public class FileHelperTest
	{
		#region Fields
		private static string s_directoryPath;
		#endregion

		#region Initialize / Cleanup
		[ClassInitialize]
		public static void Initialize(TestContext context)
		{
			s_directoryPath = Directory.CreateDirectory("FileHelperTest").FullName;
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
		public void GetLastModificationTest()
		{
			var fileName = Path.Combine(s_directoryPath, "file.txt");
			var actual = FileHelper.GetLastModification(fileName);

			// http://msdn.microsoft.com/en-us/library/system.io.file.getlastaccesstime.aspx
			DateTimeAssert.AreEqualIgnoringMilliseconds(new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc), actual.ToUniversalTime());

			FileHelper.CreateFiles(s_directoryPath, "file.txt");
			actual = FileHelper.GetLastModification(fileName);
			DateTimeAssert.AreEqualIgnoringMilliseconds(DateTime.UtcNow, actual.ToUniversalTime());

			Thread.Sleep(1000);
			FileHelper.Touch(fileName);
			actual = FileHelper.GetLastModification(fileName);
			DateTimeAssert.AreEqualIgnoringMilliseconds(DateTime.UtcNow, actual.ToUniversalTime());
		}

		[TestMethod]
		public void CreateFilesTest()
		{
			FileHelper.CreateFiles(s_directoryPath, "file1.txt");
			FileAssert.Exists(Path.Combine(s_directoryPath, "file1.txt"));

			FileHelper.CreateFiles(s_directoryPath, "file1.txt", "file2.doc");
			FileAssert.Exists(Path.Combine(s_directoryPath, "file1.txt"));
			FileAssert.Exists(Path.Combine(s_directoryPath, "file2.doc"));
		}



		[TestMethod]
		public void DeleteFilesFromDirectoryTest()
		{
			FileHelper.DeleteFilesFromDirectory(s_directoryPath);
			FileHelper.CreateFiles(s_directoryPath, "file1.txt");
			FileHelper.DeleteFilesFromDirectory(s_directoryPath, "file1.txt");
			FileAssert.NonExists(Path.Combine(s_directoryPath, "file1.txt"));

			FileHelper.CreateFiles(s_directoryPath, "file1.txt", "file2.doc");
			FileHelper.DeleteFilesFromDirectory(s_directoryPath, "file1.txt", "file2.doc");
			FileAssert.NonExists(Path.Combine(s_directoryPath, "file1.txt"));
			FileAssert.NonExists(Path.Combine(s_directoryPath, "file2.doc"));
		}

		[TestMethod]
		public void DeleteFilesFromDirectory_DirectoryNotExist_ArgumentException()
		{
			ExceptionAssert.IsThrowing(new ArgumentException(@"Directory 'X:\TESTE\TESTE\TESTE' does not exists.", "directoryPath"), () => 
			{
				FileHelper.DeleteFilesFromDirectory(@"X:\TESTE\TESTE\TESTE", "Teste.tst");
			});
		}		

		[TestMethod]
		public void DeleteFilesTest()
		{
			var file1 = Path.Combine(s_directoryPath, "file1.txt");
			var file2 = Path.Combine(s_directoryPath, "file2.doc");

			FileHelper.CreateFiles(s_directoryPath, "file1.txt");
			FileHelper.DeleteFiles(file1);
			FileAssert.NonExists(file1);

			FileHelper.CreateFiles(s_directoryPath, "file1.txt", "file2.doc");
			FileHelper.DeleteFiles(file1, file2);
			FileAssert.NonExists(file1);
			FileAssert.NonExists(file2);
		}

		[TestMethod]
		public void ReadAllTextWithoutLockTest()
		{
			var filePath = Path.Combine(s_directoryPath, "file1.txt");
			
			using (var stream = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
			{
				stream.Write("ReadAllTextWithoutLockTest1");
				stream.Flush();
				FileAssert.IsContent("ReadAllTextWithoutLockTest1", filePath);

				stream.Write("ReadAllTextWithoutLockTest2");
				stream.Flush();
				FileAssert.IsContent("ReadAllTextWithoutLockTest1ReadAllTextWithoutLockTest2", filePath);
			}

			FileAssert.IsContent("ReadAllTextWithoutLockTest1ReadAllTextWithoutLockTest2", filePath);
		}
		
		[TestMethod]
		public void ReadAllLinesTest()
		{
			FileHelper.CreateFilesWithContent(s_directoryPath, (filePath) => { return "teste1" + Environment.NewLine + "teste2"; }, "file1.txt");
			var lines = FileHelper.ReadAllLines(Path.Combine(s_directoryPath, "file1.txt"));

			Assert.AreEqual(2, lines.Length);
			Assert.AreEqual("teste1", lines[0]);
			Assert.AreEqual("teste2", lines[1]);
		}

		[TestMethod]
		public void ReadLastLinetWithoutLockTest()
		{
			var filePath = Path.Combine(s_directoryPath, "file1.txt");

			using (var stream = new StreamWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)))
			{
				stream.WriteLine("ReadLastLinetWithoutLockTest1");
				stream.Flush();
				FileAssert.IsLastLineContent("ReadLastLinetWithoutLockTest1", filePath);

				stream.WriteLine("ReadLastLinetWithoutLockTest2");
				stream.Flush();
				FileAssert.IsLastLineContent("ReadLastLinetWithoutLockTest2", filePath);
			}

			FileAssert.IsLastLineContent("ReadLastLinetWithoutLockTest2", filePath);
		}

		[TestMethod]
		public void CountLinesTest()
		{
			FileHelper.CreateFilesWithContent(s_directoryPath, (filePath) => { return "teste1" + Environment.NewLine + "teste2"; }, "file1.txt");
			var lines = FileHelper.ReadAllLines(Path.Combine(s_directoryPath, "file1.txt"));

			Assert.AreEqual(2, FileHelper.CountLines(Path.Combine(s_directoryPath, "file1.txt")));
		}

		[TestMethod]
		public void TouchTest()
		{
			var fileName1 = Path.Combine(s_directoryPath, "file1.txt");
			FileHelper.CreateFiles(s_directoryPath, "file1.txt");
			FileAssert.IsLastModification(DateTime.Now, fileName1);

			var fileName2 = Path.Combine(s_directoryPath, "file2.txt");
			FileHelper.CreateFiles(s_directoryPath, "file2.txt");
			FileAssert.IsLastModification(DateTime.Now, fileName2);

			Thread.Sleep(1000);
			FileHelper.Touch(new string[] { fileName1, fileName2 }, DateTime.Now);
			FileAssert.IsLastModification(DateTime.Now, fileName1);
		}

		[TestMethod]
		public void WaitForFileContentContainsTest()
		{
			var fileName = Path.Combine(s_directoryPath, "file1.txt");
			FileHelper.CreateFiles(s_directoryPath, "file1.txt");

			Parallel.Invoke(
				() =>
				{
					FileHelper.WaitForFileContentContains(fileName, "TESTE", 10);
					FileAssert.IsEmpty(fileName);
				},
				() =>
				{
					Thread.Sleep(15000);
					File.WriteAllText(fileName, "TEST");
				});

			FileHelper.DeleteFiles(fileName);
			FileHelper.CreateFiles(s_directoryPath, "file1.txt");

			Parallel.Invoke(
				() =>
				{
					FileHelper.WaitForFileContentContains(fileName, "TESTE", 10);
					FileAssert.IsContent("TESTE", fileName);
				},
				() =>
				{
					Thread.Sleep(5);
					File.WriteAllText(fileName, "TESTE");
				});

			
		}

		[TestMethod]
		public void ContainsContentTest()
		{
			var fileName = Path.Combine(s_directoryPath, "file1.txt");
			
			FileHelper.CreateFilesWithContent(
				s_directoryPath,
				(filePath) =>
				{
					return Path.GetFileName(filePath) + "_content";
				}, "file1.txt");

			Assert.IsTrue(FileHelper.ContainsContent(fileName, "_content"));

		}

		[TestMethod]
		public void CreateFilesWithContent_GetContentIsNull_ArgumentNullException()
		{
			ExceptionAssert.IsThrowing(new ArgumentNullException("getContent"), () =>
			{
				FileHelper.CreateFilesWithContent(s_directoryPath, null);
			});
		}

		[TestMethod]
		public void CreateFilesWithContent_filesNamesIsNull_ArgumentNullException()
		{
			ExceptionAssert.IsThrowing(new ArgumentNullException("filesNames"), () =>
			{
				FileHelper.CreateFilesWithContent(s_directoryPath, (filePath) => { return String.Empty; }, null);
			});
		}
		#endregion
	}
}
