using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Alt.Composition.Hosting;

namespace Alt.Composition.Internal
{
    class ComposedDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            object export;
            if (!MvcCompositionProvider.CurrentRequestContext.TryGetExport(serviceType, null, out export))
                return null;

            return export;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return MvcCompositionProvider.CurrentRequestContext.GetExports(serviceType);
        }
    }
}
