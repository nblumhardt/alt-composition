﻿using System;
using System.Composition.Hosting;

namespace Alt.Composition.Hosting
{
    /// <summary>
    /// Extension methods provided by this library.
    /// </summary>
    public static class SettingsExtensions
    {
        /// <summary>
        /// Enables use of the [Setting("key")] syntax on imports to retrieve values from *.config.
        /// </summary>
        /// <param name="configuration">The container configuration.</param>
        /// <returns>Container configuration allowing method chaining.</returns>
        public static ContainerConfiguration WithApplicationSettings(this ContainerConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            return configuration.WithProvider(new SettingsExportDescriptorProvider());
        }

        /// <summary>
        /// Enables the use of the [ConnectionString("name")] syntax on imports to retrieve values from *.config.
        /// </summary>
        /// <param name="configuration">The container configuration.</param>
        /// <returns>Container configuration allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">The given <c>ContainerConfiguration</c> instance was null.</exception>
        public static ContainerConfiguration WithConnectionStrings(this ContainerConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            return configuration.WithProvider(new ConnectionStringsExportDescriptorProvider());
        }
    }
}
