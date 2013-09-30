using System;
using System.Collections.Generic;
using System.Composition.Hosting.Core;
using System.Configuration;
using System.Data.Common;
using System.Linq;

namespace Alt.Composition.Hosting
{
    class ConnectionStringsExportDescriptorProvider : ExportDescriptorProvider
    {
        private const string NameKey = "ConnectionStringName";

        public override IEnumerable<ExportDescriptorPromise> GetExportDescriptors(CompositionContract contract, DependencyAccessor descriptorAccessor)
        {
            if (contract == null) throw new ArgumentNullException("contract");
            if (descriptorAccessor == null) throw new ArgumentNullException("descriptorAccessor");

            string name;
            CompositionContract unwrapped;

            var connectionStringSettings = ConfigurationManager.ConnectionStrings.OfType<ConnectionStringSettings>().ToArray();

            if (!contract.TryUnwrapMetadataConstraint(NameKey, out name, out unwrapped) ||
                !unwrapped.Equals(new CompositionContract(unwrapped.ContractType)) ||
                !(unwrapped.ContractType == typeof(string) || typeof(DbConnectionStringBuilder).IsAssignableFrom(unwrapped.ContractType)) ||
                !connectionStringSettings.Any(cs => cs.Name == name))
                yield break;

            var stringValue = connectionStringSettings.Single(cs => cs.Name == name).ConnectionString;
            object value = stringValue;

            if (contract.ContractType != typeof(string))
            {
                var stringBuilder = Activator.CreateInstance(contract.ContractType) as DbConnectionStringBuilder;
                if (stringBuilder == null) yield break;
                stringBuilder.ConnectionString = stringValue;
                value = stringBuilder;
            }

            yield return new ExportDescriptorPromise(
                    contract,
                    "System.Configuration.ConfigurationManager.ConnectionStrings",
                    true,
                    NoDependencies,
                    _ => ExportDescriptor.Create((c, o) => value, NoMetadata));
        }
    }
}
