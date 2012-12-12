﻿using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web;
using Alt.Composition.Convention;

namespace Alt.Composition.Hosting
{
    class CompositionProvider
    {
        static readonly IList<Assembly> _partAssemblies = new List<Assembly>();

        public static void CompleteInitialization()
        {
            var conventions = new ConventionBuilder();
            conventions.ForTypesUnderNamespace("Parts").Export().ExportInterfaces();
            conventions.AddDefaultMvcConventions();

            var partsAssemblies = _partAssemblies.Union(FindWebApplicationAssemblies());

            var container = new ContainerConfiguration()
                .WithDefaultConventions(conventions)
                .WithAssemblies(partsAssemblies)
                .WithApplicationSettings()
                .CreateContainer();

            MvcCompositionProvider.Initialize(container);
        }

        private static IEnumerable<Assembly> FindWebApplicationAssemblies()
        {
            if (HttpContext.Current == null)
            {
                System.Diagnostics.Debug.WriteLine("CompositionProvider: cannot find application assembly because HttpContext.Current is null.");
                yield break;
            }

// ReSharper disable PossibleNullReferenceException
            yield return HttpContext.Current.ApplicationInstance.GetType().BaseType.Assembly;
// ReSharper restore PossibleNullReferenceException
        }

        /// <summary>
        /// Add assemblies containing MEF parts.
        /// </summary>
        /// <param name="assemblies">Assemblies containing MEF parts.</param>
        public static void AddAssemblies(params Assembly[] assemblies)
        {
            AddAssemblies((IEnumerable<Assembly>)assemblies);
        }

        /// <summary>
        /// Add assemblies containing MEF parts.
        /// </summary>
        /// <param name="assemblies">Assemblies containing MEF parts.</param>
        public static void AddAssemblies(IEnumerable<Assembly> assemblies)
        {
            if (assemblies == null) throw new ArgumentException("assemblies");

            foreach (var assembly in assemblies)
                AddAssembly(assembly);
        }

        /// <summary>
        /// Add an assembly containing MEF parts.
        /// </summary>
        /// <param name="assembly">An assembly containing MEF parts.</param>
        public static void AddAssembly(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            _partAssemblies.Add(assembly);
        }
    }
}
