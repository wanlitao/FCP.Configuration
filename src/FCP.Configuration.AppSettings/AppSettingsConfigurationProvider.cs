using FCP.Util;
using System;
using System.Configuration;
using System.Linq;

namespace FCP.Configuration.AppSettings
{
    public class AppSettingsConfigurationProvider : BaseConfigurationProvider<string>
    {
        public AppSettingsConfigurationProvider(ISerializer serializer)
            : base(serializer)
        { }

        #region Name
        protected string GetEntryName(string name, string region)
        {
            if (name.isNullOrEmpty())
                throw new ArgumentNullException(nameof(name));

            if (region.isNullOrEmpty())
                return name;

            return $"{GetRegionPrefix(region)}{name}";
        }

        protected string GetRegionPrefix(string region)
        {
            if (region.isNullOrEmpty())
                throw new ArgumentNullException(nameof(region));

            return $"{region}::";
        }
        #endregion

        #region Get
        protected override ConfigEntry<string, TValue> GetConfigEntryInternal<TValue>(string name, string region)
        {
            var fullName = GetEntryName(name, region);
            var valueStr = ConfigurationManager.AppSettings[fullName];

            if (valueStr.isNullOrEmpty())
                return null;

            var configEntry = new ConfigEntry<string, TValue>(name, region, FromStringValue<TValue>(valueStr));
            return configEntry;
        }
        #endregion

        #region Get Names
        protected override string[] GetNamesInternal()
        {
            return GetRegionNamesInternal(string.Empty);
        }

        protected override string[] GetRegionNamesInternal(string region)
        {
            var allKeys = ConfigurationManager.AppSettings.AllKeys;

            if (region.isNullOrEmpty())
                return allKeys;

            var regionPrefix = GetRegionPrefix(region);
            return allKeys.Where(m => StringUtil.startsWith(m, regionPrefix)).ToArray();           
        }
        #endregion

        #region Add
        protected override bool AddInternal<TValue>(ConfigEntry<string, TValue> entry)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update
        protected override bool UpdateInternal<TValue>(ConfigEntry<string, TValue> entry)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region AddOrUpdate
        protected override bool AddOrUpdateInternal<TValue>(ConfigEntry<string, TValue> entry)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Delete
        protected override bool DeleteInternal(string name, string region)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
