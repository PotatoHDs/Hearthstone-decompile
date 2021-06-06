using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F0F RID: 3855
	[ActionCategory("Pegasus")]
	[Tooltip("Call UpdateAllComponents on an actor, to refresh it's textures, health icon, attack icon, etc.")]
	public class ActorUpdateAllComponentsAction : ActorAction
	{
		// Token: 0x0600ABBA RID: 43962 RVA: 0x00359B45 File Offset: 0x00357D45
		protected override GameObject GetActorOwner()
		{
			return this.m_ActorObject.Value;
		}

		// Token: 0x0600ABBB RID: 43963 RVA: 0x00359B52 File Offset: 0x00357D52
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_actor == null)
			{
				base.Finish();
				return;
			}
			this.m_actor.UpdateAllComponents();
			base.Finish();
		}

		// Token: 0x0400926E RID: 37486
		public FsmGameObject m_ActorObject;
	}
}
