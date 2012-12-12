using System;
using System.Web;
using Alt.Composition.Hosting;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Composition;

namespace Alt.Composition.Internal
{
    /// <summary>
    /// Disposes the composition lifetime scope on completion of web requests. This module
    /// is automatically registered and should not be called directly.
    /// </summary>
    public class RequestContextDisposalModule : IHttpModule
    {
        static bool _isRegistered;

        /// <summary>
        /// For internal use.
        /// </summary>
        public static void Register()
        {
            if (_isRegistered) return;
            _isRegistered = true;
            DynamicModuleUtility.RegisterModule(typeof(RequestContextDisposalModule));
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        /// <param name="context">For internal use.</param>
        public void Init(HttpApplication context)
        {
            context.EndRequest += DisposeCompositionScope;
        }

        static void DisposeCompositionScope(object sender, EventArgs e)
        {
            Export<CompositionContext> scope;
            if (MvcCompositionProvider.TryGetCurrentRequestContext(out scope))
                scope.Dispose();
        }
    }
}
