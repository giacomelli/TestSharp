using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace TestSharp.Tests
{
	[TestClass]
	public class ProcessHelperTest
	{
		#region Fields
		private string m_notePadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"notepad.exe");
		#endregion

		[TestMethod]
		public void CountInstancesTest()
		{
			var processName = "notepad";
			ProcessHelper.KillAll(processName);
			ProcessAssert.IsProcessInstancesCount(0, processName);

			ProcessHelper.Run(m_notePadPath, String.Empty, false);
			ProcessAssert.IsProcessInstancesCount(1, processName);

			ProcessHelper.Run(m_notePadPath, String.Empty, false);
			ProcessAssert.IsProcessInstancesCount(2, processName);

			ProcessHelper.Run(m_notePadPath, String.Empty, false);
			ProcessAssert.IsProcessInstancesCount(3, processName);

			ProcessHelper.KillAll(processName);
			ProcessAssert.IsProcessInstancesCount(0, processName);
		}

		[TestMethod]
		public void KillAllTest()
		{
			var processName = "notepad";
			ProcessHelper.KillAll(processName);
			ProcessAssert.IsProcessInstancesCount(0, processName);

			ProcessHelper.Run(m_notePadPath, String.Empty, false);
			ProcessHelper.Run(m_notePadPath, String.Empty, false);
			ProcessHelper.Run(m_notePadPath, String.Empty, false);
			
			ProcessAssert.IsProcessInstancesCount(3, processName);
			ProcessHelper.KillAll(processName);
			ProcessAssert.IsProcessInstancesCount(0, processName);

			Parallel.For(0, 100, (i) =>
			{
				try
				{
					ProcessHelper.Run(m_notePadPath, String.Empty, false);
				}
				catch
				{
					// Apenas para teste.
				}

				ProcessHelper.KillAll(processName);
			});
		}

		[TestMethod]
		public void KillFirstTest()
		{
			var processName = "notepad";
			ProcessHelper.KillFirst(processName);
			ProcessAssert.IsProcessInstancesCount(0, processName);

			ProcessHelper.Run(m_notePadPath, String.Empty, false);
			ProcessHelper.Run(m_notePadPath, String.Empty, false);
			ProcessHelper.Run(m_notePadPath, String.Empty, false);

			ProcessAssert.IsProcessInstancesCount(3, processName);
			ProcessHelper.KillFirst(processName);
			ProcessAssert.IsProcessInstancesCount(2, processName);

			ProcessHelper.KillFirst(processName);
			ProcessAssert.IsProcessInstancesCount(1, processName);

			ProcessHelper.KillFirst(processName);
			ProcessAssert.IsProcessInstancesCount(0, processName);
		}

		[TestMethod]
		public void RunWithFullPathTest()
		{
			var processName = @"c:\Windows\System32\cmd.exe";
			var args = @"dir";

			var actualOutput = ProcessHelper.Run(processName, args);
			Assert.IsFalse(String.IsNullOrEmpty(actualOutput));
			StringAssert.StartsWith(actualOutput, "Microsoft");
			StringAssert.EndsWith(actualOutput, "Out>");

			actualOutput = ProcessHelper.Run(processName, args, false);
			Assert.IsTrue(String.IsNullOrEmpty(actualOutput));
		}

		[TestMethod]
		public void RunWithSystemVariableTest()
		{
			var processName = @"%windir%\system32\cmd.exe";
			var args = @"dir";

			var actualOutput = ProcessHelper.Run(processName, args);
			Assert.IsFalse(String.IsNullOrEmpty(actualOutput));
			StringAssert.StartsWith(actualOutput, "Microsoft");
			StringAssert.EndsWith(actualOutput, "Out>");

			actualOutput = ProcessHelper.Run(processName, args, false);
			Assert.IsTrue(String.IsNullOrEmpty(actualOutput));
		}

		[TestMethod]
		public void WaitForExitTest()
		{
			Parallel.Invoke(
				() =>
				{
					ProcessHelper.Run(m_notePadPath, "", false);
					var beforeTime = DateTime.Now;
					ProcessHelper.WaitForExit("notepad");
					Assert.AreNotEqual(beforeTime, DateTime.Now);
				},
				() =>
				{
					Thread.Sleep(1000);
					ProcessHelper.KillAll("notepad");
				}
				);
		}
	}
}
