using FCP.Configuration.Consul;

namespace FCP.Configuration.Test
{
    public class ConsulConfigProviderTest : ConfigurationProviderTest
    {
        protected override IDistributedConfigurationProvider GetConfigurationProvider()
        {
            //return new ConsulConfigurationProvider();
            return null;
        }
    }
}
