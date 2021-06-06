using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F6D RID: 3949
	[ActionCategory("Pegasus")]
	[Tooltip("Fires an event based on a screen category")]
	public class SendEventBasedOnScreenAction : FsmStateAction
	{
		// Token: 0x0600AD38 RID: 44344 RVA: 0x003604E8 File Offset: 0x0035E6E8
		public override void Reset()
		{
			this.m_PhoneEvent = null;
			this.m_MiniTabletEvent = null;
			this.m_TabletEvent = null;
			this.m_PCEvent = null;
		}

		// Token: 0x0600AD39 RID: 44345 RVA: 0x00360508 File Offset: 0x0035E708
		public override void OnEnter()
		{
			base.OnEnter();
			switch (PlatformSettings.Screen)
			{
			case ScreenCategory.Phone:
				base.Fsm.Event(this.m_PhoneEvent);
				goto IL_70;
			case ScreenCategory.MiniTablet:
				base.Fsm.Event(this.m_MiniTabletEvent);
				goto IL_70;
			case ScreenCategory.Tablet:
				base.Fsm.Event(this.m_TabletEvent);
				goto IL_70;
			}
			base.Fsm.Event(this.m_PCEvent);
			IL_70:
			base.Finish();
		}

		// Token: 0x0400941B RID: 37915
		public FsmEvent m_PhoneEvent;

		// Token: 0x0400941C RID: 37916
		public FsmEvent m_MiniTabletEvent;

		// Token: 0x0400941D RID: 37917
		public FsmEvent m_TabletEvent;

		// Token: 0x0400941E RID: 37918
		public FsmEvent m_PCEvent;
	}
}
