using System;
using System.Composition;

namespace Alt.Composition
{
    /// <summary>
    /// Marks an export or import as corresponding to an application-level
    /// setting.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    [MetadataAttribute]
    public class SettingAttribute : Attribute
    {
        readonly string _key;

        /// <summary>
        /// Construct a <see cref="SettingAttribute"/>.
        /// </summary>
        /// <param name="key">The key of the setting in the application's configuration file.</param>
        /// <remarks>
        /// An import marked with:
        /// <code>
        ///     [Setting("someSetting")]
        /// </code>
        /// will be provided with a value from App.config or Web.config's &lt;appSettings&gt; collection
        /// like:
        /// <code>
        ///     &lt;add key="someSetting" value="..." /&gt;
        /// </code>
        /// </remarks>
        public SettingAttribute(string key)
        {
            _key = key;
        }

        /// <summary>
        /// The key of the setting in App.config or Web.config.
        /// </summary>
        public string SettingKey { get { return _key; } }
    }
}
