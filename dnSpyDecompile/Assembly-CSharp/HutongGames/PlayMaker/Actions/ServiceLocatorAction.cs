using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone.Core;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F6E RID: 3950
	[ActionCategory("Pegasus Debug")]
	[Tooltip("Waits for services to be dynamically loaded and ready")]
	public class ServiceLocatorAction : FsmStateAction
	{
		// Token: 0x0600AD3B RID: 44347 RVA: 0x0036058B File Offset: 0x0035E78B
		public override void Reset()
		{
			this.m_sound = false;
		}

		// Token: 0x0600AD3C RID: 44348 RVA: 0x00360594 File Offset: 0x0035E794
		public override void OnEnter()
		{
			List<Type> list = new List<Type>();
			if (this.m_sound)
			{
				list.Add(typeof(SoundManager));
			}
			if (list.Count == 0)
			{
				base.Finish();
				return;
			}
			IJobDependency[] dependencies = null;
			HearthstoneServices.InitializeDynamicServicesIfEditor(out dependencies, list.ToArray());
			Processor.QueueJob("Playmaker.ServiceLocator", this.Job_Initialize(), dependencies);
		}

		// Token: 0x0600AD3D RID: 44349 RVA: 0x003605F0 File Offset: 0x0035E7F0
		private IEnumerator<IAsyncJobResult> Job_Initialize()
		{
			base.Finish();
			yield break;
		}

		// Token: 0x0400941F RID: 37919
		public bool m_sound;
	}
}
