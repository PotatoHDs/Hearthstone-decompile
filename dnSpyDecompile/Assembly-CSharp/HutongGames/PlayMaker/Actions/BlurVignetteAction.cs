using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F29 RID: 3881
	[ActionCategory("Pegasus")]
	[Tooltip("Trigger blur+vignette effects action.")]
	public class BlurVignetteAction : FsmStateAction
	{
		// Token: 0x0600AC2D RID: 44077 RVA: 0x0035B7DC File Offset: 0x003599DC
		public override void OnEnter()
		{
			if (this.m_isBlurred.Value)
			{
				BlurVignetteAction.ActionType actionType = this.m_actionType;
				if (actionType != BlurVignetteAction.ActionType.Start)
				{
					if (actionType == BlurVignetteAction.ActionType.End)
					{
						FullScreenFXMgr.Get().EndStandardBlurVignette(this.m_blurTime.Value, null);
					}
				}
				else
				{
					FullScreenFXMgr.Get().StartStandardBlurVignette(this.m_blurTime.Value);
				}
			}
			else
			{
				BlurVignetteAction.ActionType actionType = this.m_actionType;
				if (actionType != BlurVignetteAction.ActionType.Start)
				{
					if (actionType == BlurVignetteAction.ActionType.End)
					{
						FullScreenFXMgr.Get().StopVignette(this.m_blurTime.Value, iTween.EaseType.easeInOutCubic, null, null);
					}
				}
				else
				{
					FullScreenFXMgr.Get().Vignette(this.m_vignetteVal.Value, this.m_blurTime.Value, iTween.EaseType.easeInOutCubic, null, null);
				}
			}
			base.Finish();
		}

		// Token: 0x040092F5 RID: 37621
		[Tooltip("Blur or Vignette time that it takes to get to full value")]
		public FsmFloat m_blurTime;

		// Token: 0x040092F6 RID: 37622
		public FsmFloat m_vignetteVal = 0.8f;

		// Token: 0x040092F7 RID: 37623
		public FsmBool m_isBlurred;

		// Token: 0x040092F8 RID: 37624
		public BlurVignetteAction.ActionType m_actionType;

		// Token: 0x020027B9 RID: 10169
		public enum ActionType
		{
			// Token: 0x0400F576 RID: 62838
			Start,
			// Token: 0x0400F577 RID: 62839
			End
		}
	}
}
