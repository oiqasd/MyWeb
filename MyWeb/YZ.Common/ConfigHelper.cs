using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YZ.Common
{
    public class ConfigHelper
    {
        /// <summary>
        /// Get a required value from AppSettings section of the configuration pipeline
        /// </summary>
        /// <typeparam name="T">Type to cast result to</typeparam>
        /// <param name="key">Key in AppSettings section containing value</param>
        /// <returns>Converted type</returns>
        /// <exception cref="ConfigurationErrorsException">Thrown if key cannot be found or converted into requested type</exception>
        public static T AppSetting<T>(string key)
        {
            string configValue = ConfigurationManager.AppSettings[key];
            if (configValue == null)
            {
                string msg = string.Format("Unable to find required <AppSettings> key = \"{0}\" in .config file or configuration pipeline", key);

                throw new ConfigurationErrorsException(msg);
            }
            try
            {
                return ConvertHelper.ConvertTo<T>(configValue);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Unable to convert '{0}' to type of {1}", configValue, typeof(T).FullName), ex);
            }
        }

        /// <summary>
        /// Get an optional value from AppSettings section of the configuration pipeline
        /// </summary>
        /// <typeparam name="T">Type to cast result to</typeparam>
        /// <param name="key">Key in AppSettings section containing value</param>
        /// <param name="defaultValue">default to use if key is not found in configuration file</param>
        /// <returns>Converted type</returns>
        public static T AppSetting<T>(string key, T defaultValue)
        {
            string configValue = ConfigurationManager.AppSettings[key];
            if (configValue == null)
            {
                string msg = string.Format("Unable to find <AppSettings> key=\"{0}\" in .config or configuration pipeline.  Using default value '{1}' instead",
                    key, defaultValue.ToString());

                return defaultValue;
            }
            try
            {
                return ConvertHelper.ConvertTo<T>(configValue);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Unable to convert '{0}' to type of {1}.  Using '{2}' instead", configValue, typeof(T).FullName, defaultValue.ToString()), ex);
            }
        }

        /// <summary>
        /// Gets a connection string from .config or configuration pipeline
        /// </summary>
        /// <param name="key">Key in connection string to get</param>
        /// <returns>Connection string as found in .config file</returns>
        /// <exception cref="ConfigurationException">If key cannot be found in .config file</exception>
        public static string ConnectionString(string key)
        {
            ConnectionStringSettings configValue = ConfigurationManager.ConnectionStrings[key];
            if (configValue == null)
            {
                string msg = string.Format("Unable to find <ConnectionStrings> key = \"{0}\" in .config file or configuration pipeline",
                    key);

                throw new ConfigurationErrorsException(msg);
            }
            return configValue.ConnectionString;
        }
    }
}
