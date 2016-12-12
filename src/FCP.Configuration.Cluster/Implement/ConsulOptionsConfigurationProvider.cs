using System;
using System.Threading.Tasks;

namespace FCP.Configuration.Cluster
{
    public class ConsulOptionsConfigurationProvider : IConsulOptionsConfigurationProvider
    {
        private readonly IDistributedConfigurationProvider _innerConfigurationProvider;

        public ConsulOptionsConfigurationProvider(IDistributedConfigurationProvider configurationProvider)
        {
            if (configurationProvider == null)
                throw new ArgumentNullException(nameof(configurationProvider));

            _innerConfigurationProvider = configurationProvider;
        }

        protected IDistributedConfigurationProvider InnerConfigurationProvider
        {
            get { return _innerConfigurationProvider; }
        }

        protected virtual string ConsulOptionsRegion
        {
            get { return FCPClusterConfigurationConstants.consulConfigRegion; }
        }

        protected virtual string ConsulCheckIntervalConfigName
        {
            get { return FCPClusterConfigurationConstants.consulCheckIntervalConfigName; }
        }

        /// <summary>
        /// 获取 Consul 配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<T> GetConsulConfigAsync<T>(string name)
        {
            return InnerConfigurationProvider.GetAsync<T>(name, ConsulOptionsRegion);
        }

        /// <summary>
        /// 添加或更新 Consul 配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="configValue"></param>
        /// <returns></returns>
        public Task<bool> AddOrUpdateConsulConfigAsync<T>(string name, T configValue)
        {
            return InnerConfigurationProvider.AddOrUpdateAsync(name, configValue, ConsulOptionsRegion);
        }

        /// <summary>
        /// 获取 Consul服务检测间隔 配置
        /// </summary>        
        /// <returns></returns>
        public Task<int> GetConsulCheckIntervalAsync()
        {
            return GetConsulConfigAsync<int>(ConsulCheckIntervalConfigName);
        }

        /// <summary>
        /// 添加或更新 Consul服务检测间隔 配置
        /// </summary>
        /// <param name="checkInterval">检测间隔</param>
        /// <returns></returns>
        public Task<bool> AddOrUpdateConsulCheckIntervalAsync(int checkInterval)
        {
            return AddOrUpdateConsulConfigAsync(ConsulCheckIntervalConfigName, checkInterval);
        }
    }
}
