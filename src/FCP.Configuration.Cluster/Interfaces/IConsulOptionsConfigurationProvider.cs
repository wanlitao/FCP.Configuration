using System.Threading.Tasks;

namespace FCP.Configuration.Cluster
{
    public interface IConsulOptionsConfigurationProvider
    {
        /// <summary>
        /// 获取 Consul 配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<T> GetConsulConfigAsync<T>(string name);

        /// <summary>
        /// 添加或更新 Consul 配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="configValue"></param>
        /// <returns></returns>
        Task<bool> AddOrUpdateConsulConfigAsync<T>(string name, T configValue);

        /// <summary>
        /// 获取 Consul服务检测间隔 配置
        /// </summary>        
        /// <returns></returns>
        Task<int> GetConsulCheckIntervalConfigAsync();

        /// <summary>
        /// 添加或更新 Consul服务检测间隔 配置
        /// </summary>
        /// <param name="checkInterval">检测间隔</param>
        /// <returns></returns>
        Task<bool> AddOrUpdateConsulCheckIntervalConfigAsync(int checkInterval);
    }
}
