using System;
using NUnit.Framework;
using System.Data.SqlClient;

namespace TestSharp.Tests
{
	[TestFixture()]
	public class SqlHelperTest
	{
		[Test]
		public void ConnectionStringTest()
		{
			ExceptionAssert.IsThrowing(
				new InvalidOperationException("Para utilizar o SqlHelper, antes configure uma ConnectionString com o nome 'Test' na seção ConnectionStrings ou defina o valor da propriedade SqlHelper.ConnectionString."),
				() =>
				{
					var cs1 = SqlHelper.ConnectionString;
				});

			SqlHelper.ConnectionString = "My wrong connection string";
			var cs2 = SqlHelper.ConnectionString;

			SqlHelper.ConnectionString = null;
		}

		[Test]
		public void NoConnectionStringExecutes()
		{
			SqlHelper.ConnectionString = "User ID=test;Password=test;Database=test;Server=test;Connection Timeout=5;";
			var expectedExceptionType = typeof(SqlException);

			ExceptionAssert.IsThrowing(expectedExceptionType,
			() =>
			{
				SqlHelper.ExecuteDateSet("SELECT * TestTable");
			});

			ExceptionAssert.IsThrowing(expectedExceptionType,
			() =>
			{
				SqlHelper.ExecuteDateTable("SELECT * TestTable");
			});

			ExceptionAssert.IsThrowing(expectedExceptionType,
			() =>
			{
				SqlHelper.ExecuteNonQuery("DELETE FROM TestTable");
			});

			ExceptionAssert.IsThrowing(expectedExceptionType,
			() =>
			{
				SqlHelper.ExecuteScalar("SELECT COUNT(1)  TestTable");
			});

			ExceptionAssert.IsThrowing(expectedExceptionType,
			() =>
			{
				SqlHelper.GetInt("SELECT COUNT(1) TestTable");
			});
		}

		[Test]
		public void WrongConnectionStringExecutes()
		{
			SqlHelper.ConnectionString = "My wrong connection string";
			var expectedException = new InvalidOperationException("A ConnectionString 'Test' parece estar errada. Por favor, verifique-a.");

			ExceptionAssert.IsThrowing(expectedException,
			() =>
			{
				SqlHelper.ExecuteDateSet("SELECT * TestTable");
			});

			ExceptionAssert.IsThrowing(expectedException,
			() =>
			{
				SqlHelper.ExecuteDateTable("SELECT * TestTable");
			});

			ExceptionAssert.IsThrowing(expectedException,
			() =>
			{
				SqlHelper.ExecuteNonQuery("DELETE FROM TestTable");
			});

			ExceptionAssert.IsThrowing(expectedException,
			() =>
			{
				SqlHelper.ExecuteScalar("SELECT COUNT(1)  TestTable");
			});

			ExceptionAssert.IsThrowing(expectedException,
			() =>
			{
				SqlHelper.GetInt("SELECT COUNT(1) TestTable");
			});
		}
	}
}
