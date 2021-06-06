namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sets the global time scale.")]
	public class SetTimeScaleAction : FsmStateAction
	{
		public FsmFloat m_Scale;

		public bool m_EveryFrame;

		public override void Reset()
		{
			m_Scale = 1f;
			m_EveryFrame = false;
		}

		public override void OnEnter()
		{
			UpdateScale();
			if (!m_EveryFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			UpdateScale();
		}

		private void UpdateScale()
		{
			if (!m_Scale.IsNone)
			{
				TimeScaleMgr.Get().SetGameTimeScale(m_Scale.Value);
			}
		}
	}
}
