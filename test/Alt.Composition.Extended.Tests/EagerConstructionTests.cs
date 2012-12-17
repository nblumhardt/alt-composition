using System.Composition;
using System.Composition.Convention;
using System.Composition.Hosting;
using Alt.Composition.Convention;
using Alt.Composition.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alt.Composition.Extended.Tests
{
    [Shared]
    public class ConstructionFlag
    {
        public bool WasConstructed { get; set; }
    }

    [EagerlyConstructed]
    public class EagerPart
    {
        public EagerPart(ConstructionFlag flag)
        {
            flag.WasConstructed = true;
        }
    }

    [TestClass]
    public class EagerConstructionTests
    {
        [TestMethod]
        public void TypesMarkedWithEagerlyConstructedAttributeAreConstructedEagerly()
        {
            var conventions = new ConventionBuilder()
                .WithEagerConstructionSupport();

            conventions.ForTypesMatching(t => true).Export();

            var configuration = new ContainerConfiguration()
                .WithDefaultConventions(conventions)
                .WithPart<ConstructionFlag>()
                .WithPart<EagerPart>();

            var container = configuration.CreateContainer();

            var flag = container.GetExport<ConstructionFlag>();
            Assert.IsFalse(flag.WasConstructed);

            container.ConstructEagerParts();

            Assert.IsTrue(flag.WasConstructed);

            EagerPart stillExported;
            Assert.IsTrue(container.TryGetExport(out stillExported));
        }
    }
}
