using System.Web;
using Alt.Composition.Hosting;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace Alt.Composition.Internal
{
    /// <summary>
    /// Initializes the <see cref="CompositionProvider"/> automatically if not
    /// done so already after startup. This module is registered automatically and
    /// should not be used directly.
    /// </summary>
    public class DefaultInitializationModule : IHttpModule
    {
        static bool _isRegistered;

        /// <summary>
        /// For internal use.
        /// </summary>
        public static void Register()
        {
            if (_isRegistered) return;
            _isRegistered = true;
            DynamicModuleUtility.RegisterModule(typeof(DefaultInitializationModule));
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        /// <param name="context">For internal use.</param>
        public void Init(HttpApplication context)
        {
            CompositionProvider.CompleteInitialization();
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
