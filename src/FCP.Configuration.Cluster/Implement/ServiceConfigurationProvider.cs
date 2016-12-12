using System;
using System.Threading.Tasks;

namespace FCP.Configuration.Cluster
{
    public class ServiceConfigurationProvider : IServiceConfigurationProvider
    {
        private readonly IDistributedConfigurationProvider _innerConfigurationProvider;

        public ServiceConfigurationProvider(IDistributedConfigurationProvider configurationProvider)
        {
            if (configurationProvider == null)
                throw new ArgumentNullException(nameof(configurationProvider));

            _innerConfigurationProvider = configurationProvider;
        }

        protected IDistributedConfigurationProvider InnerConfigurationProvider { get { return _innerConfigurationProvider; } }

        protected virtual string ServiceRegion
        {
            get { return FCPClusterConfigurationConstants.serviceConfigRegion; }
        }
        
        /// <summary>
        /// 获取 服务编码 集合
        /// </summary>        
        /// <returns></returns>
        public Task<string[]> GetServiceCodesAsync()
        {
            return InnerConfigurationProvider.GetRegionNamesAsync(ServiceRegion);
        }

        /// <summary>
        /// 获取 服务配置
        /// </summary>
        /// <param name="serviceCode">服务编码</param>
        /// <returns></returns>
        public Task<T> GetServiceConfigAsync<T>(string serviceCode)
        {
            return InnerConfigurationProvider.GetAsync<T>(serviceCode, ServiceRegion);
        }

        /// <summary>
        /// 添加或更新 服务配置
        /// </summary>
        /// <param name="serviceCode">服务编码</param>
        /// <param name="configValue">配置值</param>
        /// <returns></returns>
        public Task<bool> AddOrUpdateServiceConfigAsync<T>(string serviceCode, T configValue)
        {
            return InnerConfigurationProvider.AddOrUpdateAsync(serviceCode, configValue, ServiceRegion);
        }
    }
}
