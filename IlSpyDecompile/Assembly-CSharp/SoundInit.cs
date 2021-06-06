using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using UnityEngine;

[AddComponentMenu("Hearthstone/Services/Sound")]
public class SoundInit : MonoBehaviour
{
	public bool m_ready;

	private void Start()
	{
		m_ready = false;
		HearthstoneServices.InitializeDynamicServicesIfEditor(out var serviceDependencies, typeof(SoundManager));
		Processor.QueueJob("SoundInit.Initialize", Job_Initialize(), serviceDependencies);
	}

	private IEnumerator<IAsyncJobResult> Job_Initialize()
	{
		m_ready = true;
		yield break;
	}
}
