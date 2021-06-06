using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F08 RID: 3848
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on an Actor's Card's elite flag.")]
	public class ActorEliteEventAction : ActorAction
	{
		// Token: 0x0600AB96 RID: 43926 RVA: 0x003593FF File Offset: 0x003575FF
		protected override GameObject GetActorOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_ActorObject);
		}

		// Token: 0x0600AB97 RID: 43927 RVA: 0x00359412 File Offset: 0x00357612
		public override void Reset()
		{
			this.m_ActorObject = null;
			this.m_EliteEvent = null;
			this.m_NonEliteEvent = null;
		}

		// Token: 0x0600AB98 RID: 43928 RVA: 0x0035942C File Offset: 0x0035762C
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_actor == null)
			{
				base.Finish();
				return;
			}
			if (this.m_actor.IsElite())
			{
				base.Fsm.Event(this.m_EliteEvent);
			}
			else
			{
				base.Fsm.Event(this.m_NonEliteEvent);
			}
			base.Finish();
		}

		// Token: 0x04009251 RID: 37457
		public FsmOwnerDefault m_ActorObject;

		// Token: 0x04009252 RID: 37458
		public FsmEvent m_EliteEvent;

		// Token: 0x04009253 RID: 37459
		public FsmEvent m_NonEliteEvent;
	}
}
