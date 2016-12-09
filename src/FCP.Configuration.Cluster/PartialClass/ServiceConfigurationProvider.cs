using System.Threading.Tasks;

namespace FCP.Configuration.Cluster
{
    public partial class ClusterConfigurationProvider
    {
        protected abstract string ServiceRegion { get; }

        /// <summary>
        /// 获取 服务编码 集合
        /// </summary>        
        /// <returns></returns>
        public Task<string[]> GetServiceCodesAsync()
        {
            return GetRegionNamesAsync(ServiceRegion);
        }

        /// <summary>
        /// 获取 服务配置
        /// </summary>
        /// <param name="serviceCode">服务编码</param>
        /// <returns></returns>
        public Task<T> GetServiceConfigAsync<T>(string serviceCode)
        {
            return GetAsync<T>(serviceCode, ServiceRegion);
        }

        /// <summary>
        /// 添加或更新 服务配置
        /// </summary>
        /// <param name="serviceCode">服务编码</param>
        /// <param name="configValue">配置值</param>
        /// <returns></returns>
        public Task<bool> AddOrUpdateServiceConfigAsync<T>(string serviceCode, T configValue)
        {
            return AddOrUpdateAsync(serviceCode, configValue, ServiceRegion);
        }
    }
}
