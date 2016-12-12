namespace FCP.Configuration.Cluster
{
    public static class FCPClusterConfigurationConstants
    {
        #region Consul配置
        /// <summary>
        /// Consul 配置Region
        /// </summary>
        public const string consulConfigRegion = "consul";
        /// <summary>
        /// Consul 服务检测间隔 配置名称
        /// </summary>
        public const string consulCheckIntervalConfigName = "serviceCheckInterval";
        #endregion

        #region 服务配置
        /// <summary>
        /// 服务 配置Region
        /// </summary>
        public const string serviceConfigRegion = "service";
        #endregion
    }
}
