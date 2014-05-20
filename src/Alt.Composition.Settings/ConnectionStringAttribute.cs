using System;
using System.Composition;

namespace Alt.Composition
{
    /// <summary>
    /// Marks an export or import as corresponding to an application-level connection string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    [MetadataAttribute]
    public class ConnectionStringAttribute : Attribute
    {
        private readonly string _name;

        /// <summary>
        /// Construct a <see cref="ConnectionStringAttribute"/>.
        /// </summary>
        /// <param name="name">The name of the connection string in the application's configuration file.</param>
        /// <remarks>
        /// An import marked with:
        /// <code>
        ///     [ConnectionString("someName")]
        /// </code>
        /// will be provided with a value from App.config or Web.config's &lt;connectionStrings&gt; collection
        /// like:
        /// <code>
        ///     &lt;add name="someName" connectionString="..." providerName="..." /&gt;
        /// </code>
        /// as long as the type is either <see cref="System.String">string</see> or one of the provider-specific
        /// subclasses of <see cref="System.Data.Common.DbConnectionStringBuilder">DbConnectionStringBuilder</see>.
        /// </remarks>
        public ConnectionStringAttribute(string name)
        {
            _name = name;
        }

        /// <summary>
        /// The name of the connection string in App.config or Web.config.
        /// </summary>
        public string ConnectionStringName { get { return _name; } }
    }
}
