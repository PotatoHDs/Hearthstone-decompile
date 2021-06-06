using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F0A RID: 3850
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on an Actor's Card's Golden state.")]
	public class ActorGoldEventAction : ActorAction
	{
		// Token: 0x0600AB9E RID: 43934 RVA: 0x003595A3 File Offset: 0x003577A3
		protected override GameObject GetActorOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_ActorObject);
		}

		// Token: 0x0600AB9F RID: 43935 RVA: 0x003595B6 File Offset: 0x003577B6
		public override void Reset()
		{
			this.m_ActorObject = null;
			this.m_GoldenCardEvent = null;
			this.m_StandardCardEvent = null;
		}

		// Token: 0x0600ABA0 RID: 43936 RVA: 0x003595D0 File Offset: 0x003577D0
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_actor == null)
			{
				base.Finish();
				return;
			}
			if (this.m_actor.GetPremium() == TAG_PREMIUM.GOLDEN)
			{
				base.Fsm.Event(this.m_GoldenCardEvent);
			}
			else
			{
				base.Fsm.Event(this.m_StandardCardEvent);
			}
			base.Finish();
		}

		// Token: 0x04009258 RID: 37464
		public FsmOwnerDefault m_ActorObject;

		// Token: 0x04009259 RID: 37465
		public FsmEvent m_GoldenCardEvent;

		// Token: 0x0400925A RID: 37466
		public FsmEvent m_StandardCardEvent;
	}
}
