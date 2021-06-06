using System;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x0200069B RID: 1691
public class InitializeDynamicServicesIfNeeded : CustomYieldInstruction
{
	// Token: 0x1700059A RID: 1434
	// (get) Token: 0x06005E76 RID: 24182 RVA: 0x001EB220 File Offset: 0x001E9420
	public override bool keepWaiting
	{
		get
		{
			Type[] serviceTypes = this.m_serviceTypes;
			for (int i = 0; i < serviceTypes.Length; i++)
			{
				if (HearthstoneServices.GetServiceState(serviceTypes[i]) != ServiceLocator.ServiceState.Ready)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x06005E77 RID: 24183 RVA: 0x001EB250 File Offset: 0x001E9450
	public InitializeDynamicServicesIfNeeded(params Type[] serviceTypes)
	{
		this.m_serviceTypes = serviceTypes;
		HearthstoneServices.InitializeDynamicServicesIfNeeded(this.m_serviceTypes);
	}

	// Token: 0x04004F8C RID: 20364
	private Type[] m_serviceTypes;
}
