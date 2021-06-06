using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000B9B RID: 2971
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on an Actor's Card's premium.")]
	public class ActorPremiumEventAction : ActorAction
	{
		// Token: 0x06009B99 RID: 39833 RVA: 0x0031FEBC File Offset: 0x0031E0BC
		protected override GameObject GetActorOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_ActorObject);
		}

		// Token: 0x06009B9A RID: 39834 RVA: 0x0031FECF File Offset: 0x0031E0CF
		public override void Reset()
		{
			this.m_ActorObject = null;
			this.m_NormalEvent = null;
			this.m_GoldenEvent = null;
			this.m_DiamondEvent = null;
		}

		// Token: 0x06009B9B RID: 39835 RVA: 0x0031FEF0 File Offset: 0x0031E0F0
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_actor == null)
			{
				base.Finish();
				return;
			}
			TAG_PREMIUM premium = this.m_actor.GetPremium();
			switch (premium)
			{
			case TAG_PREMIUM.NORMAL:
				base.Fsm.Event(this.m_NormalEvent);
				break;
			case TAG_PREMIUM.GOLDEN:
				base.Fsm.Event(this.m_GoldenEvent);
				break;
			case TAG_PREMIUM.DIAMOND:
				base.Fsm.Event(this.m_DiamondEvent);
				break;
			default:
				Debug.LogError(string.Format("ActorPremiumEventAction.OnEnter() - unknown premium {0} on actor {1}", premium, this.m_actor));
				break;
			}
			base.Finish();
		}

		// Token: 0x040080E3 RID: 32995
		public FsmOwnerDefault m_ActorObject;

		// Token: 0x040080E4 RID: 32996
		public FsmEvent m_NormalEvent;

		// Token: 0x040080E5 RID: 32997
		public FsmEvent m_GoldenEvent;

		// Token: 0x040080E6 RID: 32998
		public FsmEvent m_DiamondEvent;
	}
}
