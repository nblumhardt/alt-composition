using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Alt.Composition.Hosting
{
    /// <summary>
    /// Retrieves filters from the global composition provider.
    /// </summary>
    public class CompositionFilterProvider : IFilterProvider
    {
        /// <summary>
        /// Replace the default filter attribute filter provider with an instance
        /// of this type.
        /// </summary>
        /// <param name="filterProviders">The global filter provider collection.</param>
        public static void Install(FilterProviderCollection filterProviders)
        {
            if (filterProviders == null) throw new ArgumentNullException("filterProviders");
            filterProviders.Add(new CompositionFilterProvider());
        }

        /// <summary>
        /// Retrieve action, authorization, exception and result filters from the composition provider.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param><param name="actionDescriptor">The action descriptor.</param>
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            foreach (var filterType in new[] {
                typeof(IActionFilter),
                typeof(IAuthorizationFilter),
                typeof(IExceptionFilter),
                typeof(IResultFilter) })
            {
                foreach (var export in MvcCompositionProvider.CurrentRequestContext.GetExports(filterType))
                {
                    yield return new Filter(export, FilterScope.Action, null);
                }
            }
        }
    }
}
