using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F6B RID: 3947
	[ActionCategory("Pegasus")]
	[Tooltip("Fires an event based on the tag value of a specified entity")]
	public class SendEventBasedOnEntityTagAction : SpellAction
	{
		// Token: 0x0600AD31 RID: 44337 RVA: 0x00360338 File Offset: 0x0035E538
		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(this.m_spellObject);
		}

		// Token: 0x0600AD32 RID: 44338 RVA: 0x0036034C File Offset: 0x0035E54C
		public override void OnEnter()
		{
			base.OnEnter();
			Entity entity = base.GetEntity(this.m_whichEntity);
			if (entity == null)
			{
				global::Log.All.PrintWarning("{0}.OnEnter() - FAILED to find relevant entity: \"{1}\"", new object[]
				{
					this,
					this.m_whichEntity
				});
				base.Finish();
				return;
			}
			FsmEvent fsmEvent = this.DetermineEventToSend(entity.GetTag(this.m_tagToCheck));
			if (fsmEvent != null)
			{
				base.Fsm.Event(fsmEvent);
			}
			else
			{
				global::Log.All.PrintError("{0}.OnEnter() - FAILED to find any event that could be sent, did you set up ranges?", new object[]
				{
					this
				});
			}
			base.Finish();
		}

		// Token: 0x0600AD33 RID: 44339 RVA: 0x003603E0 File Offset: 0x0035E5E0
		private FsmEvent DetermineEventToSend(int tagValue)
		{
			SendEventBasedOnEntityTagAction.ValueRangeEvent appropriateElementAccordingToRanges = SpellUtils.GetAppropriateElementAccordingToRanges<SendEventBasedOnEntityTagAction.ValueRangeEvent>(this.m_rangeEventArray, (SendEventBasedOnEntityTagAction.ValueRangeEvent x) => x.m_range, tagValue);
			if (appropriateElementAccordingToRanges != null)
			{
				return appropriateElementAccordingToRanges.m_event;
			}
			return null;
		}

		// Token: 0x04009415 RID: 37909
		public FsmOwnerDefault m_spellObject;

		// Token: 0x04009416 RID: 37910
		public SendEventBasedOnEntityTagAction.ValueRangeEvent[] m_rangeEventArray;

		// Token: 0x04009417 RID: 37911
		public SpellAction.Which m_whichEntity;

		// Token: 0x04009418 RID: 37912
		public GAME_TAG m_tagToCheck;

		// Token: 0x020027BD RID: 10173
		public class ValueRangeEvent
		{
			// Token: 0x0400F582 RID: 62850
			public ValueRange m_range;

			// Token: 0x0400F583 RID: 62851
			public FsmEvent m_event;
		}
	}
}
