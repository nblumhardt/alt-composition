using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Web.Mvc;

namespace Alt.Composition.Hosting
{
    /// <summary>
    /// Extends <see cref="FilterAttributeFilterProvider"/> with the ability to import
    /// composition contracts into attribute properties. Important: an instance of
    /// this class must replace the built in provider. This is achived using the
    /// Install() method.
    /// </summary>
    public class ImportCapableFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        /// <summary>
        /// Replace the default filter attribute filter provider with an instance
        /// of this type.
        /// </summary>
        /// <param name="filterProviders">The global filter provider collection.</param>
        public static void Install(FilterProviderCollection filterProviders)
        {
            if (filterProviders == null) throw new ArgumentNullException("filterProviders");

            var existing = filterProviders.OfType<FilterAttributeFilterProvider>().SingleOrDefault();
            if (existing != null)
                filterProviders.Remove(existing);
            filterProviders.Add(new ImportCapableFilterAttributeFilterProvider());
        }

        /// <summary>
        /// Construct a <see cref="ImportCapableFilterAttributeFilterProvider"/>.
        /// </summary>
        public ImportCapableFilterAttributeFilterProvider()
            : base(cacheAttributeInstances: false)
        {
        }

        /// <summary>
        /// Retrieve the filter attributes, satisfying property imports where required.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>Filters.</returns>
        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            foreach (var attr in base.GetFilters(controllerContext, actionDescriptor))
            {
                MvcCompositionProvider.CurrentRequestContext.SatisfyImports(attr);
                yield return attr;
            }
        }
    }
}
