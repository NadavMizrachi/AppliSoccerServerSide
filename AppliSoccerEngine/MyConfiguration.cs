using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace AppliSoccerEngine
{
    public class MyConfiguration
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Configuration _config;
        private static MyConfiguration _myConfiguration = new MyConfiguration();


        private MyConfiguration()
        {
            _config = InitConfigurationSource();
        }

        private Configuration InitConfigurationSource()
        {
            string exeConfigPath = this.GetType().Assembly.Location;
            Configuration config = null;
            try
            {
                config = ConfigurationManager.OpenExeConfiguration(exeConfigPath);
            }
            catch (Exception ex)
            {
                _logger.Error("Cannot initialize configuration");
                _logger.Error(ex);
                _logger.Error(ex.StackTrace);
            }
            return config;
        }

        private T GetAppSetting<T>(string key)
        {
            KeyValueConfigurationElement element = _config.AppSettings.Settings[key];
            string value = element.Value;
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static bool GetBoolFromAppSetting(string key)
        {
            try
            {
                return _myConfiguration.GetAppSetting<bool>(key);
            }catch(Exception ex)
            {
                _logger.Error(ex);
                return false;
            }
        }

        public static string GetStringFromAppSetting(string key)
        {
            try
            {
                return _myConfiguration.GetAppSetting<string>(key);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return string.Empty;
            }
        }
    }
}
