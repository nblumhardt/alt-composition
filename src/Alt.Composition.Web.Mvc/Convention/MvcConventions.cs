using System.Composition.Convention;
using System.Web.Mvc;

namespace Alt.Composition.Convention
{
    /// <summary>
    /// Extension methods supporting ASP.NET MVC.
    /// </summary>
    public static class MvcConventions
    {
        /// <summary>
        /// Configures conventions supporting ASP.NET MVC controllers.
        /// </summary>
        /// <param name="conventions">Convention builder to configure.</param>
        public static void AddDefaultMvcConventions(this ConventionBuilder conventions)
        {
            conventions.ForTypesDerivedFrom<IController>().Export();
        }
    }
}
