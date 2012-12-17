using System.Composition;
using System.Composition.Hosting;
using Alt.Composition;
using Alt.Composition.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alt.Settings.Tests
{
    [TestClass, DeploymentItem("Alt.Settings.Tests.dll.config") ]
    public class ApplicationSettingsTests
    {
        [Export]
        public class StringImporter
        {
            [Import, Setting("aString")]
            public string AString { get; set; }
        }

        [TestMethod]
        public void ASettingCanBeReadAsString()
        {
            var si = ComposeWithAppSettings<StringImporter>();
            Assert.AreEqual("Hello, World!", si.AString);
        }

        [Export]
        public class BooleanImporter
        {
            [Import, Setting("aBoolean")]
            public bool ABoolean { get; set; }
        }

        [TestMethod]
        public void ASettingCanBeReadAsBoolean()
        {
            var bi = ComposeWithAppSettings<BooleanImporter>();
            Assert.IsTrue(bi.ABoolean);
        }
        
        private TPart ComposeWithAppSettings<TPart>()
        {
            return new ContainerConfiguration()
                .WithPart<TPart>()
                .WithApplicationSettings()
                .CreateContainer()
                .GetExport<TPart>();
        }
    }
}
