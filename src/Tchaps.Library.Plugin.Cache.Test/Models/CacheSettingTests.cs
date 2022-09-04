using Tchaps.Library.Plugin.Cache.Models;

namespace Tchaps.Library.Plugin.Cache.Test.Models
{

    public class CacheSettingTests
    {
        [Fact]
        public void GivenNoSetting_ShouldReturnDefaultSetting()
        {
            // Arrange
            ICacheSetting setting = new CacheSetting();

            // Assert
            Assert.True(setting.Enabled);
            Assert.Equal(30, setting.Duration);
        }
    }
}
