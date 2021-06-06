namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Gets the global time scale into a variable.")]
	public class GetTimeScaleAction : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat m_Scale;

		public bool m_EveryFrame;

		public override void Reset()
		{
			m_Scale = null;
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
				m_Scale.Value = TimeScaleMgr.Get().GetGameTimeScale();
			}
		}
	}
}
