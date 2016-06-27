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

        public virtual Task<TValue> GetAsync<TValue>(string name, string region)
        {
            throw new NotImplementedException();
        }
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
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        public Task<bool> UpdateAsync<TValue>(string name, TValue value)
        {
            return UpdateAsync(name, value, null);
        }

        public virtual Task<bool> UpdateAsync<TValue>(string name, TValue value, string region)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region AddOrUpdate
        public Task<bool> AddOrUpdateAsync<TValue>(string name, TValue value)
        {
            return AddOrUpdateAsync(name, value, null);
        }

        public virtual Task<bool> AddOrUpdateAsync<TValue>(string name, TValue value, string region)
        {
            throw new NotImplementedException();
        }
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
