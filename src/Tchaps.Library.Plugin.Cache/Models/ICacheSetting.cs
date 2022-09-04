namespace Tchaps.Library.Plugin.Cache.Models
{
    /// <summary>
    /// The cache setting interface
    /// </summary>
    public interface ICacheSetting
    {
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        short Duration { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ICacheSetting"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        bool Enabled { get; set; }
    }
}