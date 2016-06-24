using System;
using System.Threading.Tasks;
using Xunit;

namespace FCP.Configuration.Test
{
    public abstract class ConfigurationProviderTest
    {
        protected abstract IConfigurationProvider GetConfigurationProvider();

        #region sync test
        [Fact]
        public void Add_Get_Del_Config()
        {
            var configProvider = GetConfigurationProvider();
            var key = Guid.NewGuid().ToString("N");

            var result = configProvider.AddConfiguration(key, "test");
            Assert.True(result);

            Assert.Equal("test", configProvider.GetConfiguration(key));

            result = configProvider.DeleteConfiguration(key);
            Assert.True(result);

            Assert.Null(configProvider.GetConfiguration(key));
        }
        #endregion

        #region async test
        [Fact]
        public async Task Add_Get_Del_Config_Async()
        {
            var configProvider = GetConfigurationProvider();
            var key = Guid.NewGuid().ToString("N");

            var result = await configProvider.AddConfigurationAsync(key, "test").ConfigureAwait(false);
            Assert.True(result);

            Assert.Equal("test", await configProvider.GetConfigurationAsync(key).ConfigureAwait(false));

            result = await configProvider.DeleteConfigurationAsync(key).ConfigureAwait(false);
            Assert.True(result);

            Assert.Null(await configProvider.GetConfigurationAsync(key).ConfigureAwait(false));
        }
        #endregion
    }
}
