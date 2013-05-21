#if WIN
using System.Diagnostics;
using System.Linq;

namespace TestSharp
{
	/// <summary>
	/// Utilitária para questões relativas ao Event Log do Windows.
	/// </summary>
	public static class EventLogHelper
	{
		#region Fields
		private static EventLog s_applicationEventLog;
		#endregion

		#region Properties
		/// <summary>
		/// Obtém o log de evento Application.
		/// </summary>
		private static EventLog ApplicationEventLog
		{
			get
			{
				if (s_applicationEventLog == null)
				{
					s_applicationEventLog = EventLog.GetEventLogs().First(e => e.Log.Equals("Application"));
					s_applicationEventLog.Source = "EventLogHelper";
				}

				return s_applicationEventLog;
			}
		}

		/// <summary>
		/// Obtém todas as entradas do log de evento Application.
		/// </summary>
		/// <returns></returns>
		public static EventLogEntryCollection ApplicationEventLogEntries
		{
			get
			{
				return ApplicationEventLog.Entries;
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Remove todas as entradas do log de evento Application.
		/// </summary>
		public static void ClearAllApplicationEventLogEntries()
		{			
			ApplicationEventLog.Clear();
		}

		/// <summary>
		/// Conta as entradas existentes no log de evento Application.
		/// </summary>
		/// <returns>Número de entradas.</returns>
		public static int CountApplicationEventLogEntries()
		{
			return ApplicationEventLog.Entries.Count;
		}

		/// <summary>
		/// Conta as entradas existentes no log de evento Application.
		/// </summary>
		/// <param name="message">A mensagem para filtrar as entradas.</param>
		/// <param name="type">O tipo para filtrar as entradas.</param>
		/// <param name="instanceId">O id da instância para filtrar as entradas.</param>
		/// <returns>Número de entradas.</returns>
		public static int CountApplicationEventLogEntries(string message, EventLogEntryType type = EventLogEntryType.Information, int instanceId = 0)
		{
			var query = from e in ApplicationEventLog.Entries.Cast<EventLogEntry>()
						where e.Message.Equals(message)
						&& e.EntryType == type
						&& e.InstanceId == instanceId
						select e;

			return query.Count();
		}

		/// <summary>
		/// Escreve uma entrada no log de evento Application.
		/// </summary>
		/// <param name="message">A mensagem.</param>
		/// <param name="type">O tipo de entrada.</param>
		/// <param name="instanceId">O id da instância.</param>
		public static void WriteApplicationEventLogEntry(string message, EventLogEntryType type = EventLogEntryType.Information, int instanceId = 0)
		{
			ApplicationEventLog.WriteEntry(message, type, instanceId);
		}		
		#endregion
	}
}
#endif