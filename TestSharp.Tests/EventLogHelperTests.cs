using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class EventLogHelperTests
	{
		#region Tests
		[Test]
		public void ClearAllApplicationEventLogsTest()
		{
			EventLogHelper.WriteApplicationEventLogEntry("ClearAllApplicationEventLogsTest");
			Assert.IsTrue(EventLogHelper.CountApplicationEventLogEntries() > 0);

			EventLogHelper.ClearAllApplicationEventLogEntries();

			Assert.AreEqual(0, EventLogHelper.CountApplicationEventLogEntries());
		}

		[Test]
		public void CountApplicationEventLogEntriesTest()
		{
			EventLogHelper.ClearAllApplicationEventLogEntries();
			Assert.AreEqual(0, EventLogHelper.CountApplicationEventLogEntries());

			EventLogHelper.WriteApplicationEventLogEntry("CountApplicationEventLogEntriesTest1");
			Assert.AreEqual(1, EventLogHelper.CountApplicationEventLogEntries());

			EventLogHelper.WriteApplicationEventLogEntry("CountApplicationEventLogEntriesTest2");
			Assert.AreEqual(2, EventLogHelper.CountApplicationEventLogEntries());

			EventLogHelper.WriteApplicationEventLogEntry("CountApplicationEventLogEntriesTest3");
			EventLogHelper.WriteApplicationEventLogEntry("CountApplicationEventLogEntriesTest4");
			EventLogHelper.WriteApplicationEventLogEntry("CountApplicationEventLogEntriesTest5");
			Assert.AreEqual(5, EventLogHelper.CountApplicationEventLogEntries());

			Assert.AreEqual(1, EventLogHelper.CountApplicationEventLogEntries("CountApplicationEventLogEntriesTest1"));
			Assert.AreEqual(1, EventLogHelper.CountApplicationEventLogEntries("CountApplicationEventLogEntriesTest2"));
			Assert.AreEqual(1, EventLogHelper.CountApplicationEventLogEntries("CountApplicationEventLogEntriesTest3"));
			Assert.AreEqual(1, EventLogHelper.CountApplicationEventLogEntries("CountApplicationEventLogEntriesTest4"));
			Assert.AreEqual(1, EventLogHelper.CountApplicationEventLogEntries("CountApplicationEventLogEntriesTest5"));
		}

		[Test]
		public void GetApplicationEventLogEntriesTest()
		{
			EventLogHelper.ClearAllApplicationEventLogEntries();
			Assert.AreEqual(0, EventLogHelper.ApplicationEventLogEntries.Count);

			EventLogHelper.WriteApplicationEventLogEntry("GetApplicationEventLogEntriesTest");
			Assert.AreEqual(1, EventLogHelper.ApplicationEventLogEntries.Count);

			EventLogHelper.WriteApplicationEventLogEntry("GetApplicationEventLogEntriesTest");
			Assert.AreEqual(2, EventLogHelper.ApplicationEventLogEntries.Count);

			EventLogHelper.WriteApplicationEventLogEntry("GetApplicationEventLogEntriesTest");
			EventLogHelper.WriteApplicationEventLogEntry("GetApplicationEventLogEntriesTest");
			EventLogHelper.WriteApplicationEventLogEntry("GetApplicationEventLogEntriesTest");
			Assert.AreEqual(5, EventLogHelper.ApplicationEventLogEntries.Count);
		}

		[Test]
		public void WriteApplicationEventLogEntryTest()
		{
			EventLogHelper.ClearAllApplicationEventLogEntries();

			EventLogHelper.WriteApplicationEventLogEntry("WriteApplicationEventLogEntryTest1");
			Assert.AreEqual(1, EventLogHelper.CountApplicationEventLogEntries("WriteApplicationEventLogEntryTest1"));

			EventLogHelper.WriteApplicationEventLogEntry("WriteApplicationEventLogEntryTest1");
			Assert.AreEqual(2, EventLogHelper.CountApplicationEventLogEntries("WriteApplicationEventLogEntryTest1"));

			EventLogHelper.WriteApplicationEventLogEntry("WriteApplicationEventLogEntryTest2");
			Assert.AreEqual(1, EventLogHelper.CountApplicationEventLogEntries("WriteApplicationEventLogEntryTest2"));

			EventLogHelper.WriteApplicationEventLogEntry("WriteApplicationEventLogEntryTest3");
			Assert.AreEqual(1, EventLogHelper.CountApplicationEventLogEntries("WriteApplicationEventLogEntryTest3"));

			EventLogHelper.WriteApplicationEventLogEntry("WriteApplicationEventLogEntryTest4");
			Assert.AreEqual(1, EventLogHelper.CountApplicationEventLogEntries("WriteApplicationEventLogEntryTest4"));

			Assert.AreEqual(2, EventLogHelper.CountApplicationEventLogEntries("WriteApplicationEventLogEntryTest1"));
		}
		#endregion
	}
}
