namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Trigger blur+vignette effects action.")]
	public class BlurVignetteAction : FsmStateAction
	{
		public enum ActionType
		{
			Start,
			End
		}

		[Tooltip("Blur or Vignette time that it takes to get to full value")]
		public FsmFloat m_blurTime;

		public FsmFloat m_vignetteVal = 0.8f;

		public FsmBool m_isBlurred;

		public ActionType m_actionType;

		public override void OnEnter()
		{
			if (m_isBlurred.Value)
			{
				switch (m_actionType)
				{
				case ActionType.Start:
					FullScreenFXMgr.Get().StartStandardBlurVignette(m_blurTime.Value);
					break;
				case ActionType.End:
					FullScreenFXMgr.Get().EndStandardBlurVignette(m_blurTime.Value);
					break;
				}
			}
			else
			{
				switch (m_actionType)
				{
				case ActionType.Start:
					FullScreenFXMgr.Get().Vignette(m_vignetteVal.Value, m_blurTime.Value, iTween.EaseType.easeInOutCubic);
					break;
				case ActionType.End:
					FullScreenFXMgr.Get().StopVignette(m_blurTime.Value, iTween.EaseType.easeInOutCubic);
					break;
				}
			}
			Finish();
		}
	}
}
