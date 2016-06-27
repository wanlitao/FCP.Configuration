using System.Threading.Tasks;

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
        #endregion

        #region Get Keys
        /// <summary>
        /// Get Keys
        /// </summary>
        /// <returns></returns>
        Task<string[]> GetKeysAsync();

        /// <summary>
        /// Get Region Keys
        /// </summary>
        /// <param name="region">The config region</param>
        /// <returns></returns>
        Task<string[]> GetRegionKeysAsync(string region);
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
