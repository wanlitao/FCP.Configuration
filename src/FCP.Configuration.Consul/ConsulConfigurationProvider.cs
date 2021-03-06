﻿using System;
using System.Threading.Tasks;
using Consul.RsetApi.Client;
using FCP.Util;
using System.Linq;
using FCP.Util.Async;

namespace FCP.Configuration.Consul
{
    public class ConsulConfigurationProvider : BaseDistributedConfigurationProvider
    {
        private readonly Uri _apiBaseUri;
        private readonly IConsulRsetApiClient _client;        

        #region 构造函数
        public ConsulConfigurationProvider()
            : this(ConsulConstants.DefaultApiBaseUri)
        { }

        public ConsulConfigurationProvider(Uri apiBaseUri)
            : this(apiBaseUri, SerializerFactory.JsonSerializer)
        { }

        public ConsulConfigurationProvider(Uri apiBaseUri, ISerializer serializer)
            : base(serializer)
        {
            if (apiBaseUri == null)
                throw new ArgumentNullException(nameof(apiBaseUri));

            _apiBaseUri = apiBaseUri;
            _client = new ConsulRsetApiClient(apiBaseUri);
        }
        #endregion

        protected IConsulRsetApiClient client { get { return _client; } }

        #region Name
        protected string GetEntryName(string name, string region)
        {
            if (name.isNullOrEmpty())
                throw new ArgumentNullException(nameof(name));

            if (region.isNullOrEmpty())
                return name;

            return string.Format("{0}/{1}", region, name);
        }
        #endregion

        #region Get
        protected override ConfigEntry<string, TValue> GetConfigEntryInternal<TValue>(string name, string region)
        {
            return AsyncFuncHelper.RunSync(() => GetConfigEntryInternalAsync<TValue>(name, region));
        }

        protected override async Task<ConfigEntry<string, TValue>> GetConfigEntryInternalAsync<TValue>(string name, string region)
        {            
            var fullName = GetEntryName(name, region);
            var response = await client.kvGetRawAsync(fullName).ConfigureAwait(false);

            if (response.ResponseData.isNullOrEmpty())
                return null;

            var configEntry = new ConfigEntry<string, TValue>(name, region, FromStringValue<TValue>(response.ResponseData));
            return configEntry;           
        }
        #endregion

        #region Get Names
        protected override string[] GetNamesInternal()
        {
            return AsyncFuncHelper.RunSync(() => GetNamesInternalAsync());
        }

        protected override Task<string[]> GetNamesInternalAsync()
        {
            return GetRegionNamesInternalAsync(string.Empty);
        }

        protected override string[] GetRegionNamesInternal(string region)
        {
            return AsyncFuncHelper.RunSync(() => GetRegionNamesInternalAsync(region));
        }

        protected override async Task<string[]> GetRegionNamesInternalAsync(string region)
        {
            var response = await client.kvGetKeysAsync(region).ConfigureAwait(false);
            var keys = response.ResponseData;

            //substring the region prefix
            if (!region.isNullOrEmpty() && keys.isNotEmpty())
            {
                var startIndex = region.Length + 1;
                keys = keys.Select(m => m.Substring(startIndex)).ToArray();
            }
            return keys;            
        }
        #endregion

        #region Add
        protected override bool AddInternal<TValue>(ConfigEntry<string, TValue> entry)
        {
            return AsyncFuncHelper.RunSync(() => AddInternalAsync(entry));
        }

        protected override async Task<bool> AddInternalAsync<TValue>(ConfigEntry<string, TValue> entry)
        {
            var fullName = GetEntryName(entry.Name, entry.Region);

            var response = await client.kvPutAsync(fullName, ToStringValue(entry.Value), true).ConfigureAwait(false);
            return response.ResponseData;            
        }
        #endregion

        #region Update
        protected override bool UpdateInternal<TValue>(ConfigEntry<string, TValue> entry)
        {
            return AsyncFuncHelper.RunSync(() => UpdateInternalAsync(entry));
        }

        protected override async Task<bool> UpdateInternalAsync<TValue>(ConfigEntry<string, TValue> entry)
        {            
            var fullName = GetEntryName(entry.Name, entry.Region);

            var queryResponse = await client.kvGetAsync(fullName).ConfigureAwait(false);
            if (queryResponse.ResponseData == null)
                return false;

            var response = await client.kvPutAsync(fullName, ToStringValue(entry.Value)).ConfigureAwait(false);
            return response.ResponseData;
        }
        #endregion

        #region AddOrUpdate
        protected override bool AddOrUpdateInternal<TValue>(ConfigEntry<string, TValue> entry)
        {
            return AsyncFuncHelper.RunSync(() => AddOrUpdateInternalAsync(entry));
        }

        protected override async Task<bool> AddOrUpdateInternalAsync<TValue>(ConfigEntry<string, TValue> entry)
        {            
            var fullName = GetEntryName(entry.Name, entry.Region);

            var response = await client.kvPutAsync(fullName, ToStringValue(entry.Value)).ConfigureAwait(false);
            return response.ResponseData;            
        }
        #endregion

        #region Delete
        protected override bool DeleteInternal(string name, string region)
        {
            return AsyncFuncHelper.RunSync(() => DeleteInternalAsync(name, region));
        }

        protected override async Task<bool> DeleteInternalAsync(string name, string region)
        {            
            var fullName = GetEntryName(name, region);

            var response = await client.kvDeleteAsync(fullName).ConfigureAwait(false);
            return response.ResponseData;            
        }
        #endregion

        #region IDisposable Support
        protected override void DisposeInternal()
        {
            _client.Dispose();
            base.DisposeInternal();
        }
        #endregion
    }
}
