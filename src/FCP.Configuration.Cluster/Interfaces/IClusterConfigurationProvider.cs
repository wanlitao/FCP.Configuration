namespace FCP.Configuration.Cluster
{
    public interface IClusterConfigurationProvider : IDistributedConfigurationProvider,
        IConsulOptionsConfigurationProvider, IServiceConfigurationProvider
    {

    }
}
