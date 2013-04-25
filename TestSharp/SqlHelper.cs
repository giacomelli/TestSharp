using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace TestSharp
{
	/// <summary>
	/// Utilitária para questões relativas a SQL.
	/// <remarks>
	/// Para utilizar o SqlHelper, antes configure uma ConnectionString com o nome 'Test' na seção ConnectionStrings."
	/// </remarks>
	/// </summary>
	public static class SqlHelper
	{
		#region Fields
		private static string s_connectionString;
		#endregion

		#region Properties
		/// <summary>
		/// Obtém ou define a string de conexão que será utilizada.
		/// </summary>
		public static string ConnectionString
		{
			get
			{
				if (s_connectionString == null)
				{
					if (ConfigurationManager.ConnectionStrings["Test"] == null)
					{
						throw new InvalidOperationException("Para utilizar o SqlHelper, antes configure uma ConnectionString com o nome 'Test' na seção ConnectionStrings ou defina o valor da propriedade SqlHelper.ConnectionString.");
					}

					s_connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
				}

				return s_connectionString;
			}

			set
			{
				s_connectionString = value;
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Realiza um SqlCommand.ExecuteScalar.
		/// </summary>
		/// <param name="command">O comando escalar a ser executado.</param>
		/// <param name="parameters">Os parâmetros (SqlParameter) do comando.</param>
		/// <returns>O resultado da execução do comando.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public static object ExecuteScalar(string command, params object[] parameters)
		{
			using (var conn = CreateConnection())
			{
				conn.Open();
				var cmd = conn.CreateCommand();
				cmd.CommandText = command;

				AddParameters(cmd, parameters);

				return cmd.ExecuteScalar();
			}
		}

		/// <summary>
		/// Obtém um resultado inteiro da execução do comando escalar informado.
		/// </summary>
		/// <param name="command">O comando escalar a ser executado.</param>
		/// <param name="parameters">Os parâmetros (SqlParameter) do comando.</param>
		/// <returns>O resultado da execução do comando.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int")]
		public static int GetInt(string command, params object[] parameters)
		{
			return Convert.ToInt32(ExecuteScalar(command, parameters), CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Realiza um SqlCommand.ExecuteNowQuery.
		/// </summary>
		/// <param name="command">O comando a ser executado.</param>
		/// <param name="parameters">Os parâmetros (SqlParameter) do comando.</param>
		/// <returns>O resultado da execução do comando.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public static int ExecuteNonQuery(string command, params object[] parameters)
		{
			using (var conn = CreateConnection())
			{
				conn.Open();
				var cmd = conn.CreateCommand();
				cmd.CommandText = command;

				if (parameters != null)
					AddParameters(cmd, parameters);

				return cmd.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// Retorna um DataTable preenchido com o resultado da execução do comando informado.		
		/// </summary>
		/// <param name="command">O comando a ser executado.</param>
		/// <param name="parameters">Os parâmetros (SqlParameter) do comando.</param>
		/// <returns>O DataTable com o resultado da execução do comando.</returns>
		public static DataTable ExecuteDateTable(string command, params object[] parameters)
		{
			return ExecuteDateSet(command, parameters).Tables[0];
		}

		/// <summary>
		/// Retorna um DataSet preenchido com o resultado da execução do comando informado.
		/// </summary>
		/// <param name="command">O comando a ser executado.</param>
		/// <param name="parameters">Os parâmetros (SqlParameter) do comando.</param>
		/// <returns>O DataSet com o resultado da execução do comando.</returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		public static DataSet ExecuteDateSet(string command, params object[] parameters)
		{
			using (var conn = CreateConnection())
			{
				conn.Open();
				var cmd = conn.CreateCommand();
				cmd.CommandText = command;
				AddParameters(cmd, parameters);
				var ds = new DataSet();
				ds.Locale = CultureInfo.CurrentCulture;

				using (var adapter = new SqlDataAdapter(cmd))
				{
					adapter.Fill(ds);
				}

				return ds;
			}
		}
		#endregion

		#region Private Methods
		private static SqlConnection CreateConnection()
		{
			try
			{
				return new SqlConnection(ConnectionString);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidOperationException("A ConnectionString 'Test' parece estar errada. Por favor, verifique-a.", ex);
			}
		}
		private static void AddParameters(SqlCommand cmd, params object[] parameters)
		{
			if (parameters != null)
			{
				for (int i = 0; i < parameters.Length; i += 2)
				{
					var parameterName = parameters[i].ToString();
					var parameterValue = parameters[i + 1];

					if (parameterValue == null)
					{
						parameterValue = DBNull.Value;
					}

					cmd.Parameters.Add(new SqlParameter(parameterName, parameterValue));
				}
			}
		}
		#endregion
	}
}
