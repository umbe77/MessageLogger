using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace Umbe.Web.MessageLogger.Config
{
    public class MessageLoggerConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get
            {
                return base["providers"] as ProviderSettingsCollection;
            }
        }

        [ConfigurationProperty("default", DefaultValue = "FileProvider")]
        public string Default
        {
            get
            {
                return (string)base["default"];
            }
            set
            {
                base["default"] = value;
            }
        }

        [ConfigurationProperty("activated", DefaultValue = false)]
        public bool Activated
        {
            get
            {
                return (bool)base["activated"];
            }
            set
            {
                base["activated"] = value;
            }
        }
    }
}
