using System.Threading.Tasks;

namespace FCP.Configuration
{
    /// <summary>
    /// 配置提供程序 接口
    /// </summary>
    public interface IConfigurationProvider
    {
        #region Get
        /// <summary>
        /// 查询配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        string GetConfiguration(string name);

        /// <summary>
        /// 查询配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        Task<string> GetConfigurationAsync(string name);
        #endregion

        #region Get Keys
        /// <summary>
        /// 获取配置Key列表
        /// </summary>
        /// <param name="prefix">配置Key前缀</param>
        /// <returns></returns>
        string[] GetConfigurationKeys(string prefix);

        /// <summary>
        /// 获取配置Key列表
        /// </summary>
        /// <param name="prefix">配置Key前缀</param>
        /// <returns></returns>
        Task<string[]> GetConfigurationKeysAsync(string prefix);
        #endregion

        #region Add
        /// <summary>
        /// 添加配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool AddConfiguration(string name, string value);

        /// <summary>
        /// 添加配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Task<bool> AddConfigurationAsync(string name, string value);
        #endregion

        #region Update
        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool UpdateConfiguration(string name, string value);

        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Task<bool> UpdateConfigurationAsync(string name, string value);
        #endregion

        #region AddOrUpdate
        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool AddOrUpdateConfiguration(string name, string value);

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Task<bool> AddOrUpdateConfigurationAsync(string name, string value);
        #endregion

        #region Delete
        /// <summary>
        /// 删除配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        bool DeleteConfiguration(string name);

        /// <summary>
        /// 删除配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        Task<bool> DeleteConfigurationAsync(string name);
        #endregion
    }
}
