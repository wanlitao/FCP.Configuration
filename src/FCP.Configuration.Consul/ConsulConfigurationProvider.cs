using System;
using System.Threading.Tasks;
using Consul.RsetApi.Client;
using FCP.Util;

namespace FCP.Configuration.Consul
{
    public class ConsulConfigurationProvider : BaseDistributedConfigurationProvider
    {
        private readonly Uri _apiBaseUri;
        private readonly ISerializer _serializer;

        #region 构造函数
        public ConsulConfigurationProvider()
            : this(ConsulConstants.DefaultApiBaseUri)
        { }

        public ConsulConfigurationProvider(Uri apiBaseUri)
            : this(apiBaseUri, SerializerFactory.JsonSerializer)
        { }

        public ConsulConfigurationProvider(Uri apiBaseUri, ISerializer serializer)
        {
            if (apiBaseUri == null)
                throw new ArgumentNullException(nameof(apiBaseUri));

            if (serializer == null)
                throw new ArgumentNullException(nameof(serializer));

            _apiBaseUri = apiBaseUri;
            _serializer = serializer;
        }        
        #endregion

        #region Name
        protected string GetEntryName(string name, string region)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(region))
                return name;

            return string.Format("{0}/{1}", region, name);
        }
        #endregion

        #region Serialize
        protected string ToStringValue<TValue>(TValue value)
        {
            return _serializer.SerializeString(value);
        }

        protected TValue FromStringValue<TValue>(string dataStr)
        {
            return _serializer.DeserializeString<TValue>(dataStr);
        }
        #endregion

        #region Add
        protected override bool AddInternal<TValue>(ConfigEntry<string, TValue> entry)
        {
            return AsyncFuncHelper.RunSync(() => AddInternalAsync(entry));
        }

        protected override async Task<bool> AddInternalAsync<TValue>(ConfigEntry<string, TValue> entry)
        {
            using (var client = new ConsulRsetApiClient(_apiBaseUri))
            {
                var fullName = GetEntryName(entry.Name, entry.Region);

                var response = await client.kvPutAsync(fullName, ToStringValue(entry.Value), true).ConfigureAwait(false);
                return response.ResponseData;
            }
        }
        #endregion

        #region AddOrUpdate        

        protected override bool AddOrUpdateInternal<TValue>(ConfigEntry<string, TValue> entry)
        {
            return AsyncFuncHelper.RunSync(() => AddOrUpdateInternalAsync(entry));
        }

        protected override async Task<bool> AddOrUpdateInternalAsync<TValue>(ConfigEntry<string, TValue> entry)
        {
            using (var client = new ConsulRsetApiClient(_apiBaseUri))
            {
                var fullName = GetEntryName(entry.Name, entry.Region);

                var response = await client.kvPutAsync(fullName, ToStringValue(entry.Value)).ConfigureAwait(false);
                return response.ResponseData;
            }            
        }
        #endregion

        #region Delete
        protected override bool DeleteInternal(string name, string region)
        {
            return AsyncFuncHelper.RunSync(() => DeleteInternalAsync(name, region));
        }

        protected override async Task<bool> DeleteInternalAsync(string name, string region)
        {
            using (var client = new ConsulRsetApiClient(_apiBaseUri))
            {
                var fullName = GetEntryName(name, region);

                var response = await client.kvDeleteAsync(fullName).ConfigureAwait(false);
                return response.ResponseData;
            }
        }
        #endregion

        #region Get
        protected override ConfigEntry<string, TValue> GetConfigEntryInternal<TValue>(string name, string region)
        {
            return AsyncFuncHelper.RunSync(() => GetConfigEntryInternalAsync<TValue>(name, region));
        }

        protected override async Task<ConfigEntry<string, TValue>> GetConfigEntryInternalAsync<TValue>(string name, string region)
        {
            using (var client = new ConsulRsetApiClient(_apiBaseUri))
            {
                var fullName = GetEntryName(name, region);
                var response = await client.kvGetRawAsync(fullName).ConfigureAwait(false);

                if (response.ResponseData.isNullOrEmpty())
                    return null;

                var configEntry = new ConfigEntry<string, TValue>(name, region, FromStringValue<TValue>(response.ResponseData));
                return configEntry;
            }            
        }
        #endregion

        #region Get Keys
        protected override string[] GetKeysInternal()
        {
            return AsyncFuncHelper.RunSync(() => GetKeysInternalAsync());
        }

        protected override Task<string[]> GetKeysInternalAsync()
        {
            return GetRegionKeysInternalAsync(string.Empty);
        }

        protected override string[] GetRegionKeysInternal(string region)
        {
            return AsyncFuncHelper.RunSync(() => GetRegionKeysInternalAsync(region));
        }

        protected override async Task<string[]> GetRegionKeysInternalAsync(string region)
        {
            using (var client = new ConsulRsetApiClient(_apiBaseUri))
            {
                var response = await client.kvGetKeysAsync(region).ConfigureAwait(false);
                return response.ResponseData;
            }
        }
        #endregion

        #region Update
        protected override bool UpdateInternal<TValue>(ConfigEntry<string, TValue> entry)
        {
            return AsyncFuncHelper.RunSync(() => UpdateInternalAsync(entry));
        }

        protected override async Task<bool> UpdateInternalAsync<TValue>(ConfigEntry<string, TValue> entry)
        {
            using (var client = new ConsulRsetApiClient(_apiBaseUri))
            {
                var fullName = GetEntryName(entry.Name, entry.Region);

                var queryResponse = await client.kvGetAsync(fullName).ConfigureAwait(false);
                if (queryResponse.ResponseData == null)
                    return false;

                var response = await client.kvPutAsync(fullName, ToStringValue(entry.Value)).ConfigureAwait(false);
                return response.ResponseData;
            }            
        }
        #endregion
    }
}
