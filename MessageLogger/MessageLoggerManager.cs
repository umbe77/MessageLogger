using System;
using System.Configuration;
using System.Web.Configuration;

namespace Umbe.Web.MessageLogger
{
    public class MessageLoggerManager
    {
        public static bool IsActive { get; private set; }
        
        public static MessageLoggerProvider DefaultProvider { get; private set; }
        public static MessageLoggerCollection Providers { get; private set; }

        static MessageLoggerManager()
        {
            Initialize();
        }

        public static void Activate()
        {
            IsActive = true;
        }

        public static void Deactivate()
        {
            IsActive = false;
        }

        private static void Initialize()
        {
            var configuration = ConfigurationManager.GetSection("MessageLogger") as Config.MessageLoggerConfiguration;
            if (configuration == null)
            {
                throw new ConfigurationErrorsException("MessageLogger Configuration section not setted properly");
            }

            Providers = new MessageLoggerCollection();
            ProvidersHelper.InstantiateProviders(configuration.Providers, Providers, typeof(MessageLoggerProvider));

            Providers.SetReadOnly();

            DefaultProvider = Providers[configuration.Default];
            IsActive = configuration.Activated;

            if (DefaultProvider == null)
            {
                throw new Exception("MessageLogger DeafultProvider not setted");
            }
        }
    }
}
