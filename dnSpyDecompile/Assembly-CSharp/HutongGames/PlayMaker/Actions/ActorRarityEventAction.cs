using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F0C RID: 3852
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on an Actor's Card's rarity.")]
	public class ActorRarityEventAction : ActorAction
	{
		// Token: 0x0600ABA6 RID: 43942 RVA: 0x0035968F File Offset: 0x0035788F
		protected override GameObject GetActorOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_ActorObject);
		}

		// Token: 0x0600ABA7 RID: 43943 RVA: 0x003596A2 File Offset: 0x003578A2
		public override void Reset()
		{
			this.m_ActorObject = null;
			this.m_FreeEvent = null;
			this.m_CommonEvent = null;
			this.m_RareEvent = null;
			this.m_EpicEvent = null;
			this.m_LegendaryEvent = null;
		}

		// Token: 0x0600ABA8 RID: 43944 RVA: 0x003596D0 File Offset: 0x003578D0
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_actor == null)
			{
				base.Finish();
				return;
			}
			TAG_RARITY rarity = this.m_actor.GetRarity();
			switch (rarity)
			{
			case TAG_RARITY.COMMON:
				base.Fsm.Event(this.m_CommonEvent);
				break;
			case TAG_RARITY.FREE:
				base.Fsm.Event(this.m_FreeEvent);
				break;
			case TAG_RARITY.RARE:
				base.Fsm.Event(this.m_RareEvent);
				break;
			case TAG_RARITY.EPIC:
				base.Fsm.Event(this.m_EpicEvent);
				break;
			case TAG_RARITY.LEGENDARY:
				base.Fsm.Event(this.m_LegendaryEvent);
				break;
			default:
				Debug.LogError(string.Format("ActorRarityEventAction.OnEnter() - unknown rarity {0} on actor {1}", rarity, this.m_actor));
				break;
			}
			base.Finish();
		}

		// Token: 0x0400925D RID: 37469
		public FsmOwnerDefault m_ActorObject;

		// Token: 0x0400925E RID: 37470
		public FsmEvent m_FreeEvent;

		// Token: 0x0400925F RID: 37471
		public FsmEvent m_CommonEvent;

		// Token: 0x04009260 RID: 37472
		public FsmEvent m_RareEvent;

		// Token: 0x04009261 RID: 37473
		public FsmEvent m_EpicEvent;

		// Token: 0x04009262 RID: 37474
		public FsmEvent m_LegendaryEvent;
	}
}
