using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F4E RID: 3918
	[ActionCategory("Pegasus")]
	[Tooltip("Sends Events based on whether or not an option is enabled by the player.")]
	public class IsOptionEnabledAction : FsmStateAction
	{
		// Token: 0x0600ACBC RID: 44220 RVA: 0x0035E25D File Offset: 0x0035C45D
		public override void Reset()
		{
			this.m_Option = Option.INVALID;
			this.m_TrueEvent = null;
			this.m_FalseEvent = null;
			this.m_EveryFrame = false;
		}

		// Token: 0x0600ACBD RID: 44221 RVA: 0x0035E27B File Offset: 0x0035C47B
		public override void OnEnter()
		{
			this.FireEvents();
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600ACBE RID: 44222 RVA: 0x0035E291 File Offset: 0x0035C491
		public override void OnUpdate()
		{
			this.FireEvents();
		}

		// Token: 0x0600ACBF RID: 44223 RVA: 0x0035E299 File Offset: 0x0035C499
		public override string ErrorCheck()
		{
			if (!FsmEvent.IsNullOrEmpty(this.m_TrueEvent))
			{
				return string.Empty;
			}
			if (!FsmEvent.IsNullOrEmpty(this.m_FalseEvent))
			{
				return string.Empty;
			}
			return "Action sends no events!";
		}

		// Token: 0x0600ACC0 RID: 44224 RVA: 0x0035E2C6 File Offset: 0x0035C4C6
		private void FireEvents()
		{
			if (Options.Get().GetBool(this.m_Option))
			{
				base.Fsm.Event(this.m_TrueEvent);
				return;
			}
			base.Fsm.Event(this.m_FalseEvent);
		}

		// Token: 0x0400938D RID: 37773
		[Tooltip("The option to check.")]
		public Option m_Option;

		// Token: 0x0400938E RID: 37774
		[Tooltip("Event sent if the option is on.")]
		public FsmEvent m_TrueEvent;

		// Token: 0x0400938F RID: 37775
		[Tooltip("Event sent if the option is off.")]
		public FsmEvent m_FalseEvent;

		// Token: 0x04009390 RID: 37776
		[Tooltip("Check if the option is enabled every frame.")]
		public bool m_EveryFrame;
	}
}
