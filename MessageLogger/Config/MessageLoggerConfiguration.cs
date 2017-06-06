using System.Configuration;


namespace Umbe.Web.MessageLogger.Config
{
    public class MessageLoggerConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers => base["providers"] as ProviderSettingsCollection;

        [ConfigurationProperty("default", DefaultValue = "FileProvider")]
        public string Default
        {
            get => (string)base["default"];
            set => base["default"] = value;
        }

        [ConfigurationProperty("activated", DefaultValue = false)]
        public bool Activated
        {
            get => (bool)base["activated"];
            set => base["activated"] = value;
        }
    }
}
