using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F96 RID: 3990
	[ActionCategory("Pegasus")]
	[Tooltip("Gets the global time scale into a variable.")]
	public class GetTimeScaleAction : FsmStateAction
	{
		// Token: 0x0600ADE9 RID: 44521 RVA: 0x00362A75 File Offset: 0x00360C75
		public override void Reset()
		{
			this.m_Scale = null;
			this.m_EveryFrame = false;
		}

		// Token: 0x0600ADEA RID: 44522 RVA: 0x00362A85 File Offset: 0x00360C85
		public override void OnEnter()
		{
			this.UpdateScale();
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600ADEB RID: 44523 RVA: 0x00362A9B File Offset: 0x00360C9B
		public override void OnUpdate()
		{
			this.UpdateScale();
		}

		// Token: 0x0600ADEC RID: 44524 RVA: 0x00362AA3 File Offset: 0x00360CA3
		private void UpdateScale()
		{
			if (this.m_Scale.IsNone)
			{
				return;
			}
			this.m_Scale.Value = TimeScaleMgr.Get().GetGameTimeScale();
		}

		// Token: 0x040094BB RID: 38075
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat m_Scale;

		// Token: 0x040094BC RID: 38076
		public bool m_EveryFrame;
	}
}
