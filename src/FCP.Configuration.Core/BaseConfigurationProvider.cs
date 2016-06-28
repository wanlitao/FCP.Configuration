using System;

namespace FCP.Configuration
{
    public abstract class BaseConfigurationProvider<TName> : IConfigurationProvider<TName>
    {
        #region Get
        public TValue Get<TValue>(TName name)
        {
            return Get<TValue>(name, null);
        }

        public virtual TValue Get<TValue>(TName name, string region)
        {
            var configEntry = GetConfigEntry<TValue>(name, region);

            if (configEntry != null && configEntry.Name.Equals(name))
            {
                return configEntry.Value;
            }
            return default(TValue);
        }

        public ConfigEntry<TName, TValue> GetConfigEntry<TValue>(TName name)
        {
            return GetConfigEntry<TValue>(name, null);
        }

        public virtual ConfigEntry<TName, TValue> GetConfigEntry<TValue>(TName name, string region)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            CheckDisposed();

            return GetConfigEntryInternal<TValue>(name, region);
        }

        protected abstract ConfigEntry<TName, TValue> GetConfigEntryInternal<TValue>(TName name, string region);
        #endregion

        #region Get Keys
        public virtual TName[] GetKeys()
        {
            CheckDisposed();

            return GetKeysInternal();
        }

        protected abstract TName[] GetKeysInternal();

        public virtual TName[] GetRegionKeys(string region)
        {
            if (string.IsNullOrEmpty(region))
                throw new ArgumentNullException(nameof(region));

            CheckDisposed();

            return GetRegionKeysInternal(region);
        }

        protected abstract TName[] GetRegionKeysInternal(string region);
        #endregion

        #region Add
        public bool Add<TValue>(TName name, TValue value)
        {
            return Add(name, value, null);
        }

        public virtual bool Add<TValue>(TName name, TValue value, string region)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var configEntry = new ConfigEntry<TName, TValue>(name, region, value);

            return Add(configEntry);
        }

        public virtual bool Add<TValue>(ConfigEntry<TName, TValue> entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            CheckDisposed();

            return AddInternal(entry);
        }

        protected abstract bool AddInternal<TValue>(ConfigEntry<TName, TValue> entry);
        #endregion

        #region Update
        public bool Update<TValue>(TName name, TValue value)
        {
            return Update(name, value, null);
        }

        public virtual bool Update<TValue>(TName name, TValue value, string region)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var configEntry = new ConfigEntry<TName, TValue>(name, region, value);

            return Update(configEntry);
        }

        public virtual bool Update<TValue>(ConfigEntry<TName, TValue> entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            CheckDisposed();

            return UpdateInternal(entry);
        }

        protected abstract bool UpdateInternal<TValue>(ConfigEntry<TName, TValue> entry);
        #endregion

        #region AddOrUpdate
        public bool AddOrUpdate<TValue>(TName name, TValue value)
        {
            return AddOrUpdate(name, value, null);
        }

        public virtual bool AddOrUpdate<TValue>(TName name, TValue value, string region)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var configEntry = new ConfigEntry<TName, TValue>(name, region, value);

            return AddOrUpdate(configEntry);
        }

        public virtual bool AddOrUpdate<TValue>(ConfigEntry<TName, TValue> entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            CheckDisposed();

            return AddOrUpdateInternal(entry);
        }

        protected abstract bool AddOrUpdateInternal<TValue>(ConfigEntry<TName, TValue> entry);
        #endregion

        #region Delete
        public bool Delete(TName name)
        {
            return Delete(name, null);
        }

        public virtual bool Delete(TName name, string region)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            CheckDisposed();

            return DeleteInternal(name, region);
        }

        protected abstract bool DeleteInternal(TName name, string region);
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected void CheckDisposed()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }                
                disposedValue = true;
            }
        }
        
        ~BaseConfigurationProvider() {
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
