using System.Threading.Tasks;

namespace FCP.Configuration.Exceptionless
{
    public interface IExceptionlessConfigurationProvider
    {
        /// <summary>
        /// 获取 Exceptionless App编码 集合
        /// </summary>        
        /// <returns></returns>
        Task<string[]> GetExceptionlessAppCodesAsync();

        /// <summary>
        /// 获取 App Exceptionless配置
        /// </summary>
        /// <returns></returns>
        Task<ExceptionlessApiInfo> GetAppExceptionlessApiAsync(string appCode);

        /// <summary>
        /// 添加或更新 App Exceptionless配置
        /// </summary>
        /// <returns></returns>
        Task<bool> AddOrUpdateAppExceptionlessApiAsync(string appCode, ExceptionlessApiInfo configModel);
    }
}
