using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FCP.Configuration.Test
{
    public abstract class ConfigurationProviderTest
    {
        protected abstract IDistributedConfigurationProvider GetConfigurationProvider();

        #region sync test
        private void Add_Get_Update_Del_Test(string region = null)
        {
            var configProvider = GetConfigurationProvider();
            var key = Guid.NewGuid().ToString("N");            

            var result = configProvider.Update(key, "something2", region);
            Assert.False(result);

            result = configProvider.Add(key, "something", region);
            Assert.True(result);
            Assert.Equal("something", configProvider.Get<string>(key, region));

            result = configProvider.Add(key, "something", region);
            Assert.False(result);

            result = configProvider.Update(key, "something2", region);
            Assert.True(result);
            Assert.Equal("something2", configProvider.Get<string>(key, region));

            result = configProvider.Delete(key, region);
            Assert.True(result);
            Assert.Null(configProvider.Get<string>(key, region));
        }

        [Fact]
        public void Add_Get_Update_Del_Config()
        {
            Add_Get_Update_Del_Test();
        }

        [Fact]
        public void Add_Get_Update_Del_Config_Region()
        {
            Add_Get_Update_Del_Test("test");
        }

        private void AddOrUpdate_Del_Test(string region = null)
        {
            var configProvider = GetConfigurationProvider();
            var key = Guid.NewGuid().ToString("N");            

            var result = configProvider.AddOrUpdate(key, "something", region);
            Assert.True(result);
            Assert.Equal("something", configProvider.Get<string>(key, region));

            result = configProvider.AddOrUpdate(key, "something2", region);
            Assert.True(result);
            Assert.Equal("something2", configProvider.Get<string>(key, region));

            result = configProvider.Delete(key, region);
            Assert.True(result);
            Assert.Null(configProvider.Get<string>(key, region));
        }

        [Fact]
        public void AddOrUpdate_Del_Config()
        {
            AddOrUpdate_Del_Test();
        }

        [Fact]
        public void AddOrUpdate_Del_Config_Region()
        {
            AddOrUpdate_Del_Test("test");
        }

        #region Test GetNames
        private void GetNames_Test(string region = null)
        {
            var configProvider = GetConfigurationProvider();
            var keys = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                var key = Guid.NewGuid().ToString("N");
                var result = configProvider.Add(key, "something", region);
                Assert.True(result);
                keys.Add(key);
            }

            var configKeys = string.IsNullOrEmpty(region) ? configProvider.GetNames()
                : configProvider.GetRegionNames(region);
            foreach (var key in keys)
            {
                Assert.Contains(key, configKeys);
                Assert.True(configProvider.Delete(key, region));
            }
        }

        [Fact]
        public void GetNames_Config()
        {
            GetNames_Test();
        }

        [Fact]
        public void GetNames_Config_Region()
        {
            GetNames_Test("test");
        }
        #endregion

        #endregion

        #region async test
        private async Task Add_Get_Update_Del_Async_Test(string region = null)
        {
            var configProvider = GetConfigurationProvider();
            var key = Guid.NewGuid().ToString("N");

            var result = await configProvider.UpdateAsync(key, "something2", region).ConfigureAwait(false);
            Assert.False(result);

            result = await configProvider.AddAsync(key, "something", region).ConfigureAwait(false);
            Assert.True(result);
            Assert.Equal("something", await configProvider.GetAsync<string>(key, region).ConfigureAwait(false));

            result = await configProvider.AddAsync(key, "something", region).ConfigureAwait(false);
            Assert.False(result);

            result = await configProvider.UpdateAsync(key, "something2", region).ConfigureAwait(false);
            Assert.True(result);
            Assert.Equal("something2", await configProvider.GetAsync<string>(key, region).ConfigureAwait(false));

            result = await configProvider.DeleteAsync(key, region).ConfigureAwait(false);
            Assert.True(result);
            Assert.Null(await configProvider.GetAsync<string>(key, region).ConfigureAwait(false));
        }

        [Fact]
        public Task Add_Get_Update_Del_Config_Async()
        {
            return Add_Get_Update_Del_Async_Test();
        }

        [Fact]
        public Task Add_Get_Update_Del_Config_Region_Async()
        {
            return Add_Get_Update_Del_Async_Test("test");
        }

        private async Task AddOrUpdate_Del_Async_Test(string region = null)
        {
            var configProvider = GetConfigurationProvider();
            var key = Guid.NewGuid().ToString("N");

            var result = await configProvider.AddOrUpdateAsync(key, "something", region).ConfigureAwait(false);
            Assert.True(result);
            Assert.Equal("something", await configProvider.GetAsync<string>(key, region).ConfigureAwait(false));

            result = await configProvider.AddOrUpdateAsync(key, "something2", region).ConfigureAwait(false);
            Assert.True(result);
            Assert.Equal("something2", await configProvider.GetAsync<string>(key, region).ConfigureAwait(false));

            result = await configProvider.DeleteAsync(key, region).ConfigureAwait(false);
            Assert.True(result);
            Assert.Null(await configProvider.GetAsync<string>(key, region).ConfigureAwait(false));
        }

        [Fact]
        public Task AddOrUpdate_Del_Config_Async()
        {
            return AddOrUpdate_Del_Async_Test();
        }

        [Fact]
        public Task AddOrUpdate_Del_Config_Region_Async()
        {
            return AddOrUpdate_Del_Async_Test("test");
        }

        #region Test GetNames
        private async Task GetNames_Async_Test(string region = null)
        {
            var configProvider = GetConfigurationProvider();
            var keys = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                var key = Guid.NewGuid().ToString("N");
                var result = await configProvider.AddAsync(key, "something", region).ConfigureAwait(false);
                Assert.True(result);
                keys.Add(key);
            }

            var configKeys = string.IsNullOrEmpty(region) ? await configProvider.GetNamesAsync().ConfigureAwait(false)
                : await configProvider.GetRegionNamesAsync(region).ConfigureAwait(false);
            foreach (var key in keys)
            {
                Assert.Contains(key, configKeys);
                Assert.True(await configProvider.DeleteAsync(key, region).ConfigureAwait(false));
            }
        }

        [Fact]
        public Task GetNames_Config_Async()
        {
            return GetNames_Async_Test();
        }

        [Fact]
        public Task GetNames_Config_Region_Async()
        {
            return GetNames_Async_Test("test");
        }
        #endregion

        #endregion
    }
}
