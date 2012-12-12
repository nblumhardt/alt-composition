using System;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;

namespace Alt.Composition.Hosting
{
    /// <summary>
    /// Adds functionality to the composition hosting API.
    /// </summary>
    public static class ExtendedHosting
    {
        internal static readonly CompositionContract EagerConstructionContract = new CompositionContract(typeof(object), "EagerlyConstructed");

        /// <summary>
        /// Create instances of all parts marked with the <see cref="EagerlyConstructedAttribute"/>.
        /// </summary>
        /// <param name="compositionHost">Container with parts.</param>
        public static void ConstructEagerParts(this CompositionHost compositionHost)
        {
            if (compositionHost == null) throw new ArgumentNullException("compositionHost");

            compositionHost.GetExports(EagerConstructionContract.ContractType, EagerConstructionContract.ContractName);
        }
    }
}
