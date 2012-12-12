using System;
using System.Collections.Generic;
using System.Composition.Hosting.Core;
using System.Configuration;
using System.Linq;

namespace Alt.Composition.Settings
{
    class SettingsExportDescriptorProvider : ExportDescriptorProvider
    {
        static readonly Type[] SupportedSettingTypes = new[] { typeof(string), typeof(int), typeof(double), typeof(DateTime), typeof(TimeSpan) };
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
