using System.Management;
using System.Collections.Generic;

namespace TestSharp
{
	/// <summary>
	/// Utilitária para WMI (Windows Management Instrumentation).
	/// </summary>
    public static class WmiHelper
    {
       #region Public Methods
		/// <summary>
		/// Obtém o valor de uma propriedade de uma entidade publicada no WMI.
		/// </summary>
		/// <typeparam name="TValue">O tipo da propriedade.</typeparam>
		/// <param name="scope">O escopo onde está publicada a entidade WMI. Por exemplo: "\root\cimvC:\CWI\Terra\Terra.Adv.CatalogCleaner\trunk\SRC\Terra.Adv.CatalogCleaner.Infrastructure\Monitoring\CatalogCleanerEntity.cs2"</param>
		/// <param name="entityName">O nome da entidade WMI.</param>
		/// <param name="propertyName">O nome da propriedade a ser lida.</param>
		/// <returns>O valor da propriedade.</returns>
        public static TValue GetPropertyValue<TValue>(string scope, string entityName, string propertyName)
        {
            TValue value = default(TValue);

			using (var searcher = new ManagementObjectSearcher(scope, "select * from " + entityName))
			{
				foreach (var instance in searcher.Get())
				{
					value = (TValue)instance.GetPropertyValue(propertyName);
				}

				return value;
			}
       }

		/// <summary>
		/// Obtém todos valores em todas as entidades de uma propriedade de uma entidade publicada no WMI.
		/// </summary>
		/// <typeparam name="TValue">O tipo da propriedade.</typeparam>
		/// <param name="scope">O escopo onde está publicada a entidade WMI. Por exemplo: "\root\cimvC:\CWI\Terra\Terra.Adv.CatalogCleaner\trunk\SRC\Terra.Adv.CatalogCleaner.Infrastructure\Monitoring\CatalogCleanerEntity.cs2"</param>
		/// <param name="entityName">O nome da entidade WMI.</param>
		/// <param name="propertyName">O nome da propriedade a ser lida.</param>
		/// <returns>O valor da propriedade.</returns>
		public static TValue[] GetPropertyValues<TValue>(string scope, string entityName, string propertyName)
		{
			var values = new List<TValue>();

			using (var searcher = new ManagementObjectSearcher(scope, "select * from " + entityName))
			{
				foreach (var instance in searcher.Get())
				{
					values.Add((TValue)instance.GetPropertyValue(propertyName));
				}

				return values.ToArray();
			}
		}
       #endregion
    }
}
