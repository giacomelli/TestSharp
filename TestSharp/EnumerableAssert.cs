using System.Collections;

namespace TestSharp
{
	/// <summary>
	/// Asserts para enumerables
	/// </summary>
	public static class EnumerableAssert
	{
		/// <summary>
		/// Verifica se todos os items não são nulos
		/// </summary>
		/// <param name="items">Items a serem avaliados.</param>
		public static void AllItemsAreNotNull(IEnumerable items)
		{
			foreach (var item in items)
			{
				if (item == null)
				{
					AssertHelper.ThrowAssert("EnumerableAssert", "AllItemsAreNotNull", true, false);
				}
			}
		}
	}
}
