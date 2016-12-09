using System.Threading.Tasks;

namespace FCP.Configuration.Cluster
{
    public partial class ClusterConfigurationProvider
    {
        protected abstract string ConsulOptionsRegion { get; }

        protected abstract string ConsulCheckIntervalConfigName { get; }

        /// <summary>
        /// 获取 Consul 配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<T> GetConsulConfigAsync<T>(string name)
        {
            return GetAsync<T>(name, ConsulOptionsRegion);
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
            return AddOrUpdateAsync(name, configValue, ConsulOptionsRegion);
        }

        /// <summary>
        /// 获取 Consul服务检测间隔 配置
        /// </summary>        
        /// <returns></returns>
        public Task<int> GetConsulCheckIntervalConfigAsync()
        {
            return GetConsulConfigAsync<int>(ConsulCheckIntervalConfigName);
        }

        /// <summary>
        /// 添加或更新 Consul服务检测间隔 配置
        /// </summary>
        /// <param name="checkInterval">检测间隔</param>
        /// <returns></returns>
        public Task<bool> AddOrUpdateConsulCheckIntervalConfigAsync(int checkInterval)
        {
            return AddOrUpdateConsulConfigAsync(ConsulCheckIntervalConfigName, checkInterval);
        }
    }
}
