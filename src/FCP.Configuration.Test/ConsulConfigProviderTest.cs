using FCP.Configuration.Consul;

namespace FCP.Configuration.Test
{
    public class ConsulConfigProviderTest : ConfigurationProviderTest
    {
        protected override IConfigurationProvider GetConfigurationProvider()
        {
            return new ConsulConfigurationProvider();
        }
    }
}
