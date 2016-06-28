using System;
using System.Threading.Tasks;

namespace FCP.Configuration
{
    public abstract class BaseDistributedConfigurationProvider : BaseConfigurationProvider<string>, IDistributedConfigurationProvider
    {
        #region Get
        public Task<TValue> GetAsync<TValue>(string name)
        {
            return GetAsync<TValue>(name, null);
        }

        public virtual async Task<TValue> GetAsync<TValue>(string name, string region)
        {
            var configEntry = await GetConfigEntryAsync<TValue>(name, region).ConfigureAwait(false);

            if (configEntry != null && string.Compare(configEntry.Name, name, true) == 0)
            {
                return configEntry.Value;
            }
            return default(TValue);
        }

        public Task<ConfigEntry<string, TValue>> GetConfigEntryAsync<TValue>(string name)
        {
            return GetConfigEntryAsync<TValue>(name, null);
        }

        public virtual Task<ConfigEntry<string, TValue>> GetConfigEntryAsync<TValue>(string name, string region)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            CheckDisposed();

            return GetConfigEntryInternalAsync<TValue>(name, region);
        }

        protected abstract Task<ConfigEntry<string, TValue>> GetConfigEntryInternalAsync<TValue>(string name, string region);
        #endregion

        #region Get Keys
        public virtual Task<string[]> GetKeysAsync()
        {
            CheckDisposed();

            return GetKeysInternalAsync();
        }

        protected abstract Task<string[]> GetKeysInternalAsync();

        public virtual Task<string[]> GetRegionKeysAsync(string region)
        {
            if (string.IsNullOrEmpty(region))
                throw new ArgumentNullException(nameof(region));

            CheckDisposed();

            return GetRegionKeysInternalAsync(region);
        }

        protected abstract Task<string[]> GetRegionKeysInternalAsync(string region);
        #endregion

        #region Add
        public Task<bool> AddAsync<TValue>(string name, TValue value)
        {
            return AddAsync(name, value, null);
        }

        public virtual Task<bool> AddAsync<TValue>(string name, TValue value, string region)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var configEntry = new ConfigEntry<string, TValue>(name, region, value);

            return AddAsync(configEntry);
        }

        public virtual Task<bool> AddAsync<TValue>(ConfigEntry<string, TValue> entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            CheckDisposed();

            return AddInternalAsync(entry);
        }

        protected abstract Task<bool> AddInternalAsync<TValue>(ConfigEntry<string, TValue> entry);
        #endregion

        #region Update
        public Task<bool> UpdateAsync<TValue>(string name, TValue value)
        {
            return UpdateAsync(name, value, null);
        }

        public virtual Task<bool> UpdateAsync<TValue>(string name, TValue value, string region)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var configEntry = new ConfigEntry<string, TValue>(name, region, value);

            return UpdateAsync(configEntry);
        }

        public virtual Task<bool> UpdateAsync<TValue>(ConfigEntry<string, TValue> entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            CheckDisposed();

            return UpdateInternalAsync(entry);
        }

        protected abstract Task<bool> UpdateInternalAsync<TValue>(ConfigEntry<string, TValue> entry);
        #endregion

        #region AddOrUpdate
        public Task<bool> AddOrUpdateAsync<TValue>(string name, TValue value)
        {
            return AddOrUpdateAsync(name, value, null);
        }

        public virtual Task<bool> AddOrUpdateAsync<TValue>(string name, TValue value, string region)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var configEntry = new ConfigEntry<string, TValue>(name, region, value);

            return AddOrUpdateAsync(configEntry);
        }

        public virtual Task<bool> AddOrUpdateAsync<TValue>(ConfigEntry<string, TValue> entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            CheckDisposed();

            return AddOrUpdateInternalAsync(entry);
        }

        protected abstract Task<bool> AddOrUpdateInternalAsync<TValue>(ConfigEntry<string, TValue> entry);
        #endregion

        #region Delete
        public Task<bool> DeleteAsync(string name)
        {
            return DeleteAsync(name, null);
        }

        public virtual Task<bool> DeleteAsync(string name, string region)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            CheckDisposed();

            return DeleteInternalAsync(name, region);
        }

        protected abstract Task<bool> DeleteInternalAsync(string name, string region);
        #endregion
    }
}
