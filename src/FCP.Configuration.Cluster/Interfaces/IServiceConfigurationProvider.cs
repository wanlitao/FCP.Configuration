using System.Threading.Tasks;

namespace FCP.Configuration.Cluster
{
    public interface IServiceConfigurationProvider
    {
        /// <summary>
        /// 获取 服务编码 集合
        /// </summary>        
        /// <returns></returns>
        Task<string[]> GetServiceCodesAsync();        

        /// <summary>
        /// 获取 服务配置
        /// </summary>
        /// <param name="serviceCode">服务编码</param>
        /// <returns></returns>
        Task<T> GetServiceConfigAsync<T>(string serviceCode);

        /// <summary>
        /// 添加或更新 服务配置
        /// </summary>
        /// <param name="serviceCode">服务编码</param>
        /// <param name="configValue">配置值</param>
        /// <returns></returns>
        Task<bool> AddOrUpdateServiceConfigAsync<T>(string serviceCode, T configValue);
    }
}
