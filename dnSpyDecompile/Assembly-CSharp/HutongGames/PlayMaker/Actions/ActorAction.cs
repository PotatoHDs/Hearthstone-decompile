using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F07 RID: 3847
	[ActionCategory("Pegasus")]
	[Tooltip("INTERNAL USE ONLY. Do not put this on your FSMs.")]
	public abstract class ActorAction : FsmStateAction
	{
		// Token: 0x0600AB91 RID: 43921 RVA: 0x00359394 File Offset: 0x00357594
		public Actor GetActor()
		{
			if (this.m_actor == null)
			{
				GameObject actorOwner = this.GetActorOwner();
				if (actorOwner != null)
				{
					this.m_actor = SceneUtils.FindComponentInThisOrParents<Actor>(actorOwner);
				}
			}
			return this.m_actor;
		}

		// Token: 0x0600AB92 RID: 43922
		protected abstract GameObject GetActorOwner();

		// Token: 0x0600AB93 RID: 43923 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600AB94 RID: 43924 RVA: 0x003593D1 File Offset: 0x003575D1
		public override void OnEnter()
		{
			this.GetActor();
			if (this.m_actor == null)
			{
				Debug.LogError(string.Format("{0}.OnEnter() - FAILED to find Actor component on Owner \"{1}\"", this, base.Owner));
				return;
			}
		}

		// Token: 0x04009250 RID: 37456
		protected Actor m_actor;
	}
}
