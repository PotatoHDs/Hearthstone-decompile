using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x02000B39 RID: 2873
[AddComponentMenu("Hearthstone/Services/Sound")]
public class SoundInit : MonoBehaviour
{
	// Token: 0x06009879 RID: 39033 RVA: 0x00315D60 File Offset: 0x00313F60
	private void Start()
	{
		this.m_ready = false;
		IJobDependency[] dependencies;
		HearthstoneServices.InitializeDynamicServicesIfEditor(out dependencies, new Type[]
		{
			typeof(SoundManager)
		});
		Processor.QueueJob("SoundInit.Initialize", this.Job_Initialize(), dependencies);
	}

	// Token: 0x0600987A RID: 39034 RVA: 0x00315DA1 File Offset: 0x00313FA1
	private IEnumerator<IAsyncJobResult> Job_Initialize()
	{
		this.m_ready = true;
		yield break;
	}

	// Token: 0x04007F79 RID: 32633
	public bool m_ready;
}
