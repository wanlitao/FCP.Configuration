using System;
using System.Threading.Tasks;
using Consul.RsetApi.Client;
using FCP.Util;

namespace FCP.Configuration.Consul
{
    public class ConsulConfigurationProvider
    {
        private Uri _apiBaseUri;

        #region 构造函数
        public ConsulConfigurationProvider()
            : this(ConsulConstants.DefaultApiBaseUri)
        { }

        public ConsulConfigurationProvider(Uri apiBaseUri)
        {
            _apiBaseUri = apiBaseUri ?? ConsulConstants.DefaultApiBaseUri;
        }
        #endregion

        #region Add
        /// <summary>
        /// 添加配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool Add(string name, string value)
        {
            return AsyncFuncHelper.RunSync(() => AddAsync(name, value));
        }

        /// <summary>
        /// 添加配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string name, string value)
        {
            if (name.isNullOrEmpty())
                return false;

            using (var client = new ConsulRsetApiClient(_apiBaseUri))
            {
                var response = await client.kvPutAsync(name, value, true).ConfigureAwait(false);
                return response.ResponseData;
            }
        }
        #endregion

        #region AddOrUpdate
        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool AddOrUpdate(string name, string value)
        {
            return AsyncFuncHelper.RunSync(() => AddOrUpdateAsync(name, value));
        }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<bool> AddOrUpdateAsync(string name, string value)
        {
            if (name.isNullOrEmpty())
                return false;

            using (var client = new ConsulRsetApiClient(_apiBaseUri))
            {
                var response = await client.kvPutAsync(name, value).ConfigureAwait(false);
                return response.ResponseData;
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        public bool Delete(string name)
        {
            return AsyncFuncHelper.RunSync(() => DeleteAsync(name));
        }

        /// <summary>
        /// 删除配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string name)
        {
            if (name.isNullOrEmpty())
                return false;

            using (var client = new ConsulRsetApiClient(_apiBaseUri))
            {
                var response = await client.kvDeleteAsync(name).ConfigureAwait(false);
                return response.ResponseData;
            }
        }
        #endregion

        #region Get
        /// <summary>
        /// 查询配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        public string Get(string name)
        {
            return AsyncFuncHelper.RunSync(() => GetAsync(name));
        }

        /// <summary>
        /// 查询配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string name)
        {
            if (name.isNullOrEmpty())
                return string.Empty;

            using (var client = new ConsulRsetApiClient(_apiBaseUri))
            {
                var response = await client.kvGetRawAsync(name).ConfigureAwait(false);
                return response.ResponseData;
            }
        }
        #endregion

        #region Get Keys
        /// <summary>
        /// 获取配置Key列表
        /// </summary>
        /// <param name="prefix">配置Key前缀</param>
        /// <returns></returns>
        public string[] GetKeys(string prefix)
        {
            return AsyncFuncHelper.RunSync(() => GetKeysAsync(prefix));
        }

        /// <summary>
        /// 获取配置Key列表
        /// </summary>
        /// <param name="prefix">配置Key前缀</param>
        /// <returns></returns>
        public async Task<string[]> GetKeysAsync(string prefix)
        {
            if (prefix.isNullOrEmpty())
                return null;

            using (var client = new ConsulRsetApiClient(_apiBaseUri))
            {
                var response = await client.kvGetKeysAsync(prefix).ConfigureAwait(false);
                return response.ResponseData;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool Update(string name, string value)
        {
            return AsyncFuncHelper.RunSync(() => UpdateAsync(name, value));
        }

        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(string name, string value)
        {
            if (name.isNullOrEmpty())
                return false;

            using (var client = new ConsulRsetApiClient(_apiBaseUri))
            {
                var queryResponse = await client.kvGetAsync(name).ConfigureAwait(false);
                if (queryResponse.ResponseData == null)
                    return false;  //不存在相应配置信息

                var response = await client.kvPutAsync(name, value).ConfigureAwait(false);
                return response.ResponseData;
            }
        }
        #endregion
    }
}
