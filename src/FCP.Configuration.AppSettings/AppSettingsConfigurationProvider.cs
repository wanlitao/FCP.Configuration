using FCP.Util;
using System;
using System.Configuration;
using System.Linq;
using System.Diagnostics;

namespace FCP.Configuration.AppSettings
{
    public class AppSettingsConfigurationProvider : BaseConfigurationProvider<string>
    {
        #region 构造函数
        public AppSettingsConfigurationProvider()
            : this(SerializerFactory.JsonSerializer)
        { }

        public AppSettingsConfigurationProvider(ISerializer serializer)
            : base(serializer)
        { }
        #endregion

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

        protected bool CheckNameExists(string name)
        {
            if (name.isNullOrEmpty())
                throw new ArgumentNullException(nameof(name));

            return ConfigurationManager.AppSettings.AllKeys.Contains(name);
        }
        #endregion

        #region Modify
        protected bool ModifyAppSettingsConfig(Action<AppSettingsSection> modifyAction)
        {
            if (modifyAction == null)
                throw new ArgumentNullException(nameof(modifyAction));

            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                modifyAction(config.AppSettings);

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);

                return true;
            }
            catch(Exception ex)
            {
                Trace.TraceError("modify appsettings error: {0}", ex);
                return false;
            }
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
            var startIndex = regionPrefix.Length;
            return allKeys.Where(m => StringUtil.startsWith(m, regionPrefix))
                .Select(m => m.Substring(startIndex)).ToArray();
        }
        #endregion

        #region Add
        protected override bool AddInternal<TValue>(ConfigEntry<string, TValue> entry)
        {
            var fullName = GetEntryName(entry.Name, entry.Region);

            if (CheckNameExists(fullName))
                return false;

            return ModifyAppSettingsConfig((appSettings) => appSettings.Settings.Add(fullName, ToStringValue(entry.Value)));
        }
        #endregion

        #region Update
        protected override bool UpdateInternal<TValue>(ConfigEntry<string, TValue> entry)
        {
            var fullName = GetEntryName(entry.Name, entry.Region);

            if (!CheckNameExists(fullName))
                return false;

            return ModifyAppSettingsConfig((appSettings) =>
            {
                appSettings.Settings[fullName].Value = ToStringValue(entry.Value);
            });
        }
        #endregion

        #region AddOrUpdate
        protected override bool AddOrUpdateInternal<TValue>(ConfigEntry<string, TValue> entry)
        {
            var fullName = GetEntryName(entry.Name, entry.Region);

            return ModifyAppSettingsConfig((appSettings) =>
            {
                if (CheckNameExists(fullName))
                {
                    appSettings.Settings[fullName].Value = ToStringValue(entry.Value);
                }
                else
                {
                    appSettings.Settings.Add(fullName, ToStringValue(entry.Value));
                }
            });
        }
        #endregion

        #region Delete
        protected override bool DeleteInternal(string name, string region)
        {
            var fullName = GetEntryName(name, region);

            if (!CheckNameExists(fullName))
                return false;

            return ModifyAppSettingsConfig((appSettings) => appSettings.Settings.Remove(fullName));
        }
        #endregion
    }
}
