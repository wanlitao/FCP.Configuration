using System;
using System.Threading.Tasks;
using Xunit;

namespace FCP.Configuration.Test
{
    public abstract class ConfigurationProviderTest
    {
        protected abstract IDistributedConfigurationProvider GetConfigurationProvider();

        #region sync test
        [Fact]
        public void Add_Get_Del_Config()
        {
            var configProvider = GetConfigurationProvider();
            var key = Guid.NewGuid().ToString("N");

            var result = configProvider.Add(key, "test");
            Assert.True(result);

            Assert.Equal("test", configProvider.Get<string>(key));

            result = configProvider.Delete(key);
            Assert.True(result);

            Assert.Null(configProvider.Get<string>(key));
        }
        #endregion

        #region async test
        [Fact]
        public async Task Add_Get_Del_Config_Async()
        {
            var configProvider = GetConfigurationProvider();
            var key = Guid.NewGuid().ToString("N");

            var result = await configProvider.AddAsync(key, "test").ConfigureAwait(false);
            Assert.True(result);

            Assert.Equal("test", await configProvider.GetAsync<string>(key).ConfigureAwait(false));

            result = await configProvider.DeleteAsync(key).ConfigureAwait(false);
            Assert.True(result);

            Assert.Null(await configProvider.GetAsync<string>(key).ConfigureAwait(false));
        }
        #endregion
    }
}
