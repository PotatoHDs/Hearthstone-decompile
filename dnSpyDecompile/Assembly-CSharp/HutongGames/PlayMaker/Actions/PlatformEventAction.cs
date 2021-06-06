using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F62 RID: 3938
	[ActionCategory("Pegasus")]
	[Tooltip("Event based on current platform")]
	public class PlatformEventAction : FsmStateAction
	{
		// Token: 0x0600AD13 RID: 44307 RVA: 0x0035FDB2 File Offset: 0x0035DFB2
		public override void Reset()
		{
			this.m_PhoneEvent = null;
			this.m_DefaultEvent = null;
		}

		// Token: 0x0600AD14 RID: 44308 RVA: 0x0035FDC2 File Offset: 0x0035DFC2
		public override void OnEnter()
		{
			if (UniversalInputManager.UsePhoneUI && this.m_PhoneEvent != null)
			{
				base.Fsm.Event(this.m_PhoneEvent);
				return;
			}
			base.Fsm.Event(this.m_DefaultEvent);
		}

		// Token: 0x0600AD15 RID: 44309 RVA: 0x000D5239 File Offset: 0x000D3439
		public override string ErrorCheck()
		{
			return "";
		}

		// Token: 0x040093F7 RID: 37879
		public FsmEvent m_DefaultEvent;

		// Token: 0x040093F8 RID: 37880
		public FsmEvent m_PhoneEvent;
	}
}
