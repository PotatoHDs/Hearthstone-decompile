using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B93 RID: 2963
	public abstract class PlayMakerUiEventBase : MonoBehaviour
	{
		// Token: 0x06009B71 RID: 39793 RVA: 0x0031F7F0 File Offset: 0x0031D9F0
		public void AddTargetFsm(PlayMakerFSM fsm)
		{
			if (!this.TargetsFsm(fsm))
			{
				this.targetFsms.Add(fsm);
			}
			this.Initialize();
		}

		// Token: 0x06009B72 RID: 39794 RVA: 0x0031F810 File Offset: 0x0031DA10
		private bool TargetsFsm(PlayMakerFSM fsm)
		{
			for (int i = 0; i < this.targetFsms.Count; i++)
			{
				PlayMakerFSM y = this.targetFsms[i];
				if (fsm == y)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009B73 RID: 39795 RVA: 0x0031F84C File Offset: 0x0031DA4C
		protected void OnEnable()
		{
			this.Initialize();
		}

		// Token: 0x06009B74 RID: 39796 RVA: 0x0031F84C File Offset: 0x0031DA4C
		public void PreProcess()
		{
			this.Initialize();
		}

		// Token: 0x06009B75 RID: 39797 RVA: 0x0031F854 File Offset: 0x0031DA54
		protected virtual void Initialize()
		{
			this.initialized = true;
		}

		// Token: 0x06009B76 RID: 39798 RVA: 0x0031F860 File Offset: 0x0031DA60
		protected void SendEvent(FsmEvent fsmEvent)
		{
			for (int i = 0; i < this.targetFsms.Count; i++)
			{
				this.targetFsms[i].Fsm.Event(base.gameObject, fsmEvent);
			}
		}

		// Token: 0x040080D6 RID: 32982
		public List<PlayMakerFSM> targetFsms = new List<PlayMakerFSM>();

		// Token: 0x040080D7 RID: 32983
		[NonSerialized]
		protected bool initialized;
	}
}
