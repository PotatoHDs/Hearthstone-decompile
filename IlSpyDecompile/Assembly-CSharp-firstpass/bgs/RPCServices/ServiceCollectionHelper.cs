using System.Collections.Generic;

namespace bgs.RPCServices
{
	public sealed class ServiceCollectionHelper
	{
		private Dictionary<uint, ServiceDescriptor> importedServices = new Dictionary<uint, ServiceDescriptor>();

		private Dictionary<uint, ServiceDescriptor> exportedServices = new Dictionary<uint, ServiceDescriptor>();

		private BattleNetLogSource m_logSource = new BattleNetLogSource("Service");

		public Dictionary<uint, ServiceDescriptor> ImportedServices => importedServices;

		public void AddImportedService(ServiceDescriptor serviceDescriptor)
		{
			if (!serviceDescriptor.Imported)
			{
				m_logSource.LogWarning("Importing service NOT identified as imported id={0} name={1} hash={2}", serviceDescriptor.Id, serviceDescriptor.Name, serviceDescriptor.Hash);
				serviceDescriptor.Imported = true;
			}
			importedServices.Add(serviceDescriptor.Id, serviceDescriptor);
			importedServices.Add(serviceDescriptor.Hash, serviceDescriptor);
		}

		public void AddExportedService(ServiceDescriptor serviceDescriptor)
		{
			if (!serviceDescriptor.Exported)
			{
				m_logSource.LogWarning("Exporting service NOT identified as exported id={0} name={1} hash={2}", serviceDescriptor.Id, serviceDescriptor.Name, serviceDescriptor.Hash);
				serviceDescriptor.Exported = true;
			}
			exportedServices.Add(serviceDescriptor.Id, serviceDescriptor);
			exportedServices.Add(serviceDescriptor.Hash, serviceDescriptor);
		}

		public ServiceDescriptor GetImportedServiceByHash(uint serviceHash)
		{
			if (importedServices == null)
			{
				return null;
			}
			importedServices.TryGetValue(serviceHash, out var value);
			return value;
		}

		public ServiceDescriptor GetExportedServiceByHash(uint serviceHash)
		{
			exportedServices.TryGetValue(serviceHash, out var value);
			return value;
		}
	}
}
