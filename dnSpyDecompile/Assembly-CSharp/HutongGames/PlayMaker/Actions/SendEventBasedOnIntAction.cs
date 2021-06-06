using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F6C RID: 3948
	[ActionCategory("Pegasus")]
	[Tooltip("Fires an event based on a supplied Int")]
	public class SendEventBasedOnIntAction : FsmStateAction
	{
		// Token: 0x0600AD35 RID: 44341 RVA: 0x00360424 File Offset: 0x0035E624
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.m_Int == null)
			{
				global::Log.All.PrintError("{0}.OnEnter() - No Int supplied!", new object[]
				{
					this
				});
				base.Finish();
				return;
			}
			FsmEvent fsmEvent = this.DetermineEventToSend(this.m_Int.Value);
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

		// Token: 0x0600AD36 RID: 44342 RVA: 0x003604A4 File Offset: 0x0035E6A4
		private FsmEvent DetermineEventToSend(int tagValue)
		{
			SendEventBasedOnIntAction.ValueRangeEvent appropriateElementAccordingToRanges = SpellUtils.GetAppropriateElementAccordingToRanges<SendEventBasedOnIntAction.ValueRangeEvent>(this.m_rangeEventArray, (SendEventBasedOnIntAction.ValueRangeEvent x) => x.m_range, tagValue);
			if (appropriateElementAccordingToRanges != null)
			{
				return appropriateElementAccordingToRanges.m_event;
			}
			return null;
		}

		// Token: 0x04009419 RID: 37913
		public SendEventBasedOnIntAction.ValueRangeEvent[] m_rangeEventArray;

		// Token: 0x0400941A RID: 37914
		[RequiredField]
		public FsmInt m_Int;

		// Token: 0x020027BF RID: 10175
		public class ValueRangeEvent
		{
			// Token: 0x0400F586 RID: 62854
			public ValueRange m_range;

			// Token: 0x0400F587 RID: 62855
			public FsmEvent m_event;
		}
	}
}
