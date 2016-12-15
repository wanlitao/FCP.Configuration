using FCP.Configuration.AppSettings;

namespace FCP.Configuration.Test
{
    public class AppSettingsConfigProviderTest : ConfigurationProviderTest
    {
        protected override IDistributedConfigurationProvider GetConfigurationProvider()
        {
            return new AppSettingsConfigurationProvider().AsDistributedConfiguration();
        }
    }
}