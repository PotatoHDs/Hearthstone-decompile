using System;
using System.Collections.Generic;
using Blizzard.T5.Services;
using UnityEngine;

public class InitializeTestServices : CustomYieldInstruction
{
	private Type[] m_serviceTypes;

	private List<(Type, IService)> m_serviceOverrides;

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

	public InitializeTestServices(params Type[] serviceTypes)
	{
		m_serviceTypes = serviceTypes;
		HearthstoneServices.InitializeTestServices(m_serviceTypes);
	}

	public InitializeTestServices(List<(Type, IService)> serviceOverrides, params Type[] serviceTypes)
	{
		m_serviceOverrides = serviceOverrides;
		m_serviceTypes = serviceTypes;
		HearthstoneServices.InitializeTestServices(m_serviceOverrides, m_serviceTypes);
	}
}
