namespace FCP.Configuration
{
    public static class ConfigurationProviderExtensions
    {
        public static IDistributedConfigurationProvider AsDistributedConfiguration(this IConfigurationProvider<string> configurationProvider)
        {
            return new ConfigurationProviderDistributedDecorator(configurationProvider);
        }
    }
}
