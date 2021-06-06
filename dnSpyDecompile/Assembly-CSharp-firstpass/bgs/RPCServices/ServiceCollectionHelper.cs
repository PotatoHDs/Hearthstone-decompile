using System;
using System.Collections.Generic;

namespace bgs.RPCServices
{
	// Token: 0x02000283 RID: 643
	public sealed class ServiceCollectionHelper
	{
		// Token: 0x060025F4 RID: 9716 RVA: 0x0008690C File Offset: 0x00084B0C
		public void AddImportedService(ServiceDescriptor serviceDescriptor)
		{
			if (!serviceDescriptor.Imported)
			{
				this.m_logSource.LogWarning("Importing service NOT identified as imported id={0} name={1} hash={2}", new object[]
				{
					serviceDescriptor.Id,
					serviceDescriptor.Name,
					serviceDescriptor.Hash
				});
				serviceDescriptor.Imported = true;
			}
			this.importedServices.Add(serviceDescriptor.Id, serviceDescriptor);
			this.importedServices.Add(serviceDescriptor.Hash, serviceDescriptor);
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x00086988 File Offset: 0x00084B88
		public void AddExportedService(ServiceDescriptor serviceDescriptor)
		{
			if (!serviceDescriptor.Exported)
			{
				this.m_logSource.LogWarning("Exporting service NOT identified as exported id={0} name={1} hash={2}", new object[]
				{
					serviceDescriptor.Id,
					serviceDescriptor.Name,
					serviceDescriptor.Hash
				});
				serviceDescriptor.Exported = true;
			}
			this.exportedServices.Add(serviceDescriptor.Id, serviceDescriptor);
			this.exportedServices.Add(serviceDescriptor.Hash, serviceDescriptor);
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060025F6 RID: 9718 RVA: 0x00086A03 File Offset: 0x00084C03
		public Dictionary<uint, ServiceDescriptor> ImportedServices
		{
			get
			{
				return this.importedServices;
			}
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x00086A3C File Offset: 0x00084C3C
		public ServiceDescriptor GetImportedServiceByHash(uint serviceHash)
		{
			if (this.importedServices == null)
			{
				return null;
			}
			ServiceDescriptor result;
			this.importedServices.TryGetValue(serviceHash, out result);
			return result;
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x00086A64 File Offset: 0x00084C64
		public ServiceDescriptor GetExportedServiceByHash(uint serviceHash)
		{
			ServiceDescriptor result;
			this.exportedServices.TryGetValue(serviceHash, out result);
			return result;
		}

		// Token: 0x04001037 RID: 4151
		private Dictionary<uint, ServiceDescriptor> importedServices = new Dictionary<uint, ServiceDescriptor>();

		// Token: 0x04001038 RID: 4152
		private Dictionary<uint, ServiceDescriptor> exportedServices = new Dictionary<uint, ServiceDescriptor>();

		// Token: 0x04001039 RID: 4153
		private BattleNetLogSource m_logSource = new BattleNetLogSource("Service");
	}
}
