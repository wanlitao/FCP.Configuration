﻿using System.Threading.Tasks;

namespace FCP.Configuration
{
    public interface IDistributedConfigurationProvider : IConfigurationProvider<string>
    {
        #region Get
        Task<TValue> GetAsync<TValue>(string name);

        /// <summary>
        /// Gets value for the specified name and region
        /// </summary>
        /// <param name="name"></param>
        /// <param name="region">>The config region</param>
        /// <returns></returns>
        Task<TValue> GetAsync<TValue>(string name, string region);

        Task<ConfigEntry<string, TValue>> GetConfigEntryAsync<TValue>(string name);

        Task<ConfigEntry<string, TValue>> GetConfigEntryAsync<TValue>(string name, string region);
        #endregion

        #region Get Names
        /// <summary>
        /// Get Names
        /// </summary>
        /// <returns></returns>
        Task<string[]> GetNamesAsync();

        /// <summary>
        /// Get Region Names
        /// </summary>
        /// <param name="region">The config region</param>
        /// <returns></returns>
        Task<string[]> GetRegionNamesAsync(string region);
        #endregion

        #region Add
        Task<bool> AddAsync<TValue>(string name, TValue value);

        /// <summary>
        /// Add config
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="region">The config region</param>
        /// <returns></returns>
        Task<bool> AddAsync<TValue>(string name, TValue value, string region);

        Task<bool> AddAsync<TValue>(ConfigEntry<string, TValue> entry);
        #endregion

        #region Update
        Task<bool> UpdateAsync<TValue>(string name, TValue value);

        /// <summary>
        /// Update config
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="region">The config region</param>
        /// <returns></returns>
        Task<bool> UpdateAsync<TValue>(string name, TValue value, string region);

        Task<bool> UpdateAsync<TValue>(ConfigEntry<string, TValue> entry);
        #endregion

        #region AddOrUpdate
        Task<bool> AddOrUpdateAsync<TValue>(string name, TValue value);

        /// <summary>
        /// Save config
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="region">The config region</param>
        /// <returns></returns>
        Task<bool> AddOrUpdateAsync<TValue>(string name, TValue value, string region);

        Task<bool> AddOrUpdateAsync<TValue>(ConfigEntry<string, TValue> entry);
        #endregion

        #region Delete
        Task<bool> DeleteAsync(string name);

        /// <summary>
        /// Delete config
        /// </summary>
        /// <param name="name"></param>
        /// <param name="region">The config region</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string name, string region);
        #endregion
    }
}
