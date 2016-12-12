using System;
using System.Threading.Tasks;

namespace FCP.Configuration.Exceptionless
{
    public class ExceptionlessConfigurationProvider : IExceptionlessConfigurationProvider
    {
        private readonly IDistributedConfigurationProvider _innerConfigurationProvider;

        public ExceptionlessConfigurationProvider(IDistributedConfigurationProvider configurationProvider)
        {
            if (configurationProvider == null)
                throw new ArgumentNullException(nameof(configurationProvider));

            _innerConfigurationProvider = configurationProvider;
        }

        protected IDistributedConfigurationProvider InnerConfigurationProvider { get { return _innerConfigurationProvider; } }

        protected virtual string ExceptionlessRegion
        {
            get { return FCPExceptionlessConfigurationConstants.exceptionlessConfigRegion; }
        }

        /// <summary>
        /// 获取 Exceptionless App编码 集合
        /// </summary>        
        /// <returns></returns>
        public Task<string[]> GetExceptionlessAppCodesAsync()
        {
            return InnerConfigurationProvider.GetRegionNamesAsync(ExceptionlessRegion);
        }

        /// <summary>
        /// 获取 App Exceptionless配置
        /// </summary>
        /// <returns></returns>
        public Task<ExceptionlessApiInfo> GetAppExceptionlessApiAsync(string appCode)
        {
            return InnerConfigurationProvider.GetAsync<ExceptionlessApiInfo>(appCode, ExceptionlessRegion);
        }

        /// <summary>
        /// 添加或更新 App Exceptionless配置
        /// </summary>
        /// <returns></returns>
        public Task<bool> AddOrUpdateAppExceptionlessApiAsync(string appCode, ExceptionlessApiInfo configModel)
        {
            return InnerConfigurationProvider.AddOrUpdateAsync(appCode, configModel, ExceptionlessRegion);
        }
    }
}
