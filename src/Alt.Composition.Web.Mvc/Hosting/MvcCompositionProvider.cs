using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting.Core;
using System.Web;
using System.Web.Mvc;
using Alt.Composition.Internal;

namespace Alt.Composition.Hosting
{
    /// <summary>
    /// Provides composition for <see cref="IController"/> instances and other key MVC types.
    /// </summary>
    public class MvcCompositionProvider
    {
        static CompositionContext _applicationContext;
        static ExportFactory<CompositionContext> _requestContextFactory; 
        static readonly object _initLock = new object();
        
        /// <summary>
        /// Initialize the composition provider.
        /// </summary>
        /// <param name="applicationContext">A container supplying composed parts.</param>
        public static void Initialize(CompositionContext applicationContext)
        {
            if (applicationContext == null) throw new ArgumentNullException("applicationContext");

            lock (_initLock)
            {
                if (_applicationContext != null) throw new InvalidOperationException("Composition provider is already initialized.");
                _applicationContext = applicationContext;
            }

            var rcfContract = new CompositionContract(
                typeof(ExportFactory<CompositionContext>),
                null,
                new Dictionary<string, object> {
                    { "SharingBoundaryNames", new[] { Boundaries.HttpContext, Boundaries.DataConsistency, Boundaries.UserIdentity }}
            });

            _requestContextFactory = (ExportFactory<CompositionContext>)_applicationContext.GetExport(rcfContract);

            DependencyResolver.SetResolver(new ComposedDependencyResolver());
        }

        internal static bool TryGetCurrentRequestContext(out Export<CompositionContext> context)
        {
            context = (Export<CompositionContext>)HttpContext.Current.Items[typeof(MvcCompositionProvider)];
            return context != null;
        }

        static void SetCurrentRequestContext(Export<CompositionContext> context)
        {
            if (context == null) throw new ArgumentNullException("context");
            HttpContext.Current.Items[typeof(MvcCompositionProvider)] = context;
        }

        /// <summary>
        /// The composition context corresponding with the current HTTP request.
        /// </summary>
        public static CompositionContext CurrentRequestContext
        {
            get
            {
                Export<CompositionContext> context;
                if (!TryGetCurrentRequestContext(out context))
                {
                    context = _requestContextFactory.CreateExport();
                    SetCurrentRequestContext(context);
                }
                return context.Value;
            }
        }

        /// <summary>
        /// The application-level composition context.
        /// </summary>
        public static CompositionContext ApplicationContext
        {
            get { return _applicationContext; }
        }
    }
}
