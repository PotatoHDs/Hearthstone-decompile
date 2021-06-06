using System;
using Blizzard.T5.Services;
using UnityEngine;

public class InitializeDynamicServicesIfNeeded : CustomYieldInstruction
{
	private Type[] m_serviceTypes;

	public override bool keepWaiting
	{
		get
		{
			Type[] serviceTypes = m_serviceTypes;
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

	public InitializeDynamicServicesIfNeeded(params Type[] serviceTypes)
	{
		m_serviceTypes = serviceTypes;
		HearthstoneServices.InitializeDynamicServicesIfNeeded(m_serviceTypes);
	}
}
