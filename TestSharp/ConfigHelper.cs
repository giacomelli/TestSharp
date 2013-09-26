using System.Configuration;
using System.IO;
using System.Xml;
using System;

namespace TestSharp
{
	/// <summary>
	/// Utilitários para web.config e app.config.
	/// </summary>
	public static class ConfigHelper
	{
		#region Methods
		/// <summary>
		/// Lê o web.config/app.config do projeto da pasta informada.
		/// </summary>
		/// <param name="projectFolderName">O nome da pasta do projeto onde será lido o web.config/app.config.</param>
		/// <returns>Retornar a configuração lida do web.config/app.config.</returns>
		public static Configuration ReadConfig(string projectFolderName)
		{
			var configMap = new ExeConfigurationFileMap();
			configMap.ExeConfigFilename = GetFileConfigPath(projectFolderName);
			
			return ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
		}

		/// <summary>
		/// Lê o valor de uma chave no AppSettings
		/// </summary>
		/// <param name="projectFolderName">O nome da pasta do projeto onde será lido o web.config/app.config.</param>
		/// <param name="key">O nome da chave na AppSettings.</param>
		/// <returns>O valor da chave.</returns>
		public static string ReadAppSetting(string projectFolderName, string key)
		{
			return ReadConfig(projectFolderName).AppSettings.Settings[key].Value;
		}

		/// <summary>
		/// Escreve o valor informado na chave do AppSettings
		/// </summary>
		/// <param name="projectFolderName">O nome da pasta do projeto onde será lido o web.config/app.config.</param>
		/// <param name="key">O nome da chave na AppSettings.</param>
		/// <param name="value">O valor a ser escrito na chave.</param>
		public static void WriteAppSetting(string projectFolderName, string key, string value)
		{
			var fileConfigPath = GetFileConfigPath(projectFolderName);
			var doc = new XmlDocument();
			doc.Load(fileConfigPath);

			var keyElement = doc.SelectSingleNode("/configuration/appSettings/add[@key='" + key + "']");

			if (keyElement == null)
			{
				throw new ArgumentException("A chave '" + key + "' não existe no AppSettings.", "key");
			}

			keyElement.Attributes["value"].Value = value;
			doc.Save(fileConfigPath);
		}

		/// <summary>
		/// Obtém o caminho do arquivo de configuração do projeto informado.
		/// </summary>
		/// <param name="projectFolderName">O nome da pasta do projeto onde será lido o web.config/app.config.</param>
		/// <returns>O caminho do arquivo.</returns>
		private static string GetFileConfigPath(string projectFolderName)
		{
			var folderPath = VSProjectHelper.GetProjectFolderPath(projectFolderName);
			var fileConfig = Path.Combine(folderPath, "Web.config");

			// Tenta ler um web.config, caso o arquivo não exista, então tenta ler o app.config.
			if (!File.Exists(fileConfig))
			{
				fileConfig = Path.Combine(folderPath, "app.config");
			}

			return fileConfig;
		}
		#endregion				
	}
}
