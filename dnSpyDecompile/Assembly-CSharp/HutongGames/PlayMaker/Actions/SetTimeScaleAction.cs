using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F98 RID: 3992
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the global time scale.")]
	public class SetTimeScaleAction : FsmStateAction
	{
		// Token: 0x0600ADF2 RID: 44530 RVA: 0x00362B81 File Offset: 0x00360D81
		public override void Reset()
		{
			this.m_Scale = 1f;
			this.m_EveryFrame = false;
		}

		// Token: 0x0600ADF3 RID: 44531 RVA: 0x00362B9A File Offset: 0x00360D9A
		public override void OnEnter()
		{
			this.UpdateScale();
			if (!this.m_EveryFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600ADF4 RID: 44532 RVA: 0x00362BB0 File Offset: 0x00360DB0
		public override void OnUpdate()
		{
			this.UpdateScale();
		}

		// Token: 0x0600ADF5 RID: 44533 RVA: 0x00362BB8 File Offset: 0x00360DB8
		private void UpdateScale()
		{
			if (this.m_Scale.IsNone)
			{
				return;
			}
			TimeScaleMgr.Get().SetGameTimeScale(this.m_Scale.Value);
		}

		// Token: 0x040094C0 RID: 38080
		public FsmFloat m_Scale;

		// Token: 0x040094C1 RID: 38081
		public bool m_EveryFrame;
	}
}
