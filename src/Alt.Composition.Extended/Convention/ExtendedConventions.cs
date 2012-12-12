﻿using System;
using System.Composition.Convention;
using System.Linq;

namespace Alt.Composition.Convention
{
    /// <summary>
    /// Extends <see cref="ConventionBuilder"/> for common scenarios.
    /// </summary>
    public static class ExtendedConventions
    {
        /// <summary>
        /// Matches types where one step in the namespace path is
        /// equal to <paramref name="namespaceFragment"/>.
        /// </summary>
        /// <param name="conventions">The conventions to contribute to.</param>
        /// <param name="namespaceFragment">A simple namespace step, e.g. <code>"Parts"</code>.</param>
        /// <returns>A <see cref="PartConventionBuilder"/> over the matching types.</returns>
        public static PartConventionBuilder ForTypesUnderNamespace(
            this ConventionBuilder conventions,
            string namespaceFragment)
        {
            if (conventions == null) throw new ArgumentNullException("conventions");
            if (namespaceFragment == null) throw new ArgumentNullException("namespaceFragment");

            return conventions.ForTypesMatching(t =>
                t.Namespace != null &&
                t.Namespace.Split('.').Contains(namespaceFragment));
        }
    }
}