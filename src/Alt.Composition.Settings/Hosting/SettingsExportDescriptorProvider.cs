using System;
using System.Collections.Generic;
using System.Composition.Hosting.Core;
using System.Configuration;
using System.Linq;

namespace Alt.Composition.Hosting
{
    class SettingsExportDescriptorProvider : ExportDescriptorProvider
    {
        /// <summary>
        /// Since <see cref="Convert"/> does not provide a 'Try' variant,
        /// we pre-emptively constrain the settings we support to the set that
        /// it can handle.
        /// </summary>
        static readonly HashSet<Type> SupportedSettingTypes = new HashSet<Type>(new[] {
            typeof(string),
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(Boolean), 
            typeof(DateTime), 
            typeof(TimeSpan)
        });

        const string SettingKey = "SettingKey";

        public override IEnumerable<ExportDescriptorPromise> GetExportDescriptors(
            CompositionContract contract,
            DependencyAccessor dependencyAccessor)
        {
            if (contract == null) throw new ArgumentNullException("contract");
            if (dependencyAccessor == null) throw new ArgumentNullException("dependencyAccessor");

            string key;
            CompositionContract unwrapped;

            if (!contract.TryUnwrapMetadataConstraint(SettingKey, out key, out unwrapped) ||
                !unwrapped.Equals(new CompositionContract(unwrapped.ContractType)) ||
                !SupportedSettingTypes.Contains(unwrapped.ContractType) ||
                !ConfigurationManager.AppSettings.AllKeys.Contains(key))
                yield break;

            var value = ConfigurationManager.AppSettings.Get(key);
            var converted = Convert.ChangeType(value, contract.ContractType);

            yield return new ExportDescriptorPromise(
                    contract,
                    "System.Configuration.ConfigurationManager.AppSettings",
                    true,
                    NoDependencies,
                    _ => ExportDescriptor.Create((c, o) => converted, NoMetadata));
        }
    }
}
