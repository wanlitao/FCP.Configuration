using System;
using System.Threading.Tasks;

namespace FCP.Configuration
{
    internal class ConfigurationProviderDistributedDecorator : IDistributedConfigurationProvider
    {
        private readonly IConfigurationProvider<string> _localConfigurationProvider;

        internal ConfigurationProviderDistributedDecorator(IConfigurationProvider<string> configurationProvider)
        {
            if (configurationProvider == null)
                throw new ArgumentNullException(nameof(configurationProvider));

            _localConfigurationProvider = configurationProvider;
        }

        #region Get
        public TValue Get<TValue>(string name)
        {
            return _localConfigurationProvider.Get<TValue>(name);           
        }

        public TValue Get<TValue>(string name, string region)
        {
            return _localConfigurationProvider.Get<TValue>(name, region);            
        }

        public ConfigEntry<string, TValue> GetConfigEntry<TValue>(string name)
        {
            return _localConfigurationProvider.GetConfigEntry<TValue>(name);            
        }

        public ConfigEntry<string, TValue> GetConfigEntry<TValue>(string name, string region)
        {
            return _localConfigurationProvider.GetConfigEntry<TValue>(name, region);            
        }

        public Task<TValue> GetAsync<TValue>(string name)
        {
            return Task.FromResult(Get<TValue>(name));            
        }

        public Task<TValue> GetAsync<TValue>(string name, string region)
        {
            return Task.FromResult(Get<TValue>(name, region));
        }        

        public Task<ConfigEntry<string, TValue>> GetConfigEntryAsync<TValue>(string name)
        {
            return Task.FromResult(GetConfigEntry<TValue>(name));
        }

        public Task<ConfigEntry<string, TValue>> GetConfigEntryAsync<TValue>(string name, string region)
        {
            return Task.FromResult(GetConfigEntry<TValue>(name, region));
        }
        #endregion

        #region Get Names
        public string[] GetNames()
        {
            return _localConfigurationProvider.GetNames();
        }

        public string[] GetRegionNames(string region)
        {
            return _localConfigurationProvider.GetRegionNames(region);
        }

        public Task<string[]> GetNamesAsync()
        {
            return Task.FromResult(GetNames());
        }       

        public Task<string[]> GetRegionNamesAsync(string region)
        {
            return Task.FromResult(GetRegionNames(region));           
        }
        #endregion

        #region Add
        public bool Add<TValue>(string name, TValue value)
        {
            return _localConfigurationProvider.Add(name, value);
        }

        public bool Add<TValue>(string name, TValue value, string region)
        {
            return _localConfigurationProvider.Add(name, value, region);
        }

        public bool Add<TValue>(ConfigEntry<string, TValue> entry)
        {
            return _localConfigurationProvider.Add(entry);
        }        

        public Task<bool> AddAsync<TValue>(string name, TValue value)
        {
            return Task.FromResult(Add(name, value));
        }

        public Task<bool> AddAsync<TValue>(string name, TValue value, string region)
        {
            return Task.FromResult(Add(name, value, region));
        }

        public Task<bool> AddAsync<TValue>(ConfigEntry<string, TValue> entry)
        {
            return Task.FromResult(Add(entry));
        }
        #endregion

        #region Update
        public bool Update<TValue>(string name, TValue value)
        {
            return _localConfigurationProvider.Update(name, value);
        }

        public bool Update<TValue>(string name, TValue value, string region)
        {
            return _localConfigurationProvider.Update(name, value, region);
        }

        public bool Update<TValue>(ConfigEntry<string, TValue> entry)
        {
            return _localConfigurationProvider.Update(entry);
        }        

        public Task<bool> UpdateAsync<TValue>(string name, TValue value)
        {
            return Task.FromResult(Update(name, value));
        }

        public Task<bool> UpdateAsync<TValue>(string name, TValue value, string region)
        {
            return Task.FromResult(Update(name, value, region));
        }

        public Task<bool> UpdateAsync<TValue>(ConfigEntry<string, TValue> entry)
        {
            return Task.FromResult(Update(entry));
        }
        #endregion

        #region AddOrUpdate
        public bool AddOrUpdate<TValue>(string name, TValue value)
        {
            return _localConfigurationProvider.AddOrUpdate(name, value);
        }

        public bool AddOrUpdate<TValue>(string name, TValue value, string region)
        {
            return _localConfigurationProvider.AddOrUpdate(name, value, region);
        }

        public bool AddOrUpdate<TValue>(ConfigEntry<string, TValue> entry)
        {
            return _localConfigurationProvider.AddOrUpdate(entry);           
        }        

        public Task<bool> AddOrUpdateAsync<TValue>(string name, TValue value)
        {
            return Task.FromResult(AddOrUpdate(name, value));
        }

        public Task<bool> AddOrUpdateAsync<TValue>(string name, TValue value, string region)
        {
            return Task.FromResult(AddOrUpdate(name, value, region));
        }

        public Task<bool> AddOrUpdateAsync<TValue>(ConfigEntry<string, TValue> entry)
        {
            return Task.FromResult(AddOrUpdate(entry));
        }
        #endregion

        #region Delete
        public bool Delete(string name)
        {
            return _localConfigurationProvider.Delete(name);
        }

        public bool Delete(string name, string region)
        {
            return _localConfigurationProvider.Delete(name, region);
        }

        public Task<bool> DeleteAsync(string name)
        {
            return Task.FromResult(Delete(name));
        }

        public Task<bool> DeleteAsync(string name, string region)
        {
            return Task.FromResult(Delete(name, region));
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _localConfigurationProvider.Dispose();
                }
                disposedValue = true;
            }
        }

        ~ConfigurationProviderDistributedDecorator()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
