using System;

namespace FCP.Configuration
{
    /// <summary>
    /// base interface for configuration provider
    /// </summary>
    public interface IConfigurationProvider<TName> : IDisposable
    {
        #region Get        
        TValue Get<TValue>(TName name);

        /// <summary>
        /// Gets value for the specified name and region
        /// </summary>
        /// <param name="name"></param>
        /// <param name="region">>The config region</param>
        /// <returns></returns>
        TValue Get<TValue>(TName name, string region);

        ConfigEntry<TName, TValue> GetConfigEntry<TValue>(TName name);

        ConfigEntry<TName, TValue> GetConfigEntry<TValue>(TName name, string region);
        #endregion

        #region Get Names
        /// <summary>
        /// Get Names
        /// </summary>
        /// <returns></returns>
        TName[] GetNames();

        /// <summary>
        /// Get Region Names
        /// </summary>
        /// <param name="region">The config region</param>
        /// <returns></returns>
        TName[] GetRegionNames(string region);
        #endregion

        #region Add
        bool Add<TValue>(TName name, TValue value);

        /// <summary>
        /// Add config
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="region">The config region</param>
        /// <returns></returns>
        bool Add<TValue>(TName name, TValue value, string region);

        bool Add<TValue>(ConfigEntry<TName, TValue> entry);
        #endregion

        #region Update
        bool Update<TValue>(TName name, TValue value);

        /// <summary>
        /// Update config
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="region">The config region</param>
        /// <returns></returns>
        bool Update<TValue>(TName name, TValue value, string region);

        bool Update<TValue>(ConfigEntry<TName, TValue> entry);
        #endregion

        #region AddOrUpdate
        bool AddOrUpdate<TValue>(TName name, TValue value);

        /// <summary>
        /// Save config
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="region">The config region</param>
        /// <returns></returns>
        bool AddOrUpdate<TValue>(TName name, TValue value, string region);

        bool AddOrUpdate<TValue>(ConfigEntry<TName, TValue> entry);
        #endregion

        #region Delete
        bool Delete(TName name);

        /// <summary>
        /// Delete config
        /// </summary>
        /// <param name="name"></param>
        /// <param name="region">The config region</param>
        /// <returns></returns>
        bool Delete(TName name, string region);
        #endregion
    }
}
