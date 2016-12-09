namespace FCP.Configuration.Cluster
{
    public class FCPClusterConfigurationProvider : ClusterConfigurationProvider
    {
        protected override string ServiceRegion
        {
            get
            {
                return FCPClusterConfigurationConstants.serviceConfigRegion;
            }
        }

        protected override string ConsulOptionsRegion
        {
            get
            {
                return FCPClusterConfigurationConstants.consulConfigRegion;
            }
        }

        protected override string ConsulCheckIntervalConfigName
        {
            get
            {
                return FCPClusterConfigurationConstants.consulCheckIntervalConfigName;
            }
        }
    }
}
