namespace Tchaps.Library.Plugin.Cache.Models
{
    public class CacheSetting : ICacheSetting
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CacheSetting"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled { get; set; } = true;
        /// <summary>
        /// Gets or sets the duration. The default value is 30s
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public short Duration { get; set; } = 30;
    }
}
