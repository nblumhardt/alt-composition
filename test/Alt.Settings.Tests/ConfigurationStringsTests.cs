using Alt.Composition;
using Alt.Composition.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Composition;
using System.Composition.Hosting;
using System.Data.SqlClient;

namespace Alt.Settings.Tests
{
    [TestClass, DeploymentItem("Alt.Settings.Tests.dll.config")]
    public class ConfigurationStringsTests
    {
        [Export]
        public class StringImporter
        {
            [Import, ConnectionString("First")]
            public string AConnectionString { get; set; }
        }

        [TestMethod]
        public void AConnectionStringCanBeReadAsString()
        {
            var instance = ComposeWithConnectionStrings<StringImporter>();
            Assert.AreEqual(@"Server=.\SQLEXPRESS;Database=First;Integrated Security=SSPI", instance.AConnectionString);
        }

        [Export]
        public class DbConnectionStringBuilderImporter
        {
            [Import, ConnectionString("Second")]
            public SqlConnectionStringBuilder AConnectionStringBuilder { get; set; }
        }

        [TestMethod]
        public void AConnectionStringCanBeReadAsSqlConnectionStringBuilder()
        {
            var instance = ComposeWithConnectionStrings<DbConnectionStringBuilderImporter>();
            Assert.IsNotNull(instance);
            Assert.AreEqual(@"Data Source=.\SQLEXPRESS;Initial Catalog=Second;Integrated Security=True", instance.AConnectionStringBuilder.ToString());
        }

        private TPart ComposeWithConnectionStrings<TPart>()
        {
            return new ContainerConfiguration()
                .WithPart<TPart>()
                .WithConnectionStrings()
                .CreateContainer()
                .GetExport<TPart>();
        }
    }
}
