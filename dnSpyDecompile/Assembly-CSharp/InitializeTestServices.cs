using System;
using System.Collections.Generic;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x0200069C RID: 1692
public class InitializeTestServices : CustomYieldInstruction
{
	// Token: 0x1700059B RID: 1435
	// (get) Token: 0x06005E78 RID: 24184 RVA: 0x001EB26C File Offset: 0x001E946C
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

	// Token: 0x06005E79 RID: 24185 RVA: 0x001EB29C File Offset: 0x001E949C
	public InitializeTestServices(params Type[] serviceTypes)
	{
		this.m_serviceTypes = serviceTypes;
		HearthstoneServices.InitializeTestServices(this.m_serviceTypes);
	}

	// Token: 0x06005E7A RID: 24186 RVA: 0x001EB2B7 File Offset: 0x001E94B7
	public InitializeTestServices(List<ValueTuple<Type, IService>> serviceOverrides, params Type[] serviceTypes)
	{
		this.m_serviceOverrides = serviceOverrides;
		this.m_serviceTypes = serviceTypes;
		HearthstoneServices.InitializeTestServices(this.m_serviceOverrides, this.m_serviceTypes);
	}

	// Token: 0x04004F8D RID: 20365
	private Type[] m_serviceTypes;

	// Token: 0x04004F8E RID: 20366
	private List<ValueTuple<Type, IService>> m_serviceOverrides;
}
