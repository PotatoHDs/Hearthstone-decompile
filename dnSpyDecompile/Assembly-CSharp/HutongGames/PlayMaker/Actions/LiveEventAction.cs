using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F52 RID: 3922
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on what events are currently active.")]
	public class LiveEventAction : FsmStateAction
	{
		// Token: 0x0600ACD1 RID: 44241 RVA: 0x0035EC60 File Offset: 0x0035CE60
		public override void Reset()
		{
			this.m_EventObject = null;
			this.m_FSMEvents = new FsmEvent[2];
			this.m_SpecialEvents = new SpecialEventType[2];
			this.m_SpecialEvents[0] = SpecialEventType.UNKNOWN;
		}

		// Token: 0x0600ACD2 RID: 44242 RVA: 0x0035EC8C File Offset: 0x0035CE8C
		public override void OnEnter()
		{
			base.OnEnter();
			for (int i = 0; i < this.m_FSMEvents.Length; i++)
			{
				if (this.m_FSMEvents[i] != null && SpecialEventManager.Get().IsEventActive(this.m_SpecialEvents[i], false))
				{
					base.Fsm.Event(this.m_FSMEvents[i]);
					break;
				}
			}
			base.Finish();
		}

		// Token: 0x040093A6 RID: 37798
		public FsmOwnerDefault m_EventObject;

		// Token: 0x040093A7 RID: 37799
		[RequiredField]
		[CompoundArray("Events", "Special Event", "FSM Event")]
		public SpecialEventType[] m_SpecialEvents;

		// Token: 0x040093A8 RID: 37800
		public FsmEvent[] m_FSMEvents;
	}
}
